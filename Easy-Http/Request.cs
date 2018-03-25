using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Easy_Http
{
    public interface IRequest<Request>
        where Request : IRequest<Request>
    {
        Request SetHost(string host);
        Request SetQuery(string query);
        Request SetType(RequestType type);
        Request SetContent(HttpContent content);
        Task<HttpResponseMessage> Execute();
    }

    public abstract class BaseRequest<Request> : IRequest<Request>
        where Request : BaseRequest<Request>
    {
        public string Host { get; private set; }
        public string Query { get; private set; }
        public RequestType Type { get; private set; }
        public HttpContent Content { get; private set; }

        public Request SetHost(string host)
        {
            this.Host = host;
            return (Request)this;
        }

        public Request SetQuery(string query)
        {
            this.Query = query;
            return (Request)this;
        }

        public Request SetType(RequestType type)
        {
            this.Type = type;
            return (Request)this;
        }

        public Request SetContent(HttpContent content)
        {
            this.Content = content;
            return (Request)this;
        }

        public virtual async Task<HttpResponseMessage> Execute()
        {
            switch (this.Type)
            {
                case RequestType.Get:
                    using (HttpClient client = new HttpClient())
                    {
                        return await client.GetAsync(this.Host + this.Query);
                    }

                case RequestType.Post:
                    using (HttpClient client = new HttpClient())
                    {
                        return await client.PostAsync(this.Host + this.Query, this.Content);
                    }

                default:
                    throw new ArgumentException("Content type must be an appropriate value");
            }
        }
    }

    public enum RequestType
    {
        Get,
        Post
    }

    public enum ContentType
    {
        Application_Json,
        Multipart_FormData,

    }

    public class GeneralRequest : BaseRequest<GeneralRequest>
    {

    }
}
