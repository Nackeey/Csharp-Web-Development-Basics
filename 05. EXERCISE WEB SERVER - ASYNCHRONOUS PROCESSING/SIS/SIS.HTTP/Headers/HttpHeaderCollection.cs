namespace SIS.HTTP.Headers
{
    using SIS.HTTP.Headers.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly IDictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(HttpHeader header)
        {
            var headerKey = header.Key;

            if (this.headers.ContainsKey(headerKey))
            {
                throw new InvalidOperationException($"The headers collection already contains this {header}.");
            }
            else if (header == null)
            {
                throw new ArgumentNullException("The header you trying to add in the collection is null.");
            }

            this.headers.Add(headerKey, header);
        }

        public bool ContainsHeader(string key) => this.headers.ContainsKey(key);

        public HttpHeader GetHeader(string key) => this.headers[key];

        public override string ToString()
        {
            var sb = new StringBuilder();

            var headers = this.headers.Values;
            foreach (var header in headers)
            {
                sb.AppendLine(header.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
