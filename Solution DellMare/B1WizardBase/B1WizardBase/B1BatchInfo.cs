namespace B1WizardBase
{
    using SAPbouiCOM;
    using System;
    using System.Xml;

    public class B1BatchInfo
    {
        public B1BatchInfo(Application theAppl)
        {
            string lastBatchResults = theAppl.GetLastBatchResults();
            XmlDocument document = new XmlDocument();
            document.LoadXml(lastBatchResults);
            string xpath = "result/errors/error";
            foreach (XmlNode node in document.SelectNodes(xpath))
            {
                XmlElement element = (XmlElement) node;
                XmlAttribute attribute = element.Attributes["code"];
                if (attribute != null)
                {
                    XmlAttribute attribute2 = element.Attributes["descr"];
                    new B1Info(theAppl, "ERROR " + attribute.Value + " : " + ((attribute2 != null) ? attribute2.Value : ""));
                }
            }
        }
    }
}

