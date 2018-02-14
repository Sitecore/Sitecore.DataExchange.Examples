using Sitecore.XConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Connect.Salesforce
{
    [FacetKey(DefaultFacetKey)]
    [Serializable]
    public class SalesforceAccountInformationFacet : Facet
    {
        public const string DefaultFacetKey = "SalesforceAccount";
        public string AccountName { get; set; }
        public SalesforceAccountInformationFacet()
        {
        }
    }
}
