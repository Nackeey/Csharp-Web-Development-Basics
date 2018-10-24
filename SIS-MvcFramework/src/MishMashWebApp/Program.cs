namespace MishMashWebApp
{
    using SIS.MvcFramework;

    class Program
    {
        static void Main(string[] args)
        {
            WebHost.Start(new Startup());
        }
    }
}
