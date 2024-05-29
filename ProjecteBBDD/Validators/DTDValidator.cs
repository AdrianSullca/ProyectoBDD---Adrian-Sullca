using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml;

namespace ProjecteBBDD.Validators
{
    public class DTDValidator
    {

        public static bool Validate(string xmlFilePath)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Parse;
                settings.ValidationType = ValidationType.DTD;

                settings.XmlResolver = new XmlUrlResolver { Credentials = System.Net.CredentialCache.DefaultCredentials };

                settings.ValidationEventHandler += ValidationEventHandler;

                using (XmlReader reader = XmlReader.Create(xmlFilePath, settings))
                {
                    while (reader.Read()) { }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al validar: " + ex.Message);
                return false;
            }
        }

        private static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error || e.Severity == XmlSeverityType.Warning)
            {
                Console.WriteLine("Error al validar: " + e.Message);
            }
        }
    }
}
