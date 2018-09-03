using System;
using System.Collections.Generic;
using Domain.Enums;
using Domain.Interfaces.Process;

namespace Domain.Services.Services.Process
{
    public class NextStateService : INextStateService
    {
        public IReadOnlyList<EState> NextStates(EState state)
        {
            switch (state)
            {
                case EState.Registered:
                    return new [] {EState.InProgress };
                case EState.InProgress:
                    return new[] { EState.Done, EState.NotDone };
                case EState.Done:
                    return new EState[0];
                case EState.NotDone:
                    return new EState[0];
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
