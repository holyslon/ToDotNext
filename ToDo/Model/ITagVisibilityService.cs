using System;

namespace ToDo.Model
{
    public interface ITagVisibilityService
    {
        void TagSelected(ITag tag);
        void TagDeselected(ITag tag);
        bool IsTagSelected(ITag tag);

        event Action TagVisibilityChanged;
    }
}