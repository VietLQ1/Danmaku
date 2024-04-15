﻿using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Stats;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuCombatController
    {
        DanmakuInteractionController _danmakuInteractionController;
        DanmakuBoardModel BoardModel => _danmakuInteractionController.BoardModel;
        
        private Stack<CardExecution> _playedCards = new (); 
        
        public DanmakuCombatController(DanmakuInteractionController danmakuInteractionController)
        {
            _danmakuInteractionController = danmakuInteractionController;
        }
        
        public class CardExecution
        {
            public IDanmakuCard Card;
            public IDanmakuCardRule DanmakuCardRule;
            public List<IDanmakuActivator> Activator;
            public List<IDanmakuTargetable> Targetable;
            
            public CardExecution(IDanmakuCard card, IDanmakuCardRule danmakuCardRule, List<IDanmakuActivator> activator, List<IDanmakuTargetable> targetable)
            {
                Card = card;
                DanmakuCardRule = danmakuCardRule;
                Activator = activator;
                Targetable = targetable;
            }
            
        } 

        public void AddCardExecution(CardExecution cardExecution)
        {
            _playedCards.Push(cardExecution);
            
            cardExecution.Card.RevealCard();
        }
        
        
        
        public void ResolveCombat()
        {
            while (_playedCards.TryPop(out var cardExecution))
            {
                cardExecution.Card.ExecuteCard(cardExecution.DanmakuCardRule, cardExecution.Activator, cardExecution.Targetable);
                
                BoardModel.DiscardDeckModel.AddCard(cardExecution.Card);
            }
        }
        
        
    }
}