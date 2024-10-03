using System.Collections.Specialized;

namespace Station.Core.Helpers.SelectList
{
    public class SelectListItem : SelectListItemBase<int>
    {
        public SelectListItem()
        {

        }

        public SelectListItem(int id, string name) : base(id, name)
        { }
    }
}
