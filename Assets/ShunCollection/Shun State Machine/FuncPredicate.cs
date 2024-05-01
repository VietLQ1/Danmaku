﻿using System;

namespace Shun_State_Machine
{
    public class FuncPredicate : IPredicate
    {
        private readonly Func<bool> _predicate;

        public FuncPredicate( Func<bool> predicate)
        {
            _predicate = predicate;
        }
        
        public bool Evaluate() => _predicate.Invoke();

    }
}