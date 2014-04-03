using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ToDo.Model
{
    public class Assigment : IAssigment
    {
        public Assigment(string s, ITag[] tags)
        {
            Text = s;
            Tags = new ReadOnlyCollection<ITag>(tags);
        }


        public string Text { get; private set; }
        public IEnumerable<ITag> Tags { get; private set; }
    }
}