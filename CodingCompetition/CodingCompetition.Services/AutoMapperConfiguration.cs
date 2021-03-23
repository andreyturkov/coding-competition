using AutoMapper;
using App = CodingCompetition.Application.Models;
using Dao = CodingCompetition.Data.Models;
using Comp = CodingCompetition.Compiler.Models;

namespace CodingCompetition.Application
{
	public class AutoMapperConfiguration : Profile
	{
		public AutoMapperConfiguration()
		{
			CreateMap<Dao.Challenge, App.Challenge>().ReverseMap();
			CreateMap<Dao.Language, App.Language>().ReverseMap();
			CreateMap<Dao.Player, App.Player>().ReverseMap();
			CreateMap<Dao.Solution, App.Solution>().ReverseMap();
			CreateMap<Dao.Template, App.Template>().ReverseMap();
			CreateMap<Dao.Test, App.Test>().ReverseMap();
			CreateMap<Dao.TestResult, App.TestResult>().ReverseMap();

			CreateMap<Comp.CompileResult, App.SharedPad.RunResult>()
				.ForMember(dest => dest.Success, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.Errors)))
				.ReverseMap();
		}

		public static MapperConfiguration Configure()
		{
			return new MapperConfiguration(x =>
			{
				x.AddProfile<AutoMapperConfiguration>();
			});
		}
	}
}
