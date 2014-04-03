using System;
using System.Linq;
using NSubstitute;
using ToDo.Model;
using Xunit;

namespace ToDo.Tests.Model
{

    public class AddAssigmentServiceTests
    {
        private const string Text = "some text";
        private readonly AddAssigmentService _addAssigmentService;
        private readonly IAssigmentsCache _assigmentsCache = Substitute.For<IAssigmentsCache>();
        private readonly ITagsCache _tagsCache = Substitute.For<ITagsCache>();
        private readonly Func<string, ITag[], IAssigment> _assigmentFactory = Substitute.For<Func<string, ITag[], IAssigment>>();
        private readonly Func<string, ITag> _tagFactory = Substitute.For<Func<string, ITag>>();
        private readonly string[] _tags = { "a", "b", "c"};

        public AddAssigmentServiceTests()
        {
//            _assigmentFactory(Arg.Any<string>(), Arg.Any<ITag[]>()).Returns(info =>
//            {
//                throw new Exception("Call not setuped factory");
//            });            
//            _tagFactory(Arg.Any<string>()).Returns(info =>
//            {
//                throw new Exception("Call not setuped factory");
//            });
            _addAssigmentService = new AddAssigmentService(_assigmentsCache, _tagsCache, _assigmentFactory, _tagFactory);
        }

        [Fact]
        public void CreateAssigmentForGivenString()
        {
            var expectedAssigment = CreateAssigment(Text, new ITag[0]);

            _addAssigmentService.AddAssigment(Text);

            _assigmentsCache.Received().Add(expectedAssigment);
        }

        private IAssigment CreateAssigment(string text, ITag[] tags)
        {
            var expectedAssigment = Substitute.For<IAssigment>();
            _assigmentFactory(text, Arg.Any<ITag[]>()).Returns(info =>
            {
                if(!info.Arg<ITag[]>().SequenceEqual(tags)) throw new Exception("Tags are not equal"); 
                return expectedAssigment;
            });
            return expectedAssigment;
        }        
        
        private ITag CreateTag(string text)
        {
            var tag = Substitute.For<ITag>();
            _tagFactory(text).Returns(tag);
            return tag;
        }

        [Fact]
        public void AddsMentionedTagsToTagCache()
        {
            var expectedTags = _tags.Select(CreateTag).ToArray();
            var expectedAssigment = CreateAssigment(Text, expectedTags);


            var fullAssigmentString = Text + " " + string.Join(" ", _tags.Select(s => "#" + s));

            _addAssigmentService.AddAssigment(fullAssigmentString);

            _assigmentsCache.Received().Add(expectedAssigment);

            foreach (var expectedTag in expectedTags)
            {
                _tagsCache.Received().Add(expectedTag);
            }
        }
        [Fact]
        public void AddsMentionedTagsToCreatedAssigment()
        {
            var expectedTags = _tags.Select(CreateTag).ToArray();
            var expectedAssigment = CreateAssigment(Text, expectedTags);


            var fullAssigmentString = Text + " " + string.Join(" ", _tags.Select(s => "#" + s));

            _addAssigmentService.AddAssigment(fullAssigmentString);

            _assigmentsCache.Received().Add(expectedAssigment);
        }

        [Fact]
        public void DoesNotRemoveTagFromMiddleOffString()
        {
            var expectedDotNextTag = CreateTag("dotnext");
            var expectedTags = _tags.Select(CreateTag).ToArray();
            var expectedAssigment = CreateAssigment("dotnext " + Text, new []{expectedDotNextTag}.Concat(expectedTags).ToArray());


            var fullAssigmentString = "#dotnext " + Text + " " + string.Join(" ", _tags.Select(s => "#" + s));

            _addAssigmentService.AddAssigment(fullAssigmentString);

            _assigmentsCache.Received().Add(expectedAssigment);
            _tagsCache.Received().Add(expectedDotNextTag);
            foreach (var expectedTag in expectedTags)
            {
                _tagsCache.Received().Add(expectedTag);
            }
        }
    }
}