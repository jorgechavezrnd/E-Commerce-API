using System.Collections.Generic;

namespace ECommerceAPI.Dto.Response
{
    public class BaseCollectionResponse<TDtoClass>
        where TDtoClass : class
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public int TotalPages { get; set; }
        public ICollection<TDtoClass> Collection { get; set; }
    }
}
