using AutoMapper;
using CodingCompetition.Application.Interfaces;
using CodingCompetition.Application.Models;
using CodingCompetition.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodingCompetition.Application.Services
{
	internal class ChallengeService : IChallengeService
	{
		private readonly IChallengeRepository _challengeRepository;
		private readonly IMapper _mapper;

		public ChallengeService(IChallengeRepository challengeRepository, IMapper mapper)
		{
			_challengeRepository = challengeRepository;
			_mapper = mapper;
		}

		public async Task<Challenge> Get(int id)
		{
			var result = await _challengeRepository.Get(id);

			return _mapper.Map<Challenge>(result);
		}

		public async Task<IList<Challenge>> GetAll()
		{
			var result = await _challengeRepository.GetAll();

			return _mapper.Map<IList<Challenge>>(result);
		}
	}
}
