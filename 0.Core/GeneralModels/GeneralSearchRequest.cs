using System.ComponentModel;

namespace _0.Core.GeneralModels
{
    public class GeneralSearchRequest
    {
        [Description("Example: Id, CreatedDate")]
        public string OrderByColumnName { get; set; } = "Id";
        [Description("Example: Asc, Desc")]
        public string OrderType { get; set; } = "Desc";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
