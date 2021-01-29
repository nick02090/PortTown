using System.Collections.Generic;

namespace WebAPI.Helpers
{
    public class JSONFormatter
    {
        public Dictionary<string, object> Fields { get; private set; }
        public object this[string fieldName]
        {
            get { return Fields[fieldName]; }
            set { Fields[fieldName] = value; }
        }

        public JSONFormatter()
        {
            Fields = new Dictionary<string, object>();
        }

        public void AddField(string fieldName, object fieldValue)
        {
            Fields.Add(fieldName, fieldValue);
        }

        public object GetFieldValue(string fieldName)
        {
            return Fields[fieldName];
        }

        public Dictionary<string, object> Result => Fields;
    }
}