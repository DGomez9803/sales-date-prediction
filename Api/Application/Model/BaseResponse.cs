namespace Application.Models
{
    public class BaseResponse
    {
        public string Error { get; set; }

        public bool Success { get; set; }

        public object Data { get; set; }
    }
}
