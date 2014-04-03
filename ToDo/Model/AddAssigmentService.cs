using System;
using System.Linq;

namespace ToDo.Model
{
    public class AddAssigmentService : IAddAssigmentService
    {
        private readonly IAssigmentsCache _assigmentsCache;
        private readonly ITagsCache _tagsCache;
        private readonly Func<string, ITag[], IAssigment> _assigmentFactory;
        private readonly Func<string, ITag> _tagFactory;

        public AddAssigmentService(IAssigmentsCache assigmentsCache, ITagsCache tagsCache, Func<string, ITag[], IAssigment> assigmentFactory, Func<string, ITag> tagFactory)
        {
            _assigmentsCache = assigmentsCache;
            _tagsCache = tagsCache;
            _assigmentFactory = assigmentFactory;
            _tagFactory = tagFactory;
        }

        public void AddAssigment(string assigmentText)
        {
            var splits = assigmentText.Split(' ');

            var tags = splits.Where(s => s.StartsWith("#")).ToArray();

            var tagsToRemove = splits.Reverse().TakeWhile(s => s.StartsWith("#")).Reverse().ToArray();

            assigmentText = tagsToRemove.Aggregate(assigmentText, (current, tag) => current.Replace(tag, "")).TrimEnd();
            assigmentText = tags.Except(tagsToRemove)
                .Aggregate(assigmentText, (current, tag) => current.Replace(tag, tag.Substring(1)));

            var tagModels = tags.Select(s => s.Substring(1)).Select(tag => _tagFactory(tag)).ToArray();
            _assigmentsCache.Add(_assigmentFactory(assigmentText, tagModels));
            foreach (var tagModel in tagModels)
            {
                _tagsCache.Add(tagModel);
            }
        }
    }
}