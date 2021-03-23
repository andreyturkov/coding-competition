using CodingCompetition.Application.Interfaces;
using CodingCompetition.Application.Models.SharedPad;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodingCompetition.Application.Util;
using CodingCompetition.Compiler.Interfaces;
using CodingCompetition.Compiler.Models;

namespace CodingCompetition.Application.Services
{
	public class SharedPadService : ISharedPadService
	{
		private const string PadEntry = "CodePads";
		private const string UsersEntry = "CodePadUsers";

		private static readonly MemoryCacheEntryOptions PadEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(120));

		private readonly IMemoryCache _memoryCache;
		private readonly ISharedCodePadFactory _codePadFactory;
		private readonly ICodeCompiler _codeCompiler;
		private readonly IMapper _mapper;

		public SharedPadService(IMemoryCache memoryCache, ISharedCodePadFactory codePadFactory, ICodeCompiler codeCompiler, IMapper mapper)
		{
			_memoryCache = memoryCache;
			_codePadFactory = codePadFactory;
			_codeCompiler = codeCompiler;
			_mapper = mapper;
		}

		public string CreatePad()
		{
			var pad = _codePadFactory.CreateCSharp();
			_memoryCache.Set($"{PadEntry}{pad.Id}", pad, PadEntryOptions);
			_memoryCache.Set($"{UsersEntry}{pad.Id}", new Dictionary<string, CodePadUser>(), PadEntryOptions);

			return pad.Id;
		}

		public bool UpdatePad(CodePad pad)
		{
			_memoryCache.Set($"{PadEntry}{pad.Id}", pad, PadEntryOptions);

			return true;
		}

		public bool RemovePad(string padId)
		{
			_memoryCache.Remove($"{PadEntry}{padId}");
			_memoryCache.Remove($"{UsersEntry}{padId}");

			return true;
		}

		public IList<CodePad> GetAllPads()
		{
			return _memoryCache.GetKeys<string>()
				.Where(x => x.StartsWith(PadEntry))
				.Select(x => _memoryCache.Get<CodePad>(x))
				.ToList();
		}

		public CodePad GetPad(string padId)
		{
			return _memoryCache.Get<CodePad>($"{PadEntry}{padId}");
		}

		public bool AddPadUser(string padId, CodePadUser user)
		{
			var users = _memoryCache.Get<Dictionary<string, CodePadUser>>($"{UsersEntry}{padId}");
			if (!users.ContainsKey(user.Id))
			{
				users.Add(user.Id, user);
				_memoryCache.Set($"{UsersEntry}{padId}", users);
				return true;
			}

			return false;
		}

		public bool RemovePadUser(string padId, string userId)
		{
			var users = _memoryCache.Get<Dictionary<string, CodePadUser>>($"{UsersEntry}{padId}");
			if (!users.ContainsKey(userId))
			{
				users.Remove(userId);
				_memoryCache.Set($"{UsersEntry}{padId}", users);
				return true;
			}

			return true;
		}

		public IList<CodePadUser> GetPadUsers(string padId)
		{
			return _memoryCache.Get<Dictionary<string, CodePadUser>>($"{UsersEntry}{padId}").Values.ToList();
		}

		public async Task<RunResult> Run(CodePad pad)
		{
			var request = CreateCompileRequest(pad);
			var result = await _codeCompiler.RunCode(request);

			return _mapper.Map<RunResult>(result);
		}

		private static CompileRequest CreateCompileRequest(CodePad pad)
		{
			return new CompileRequest
			{
				Code = pad.Code,
				Language = (Language)(int)pad.Language
			};
		}
	}
}
