namespace Genting.Infrastructure.CommonServices.Client.Core
{
    public class JsonSerializer: ISerializer
    {
        public T Deserialize<T>(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
        }

        public string Serialize(object info)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(info);
        }

        private static JsonSerializer _instance;
        public static JsonSerializer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new JsonSerializer();

                return _instance;
            }
        }
    }
}