﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingCompetition.Application.Interfaces;
using CodingCompetition.Application.Models;
using CodingCompetition.Application.Models.Competition;
using CodingCompetition.Compiler.Interfaces;
using CodingCompetition.Compiler.Models;
using Language = CodingCompetition.Application.Models.Language;
using TestResult = CodingCompetition.Application.Models.Competition.TestResult;

namespace CodingCompetition.Application.Services
{
	public class CompetitionService : ICompetitionService
	{
		private const string SolutionPlaceholder = "/*SOLUTION_PLACEHOLDER*/";

		private readonly ICodeCompiler _codeCompiler;
		private readonly IChallengeService _challengeService;
		private readonly IPlayerService _playerService;
		private readonly ISolutionService _solutionService;

		public CompetitionService(ICodeCompiler codeCompiler, IChallengeService challengeService, IPlayerService playerService, ISolutionService solutionService)
		{
			_codeCompiler = codeCompiler;
			_challengeService = challengeService;
			_playerService = playerService;
			_solutionService = solutionService;
		}

		public async Task<Result> RunSolution(RunRequest request)
		{
			var challenge = await _challengeService.Get(request.ChallengeId);

			var result = new Result
			{
				TestResults = new List<TestResult>()
			};

			foreach (var test in challenge.Tests)
			{
				var compileRequest = CreateCompileRequest(challenge, request.Language, request.SolutionCode, test.InputParameter);

				var compileResult = await _codeCompiler.RunCode(compileRequest);

				if (!string.IsNullOrEmpty(compileResult.Errors))
				{
					result.Errors = compileResult.Errors;
					result.Warnings = compileResult.Warnings;
					break;
				}

				result.TestResults.Add(new TestResult
				{
					Success = compileResult.Result == test.ExpectedResult,
					ActualResult = compileResult.Result,
					ExpectedResult = test.ExpectedResult,
					InputParameter = test.InputParameter,
				});
			}

			result.Success = result.TestResults.All(x => x.Success);
			result.Message = $"Passed {result.TestResults.Count(x => x.Success)} of {result.TestResults.Count}";

			return result;

		}

		public async Task<Result> SubmitSolution(SubmitRequest request)
		{
			await _playerService.AddOrUpdate(request.Player);

			var result = await RunSolution(request);

			var solution = CreateSolution(request, result);

			await _solutionService.AddSolution(solution);

			return result;
		}

		#region Mapping

		private static CompileRequest CreateCompileRequest(Challenge challenge, Language language, string solutionCode, string input)
		{
			var template = challenge.Templates.FirstOrDefault(x => x.Language == language);
			if (template == null)
			{
				throw new NotSupportedException($"{language.ToString()} language is not supported.");
			}

			return new CompileRequest
			{
				Language = (Compiler.Models.Language)(int)language,
				Code = template.CompilerAdapter.Replace(SolutionPlaceholder, solutionCode),
				Input = input
			};
		}

		private static Solution CreateSolution(SubmitRequest solution, Result result)
		{
			return new Solution
			{
				PlayerId = solution.Player.Id,
				ChallengeId = solution.ChallengeId,
				Language = solution.Language,
				SolutionCode = solution.SolutionCode,
				Success = result.Success,
				Message = result.Message,
				Runtime = result.Runtime,
				Warnings = result.Warnings,
				Errors = result.Errors
			};
		}
		#endregion
	}
}

