﻿using _Scripts.BaseGame.Views;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuInteractionSystem : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private DanmakuInteractionViewRepo _interactionViewRepo;
        
        [Header("Configurations")]
        [SerializeField] private int _playerCount;
        [SerializeField] private RoleSetConfig _roleSetConfig;
        [SerializeField] private DeckSetConfig _deckSetConfig;
        [SerializeField] private StartupStatsConfig _startupStatsConfig;
        
        [Header("Controller")]
        public DanmakuInteractionController InteractionController;
        
        
        private void Start()
        {
            InteractionController = new DanmakuInteractionController(_interactionViewRepo);
     
            
            var setupSubController =  new DanmakuSetupSubController.Builder(InteractionController, _interactionViewRepo.SetupPlayerView)
                .WithPlayerCount(_playerCount)
                .WithPlayerRoles(_roleSetConfig)
                .WithCardDeck(_deckSetConfig)
                .Build();
            
            setupSubController.SetupStartingStats(_startupStatsConfig);

            InteractionController.SetSetupSubController(setupSubController);
            InteractionController.SetupModels(setupSubController.GetBoardModel(),setupSubController.GetPlayerGroupModel());
            
            var boardController = new DanmakuBoardController(InteractionController);
            InteractionController.SetBoardController(boardController);
            
            var playerSubController = new DanmakuPlayerSubController(InteractionController);
            InteractionController.SetPlayerSubController(playerSubController);
            
            playerSubController.StartupReveal();
            boardController.StartupDraw();
            playerSubController.StartGame();


        }
        
        
    }
}