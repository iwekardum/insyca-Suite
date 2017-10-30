using BizTalkDocumentation.Helpers;
using Microsoft.BizTalk.ExplorerOM;
using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace BizTalkDocumentation
{
    internal sealed class BizTalkTopicBuilder : TopicBuilder
    {
        private Context Context { get; set; }
        public BizTalkTopicBuilder(Context _context, Dictionary<BaseObject, TopicFile> topicDictionary)
            : base()
        {
            Context = _context;
        }

        public void Parse()
        {
            Context.ReportProgress("getting BizTalk artefacts...");

            var btsCatalogExplorer = new BizTalkExplorer(Context.Configuration);

            var appsTopic = new ApplicationsTopic();

            foreach (Application application in btsCatalogExplorer.Applications)
            {
                ParseApplication(appsTopic, application);
            }

            var hostsTopic = new HostsTopic();

            foreach (Host host in btsCatalogExplorer.Hosts)
            {
                ParseHost(hostsTopic, host);
            }

            var adaptersTopic = new AdaptersTopic();

            foreach (ProtocolType adapter in btsCatalogExplorer.Adapters)
            {
                ParseAdapter(adaptersTopic, adapter);
            }

            var settingsTopic = new SettingsTopic();

            settingsTopic.Children.Add(hostsTopic);
            settingsTopic.Children.Add(adaptersTopic);

            var bizTalkTopic = new BizTalkTopic();

            bizTalkTopic.Children.Add(appsTopic);
            bizTalkTopic.Children.Add(settingsTopic);

            AddTopic(bizTalkTopic);
        }

        private void ParseApplication(ApplicationsTopic appsTopic, Application bizTalkApplication)
        {
            Context.ReportProgress("parsing application {0}...", bizTalkApplication.Name);

            var appTopic = new ApplicationTopic(bizTalkApplication);

            ParseSchemas(bizTalkApplication, appTopic);
            ParseMaps(bizTalkApplication, appTopic);
            ParseOrchestrations(bizTalkApplication, appTopic);
            ParsePipelines(bizTalkApplication, appTopic);
            ParseReceivePorts(bizTalkApplication, appTopic);
            ParseSendPorts(bizTalkApplication, appTopic);
            ParseSendPortGroups(bizTalkApplication, appTopic);
            ParseBusinessRules(bizTalkApplication, appTopic);
            ParseRoles(bizTalkApplication, appTopic);
            ParseAssemblies(bizTalkApplication, appTopic);

            appsTopic.Children.Add(appTopic);
        }

        private void ParseHost(HostsTopic hostsTopic, Host host)
        {
            Context.ReportProgress("parsing host {0}...", host.Name);

            var hostTopic = new HostTopic(host);

            hostsTopic.Children.Add(hostTopic);
        }

        private void ParseAdapter(AdaptersTopic adaptersTopic, ProtocolType adapter)
        {
            Context.ReportProgress("parsing adapter {0}...", adapter.Name);

            var adapterTopic = new AdapterTopic(adapter);

            adaptersTopic.Children.Add(adapterTopic);
        }

        private void ParseSchemas(Application bizTalkApplication, ApplicationTopic appTopic)
        {
            var schemasTopic = new SchemasTopic();

            foreach (Schema schema in bizTalkApplication.Schemas)
            {
                Context.ReportProgress("parsing schema {0}...", schema.FullName);

                var schemaTopic = new SchemaTopic(schema);
                schemasTopic.Children.Add(schemaTopic);
            }

            appTopic.Children.Add(schemasTopic);
        }

        private void ParseMaps(Application bizTalkApplication, ApplicationTopic appTopic)
        {
            var mapsTopic = new MapsTopic();

            foreach (Transform map in bizTalkApplication.Transforms)
            {
                Context.ReportProgress("parsing map {0}...", map.FullName);

                var mapTopic = new MapTopic(map);
                mapsTopic.Children.Add(mapTopic);
            }

            appTopic.Children.Add(mapsTopic);
        }

        private void ParseOrchestrations(Application bizTalkApplication, ApplicationTopic appTopic)
        {
            var orchestrationsTopic = new OrchestrationsTopic();

            foreach (BtsOrchestration orchestration in bizTalkApplication.Orchestrations)
            {
                Context.ReportProgress("parsing orchestration {0}...", orchestration.FullName);

                var orchestrationTopic = new OrchestrationTopic(orchestration);
                orchestrationsTopic.Children.Add(orchestrationTopic);
            }

            appTopic.Children.Add(orchestrationsTopic);
        }

        private void ParsePipelines(Application bizTalkApplication, ApplicationTopic appTopic)
        {
            var pipelinesTopic = new PipelinesTopic();

            foreach (Pipeline pipeline in bizTalkApplication.Pipelines)
            {
                Context.ReportProgress("parsing pipeline {0}...", pipeline.FullName);

                var pipelineTopic = new PipelineTopic(pipeline);
                pipelinesTopic.Children.Add(pipelineTopic);
            }

            appTopic.Children.Add(pipelinesTopic);
        }

        private void ParseReceivePorts(Application bizTalkApplication, ApplicationTopic appTopic)
        {
            var receivePortsTopic = new ReceivePortsTopic();

            foreach (ReceivePort receivePort in bizTalkApplication.ReceivePorts)
            {
                Context.ReportProgress("parsing receivePort {0}...", receivePort.Name);

                var receivePortTopic = new ReceivePortTopic(receivePort);
                receivePortsTopic.Children.Add(receivePortTopic);
            }

            appTopic.Children.Add(receivePortsTopic);
        }

        private void ParseSendPorts(Application bizTalkApplication, ApplicationTopic appTopic)
        {
            var sendPortsTopic = new SendPortsTopic();

            foreach (SendPort sendPort in bizTalkApplication.SendPorts)
            {
                Context.ReportProgress("parsing sendPort {0}...", sendPort.Name);

                var sendPortTopic = new SendPortTopic(sendPort);
                sendPortsTopic.Children.Add(sendPortTopic);
            }

            appTopic.Children.Add(sendPortsTopic);
        }

        private void ParseSendPortGroups(Application bizTalkApplication, ApplicationTopic appTopic)
        {
            var sendPortGroupsTopic = new SendPortGroupsTopic();

            foreach (SendPortGroup sendPortGroup in bizTalkApplication.SendPortGroups)
            {
                Context.ReportProgress("parsing sendPortGroup {0}...", sendPortGroup.Name);

                var sendPortGroupTopic = new SendPortGroupTopic(sendPortGroup);
                sendPortGroupsTopic.Children.Add(sendPortGroupTopic);
            }

            appTopic.Children.Add(sendPortGroupsTopic);
        }

        private void ParseBusinessRules(Application bizTalkApplication, ApplicationTopic appTopic)
        {
            var businessRulesTopic = new BusinessRulesTopic();

            foreach (Policy businessRule in bizTalkApplication.Policies)
            {
                Context.ReportProgress("parsing businessRule {0}...", businessRule.Name);

                var businessRuleTopic = new BusinessRuleTopic(businessRule);
                businessRulesTopic.Children.Add(businessRuleTopic);
            }

            appTopic.Children.Add(businessRulesTopic);
        }

        private void ParseRoles(Application bizTalkApplication, ApplicationTopic appTopic)
        {
            var rolesTopic = new RolesTopic();

            foreach (Role role in bizTalkApplication.Roles)
            {
                Context.ReportProgress("parsing role {0}...", role.Name);

                var roleTopic = new RoleTopic(role);
                rolesTopic.Children.Add(roleTopic);
            }

            appTopic.Children.Add(rolesTopic);
        }

        private void ParseAssemblies(Application bizTalkApplication, ApplicationTopic appTopic)
        {
            Context.ReportProgress("parsing bizTalkApplication {0}...", bizTalkApplication.Name);

            var assembliesTopic = new AssembliesTopic();

            foreach (BtsAssembly bizTalkAssembly in bizTalkApplication.Assemblies)
            {
                var assemblyTopic = new AssemblyTopic(bizTalkAssembly);
                assembliesTopic.Children.Add(assemblyTopic);
            }

            appTopic.Children.Add(assembliesTopic);
        }
    }
}