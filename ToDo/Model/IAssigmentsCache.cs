using System;
using System.Collections.Generic;

namespace ToDo.Model
{
    public interface IAssigmentsCache
    {
        IEnumerable<IAssigment> Items { get; }

        void Add(IAssigment assigment);
        event Action<IAssigment> AssimentAdded;
    }
}