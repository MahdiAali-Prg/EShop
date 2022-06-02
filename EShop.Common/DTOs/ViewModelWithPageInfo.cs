
namespace EShop.Common.DTOs
{
    public class ViewModelWithPageInfo<TModel> where TModel : class
    {
        public PaginationInfo PaginationInfo { get; set; }

        public TModel ViewModel { get; set; }
    }
}
