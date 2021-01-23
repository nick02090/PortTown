using Newtonsoft.Json;

namespace WebAPI.Helpers
{
    public class JSONErrorFormatter
    {
        public string ErrorMessage { get; private set; }
        public object ErrorValue { get; private set; }
        public string APIRoute { get; private set; }
        public string FunctionName { get; private set; }

        public JSONErrorFormatter(string errorMessage, object errorValue, string apiRoute, string functionName)
        {
            ErrorMessage = errorMessage;
            ErrorValue = errorValue;
            APIRoute = apiRoute;
            FunctionName = functionName;
        }

        public string Format()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}