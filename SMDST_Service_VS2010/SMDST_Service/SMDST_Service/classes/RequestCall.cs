using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Twitterizer;
using System.Threading;

namespace SMDST_Service
{
    class RequestCall
    {
        protected string responseFromServer;

        internal static string RequestCallToAPI(string requestString, string passParameters, OAuthTokens tokens)
        {
            try
            {
                var rc1 = new RequestCall();
                var uriString = new Uri(requestString + passParameters);

                var tRequest = new WebRequestBuilder(uriString, HTTPVerb.GET, tokens);

                var tResponse = tRequest.ExecuteRequest();

                var dataStream = tResponse.GetResponseStream();

                using (var reader = new StreamReader(dataStream))
                {
                    rc1.responseFromServer = reader.ReadToEnd();

                    return rc1.responseFromServer.ToString(); ;
                }
            }

            catch (WebException)
            {
                Thread.Sleep(new TimeSpan(0, 10, 0));

                var rc1 = new RequestCall();
                var uriString = new Uri(requestString + passParameters);

                var tRequest = new WebRequestBuilder(uriString, HTTPVerb.GET, tokens);

                var tResponse = tRequest.ExecuteRequest();

                var dataStream = tResponse.GetResponseStream();

                using (var reader = new StreamReader(dataStream))
                {
                    rc1.responseFromServer = reader.ReadToEnd();

                    return rc1.responseFromServer.ToString();
                }
            }
        }

    }
}
