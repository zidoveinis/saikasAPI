using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using SaikasApi.Models;

namespace SaikasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentFxRatesController : ControllerBase
    {
        // GET api/values
        public IActionResult Get()
        {
            Serializer ser = new Serializer();
            FxRates fxRates = new FxRates();

            var webRequest = (HttpWebRequest)WebRequest.Create("http://www.lb.lt/webservices/FxRates/FxRates.asmx/getCurrentFxRates?tp=eu");
            webRequest.Method = "GET";
            webRequest.UserAgent =  "Mozilla/5.0 (Windows NT 5.1; rv:28.0) Gecko/20100101 Firefox/28.0";
            webRequest.ContentLength = 0; // added per comment
            try
            {
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                if (webResponse.StatusCode != HttpStatusCode.OK) Console.WriteLine("{0}", webResponse.Headers);
                using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                {
                   var xml = reader.ReadToEnd();
                    reader.Close();
                    fxRates = ser.Deserialize<FxRates>(xml);
                }
                Console.Write(webResponse.StatusCode);
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    if(httpResponse == null)
                    {
                        return StatusCode(500, "503 Service Unavailable");
                    }
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        Console.WriteLine(text);
                    }
                }
            }
            return Ok(fxRates);
        }
    }

    internal class Serializer
    {
        public T Deserialize<T>(string input) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public string Serialize<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        }
    }
}
