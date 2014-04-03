using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ToDo.Model
{
    public class AssigmentCache : IAssigmentsCache
    {
        private readonly List<IAssigment> _assigments = new List<IAssigment>();

        public IEnumerable<IAssigment> Items
        {
            get { return new ReadOnlyCollection<IAssigment>(_assigments); }
        }

        public event Action<IAssigment> AssimentAdded;

        protected virtual void OnAssimentAdded(IAssigment obj)
        {
            var handler = AssimentAdded;
            if (handler != null) handler(obj);
        }

        public void Add(IAssigment assigment)
        {
            _assigments.Add(assigment);
            OnAssimentAdded(assigment);
        }
    }
}