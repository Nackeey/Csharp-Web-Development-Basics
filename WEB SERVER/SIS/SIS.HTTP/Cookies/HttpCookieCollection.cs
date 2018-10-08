﻿namespace SIS.HTTP.Cookies
{
    using Cookies.Contracts;
    using System.Collections;
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
            if (!this.ContainsCookie(cookie.Key))
            {
                this.cookies.Add(cookie.Key, cookie);
            }
        }

        public void Clear() => this.cookies.Clear();

        public bool ContainsCookie(string key) => this.cookies.ContainsKey(key);

        public HttpCookie GetCookie(string key) => this.cookies[key];

        public bool HasCookies() => this.cookies.Any();

        public IEnumerator<HttpCookie> GetEnumerator()
        {
            foreach (var cookie in this.cookies)
            {
                yield return cookie.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join("; ", this.cookies.Values);
        }
    }
}
