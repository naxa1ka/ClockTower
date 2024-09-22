﻿using System.Collections.Generic;
using System.Linq;

namespace BloodClockTower.Game
{
    public class VotingRoundFromViewModelPlayers : IVotingRound
    {
        private readonly VotingRound _votingRound;

        public IPlayer Initiator => _votingRound.Initiator;
        public IPlayer Nominee => _votingRound.Nominee;
        public IReadOnlyCollection<IPlayer> Participants => _votingRound.Participants;
        public IReadOnlyCollection<IPlayer> IgnoredParticipants => _votingRound.IgnoredParticipants;

        public VotingRoundFromViewModelPlayers(IEnumerable<PlayerViewModel> players)
        {
            var playerViewModels = players.ToList();
            _votingRound = new VotingRound(
                playerViewModels.Single(model => model.Role.Value.IsInitiator).Player,
                playerViewModels.Single(model => model.Role.Value.IsNominee).Player,
                playerViewModels
                    .Where(playerViewModel => playerViewModel.IsParticipant)
                    .Select(playerViewModel => playerViewModel.Player)
                    .ToList(),
                playerViewModels
                    .Where(playerViewModel => playerViewModel.IsIgnoredParticipant)
                    .Select(playerViewModel => playerViewModel.Player)
                    .ToList()
            );
        }

        public IVotingRound DeepClone() => _votingRound.DeepClone();
    }
}
