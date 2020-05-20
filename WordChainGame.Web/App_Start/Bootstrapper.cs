using System.Web.Http;

namespace WordChainGame.Web.App_Start
{
    public class Bootstrapper
    {

        public static void Run()
        {
            //Configure AutoFac  
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
        }

    }
}