namespace Proxy_Server.Models
{
    public class ProxyRequestModel
    {
        public string Method { get; set; }
        public string Url { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
    }
}
