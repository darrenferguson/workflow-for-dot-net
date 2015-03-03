using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using FergusonMoriyam.Workflow.Interfaces.Application;
using log4net;


namespace FergusonMoriyam.Workflow.Application.Globalisation
{
    public class GlobalisationService : IGlobalisationService
    {

        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region singleton
        private static readonly GlobalisationService Service = new GlobalisationService();


        public static GlobalisationService Instance
        {
            get { return Service; }
        }

        private GlobalisationService()
        {
        }
        #endregion

        public Dictionary<string, Dictionary<string, string>> Cultures { get; set; }

        private string DefaultValue(string key)
        {
            key = key.Replace("_", " ");
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(key).Trim();
        }

        public string GetString(string key)
        {
            var cultureName = CultureInfo.CurrentCulture.Name;

            if (Cultures.ContainsKey(cultureName))
            {
                if(Cultures[cultureName].ContainsKey(key))
                {
                    return Cultures[cultureName][key].Trim();
                }
            }
            return DefaultValue(key);
        }
    }
}
