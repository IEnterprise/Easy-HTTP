using System;
using System.Collections.Generic;
using System.Text;

namespace Easy_Http
{
    public enum Headers
    {
        Accept,
        Accept_Language,
        Authorization,
        Cache_Control,
        Content_MD5,
        Content_Type,
        From,
        If_Match,
        If_Modified_Since,
        If_None_Match,
        If_Range,
        If_Unmodified_Since,
        Max_Forwards,
        Pragma,
        Proxy_Authorization,
        Range,
        Warning,
        X_Requested_With,
        X_Do_Not_Track,
        DNT,
        x_api_key,
        Accept_Charset,
        Accept_Encoding,
        Access_Control_Request_Method,
        Connection,
        Content_Lenght,
        Cookie,
        Cookie__2,
        Content_Transfer_Encoding,
        Date,
        Expect,
        Host,
        Keep_Alive,
        Origin,
        Referer,
        TE,
        Trailer,
        Transfer_Encoding,
        Upgrade,
        User_Agent,
        Via
    }
    public class HeadersBuffer
    {
        public Headers Accept { get; private set; } = Headers.Accept;
        public Headers Accept_Language { get; private set; } = Headers.Accept_Language;
        public Headers Authorization { get; private set; } = Headers.Authorization;
        public Headers Cache_Control { get; private set; } = Headers.Cache_Control;
        public Headers Content_MD5 { get; private set; } = Headers.Content_MD5;
        public Headers Content_Type { get; private set; } = Headers.Content_Type;
        public Headers From { get; private set; } = Headers.From;
        public Headers If_Match { get; private set; } = Headers.If_Match;
        public Headers If_Modified_Since { get; private set; } = Headers.If_Modified_Since;
        public Headers If_None_Match { get; private set; } = Headers.If_None_Match;
        public Headers If_Range { get; private set; } = Headers.If_Range;
        public Headers If_Unmodified_Since { get; private set; } = Headers.If_Unmodified_Since;
        public Headers Max_Forwards { get; private set; } = Headers.Max_Forwards;
        public Headers Pragma { get; private set; } = Headers.Pragma;
        public Headers Proxy_Authorization { get; private set; } = Headers.Proxy_Authorization;
        public Headers Range { get; private set; } = Headers.Range;
        public Headers Warning { get; private set; } = Headers.Warning;
        public Headers X_Requested_With { get; private set; } = Headers.X_Requested_With;
        public Headers X_Do_Not_Track { get; private set; } = Headers.X_Do_Not_Track;
        public Headers DNT { get; private set; } = Headers.DNT;
        public Headers x_api_key { get; private set; } = Headers.x_api_key;
        public Headers Accept_Charset { get; private set; } = Headers.Accept_Charset;
        public Headers Accept_Encoding { get; private set; } = Headers.Accept_Encoding;
        public Headers Access_Control_Request_Method { get; private set; } = Headers.Access_Control_Request_Method;
        public Headers Connection { get; private set; } = Headers.Connection;
        public Headers Content_Lenght { get; private set; } = Headers.Content_Lenght;
        public Headers Cookie { get; private set; } = Headers.Cookie;
        public Headers Cookie__2 { get; private set; } = Headers.Cookie__2;
        public Headers Content_Transfer_Encoding { get; private set; } = Headers.Content_Transfer_Encoding;
        public Headers Date { get; private set; } = Headers.Date;
        public Headers Expect { get; private set; } = Headers.Expect;
        public Headers Host { get; private set; } = Headers.Host;
        public Headers Keep_Alive { get; private set; } = Headers.Keep_Alive;
        public Headers Origin { get; private set; } = Headers.Origin;
        public Headers Referer { get; private set; } = Headers.Referer;
        public Headers TE { get; private set; } = Headers.TE;
        public Headers Trailer { get; private set; } = Headers.Trailer;
        public Headers Transfer_Encoding { get; private set; } = Headers.Transfer_Encoding;
        public Headers Upgrade { get; private set; } = Headers.Upgrade;
        public Headers User_Agent { get; private set; } = Headers.User_Agent;
        public Headers Via { get; private set; } = Headers.Via;
    }
}
