﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace HTWebRemoteHost.Devices.Controllers
{
    class HttpGetControl
    {
        public static void RunCmd(string IP, string cmd, string auth)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(3);

                if (!string.IsNullOrEmpty(auth))
                {
                    if (auth.Contains("bearer"))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.Substring(7));
                    }
                    else
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(auth)));
                    }
                }

                HttpResponseMessage result;
                try
                {
                    result = httpClient.GetAsync($"{IP}{cmd}").Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception e)
                {
                    Util.ErrorHandler.SendError($"Error sending http GET request to: {IP}{cmd}\n\n{e.Message}");
                }
            }
        }
    }
}