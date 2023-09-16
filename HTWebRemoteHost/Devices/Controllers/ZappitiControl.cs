﻿using System;
using System.Net.Http;

namespace HTWebRemoteHost.Devices.Controllers
{
    class ZappitiControl
    {
        public static void RunCmd(string IP, string cmd)
        {
            using HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(3);

            HttpResponseMessage result;
            try
            {
                result = httpClient.GetAsync($"http://{IP}:8080/remoteCmd?command={cmd}").Result;

                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Util.ErrorHandler.SendError($"Error sending command to Zappiti: http://{IP}:8080/remoteCmd?command={cmd}\n\n{e.AllMessages()}");
            }
        }
    }
}