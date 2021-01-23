namespace WebAPI.Helpers
{
    public class JSONErrorFormatter
    {
        public string ErrorMessage { get; private set; }
        public object ErrorValue { get; private set; }
        public string FieldName { get; private set; }
        public string HttpMethod { get; private set; }
        public string APIRoute { get; private set; }
        public string FunctionName { get; private set; }

        public JSONErrorFormatter(string errorMessage, object errorValue, string fieldName,
            string httpMethod, string apiRoute, string functionName)
        {
            ErrorMessage = errorMessage;
            ErrorValue = errorValue;
            FieldName = fieldName;
            HttpMethod = httpMethod;
            APIRoute = apiRoute;
            FunctionName = functionName;
        }
    }
}