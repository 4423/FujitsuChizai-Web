using Microsoft.VisualStudio.TestTools.UnitTesting;
using FujitsuChizai.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace FujitsuChizai.Controllers.Tests
{
    [TestClass()]
    public class directionsControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            // 左端→右端経路の総距離を評価
            string endpoint = "https://fujitsu-chizai.azurewebsites.net/api/directions/";
            string param = "?originId=63&destinationId=82&originType=Place&destinationType=Place";
            string res = GetString(endpoint + param).Result;
           
            Assert.IsTrue(res.Contains("\"totalCost\":20544"));
        }

        private static async Task<string> GetString(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetStringAsync(uri);
            }
        }
    }
}