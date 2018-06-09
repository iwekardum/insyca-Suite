using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyCopyright(BizTalkDocumentationMetadata.Copyright)]
[assembly: AssemblyCompany("inSyca")]
[assembly: AssemblyProduct("BizTalk Documenter")]

[assembly: CLSCompliant(false)]
[assembly: ComVisible(false)]
[assembly: NeutralResourcesLanguage("en-US")]

[assembly: AssemblyVersion(BizTalkDocumentationMetadata.Version)]
[assembly: AssemblyFileVersion(BizTalkDocumentationMetadata.Version)]

internal static class BizTalkDocumentationMetadata
{
    public const string Version = "1.0.0.0";
    public const string Copyright = "Copyright © 2017 Iwe Kardum";
}