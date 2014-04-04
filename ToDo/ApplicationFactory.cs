using System;
using System.Linq;
using System.Windows.Media.Imaging;
using ToDo.Model;
using ToDo.ViewModel;

namespace ToDo
{
    public class ApplicationFactory
    {
        public static Func<string, ITag> CreateTagFactory(ITagsCache tagsCache)
        {
            return s =>
            {
                var existingTag = tagsCache.Items.FirstOrDefault(tag => tag.Text == s);
                if (existingTag != null) return existingTag;
                return new Tag(s);
            };
        }

        public static MainWindowViewModel CreateWindowViewModel()
        {
            Func<string, ITag[], IAssigment> assigmentFactory = (s, tags) => new Assigment(s, tags);
            Func<ITag, ITagViewModel> tagViewModelFactory = (m) => new TagViewModel(m);
            var assigmentsCache = new AssigmentCache();
            var tagsCache = new TagsCache();
            var addAssigmentService = new AddAssigmentService(assigmentsCache, tagsCache, assigmentFactory, CreateTagFactory(tagsCache));
            var tagVisibilityService = new TagVisibilityService();
            Func<IAssigment, IAssigmentViewModel> assigmentViewModelFactory = (m) => new AssigmentViewModel(m, tagViewModelFactory, tagVisibilityService);
            Func<ITag, ISelectableTagViewModel> selectableTagViewModelFactory = (m) => new SelectableTagViewModel(m, tagVisibilityService);
            return new MainWindowViewModel(assigmentsCache, 
                tagsCache, 
                addAssigmentService, 
                assigmentViewModelFactory,
                selectableTagViewModelFactory);
        }
    }
}