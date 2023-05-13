namespace WebApplication1.Dtos
{
    public class ResponseBase
    {
        public bool IsSuccess { get { return string.IsNullOrWhiteSpace(ErrorMessage); } }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
