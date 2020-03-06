using inSyca.foundation.integration.biztalk.tracking.diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace inSyca.foundation.integration.biztalk.tracking.helper
{
    public class test
    {
    }
    public class RestWrapper
    {
        public static JToken GetJsonResponse(string method, Uri uri, Dictionary<string, string> headers, string payLoad, IAuthenticator authenticator)
        {
            IRestResponse response;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var client = new RestClient(uri);

                if (authenticator != null)
                    client.Authenticator = authenticator;

                var request = new RestRequest((Method)Enum.Parse(typeof(Method), method, true));

                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);

                if (payLoad != null)
                    request.AddParameter("application/json; charset=utf-8", payLoad, ParameterType.RequestBody);

                response = client.Execute(request);

                if (response.IsSuccessful)
                    return JToken.Parse(response.Content);
                else
                    throw new WebException($"Failed to post {payLoad.ToString()} to {response.Server}. StatusCode: {response.StatusCode} Error: {response.ErrorMessage}");
            }
            catch (Exception ex)
            {
                throw new Exception("Restclient Error", ex);
            }
        }


        public static JToken GetJsonResponse(string method, Uri uri, Dictionary<string, string> headers, List<JObject> itemList, IAuthenticator authenticator)
        {
            try
            {
                StringBuilder postBody = new StringBuilder();

                foreach (var item in itemList)
                {
                    postBody.AppendLine("{\"index\" : {} }");
                    postBody.AppendLine(item.ToString(Formatting.None));
                }

                return GetJsonResponse(method, uri, headers, postBody.ToString(), authenticator);
            }
            catch (Exception ex)
            {
                Log.Error("Message transfer error", ex);
                throw new Exception("Restclient Error", ex);
            }
        }
    }
}
