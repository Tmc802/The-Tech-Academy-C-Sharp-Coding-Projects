using System.IO;
using System.Net;

namespace FileDataExtractor.Models
{
    public class RawData
    {
        public string dataRequest(string url) {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url); // web request
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse(); // get response
            StreamReader sr = new StreamReader(resp.GetResponseStream()); // response stream?
            sr.Close();

            return sr.ReadToEnd();
    }
        
    }
}