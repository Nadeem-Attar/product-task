using Newtonsoft.Json;

namespace ProductsTask.Middleware
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
