namespace _02._Validate_Url
{
    using System;
    using System.Net;
    using System.Text;

    public class Program
    {
        public const string InvalidUrl = "Invalid URL";

        public static void Main()
        {
            var encodedUrl = Console.ReadLine();

            var decodedUrl = WebUtility.UrlDecode(encodedUrl);

            var splittedUrl = decodedUrl.Split("://");

            var sb = new StringBuilder();

            var protocol = GetProtocol(splittedUrl);

            sb.AppendLine($"Protocol: {protocol}");

            if (protocol != "http" && protocol != "https")
            {
                Console.WriteLine(InvalidUrl);
                Environment.Exit(0);
            }

            var url = splittedUrl[1];

            if (protocol == "http" && url.Contains(":443") || protocol == "https" && url.Contains(":80"))
            {
                Console.WriteLine(InvalidUrl);
                Environment.Exit(0);
            }

            var port = GetPort(protocol);

            var host = GetHost(url);

            sb.AppendLine($"Host: {host}");
            sb.AppendLine($"Port: {port}");

            if (!host.Contains('.'))
            {
                Console.WriteLine(InvalidUrl);
                Environment.Exit(0);
            }

            var path = GetPath(url);

            sb.AppendLine($"Path: {path}");

            if (url.Contains('?'))
            {
                var query = string.Empty; 

                var startIndexQuery = url.IndexOf('?');

                if (!url.Contains('#'))
                {
                    query = url.Substring(startIndexQuery);

                    sb.AppendLine($"Query: {query}");
                }
                else
                {
                    var endIndexQuery = url.IndexOf('#');
                    query = url.Substring(startIndexQuery + 1, endIndexQuery - startIndexQuery - 1);

                    sb.AppendLine($"Query: {query}");

                    var startIndexFragment = endIndexQuery;
                    var fragment = url.Substring(startIndexFragment + 1);

                    sb.AppendLine($"Fragment: {fragment}");
                }
            }

            var output = sb.ToString().TrimEnd();
            Console.WriteLine(output);
        }

        private static string GetPath(string url)
        {
            var path = string.Empty;

            if (!url.Contains('/'))
            {
                path = "/";

                return path;
            }

            var startIndex = url.IndexOf('/');
            if (!url.Contains('?'))
            {
                path = url.Substring(startIndex);
            }
            else
            {
                var endIndex = url.IndexOf('?');
                path = url.Substring(startIndex, endIndex - startIndex);
            }

            return path;
        }

        private static string GetHost(string url)
        {
            var host = string.Empty;

            if (url.Contains(':'))
            {
                host = url.Split(':')[0];
            }
            else
            {
                host = url.Split('/')[0];
            }

            return host;
        }

        private static string GetPort(string protocol)
        {
            var port = string.Empty;

            if (protocol == "http")
            {
                port = "80";
            }
            else
            {
                port = "443";
            }

            return port;
        }

        private static string GetProtocol(string[] splittedUrl)
        {
            var protocol = splittedUrl[0];

            return protocol;
        }
    }
}
