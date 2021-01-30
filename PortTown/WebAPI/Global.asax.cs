using System;
using System.Web.Http;
using WebAPI.Configuration;

namespace WebAPI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebAPIConfig.Register);

            // Check and create buildings template
            WebAPIConfig.CheckForInitialBuildingsAsync().ContinueWith(async result =>
            {
                var hasData = await result;
                if (!hasData)
                {
                    await WebAPIConfig.CreateInitialBuildingsAsync();
                }
            });

            // Check and create items template
            WebAPIConfig.CheckForInitialItemsAsync().ContinueWith(async result =>
            {
                var hasData = await result;
                if (!hasData)
                {
                    await WebAPIConfig.CreateInitialItemsAsync();
                }
            });

            WebAPIConfig.CheckForInitialResourceBatchesAsync().ContinueWith(async result =>
            {
                var hasData = await result;
                if (!hasData)
                {
                    await WebAPIConfig.CreateInitialResourceBatchesAsync();
                }
            });
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}