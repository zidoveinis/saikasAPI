using System.Collections.Generic;
using System.Xml.Serialization;

namespace SaikasApi.Models
{
        [XmlRoot(ElementName = "CcyAmt", Namespace = "http://www.lb.lt/WebServices/FxRates")]
        public class CcyAmt
        {
            [XmlElement(ElementName = "Ccy", Namespace = "http://www.lb.lt/WebServices/FxRates")]
            public string Ccy { get; set; }
            [XmlElement(ElementName = "Amt", Namespace = "http://www.lb.lt/WebServices/FxRates")]
            public string Amt { get; set; }
        }

        [XmlRoot(ElementName = "FxRate", Namespace = "http://www.lb.lt/WebServices/FxRates")]
        public class FxRate
        {
            [XmlElement(ElementName = "Tp", Namespace = "http://www.lb.lt/WebServices/FxRates")]
            public string Tp { get; set; }
            [XmlElement(ElementName = "Dt", Namespace = "http://www.lb.lt/WebServices/FxRates")]
            public string Dt { get; set; }
            [XmlElement(ElementName = "CcyAmt", Namespace = "http://www.lb.lt/WebServices/FxRates")]
            public List<CcyAmt> CcyAmt { get; set; }
        }

        [XmlRoot(ElementName = "FxRates", Namespace = "http://www.lb.lt/WebServices/FxRates")]
        public class FxRates
        {
            [XmlElement(ElementName = "FxRate", Namespace = "http://www.lb.lt/WebServices/FxRates")]
            public List<FxRate> FxRate { get; set; }
            [XmlAttribute(AttributeName = "xmlns")]
            public string Xmlns { get; set; }
        }
    }
