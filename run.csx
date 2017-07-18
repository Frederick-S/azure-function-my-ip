#r "System.Web"

using System.Net;
using System.Web;

public static HttpResponseMessage Run(HttpRequestMessage httpRequestMessage, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    string ip = "";
    string msHttpContextKey = "MS_HttpContext";

    if (httpRequestMessage.Properties.ContainsKey(msHttpContextKey))
    {
        var httpContextWrapper = (HttpContextWrapper)httpRequestMessage.Properties[msHttpContextKey];

        if (httpContextWrapper != null)
        {
            ip = httpContextWrapper.Request.UserHostAddress;
        }
    }

    if (!string.IsNullOrEmpty(ip))
    {
        log.Info(string.Format("Your ip is: {0}", ip));

        return httpRequestMessage.CreateResponse(HttpStatusCode.OK, ip);
    }
    else
    {
        return httpRequestMessage.CreateResponse(HttpStatusCode.BadRequest, "Cannot detect your ip");
    }
}
