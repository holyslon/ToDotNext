using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using NSubstitute;
using ToDo.Model;
using ToDo.ViewModel;
using Xunit;

namespace ToDo.Tests.ViewModel
{
    public class MainWindowViewModelTests
    {
        private const string AssigmentText = "some text";
        private readonly IAssigmentsCache _assigmentsCache = Substitute.For<IAssigmentsCache>();
        private readonly ITagsCache _tagsCache = Substitute.For<ITagsCache>();

        private readonly List<IAssigment> _assigments = new List<IAssigment>
        {
            Substitute.For<IAssigment>(),
            Substitute.For<IAssigment>(),
            Substitute.For<IAssigment>()
        };

        private readonly Func<IAssigment, IAssigmentViewModel> _assigmentViewModelFactory = Substitute.For<Func<IAssigment, IAssigmentViewModel>>();

        private readonly List<IAssigmentViewModel> _assigmentsViewModels = new List<IAssigmentViewModel>
        {
            Substitute.For<IAssigmentViewModel>(),
            Substitute.For<IAssigmentViewModel>(),
            Substitute.For<IAssigmentViewModel>()
        };
        private readonly List<ITag> _tags = new List<ITag>
        {
            Substitute.For<ITag>(),
            Substitute.For<ITag>(),
            Substitute.For<ITag>()
        };

        private readonly Func<ITag, ITagViewModel> _tagViewModelFactory = Substitute.For<Func<ITag, ITagViewModel>>();

        private readonly List<ITagViewModel> _tagsViewModel = new List<ITagViewModel>
        {
            Substitute.For<ITagViewModel>(),
            Substitute.For<ITagViewModel>(),
            Substitute.For<ITagViewModel>()
        };

        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IAddAssigmentService _addAssigmentService = Substitute.For<IAddAssigmentService>();

        private ITag AddTag()
        {
            var tag = Substitute.For<ITag>();
            _tags.Add(tag);
            var tagViewModel = Substitute.For<ITagViewModel>();
            _tagsViewModel.Add(tagViewModel);

            _tagViewModelFactory(tag).Returns(tagViewModel);

            return tag;
        }

        private ITagViewModel ViewModelForTag(ITag tag)
        {
            return _tagsViewModel[_tags.IndexOf(tag)];
        }
        private IAssigment AddAssigment()
        {
            var assigment = Substitute.For<IAssigment>();
            _assigments.Add(assigment);
            var assigmentViewModel = Substitute.For<IAssigmentViewModel>();
            _assigmentsViewModels.Add(assigmentViewModel);

            _assigmentViewModelFactory(assigment).Returns(assigmentViewModel);

            return assigment;
        }

        private IAssigmentViewModel ViewModelForAssigment(IAssigment assigment)
        {
            return _assigmentsViewModels[_assigments.IndexOf(assigment)];
        }

        public MainWindowViewModelTests()
        {
            _assigmentsCache.Items.Returns(_assigments);
            _tagsCache.Items.Returns(_tags);

            _assigmentViewModelFactory(_assigments[0]).Returns(_assigmentsViewModels[0]);
            _assigmentViewModelFactory(_assigments[1]).Returns(_assigmentsViewModels[1]);
            _assigmentViewModelFactory(_assigments[2]).Returns(_assigmentsViewModels[2]);

            _tagViewModelFactory(_tags[0]).Returns(_tagsViewModel[0]);
            _tagViewModelFactory(_tags[1]).Returns(_tagsViewModel[1]);
            _tagViewModelFactory(_tags[2]).Returns(_tagsViewModel[2]);
            _mainWindowViewModel = new MainWindowViewModel(_assigmentsCache, _tagsCache, _addAssigmentService, _assigmentViewModelFactory, _tagViewModelFactory);
        }

        [Fact]
        public void ExposeAssigmentsModelFromAssigmentsCache()
        {


            Assert.Equal(_assigmentsViewModels[0], _mainWindowViewModel.Assigments[0]);
            Assert.Equal(_assigmentsViewModels[1], _mainWindowViewModel.Assigments[1]);
            Assert.Equal(_assigmentsViewModels[2], _mainWindowViewModel.Assigments[2]);   
        }

        [Fact]
        public void UpdatesAssigmentsListOnCacheChanged()
        {
            IList newIntems = null;
            ((INotifyCollectionChanged) _mainWindowViewModel.Assigments).CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    newIntems = args.NewItems;
                }
            };

            var addAssigment = AddAssigment();

            _assigmentsCache.AssimentAdded += Raise.Event<Action<IAssigment>>(addAssigment);

            Assert.Equal(ViewModelForAssigment(addAssigment), newIntems[0]);
        }

        [Fact]
        public void ExposeTagsModelFromTagsCache()
        {
            Assert.Equal(_tagsViewModel[0], _mainWindowViewModel.AvalibleTags[0]);
            Assert.Equal(_tagsViewModel[1], _mainWindowViewModel.AvalibleTags[1]);
            Assert.Equal(_tagsViewModel[2], _mainWindowViewModel.AvalibleTags[2]);   
        }

        [Fact]
        public void UpdatesTagsListOnCacheChanged()
        {
            IList newIntems = null;
            ((INotifyCollectionChanged) _mainWindowViewModel.AvalibleTags).CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    newIntems = args.NewItems;
                }
            };

            var tag = AddTag();

            _tagsCache.TagAdded += Raise.Event<Action<ITag>>(tag);

            Assert.Equal(ViewModelForTag(tag), newIntems[0]);
        }

        [Fact]
        public void CallAddAssigmentServiceOnAddAssigmentCommandExecuted()
        {
            _mainWindowViewModel.AssigmentText = AssigmentText;
            _mainWindowViewModel.AddAssigment.Execute(null);

            _addAssigmentService.Received().AddAssigment(AssigmentText);
        }
    }
}