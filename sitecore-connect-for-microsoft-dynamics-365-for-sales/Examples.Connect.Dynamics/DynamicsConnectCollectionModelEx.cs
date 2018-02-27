using Sitecore.DataExchange.Tools.DynamicsConnect.Models;
using Sitecore.XConnect;
using Sitecore.XConnect.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Connect.Dynamics
{
    public class DynamicsConnectCollectionModelEx
    {
        public DynamicsConnectCollectionModelEx()
        {
        }
        static DynamicsConnectCollectionModelEx()
        {
            _model = BuildModel();
        }
        private static XdbModel _model = null;
        public static XdbModel Model { get { return _model; } }
        private static XdbModel BuildModel()
        {
            var builder = new XdbModelBuilder(typeof(DynamicsConnectCollectionModelEx).FullName, new XdbModelVersion(1, 0));
            builder.DefineFacet<Contact, DynamicsAccountInformationFacet>(DynamicsAccountInformationFacet.DefaultFacetKey);
            builder.ReferenceModel(DynamicsConnectCollectionModel.Model);
            return builder.BuildModel();
        }
    }
}
