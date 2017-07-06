using System.Web;

namespace Sitecore.DataExchange.Examples.WebHandlers
{
    using System;
    using System.Web.SessionState;

    public class RunPipelineBatchHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var id = context.Request.Params["id"];
            var name = RunPipelineBatchRunner.RunPipelineBatch(id);
            if (!string.IsNullOrEmpty(name))
            {
                context.Response.Write(name + " is now running..");
            }
            else
            {
                context.Response.Write("Cannot run this batch");
            }
        }

        #endregion
    }
}
