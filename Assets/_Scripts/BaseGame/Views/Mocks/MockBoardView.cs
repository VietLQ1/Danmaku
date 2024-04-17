﻿using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Default
{
    public class MockBoardView : DanmakuBoardBaseView
    {
        public override void DrawCardFromMainDeck(DanmakuPlayerModel playerModel, DanmakuMainDeckCardModel card)
        {
            var handView = InteractionViewRepo.SetupPlayerView.GetPlayerView(playerModel).CardHandView;
            DanmakuMainDeckCardBaseView cardView = new GameObject("Mock Card ").AddComponent<DanmakuMainDeckCardBaseView>();
            handView.AddCard(cardView,card);
        }

        public override void DrawCardFromMainDeck(DanmakuPlayerModel playerModel, List<DanmakuMainDeckCardModel> cards)
        {
            foreach (var card in cards)
            {
                DrawCardFromMainDeck(playerModel, card);
            }
        }

        public override void DiscardCardToDiscardDeck(DanmakuPlayerModel playerModel, DanmakuMainDeckCardModel card)
        {
            
        }

        public override void DiscardCardsToDiscardDeck(DanmakuPlayerModel playerModel, List<DanmakuMainDeckCardModel> cards)
        {
            
        }
    }
}