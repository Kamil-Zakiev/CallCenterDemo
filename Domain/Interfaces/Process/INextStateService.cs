using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Interfaces.Process
{
    public interface INextStateService
    {
        IReadOnlyList<EState> NextStates(EState state);
    }
}