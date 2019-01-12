namespace inSyca.bt.gl.pipelines
{
    using System;
    using System.Collections.Generic;
    using Microsoft.BizTalk.PipelineOM;
    using Microsoft.BizTalk.Component;
    using Microsoft.BizTalk.Component.Interop;
    
    
    public sealed class remove_nil_and_empty_nodes_11 : Microsoft.BizTalk.PipelineOM.SendPipeline
    {
        
        private const string _strPipeline = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instanc"+
"e\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" MajorVersion=\"1\" MinorVersion=\"0\">  <Description /> "+
" <CategoryId>8c6b051c-0ff5-4fc2-9ae5-5016cb726282</CategoryId>  <FriendlyName>Transmit</FriendlyName"+
">  <Stages>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"1\" Name=\"Pre-Assemble\" minO"+
"ccurs=\"0\" maxOccurs=\"-1\" execMethod=\"All\" stageId=\"9d0e4101-4cce-4536-83fa-4a5040674ad6\" />      <Co"+
"mponents />    </Stage>    <Stage>      <PolicyFileStage _locAttrData=\"Name\" _locID=\"2\" Name=\"Assemb"+
"le\" minOccurs=\"0\" maxOccurs=\"1\" execMethod=\"All\" stageId=\"9d0e4107-4cce-4536-83fa-4a5040674ad6\" />  "+
"    <Components>        <Component>          <Name>inSyca.foundation.integration.biztalk.components."+
"RemoveNil,inSyca.fd.in.bt.cp, Version=1.0.4.0, Culture=neutral, PublicKeyToken=604954d691f5a615</Nam"+
"e>          <ComponentName>Remove Nil</ComponentName>          <Description>inSyca - Remove xsi:nil "+
"Elements  Component for BizTalk Piepelines</Description>          <Version>1.0.0.0</Version>        "+
"  <Properties />          <CachedDisplayName>Remove Nil</CachedDisplayName>          <CachedIsManage"+
"d>true</CachedIsManaged>        </Component>      </Components>    </Stage>    <Stage>      <PolicyF"+
"ileStage _locAttrData=\"Name\" _locID=\"3\" Name=\"Encode\" minOccurs=\"0\" maxOccurs=\"-1\" execMethod=\"All\" "+
"stageId=\"9d0e4108-4cce-4536-83fa-4a5040674ad6\" />      <Components />    </Stage>  </Stages></Docume"+
"nt>";
        
        private const string _versionDependentGuid = "a265edef-807e-4727-b2ec-69dc0f5e43d7";
        
        public remove_nil_and_empty_nodes_11()
        {
            Microsoft.BizTalk.PipelineOM.Stage stage = this.AddStage(new System.Guid("9d0e4107-4cce-4536-83fa-4a5040674ad6"), Microsoft.BizTalk.PipelineOM.ExecutionMode.all);
            IBaseComponent comp0 = Microsoft.BizTalk.PipelineOM.PipelineManager.CreateComponent("inSyca.foundation.integration.biztalk.components.RemoveNil,inSyca.fd.in.bt.cp, Version=1.0.4.0, Culture=neutral, PublicKeyToken=604954d691f5a615");;
            if (comp0 is IPersistPropertyBag)
            {
                string comp0XmlProperties = "<?xml version=\"1.0\" encoding=\"utf-16\"?><PropertyBag xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-inst"+
"ance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">  <Properties /></PropertyBag>";
                PropertyBag pb = PropertyBag.DeserializeFromXml(comp0XmlProperties);;
                ((IPersistPropertyBag)(comp0)).Load(pb, 0);
            }
            this.AddComponent(stage, comp0);
        }
        
        public override string XmlContent
        {
            get
            {
                return _strPipeline;
            }
        }
        
        public override System.Guid VersionDependentGuid
        {
            get
            {
                return new System.Guid(_versionDependentGuid);
            }
        }
    }
}
