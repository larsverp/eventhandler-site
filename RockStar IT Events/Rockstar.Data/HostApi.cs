using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Rockstar.Models;

namespace Rockstar.Data
{
    public class HostApi
    {
        private WebRequest webRequest;
        private WebResponse webResponse;
        private StreamReader streamReader;

        public Host GetHost(string id)
        {
            webRequest = WebRequest.Create("https://eventhandler-api.herokuapp.com/api/hosts/" + id);

            webResponse = webRequest.GetResponse();
            streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            string webResponseString = streamReader.ReadToEnd();
            Host output = JsonConvert.DeserializeObject<Host>(webResponseString);

            return output;
        }

        public IEnumerable<Host> GetAllHosts()
        {
            webRequest = WebRequest.Create("https://eventhandler-api.herokuapp.com/api/hosts");

            webResponse = webRequest.GetResponse();
            streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            string webResponseString = streamReader.ReadToEnd();
            var output = JsonConvert.DeserializeObject<List<Host>>(webResponseString);

            return output;
        }
    }
}