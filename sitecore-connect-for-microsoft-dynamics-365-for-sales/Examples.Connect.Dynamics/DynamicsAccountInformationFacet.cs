using Sitecore.XConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Connect.Dynamics
{
    [FacetKey(DefaultFacetKey)]
    [Serializable]
    public class DynamicsAccountInformationFacet : Facet
    {
        public const string DefaultFacetKey = "DynamicsAccount";
        public string AccountName { get; set; }
        public DynamicsAccountInformationFacet()
        {
        }
    }
}
