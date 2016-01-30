﻿using Devkoes.HttpMessage.Models.Contracts;
using Devkoes.HttpMessage.Models.Schemas;
using Devkoes.HttpMessage.Plumbing;

namespace Devkoes.HttpMessage.Headers.Request
{
    public class ContentTypeHeader : HttpSingleQuantifiedHeaderBase
    {
        internal static string NAME = "Content-Type";
        internal static string CHARSET_QUANTIFIER_NAME = "charset";

        public MediaType ContentType { get; internal set; }
        public string ContentCharset { get; internal set; }

        public ContentTypeHeader(string value, QuantifiedHeaderValue quantifiedHeaderValue) : base(NAME, value, quantifiedHeaderValue)
        {
            ContentType = HttpCodesTranslator.Default.GetMediaType(QuantifiedHeaderValue.HeaderValue);
            ContentCharset = QuantifiedHeaderValue.FindQuantifierValue(CHARSET_QUANTIFIER_NAME);
        }

        public override void Visit<T>(IHttpRequestHeaderVisitor<T> v, T arg)
        {
            v.Visit(this, arg);
        }
    }
}
