﻿using System.Collections.Generic;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Roles;
using _Scripts.CoreGame.InteractionSystems.Setups;
using Shun_Utilities;


namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuPlayerGroupModel
    {
        public readonly int PlayerCount;
        public List<DanmakuPlayerModel> Players { get; private set; }
        
        public ObservableData<int> CurrentPlayerTurnIndex { get; private set; }
        public ObservableData<DanmakuPlayerModel> CurrentPlayerTurn { get; private set; }
        
        public ObservableData<PlayStepEnum> CurrentPlayStepEnum { get; private set; }
        
        public DanmakuPlayerGroupModel(List<DanmakuPlayerModel> currentPlayer)
        {
            Players = currentPlayer;
            PlayerCount = currentPlayer.Count;
            CurrentPlayerTurnIndex = new ObservableData<int>(0);
            CurrentPlayerTurn = new ObservableData<DanmakuPlayerModel>(Players[0]);
            CurrentPlayStepEnum = new ObservableData<PlayStepEnum>(PlayStepEnum.StartOfTurnStep);
            
            
        }

        public void SetCurrentTurnPlayer(DanmakuPlayerModel player)
        {
            CurrentPlayerTurn.Value = player;
            CurrentPlayerTurnIndex.Value = Players.IndexOf(player);
            CurrentPlayStepEnum.Value = PlayStepEnum.SetupStep;
        }
        
        public DanmakuPlayerModel SetNextPlayerTurn()
        {
            int nextIndex = CurrentPlayerTurnIndex.Value + 1;
            if (nextIndex >= PlayerCount)
            {
                nextIndex = 0;
            }
            CurrentPlayerTurn.Value = Players[nextIndex];
            CurrentPlayerTurnIndex.Value = nextIndex;
            CurrentPlayStepEnum.Value = PlayStepEnum.SetupStep;

            return CurrentPlayerTurn.Value;
        }

        
    }
}