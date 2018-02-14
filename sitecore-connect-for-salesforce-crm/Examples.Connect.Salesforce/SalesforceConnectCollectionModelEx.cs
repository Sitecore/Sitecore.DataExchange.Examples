using Sitecore.DataExchange.Tools.SalesforceConnect.Models;
using Sitecore.XConnect;
using Sitecore.XConnect.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Connect.Salesforce
{
    public class SalesforceConnectCollectionModelEx
    {
        public SalesforceConnectCollectionModelEx()
        {
        }
        static SalesforceConnectCollectionModelEx()
        {
            _model = BuildModel();
        }
        private static XdbModel _model = null;
        public static XdbModel Model { get { return _model; } }
        private static XdbModel BuildModel()
        {
            var builder = new XdbModelBuilder(typeof(SalesforceConnectCollectionModelEx).FullName, new XdbModelVersion(1, 0));
            builder.DefineFacet<Contact, SalesforceAccountInformationFacet>(SalesforceAccountInformationFacet.DefaultFacetKey);
            builder.ReferenceModel(SalesforceConnectCollectionModel.Model);
            return builder.BuildModel();
        }
    }
}
