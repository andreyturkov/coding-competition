using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CodingCompetition.Application.Interfaces;
using CodingCompetition.Application.Models;
using CodingCompetition.Data.Interfaces;

namespace CodingCompetition.Application.Services
{
	internal class PlayerService : IPlayerService
	{
		private readonly IPlayerRepository _playerRepository;
		private readonly IMapper _mapper;

		public PlayerService(IPlayerRepository playerRepository, IMapper mapper)
		{
			_playerRepository = playerRepository;
			_mapper = mapper;
		}

		public async Task<IList<Player>> GetTopPlayers(int take)
		{
			var result = await _playerRepository.GetTopPlayers(take);

			return _mapper.Map<IList<Player>>(result);
		}

		public async Task AddOrUpdate(Player player)
		{
			var result = await _playerRepository.Find(player.Email);
			if (result != null) return;

			var entity = _mapper.Map<Data.Models.Player>(player);
			await _playerRepository.Add(entity);

			player.Id = entity.Id;
		}
	}
}
