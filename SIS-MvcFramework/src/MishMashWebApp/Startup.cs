namespace MishMashWebApp
{
    using SIS.MvcFramework;
    using SIS.MvcFramework.Logger;
    using SIS.MvcFramework.Services;

    class Startup : IMvcApplication
    {
        public void Configure()
        {
        }

        public void ConfigureServices(IServiceCollection collection)
        {
            collection.AddService<ILogger, ConsoleLogger>();
        }
    }
}
