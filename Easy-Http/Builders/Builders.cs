using Easy_Http.Helpers;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Easy_Http.Builders
{
    public sealed class RequestBuilder<Model> : BaseRequestBuilder<RequestBuilder<Model>, QueryBuilder<Model>, GeneralRequest, Model>
        where Model : class
    {
        public override GeneralRequest Build()
        {
            GeneralRequest request = new GeneralRequest()
                .SetHost(this.host)
                .SetQuery(this.querryBuilder.GetQuery())
                .SetType(this.Type)
                .SetContent(this.CreateContent());
            this.Dispose();

            return request;
        }
    }

    public sealed class QueryBuilder<Model> : BaseQueryBuilder<RequestBuilder<Model>, QueryBuilder<Model>, GeneralRequest, Model>
        where Model : class
    {
        public override RequestBuilder<Model> Build()
        {
            return this.requestBuilder;
        }
    }

    public abstract class BaseRequestBuilder<RBuilder, QBuilder, Request, Model> : IRequestBuilder<RBuilder, Request, Model>, IDisposable
        where RBuilder : BaseRequestBuilder<RBuilder, QBuilder, Request, Model>
        , new() where QBuilder : BaseQueryBuilder<RBuilder, QBuilder, Request, Model>
        , new() where Request : BaseRequest<Request>
        , new() where Model : class
    {
        protected QBuilder querryBuilder = new QBuilder();
        protected Model model;
        protected bool checkForAttribute;

        protected string host;

        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        protected async System.Threading.Tasks.Task<HttpContent> CreateContentAsync()
        {
            switch (this.ContentType)
            {
                case ContentType.Application_Json:
                    string json = JsonConvert.SerializeObject(this.model);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);
                    ByteArrayContent byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return byteContent;

                case ContentType.Multipart_FormData:
                    MultipartFormDataContent content = new MultipartFormDataContent();
                    await content.ParseObjectAsync(this.model);

                    return content;

                default:
                    throw new ArgumentException("Content type must be an appropriate value");
            }
        }

        protected HttpContent CreateContent()
        {
            switch (this.ContentType)
            {
                case ContentType.Application_Json:
                    string json = JsonConvert.SerializeObject(this.model);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);
                    ByteArrayContent byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return byteContent;

                case ContentType.Multipart_FormData:
                    MultipartFormDataContent content = new MultipartFormDataContent();
                    content.ParseObject(this.model);

                    return content;

                default:
                    throw new ArgumentException("Content type must be an appropriate value");
            }
        }

        public RequestType Type { get; private set; } = RequestType.Get;

        public ContentType ContentType { get; private set; } = ContentType.Application_Json;

        public QBuilder ContinueToQuery()
        {
            this.querryBuilder.SetRequestBuilder((RBuilder)this);
            return this.querryBuilder;
        }

        public RBuilder SetHost(string hostUrl)
        {
            this.host = hostUrl;
            return (RBuilder)this;
        }

        public RBuilder SetType(RequestType type)
        {
            this.Type = type;
            return (RBuilder)this;
        }

        public RBuilder SetContentType(ContentType type)
        {
            this.ContentType = type;
            return (RBuilder)this;
        }

        public RBuilder SetModelToSerialize(Model model, bool checkForAttribute = false)
        {
            this.model = model;
            this.checkForAttribute = checkForAttribute;
            return (RBuilder)this;
        }

        public abstract Request Build();

        public void Dispose()
        {
            if (disposed) throw new ObjectDisposedException(this.GetType().Name);

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            querryBuilder = null;
            host = null;
            model = null;
            disposed = true;
        }
    }

    public abstract class BaseQueryBuilder<RBuilder, QBuilder, Request, Model> : IQueryBuilder<QBuilder, RBuilder, Request, Model>, IDisposable
        where RBuilder : IRequestBuilder<RBuilder, Request, Model>,
        new() where QBuilder : BaseQueryBuilder<RBuilder, QBuilder, Request, Model>,
        new() where Request : BaseRequest<Request>,
        new() where Model : class
    {
        protected RBuilder requestBuilder;

        protected string query = "?";

        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void SetRequestBuilder(RBuilder builder)
        {
            this.requestBuilder = builder;
        }

        public abstract RBuilder Build();

        public QBuilder SetQuery(string query)
        {
            this.query = query + "?";
            return (QBuilder)this;
        }

        public QBuilder ParseModelToQuery(Model model, bool checkForAttribute = false)
        {
            Type modelType = model.GetType();
            EndpotinContract att = new EndpotinContract();

            IEnumerable<PropertyInfo> fields = checkForAttribute ?
                modelType.GetPropertiesWithAttribute(att, x => x.PutInRequest == true)
                : modelType.GetProperties();

            foreach (var field in fields)
            {
                var value = field.GetValue(model);
                if (value != null)
                    this.query += field.Name + "=" + value + "&";
            }

            return (QBuilder)this;
        }

        public string GetQuery()
        {
            return this.query;
        }

        public void Dispose()
        {
            if (disposed) throw new ObjectDisposedException(this.GetType().Name);

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            this.requestBuilder = default(RBuilder);
            this.query = null;
            disposed = true;
        }
    }

}
