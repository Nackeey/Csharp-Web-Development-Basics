namespace MishMashWebApp.Controllers
{
    using MishMashWebApp.Data;
    using SIS.MvcFramework;

    public class BaseController : Controller
    {
        public BaseController()
        {
            this.Context = new ApplicationDbContext();
        }

        protected ApplicationDbContext Context { get; }
    }
}
