namespace SIS.HTTP.Cookies
{
    using System;

    public class HttpCookie
    {
        private const int HttpCookieDefaultExpirationDays = 3;

        public HttpCookie(string key, string value, bool isNew, int expires = HttpCookieDefaultExpirationDays, string path = "/")
        {
            this.Key = key;
            this.Value = value;
            this.Path = path;
            this.IsNew = isNew;
            this.Expires = DateTime.UtcNow.AddDays(expires);
        }

        public HttpCookie(string key, string value, string path = "/", int expires = HttpCookieDefaultExpirationDays)
            : this(key, value, true, expires, path)
        {
        }

        public string Key { get; }

        public string Value { get; }

        public string Path { get; set; }

        public DateTime Expires { get; private set; }

        public bool IsNew { get; }

        public bool HttpOnly { get; set; } = true;

        public void Delete()
        {
            this.Expires = DateTime.UtcNow.AddDays(-1);
        }

        public override string ToString()
        {
            var str = $"{this.Key}={this.Value}; Expires={this.Expires:R}; Path={this.Path}";
            if (this.HttpOnly)
            {
                str += "; HttpOnly";
            }

            return str;
        }
    }
}
