using System;
using System.Linq.Expressions;

namespace Easy_Http.Builders
{
    public interface IRequestBuilder<RBuilder, Request, Model>
    where RBuilder : IRequestBuilder<RBuilder, Request, Model>
    , new() where Request : IRequest<Request>
    , new() where Model : class
    {
        RBuilder AddHeader(Expression<Func<HeadersBuffer, Headers>> expr, string value);
        RBuilder AddHeader(string header, string value);
        RBuilder SetContentType(ContentType type);
        RBuilder SetType(RequestType type);
        RBuilder SetHost(string hostUrl);
        RBuilder SetModelToSerialize(Model model, bool checkForAttribute);
        Request Build();
    }

    public interface IQueryBuilder<QBuilder, RBuilder, Request, Model>
        where QBuilder : IQueryBuilder<QBuilder, RBuilder, Request, Model>
        , new() where RBuilder : IRequestBuilder<RBuilder, Request, Model>
        , new() where Request : IRequest<Request>
        , new() where Model : class
    {
        QBuilder SetQuery(string query);
        string GetQuery();
        QBuilder ParseModelToQuery(Model model, bool checkForAttribute);
        RBuilder Build();
    }
}
