namespace _03._Request_Parser
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    public class Program
    {
        public static void Main()
        {
            Dictionary<string, HashSet<string>> paths = GetValidPaths();

            var httpRequest = Console.ReadLine();

            var splittedRequest = httpRequest.Split(' ');

            var requestMethod = splittedRequest[0].ToLower();
            var receivedPath = splittedRequest[1];
            var httpVersion = splittedRequest[2];

            var sb = new StringBuilder();

            var statusCode = string.Empty;
            var responseText = string.Empty;

            if (paths.ContainsKey(requestMethod) && paths[requestMethod].Contains(receivedPath))
            {
                statusCode = $"{(int)HttpStatusCode.OK} {HttpStatusCode.OK}";
                responseText = $"{HttpStatusCode.OK}";
            }
            else
            {
                statusCode = $"{(int)HttpStatusCode.NotFound} {HttpStatusCode.NotFound}";
                responseText = $"{HttpStatusCode.NotFound}";
            }

            sb.AppendLine($"{httpVersion} {statusCode}");
            sb.AppendLine($"Content-Length: {responseText.Length}");
            sb.AppendLine($"ContentType: text/plain");
            sb.AppendLine();
            sb.AppendLine(responseText);

            var output = sb.ToString().TrimEnd();

            Console.WriteLine(output);
        }

        private static Dictionary<string, HashSet<string>> GetValidPaths()
        {
            var paths = new Dictionary<string, HashSet<string>>();

            var validPath = string.Empty;

            while ((validPath = Console.ReadLine()) != "END")
            {
                var pathParts = validPath.Split('/');

                var path = '/' + pathParts[1];
                var method = pathParts[2];

                if (!paths.ContainsKey(method))
                {
                    paths.Add(method, new HashSet<string>());
                }

                paths[method].Add(path);
            }

            return paths;
        }
    }
}
