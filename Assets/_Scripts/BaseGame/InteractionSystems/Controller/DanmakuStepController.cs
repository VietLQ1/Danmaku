﻿using System;
using System.Collections.Generic;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Setups;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuStepController
    {
	    ISetupPlayerView _setupPlayerView;
        
        DanmakuPlayerGroupModel _playerGroupModel;

        private DanmakuSessionController _sessionController;

        private DanmakuStepController(DanmakuSessionController sessionController, DanmakuPlayerGroupModel playerGroupModel, ISetupPlayerView setupPlayerView)
        {
            _sessionController = sessionController;
            _playerGroupModel = playerGroupModel;
            _setupPlayerView = setupPlayerView;
        }
        
        public class Builder
        {
            private DanmakuPlayerGroupModel _playerGroupModel = new (new List<DanmakuPlayerModel>());
            private ISetupPlayerView _setupPlayerView;
            
            public Builder WithPlayerGroupModel(int playerCount, RoleSetConfig roleSetConfig)
            {
                List<DanmakuPlayerModel> players = new List<DanmakuPlayerModel>();
                
                for (int i = 0; i < playerCount; i++)
                {
                    var player = new DanmakuPlayerModel();
                    players.Add(player);   
                }
                
                _playerGroupModel = new DanmakuPlayerGroupModel(players);

                return this;
            }
            
            public DanmakuStepController Build(DanmakuSessionController sessionController, ISetupPlayerView setupPlayerView)
            {
                return new DanmakuStepController(sessionController, _playerGroupModel, setupPlayerView);
            }
            
        }
        
        public void SetupPlayerGroup(int playerCount, RoleSetConfig roleSetConfig)
        {
            DanmakuRoleSetupDirector roleSetupDirector = new DanmakuRoleSetupDirector(_playerGroupModel, _playerGroupModel.Players, roleSetConfig);
            
            var playerToRole = roleSetupDirector.SetupRoles();
            _setupPlayerView.SetupPlayerRoleView(playerToRole);
            
        }
        
        public void StartGame()
        {
            _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.SetupStep;
            _playerGroupModel.CurrentPlayerTurnIndex.Value = 0;
            _playerGroupModel.CurrentPlayerTurn.Value = _playerGroupModel.Players[0];
        }
        
        public void StartPlayerNextStep()
        {
            switch (_playerGroupModel.CurrentPlayStepEnum.Value)
            {
                case PlayStepEnum.SetupStep:
                    _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.StartOfTurnStep;
                    break;
                case PlayStepEnum.StartOfTurnStep:
                    _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.IncidentStep;
                    break;
                case PlayStepEnum.IncidentStep:
                    _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.DrawStep;
                    break;
                case PlayStepEnum.DrawStep:
                    _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.MainStep;
                    break;
                case PlayStepEnum.MainStep:
                    _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.DiscardStep;
                    break;
                case PlayStepEnum.DiscardStep:
                    _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.EndOfTurnStep;
                    break;
                case PlayStepEnum.EndOfTurnStep:
                    StartPlayerTurn();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private void StartPlayerTurn()
        {
            _playerGroupModel.CurrentPlayStepEnum.Value = PlayStepEnum.SetupStep;
            _playerGroupModel.CurrentPlayerTurnIndex.Value++;
            _playerGroupModel.CurrentPlayerTurn.Value = _playerGroupModel.Players[0];
        }

    }
}