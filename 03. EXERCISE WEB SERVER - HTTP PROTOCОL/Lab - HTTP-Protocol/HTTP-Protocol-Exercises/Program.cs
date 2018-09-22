namespace HTTP_Protocol_Exercises
{
    using System;
    using System.Net;

    public class Program
    {
        public static void Main()
        {
            var encodedUrl = Console.ReadLine();

            var decodedUrl = WebUtility.UrlDecode(encodedUrl);

            Console.WriteLine(decodedUrl);
        }
    }
}
