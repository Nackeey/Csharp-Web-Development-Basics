namespace SIS.HTTP.Cookies
{ 
    using Cookies.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly Dictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {
            this.cookies.Add(cookie.Key, cookie);
        }

        public bool ContainsCookie(string key) => this.cookies.ContainsKey(key);

        public HttpCookie GetCookie(string key) => this.cookies[key];

        public bool HasCookies() => this.cookies.Any();

        public override string ToString()
        {
            return string.Join("; ", this.cookies.Values);
        }
    }
}
