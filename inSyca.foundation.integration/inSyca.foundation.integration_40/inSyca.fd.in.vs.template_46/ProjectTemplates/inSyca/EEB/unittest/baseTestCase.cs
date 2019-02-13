using inSyca.foundation.integration.biztalk.test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;

namespace inSyca.eeb.ns
{
    public abstract class baseTestCase
    {
        protected BizTalkTestEnvironment bizTalkTestEnvironment;

        protected enum eApplications { entity_1, entity_2 };
        protected enum eReceiveDirectories { receive, input };
        protected enum eSendDirectories { send, output, archive };

        [TestInitialize]
        public void TestInit()
        {
            bizTalkTestEnvironment = new BizTalkTestEnvironment();

            bizTalkTestEnvironment.baseInfrastructureDirectory = @"c:\biztalk.exchange";
            bizTalkTestEnvironment.biztalkServers = new string[] { "localhost" };
            bizTalkTestEnvironment.hostInstanceNames = new string[] { "BizTalkServerApplication" };
            bizTalkTestEnvironment.outputFileExtensions = new string[] { "*.xml", "*.txt" };
            bizTalkTestEnvironment.baseTestFileDirectory = @"testfiles";

            bizTalkTestEnvironment.applicationDirectories = Enum.GetNames(typeof(eApplications));
            bizTalkTestEnvironment.receiveDirectories = Enum.GetNames(typeof(eReceiveDirectories));
            bizTalkTestEnvironment.sendDirectories = Enum.GetNames(typeof(eSendDirectories));

            Process.Start(Path.Combine(bizTalkTestEnvironment.baseTestProjectPath, @"preparation\createBizTalkTestDirectories.cmd"));
        }
    }
}
