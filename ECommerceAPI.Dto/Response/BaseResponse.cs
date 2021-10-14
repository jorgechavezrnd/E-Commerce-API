namespace ECommerceAPI.Dto.Response
{
    public class BaseResponse<TResult>
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public TResult Result { get; set; }
    }
}
