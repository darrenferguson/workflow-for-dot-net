using System.IO;
using System.Text;
using umbraco;

namespace Moriyama.Workflow.Umbraco6.Application.Filters
{
    public sealed class RegisterClientResourcesFilter : MemoryStream
    {
        private const string HtmlBodyClosing = "</body>";
        private readonly Stream _outputStream;

        private readonly StringBuilder _htmlScripts;

        public RegisterClientResourcesFilter(Stream output)
        {
            _outputStream = output;
            _htmlScripts = new StringBuilder();
            
            // JavaScript resources
            _htmlScripts.AppendFormat("<script type='text/javascript' src='{0}/{1}'></script>", GlobalSettings.Path, "plugins/fmworkflow/js/speechbubble.js").AppendLine();
            _htmlScripts.AppendLine(HtmlBodyClosing);    
        }
                
        public override void Write(byte[] buffer, int offset, int count)
        {
            // get the string from the buffer.
            var content = UTF8Encoding.UTF8.GetString(buffer);

            if (content.Contains(HtmlBodyClosing))
            {
                // append the <script> tags to the closing </body> tag.
                content = content.Replace(HtmlBodyClosing, _htmlScripts.ToString());
            }

            // write the content changes back to the buffer.
            var outputBuffer = UTF8Encoding.UTF8.GetBytes(content);
            _outputStream.Write(outputBuffer, 0, outputBuffer.Length);
        }
    }
}
