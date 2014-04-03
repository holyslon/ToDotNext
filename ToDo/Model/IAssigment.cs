using System.Collections.Generic;

namespace ToDo.Model
{
    public interface IAssigment
    {
        string Text { get; }
        IEnumerable<ITag> Tags { get; }
    }
}