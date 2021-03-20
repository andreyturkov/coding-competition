using CodingCompetition.Compiler.Interfaces;
using CodingCompetition.Compiler.Models;
using CodingCompetition.Compiler.Services.Rextester.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CodingCompetition.Compiler.Services.Rextester
{
	public class RextesterClient : ICodeCompiler
	{
		private const string ApiUrl = "https://rextester.com/rundotnet/api";
		private const string JsonMimeType = "application/json";

		private static readonly IDictionary<Language, int> SupportedLanguages = new Dictionary<Language, int>
		{
			{ Language.CSharp, 1 },
			{ Language.Java,   4 }
		};

		private static readonly HttpClient HttpClient = new HttpClient();

		public async Task<CompileResult> RunCode(CompileRequest code)
		{
			var request = new HttpRequestMessage(HttpMethod.Post, ApiUrl)
			{
				Content = Serialize(Map(code))
			};

			using var response = await HttpClient.SendAsync(request);
			if (!response.IsSuccessStatusCode)
			{
				throw new Exception($"RepDesk API throw exception with code {response.StatusCode}: {response.ReasonPhrase}");
			}
			
			//to avoid too many requests
			await Task.Delay(1000);

			var deserializedResponse = await DeserializeAsync<Response>(response.Content);

			return Map(deserializedResponse);
		}

		private static Request Map(CompileRequest code)
		{
			if (SupportedLanguages.TryGetValue(code.Language, out var language))
			{
				return new Request
				{
					LanguageChoice = language,
					Input = code.Input,
					CompilerArgs = code.CompilerArgs,
					Program = code.Code
				};
			}

			throw new NotSupportedException($"{code.Language} is not supported by {ApiUrl}");
		}

		private static CompileResult Map(Response response)
		{
			return new CompileResult
			{
				Result = response.Result,
				Statistic = response.Stats,
				Errors = response.Errors,
				Warnings = response.Warnings
			};
		}

		private static HttpContent Serialize(object requestBody)
		{
			return new StringContent(JsonConvert.SerializeObject(requestBody, new StringEnumConverter()), Encoding.UTF8, JsonMimeType);
		}

		private static async Task<T> DeserializeAsync<T>(HttpContent responseContent)
		{
			var response = await responseContent.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<T>(response, new StringEnumConverter());
		}

	}
}
