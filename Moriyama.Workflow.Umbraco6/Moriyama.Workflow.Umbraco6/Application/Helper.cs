using System.IO;
using System.Net;
using System.Web;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.template;


namespace Moriyama.Workflow.Umbraco6.Application
{
    public class Helper
    {
        private static readonly Helper Service = new Helper();
        public static Helper Instance
        {
            get { return Service; }
        }
        private Helper() { }

        public Node GetRootNode()
        {
            return Node.GetNodeByXpath("//root/*[1]");
        }

        public string RenderTemplate(int id)
        {
            const string urlPattern = "http://{0}/?altTemplate={1}";

            var t = new Template(id);
            var host = HttpContext.Current.Request.Url.Host;
            if (HttpContext.Current.Request.Url.Port != 80) host += ":" + HttpContext.Current.Request.Url.Port;

            var url = string.Format(urlPattern, host, t.Alias);
            return ReadUrl(url);
        }

        protected string ReadUrl(string u)
        {
            var client = new WebClient();
            var data = client.OpenRead(u);
            var reader = new StreamReader(data);

            var r = reader.ReadToEnd();
            reader.Close();
            return r;
        }
    }
}
