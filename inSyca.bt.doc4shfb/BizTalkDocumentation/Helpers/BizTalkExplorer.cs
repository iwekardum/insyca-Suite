using Microsoft.BizTalk.ExplorerOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using shfb.helper;

namespace BizTalkDocumentation.Helpers
{
    public class BizTalkExplorer : IBtsCatalogExplorer2
    {
        private BtsCatalogExplorer btsCatalogExplorer { get; set; }
        public BizTalkExplorer(Configuration configuration)
        {
            btsCatalogExplorer = new BtsCatalogExplorer();
            btsCatalogExplorer.ConnectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;", configuration.BizTalkDbInstance, configuration.MgmtDatabaseName);

        }

        public ICollection Applications
        {
            get
            {
                return btsCatalogExplorer.Applications;
            }
        }

        public HostCollection Hosts
        {
            get
            {
                return btsCatalogExplorer.Hosts;
            }
        }

        public ProtocolTypeCollection Adapters
        {
            get
            {
                return btsCatalogExplorer.ProtocolTypes;
            }
        }

        public string ConnectionString
        {
            get
            {
                return btsCatalogExplorer.ConnectionString;
            }
            set
            {
                btsCatalogExplorer.ConnectionString = value;
            }
        }

        public IBizTalkApplication DefaultApplication
        {
            get
            {
                return btsCatalogExplorer.DefaultApplication;
            }

            set
            {
                btsCatalogExplorer.DefaultApplication = value as Application;
            }
        }

        public string GroupName
        {
            get
            {
                return btsCatalogExplorer.GroupName;
            }
        }

        public IBizTalkApplication SystemApplication
        {
            get
            {
                return btsCatalogExplorer.SystemApplication;
            }
        }

        public IBizTalkApplication AddNewApplication()
        {
            return btsCatalogExplorer.AddNewApplication();
        }

        public IParty AddNewParty()
        {
            return btsCatalogExplorer.AddNewParty();
        }

        public IReceivePort2 AddNewReceivePort(bool twoWay)
        {
            return btsCatalogExplorer.AddNewReceivePort(twoWay);
        }

        public ISendPort2 AddNewSendPort(bool dynamicPort, bool twoWay)
        {
            return btsCatalogExplorer.AddNewSendPort(dynamicPort, twoWay);
        }

        public ISendPortGroup2 AddNewSendPortGroup()
        {
            return btsCatalogExplorer.AddNewSendPortGroup();
        }

        public void DiscardChanges()
        {
            btsCatalogExplorer.DiscardChanges();
        }

        public ICollection GetCollection(CollectionType enumCollectionType)
        {
            return btsCatalogExplorer.GetCollection(enumCollectionType);
        }

        public void Refresh()
        {
            btsCatalogExplorer.Refresh();
        }

        public void RemoveApplication(IBizTalkApplication application)
        {
            btsCatalogExplorer.RemoveApplication(application);
        }

        public void RemoveParty(IParty party)
        {
            btsCatalogExplorer.RemoveParty(party);
        }

        public void RemoveReceivePort(IReceivePort rxPort)
        {
            btsCatalogExplorer.RemoveReceivePort(rxPort);
        }

        public void RemoveReceivePort(IReceivePort2 rxPort)
        {
            btsCatalogExplorer.RemoveReceivePort(rxPort);
        }

        public void RemoveSendPort(ISendPort sendPort)
        {
            btsCatalogExplorer.RemoveSendPort(sendPort);
        }

        public void RemoveSendPort(ISendPort2 sendPort)
        {
            btsCatalogExplorer.RemoveSendPort(sendPort);
        }

        public void RemoveSendPortGroup(ISendPortGroup dl)
        {
            btsCatalogExplorer.RemoveSendPortGroup(dl);
        }

        public void RemoveSendPortGroup(ISendPortGroup2 dl)
        {
            btsCatalogExplorer.RemoveSendPortGroup(dl);
        }

        public void SaveChanges()
        {
            btsCatalogExplorer.SaveChanges();
        }

        public void SaveChangesWithTransaction(object transactionObj)
        {
            btsCatalogExplorer.SaveChangesWithTransaction(transactionObj);
        }

        IReceivePort IBtsCatalogExplorer.AddNewReceivePort(bool twoWay)
        {
            return btsCatalogExplorer.AddNewReceivePort(twoWay);
        }

        ISendPort IBtsCatalogExplorer.AddNewSendPort(bool dynamicPort, bool twoWay)
        {
            return btsCatalogExplorer.AddNewSendPort(dynamicPort, twoWay);
        }

        ISendPortGroup IBtsCatalogExplorer.AddNewSendPortGroup()
        {
            return btsCatalogExplorer.AddNewSendPortGroup();
        }
    }
}
