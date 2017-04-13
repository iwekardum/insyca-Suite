using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using inSyca.foundation.integration.visualstudio.external.fusion;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;

namespace inSyca.foundation.integration.visualstudio.external
{
    public class gac
    {
        BindingList<AssemblyDescription> assemblyList;
        dialog.dlgGAC gacDialog;

        public gac()
        {
        }

        public void LoadAssemblies()
        {
            Cursor.Current = Cursors.WaitCursor;

            assemblyList = new BindingList<AssemblyDescription>();
            //  Create an assembly cache enumerator.
            var assemblyCacheEnum = new AssemblyCacheEnumerator(null);

            //  Enumerate the assemblies.
            var assemblyName = assemblyCacheEnum.GetNextAssembly();
            while (assemblyName != null)
            {
                assemblyList.Add(new AssemblyDescription(assemblyName));

                assemblyName = assemblyCacheEnum.GetNextAssembly();
            }

            Cursor.Current = Cursors.Default;
        }

        public void ShowDialog(out string assemblyDisplayName)
        {
            Cursor.Current = Cursors.WaitCursor;

            assemblyDisplayName = string.Empty;

            gacDialog = new dialog.dlgGAC(assemblyList);
            gacDialog.refreshEvent += dlg_refreshEvent;

            if (gacDialog.ShowDialog() == DialogResult.OK && gacDialog.SelectedAssemblyDescription != null)
                assemblyDisplayName = gacDialog.SelectedAssemblyDescription.DisplayName;

            Cursor.Current = Cursors.Default;
        }

        private void dlg_refreshEvent(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadAssemblies();
            gacDialog.AssemblyBindingList = assemblyList;

            Cursor.Current = Cursors.Default;
        }
    }
    /// <summary>
    /// An AssemblyDescription holds only the most basic assembly
    /// details that would be loaded from an application such as gacutil. 
    /// </summary>
    public class AssemblyDescription
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="AssemblyDescription"/> class from being created.
        /// </summary>
        internal AssemblyDescription()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyDescription"/> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        internal AssemblyDescription(string displayName)
        {
            //  Create the lazy fusion and reflection properties.
            lazyFusionProperties = new Lazy<AssemblyFusionProperties>(DoLoadFusionProperties);
            lazyReflectionProperties = new Lazy<AssemblyReflectionProperties>(DoLoadReflectionProperties);

            //  Load properties from the display name.
            LoadPropertiesFromDisplayName(displayName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyDescription"/> class.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        internal AssemblyDescription(IAssemblyName assemblyName)
        {
            //  Get the qualified name.
            var stringBuilder = new StringBuilder(10000);
            var iLen = 10000;
            var hr = assemblyName.GetDisplayName(stringBuilder, ref iLen, ASM_DISPLAY_FLAGS.ASM_DISPLAYF_VERSION
                | ASM_DISPLAY_FLAGS.ASM_DISPLAYF_CULTURE
                | ASM_DISPLAY_FLAGS.ASM_DISPLAYF_PUBLIC_KEY_TOKEN
                | ASM_DISPLAY_FLAGS.ASM_DISPLAYF_PROCESSORARCHITECTURE);
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);
            var displayName = stringBuilder.ToString();

            //  Load properties from the display name.
            LoadPropertiesFromDisplayName(displayName);

            //  We have the assembly name, so we can use the optimised version to load the fusion properties.
            lazyFusionProperties = new Lazy<AssemblyFusionProperties>(DoLoadFusionProperties);
            lazyReflectionProperties = new Lazy<AssemblyReflectionProperties>(DoLoadReflectionProperties);
        }

        internal AssemblyFusionProperties DoLoadFusionProperties()
        {
            //  Use the enumerator to get the assembly name.
            var enumerator = new AssemblyCacheEnumerator(DisplayName);
            var assemblyName = enumerator.GetNextAssembly();

            //  Return the properties.
            return DoLoadFusionProperties(assemblyName);
        }

        internal AssemblyFusionProperties DoLoadFusionProperties(IAssemblyName assemblyName)
        {
            //  Create the fusion properties.
            var fusionProperties = new AssemblyFusionProperties();

            //  Load the properties.
            fusionProperties.Load(assemblyName);

            //  Return the properties.
            return fusionProperties;
        }

        internal void LoadPropertiesFromDisplayName(string displayName)
        {
            DisplayName = displayName;

            var properties = displayName.Split(new string[] { ", " }, StringSplitOptions.None);

            //  Name should be first.
            try
            {
                Name = properties[0];
            }
            catch (Exception)
            {
                Name = "Unknown";
            }

            var versionString = (from p in properties where p.StartsWith("Version=") select p).FirstOrDefault();
            var cultureString = (from p in properties where p.StartsWith("Culture=") select p).FirstOrDefault();
            var internalKeyTokenString = (from p in properties where p.StartsWith("internalKeyToken=") select p).FirstOrDefault();
            var publicKeyTokenString = (from p in properties where p.StartsWith("PublicKeyToken=") select p).FirstOrDefault();
            var processorArchitectureString = (from p in properties where p.StartsWith("processorArchitecture=") select p).FirstOrDefault();
            var customString = (from p in properties where p.StartsWith("Custom=") select p).FirstOrDefault();

            //  Then we should have version.
            if (!string.IsNullOrEmpty(versionString))
            {
                try
                {
                    Version = versionString.Substring(versionString.IndexOf('=') + 1);
                }
                catch (Exception)
                {
                }
            }

            //  Then culture.
            if (!string.IsNullOrEmpty(cultureString))
            {
                try
                {
                    cultureString = cultureString.Substring(cultureString.IndexOf('=') + 1);
                    Culture = cultureString;
                }
                catch (Exception)
                {
                }
            }

            //  Then internal key token.
            if (!string.IsNullOrEmpty(internalKeyTokenString))
            {
                try
                {
                    internalKeyTokenString = internalKeyTokenString.Substring(internalKeyTokenString.IndexOf('=') + 1);
                    PublicKeyToken = internalKeyTokenString;
                    internalKeyToken = HexToData(internalKeyTokenString);
                }
                catch (Exception)
                {
                    PublicKeyToken = null;
                    internalKeyToken = null;
                }
            }

            //  Then public key token.
            if (!string.IsNullOrEmpty(publicKeyTokenString))
            {
                try
                {
                    PublicKeyToken = publicKeyTokenString.Substring(publicKeyTokenString.IndexOf('=') + 1);
                }
                catch (Exception)
                {
                    PublicKeyToken = null;
                }
            }

            //  Then processor architecture.
            if (!string.IsNullOrEmpty(processorArchitectureString))
            {
                try
                {
                    processorArchitectureString =
                        processorArchitectureString.Substring(processorArchitectureString.IndexOf('=') + 1);
                    ProcessorArchitecture = processorArchitectureString;
                }
                catch (Exception)
                {
                }
            }

            if (!string.IsNullOrEmpty(customString))
            {
                //  Then custom.
                try
                {
                    customString = customString.Substring(customString.IndexOf('=') + 1);
                    Custom = customString;
                }
                catch (Exception)
                {
                }
            }

            //  Finally, get the path.
            Path = AssemblyCache.QueryAssemblyInfo(DisplayName);
        }

        internal static byte[] HexToData(string hexString)
        {
            if (hexString == null)
                return null;

            if (hexString.Length % 2 == 1)
                hexString = '0' + hexString; // Up to you whether to pad the first or last byte

            byte[] data = new byte[hexString.Length / 2];

            for (int i = 0; i < data.Length; i++)
                data[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);

            return data;
        }

        internal AssemblyReflectionProperties DoLoadReflectionProperties()
        {
            //  Create reflection properties.
            var reflectionPropties = new AssemblyReflectionProperties();

            //  Load the reflection properties.
            reflectionPropties.Load(DisplayName);

            //  Return the properties.
            return reflectionPropties;
        }

        /// <summary>
        /// Gets the short assembly name, such as mscorlib.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the qualified name of the assembly. This is useful for Install/Uninstall.
        /// v1.0/v1.1 assemblies: "name, Version=xx, Culture=xx, internalKeyToken=xx".
        /// v2.0 assemblies: "name, Version=xx, Culture=xx, internalKeyToken=xx, ProcessorArchitecture=xx".
        /// </summary>
        /// <value>
        /// The name of the qualified assembly.
        /// </value>
        internal string DisplayName { get; set; }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets the internal key token.
        /// </summary>
        internal byte[] internalKeyToken { get; set; }

        /// <summary>
        /// Gets the culture.
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// Gets the path.
        /// </summary>
        internal string Path { get; set; }

        /// <summary>
        /// Gets the processor architecture.
        /// </summary>
        public string ProcessorArchitecture { get; set; }

        /// <summary>
        /// Gets the processor architecture.
        /// </summary>
        public string PublicKeyToken { get; set; }

        /// <summary>
        /// Gets the custom.
        /// </summary>
        internal string Custom { get; set; }

        /// <summary>
        /// The lazy fusion properties are fusion properties loaded only as required.
        /// </summary>
        internal readonly Lazy<AssemblyFusionProperties> lazyFusionProperties;

        /// <summary>
        /// The lazy reflection properties are properties loaded only as needed via reflection.
        /// </summary>
        internal readonly Lazy<AssemblyReflectionProperties> lazyReflectionProperties;

        /// <summary>
        /// Gets the fusion properties.
        /// </summary>
        internal AssemblyFusionProperties FusionProperties
        {
            get { return lazyFusionProperties.Value; }
        }

        /// <summary>
        /// Gets the reflection properties.
        /// </summary>
        internal AssemblyReflectionProperties ReflectionProperties
        {
            get { return lazyReflectionProperties.Value; }
        }
    }

    /// <summary>
    /// AssemblyFusionProperties represent the properties of an assembly that
    /// are loaded by the fusion API.
    /// </summary>
    internal class AssemblyFusionProperties
    {
        internal AssemblyFusionProperties()
        {
            InstallReferences = new List<FUSION_INSTALL_REFERENCE>();
        }

        /// <summary>
        /// Loads the fusion properties given the assembly name COM object.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        internal void Load(IAssemblyName assemblyName)
        {
            //  Load the properties.
            MajorVersion = GetShortProperty(assemblyName, ASM_NAME.ASM_NAME_MAJOR_VERSION);
            MinorVersion = GetShortProperty(assemblyName, ASM_NAME.ASM_NAME_MINOR_VERSION);
            BuildNumber = GetShortProperty(assemblyName, ASM_NAME.ASM_NAME_BUILD_NUMBER);
            RevisionNumber = GetShortProperty(assemblyName, ASM_NAME.ASM_NAME_REVISION_NUMBER);
            internalKey = GetByteArrayProperty(assemblyName, ASM_NAME.ASM_NAME_PUBLIC_KEY);

            //  Create an install reference enumerator.
            var enumerator = new InstallReferenceEnumerator(assemblyName);
            var reference = enumerator.GetNextReference();
            while (reference != null)
            {
                InstallReferences.Add(reference);
                reference = enumerator.GetNextReference();
            }

            //  Load the reserved properties.
            //ReservedHashValue = GetByteArrayProperty(assemblyName, ASM_NAME.ASM_NAME_HASH_VALUE);
            //ReservedHashAlgorithmId = GetDwordProperty(assemblyName, ASM_NAME.ASM_NAME_HASH_ALGID);

        }
        internal UInt16 GetShortProperty(IAssemblyName name, ASM_NAME propertyName)
        {
            uint bufferSize = 512;
            IntPtr buffer = Marshal.AllocHGlobal((int)bufferSize);
            name.GetProperty(propertyName, buffer, ref bufferSize);
            byte low = Marshal.ReadByte(buffer);
            byte high = Marshal.ReadByte(buffer, 1);
            Marshal.FreeHGlobal(buffer);
            return (UInt16)(low + (high << 8));
        }

        internal UInt32 GetDwordProperty(IAssemblyName name, ASM_NAME propertyName)
        {
            uint bufferSize = 512;
            IntPtr buffer = Marshal.AllocHGlobal((int)bufferSize);
            name.GetProperty(propertyName, buffer, ref bufferSize);
            byte a = Marshal.ReadByte(buffer);
            byte b = Marshal.ReadByte(buffer, 1);
            byte c = Marshal.ReadByte(buffer);
            byte d = Marshal.ReadByte(buffer, 1);
            Marshal.FreeHGlobal(buffer);
            return (UInt32)(a + (b << 8) + (c << 16) + (d << 24));
        }

        internal string GetStringProperty(IAssemblyName name, ASM_NAME propertyName)
        {
            uint bufferSize = BufferLength;
            IntPtr buffer = Marshal.AllocHGlobal((int)bufferSize);
            name.GetProperty(propertyName, buffer, ref bufferSize);
            var stringVaule = Marshal.PtrToStringUni(buffer, (int)bufferSize);
            Marshal.FreeHGlobal(buffer);
            return stringVaule;
        }

        internal byte[] GetByteArrayProperty(IAssemblyName name, ASM_NAME propertyName)
        {
            uint bufferSize = 512;
            IntPtr buffer = Marshal.AllocHGlobal((int)bufferSize);
            name.GetProperty(propertyName, buffer, ref bufferSize);
            byte[] result = new byte[bufferSize];
            for (int i = 0; i < bufferSize; i++)
                result[i] = Marshal.ReadByte(buffer, i);
            Marshal.FreeHGlobal(buffer);
            return result;
        }

        internal const int BufferLength = 65535;

        /// <summary>
        /// Gets or sets the major version.
        /// </summary>
        /// <value>
        /// The major version.
        /// </value>
        internal ushort MajorVersion { get; set; }

        /// <summary>
        /// Gets or sets the minor version.
        /// </summary>
        /// <value>
        /// The minor version.
        /// </value>
        internal ushort MinorVersion { get; set; }

        /// <summary>
        /// Gets or sets the build number.
        /// </summary>
        /// <value>
        /// The build number.
        /// </value>
        internal ushort BuildNumber { get; set; }

        /// <summary>
        /// Gets or sets the revision number.
        /// </summary>
        /// <value>
        /// The revision number.
        /// </value>
        internal ushort RevisionNumber { get; set; }

        /// <summary>
        /// Gets or sets the internal key.
        /// </summary>
        /// <value>
        /// The internal key.
        /// </value>
        internal byte[] internalKey { get; set; }

        /// <summary>
        /// Gets or sets the hash value. This is currently reserved.
        /// </summary>
        /// <value>
        /// The hash value.
        /// </value>
        internal byte[] ReservedHashValue { get; set; }

        /// <summary>
        /// Gets or sets the reserved hash algorithm id.
        /// </summary>
        /// <value>
        /// The reserved hash algorithm id.
        /// </value>
        internal uint ReservedHashAlgorithmId { get; set; }

        /// <summary>
        /// Gets the install references.
        /// </summary>
        internal List<FUSION_INSTALL_REFERENCE> InstallReferences { get; set; }
    }

    /// <summary>
    /// AssemblyReflectionProperties represent the properties of an assembly that
    /// are loaded by via reflection.
    /// </summary>
    internal class AssemblyReflectionProperties
    {
        /// <summary>
        /// Loads the reflection properties from the specified path.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        internal void Load(string displayName)
        {
            //  Load reflection details.
            try
            {
                var assembly = Assembly.ReflectionOnlyLoad(displayName);
                RuntimeVersion = assembly.ImageRuntimeVersion;
            }
            catch
            {
            }
        }

        /// <summary>
        /// Gets or sets the runtime version.
        /// </summary>
        /// <value>
        /// The runtime version.
        /// </value>
        internal string RuntimeVersion { get; set; }
    }

    /// <summary>
    /// The AssemblyCacheEnumerator is an object that can be used to enumerate all assemblies in the GAC.
    /// </summary>
    [ComVisible(false)]
    internal class AssemblyCacheEnumerator
    {
        internal AssemblyCacheEnumerator()
        {
            Initialise(null);
        }

        internal AssemblyCacheEnumerator(string assemblyName)
        {
            Initialise(assemblyName);
        }

        internal void Initialise(string assemblyName)
        {
            IAssemblyName fusionName = null;
            int hr = 0;

            //  If we have an assembly name, create the assembly name object.
            if (assemblyName != null)
            {
                hr = FusionImports.CreateAssemblyNameObject(out fusionName, assemblyName,
                    CREATE_ASM_NAME_OBJ_FLAGS.CANOF_PARSE_DISPLAY_NAME, IntPtr.Zero);

                //  Check the result.
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);
            }

            //  Create the assembly enumerator.
            hr = FusionImports.CreateAssemblyEnum(out assemblyEnumerator, IntPtr.Zero,
                fusionName, ASM_CACHE_FLAGS.ASM_CACHE_GAC, IntPtr.Zero);

            //  Check the result.
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);
        }

        /// <summary>
        /// Gets the next assembly.
        /// </summary>
        /// <returns>The next assembly, or null of all assemblies have been enumerated.</returns>
        internal IAssemblyName GetNextAssembly()
        {
            int hr = 0;
            IAssemblyName fusionName = null;

            if (done)
            {
                return null;
            }

            // Now get next IAssemblyName from m_AssemblyEnum
            hr = assemblyEnumerator.GetNextAssembly((IntPtr)0, out fusionName, 0);

            if (hr < 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }

            //  If we haven't got a fusion object, we're done.
            if (fusionName == null)
            {
                done = true;
                return null;
            }

            return fusionName;
        }

        internal IAssemblyEnum assemblyEnumerator = null;
        internal bool done;
    }

    /// <summary>
    /// AssemblyMustBeStronglyNamedException.
    /// </summary>
    internal class AssemblyMustBeStronglyNamedException : Exception
    {

    }

    /// <summary>
    /// The AssemblyCache class is a managed wrapper around the fusion IAssemblyCache COM interface.
    /// </summary>
    [ComVisible(false)]
    internal static class AssemblyCache
    {
        internal static void InstallAssembly(String assemblyPath, FUSION_INSTALL_REFERENCE reference, AssemblyCommitFlags flags)
        {
            if (reference != null)
            {
                if (!InstallReferenceGuid.IsValidInstallGuidScheme(reference.GuidScheme))
                    throw new ArgumentException("Invalid reference guid.", "guid");
            }

            IAssemblyCache ac = null;

            int hr = 0;

            hr = FusionImports.CreateAssemblyCache(out ac, 0);
            if (hr >= 0)
            {
                hr = ac.InstallAssembly((int)flags, assemblyPath, reference);
            }

            if (hr < 0)
            {
                if (hr == -2146234300 /*0x80131044*/)
                    throw new AssemblyMustBeStronglyNamedException();
                else
                    Marshal.ThrowExceptionForHR(hr);
            }
        }

        // assemblyName has to be fully specified name. 
        // A.k.a, for v1.0/v1.1 assemblies, it should be "name, Version=xx, Culture=xx, internalKeyToken=xx".
        // For v2.0 assemblies, it should be "name, Version=xx, Culture=xx, internalKeyToken=xx, ProcessorArchitecture=xx".
        // If assemblyName is not fully specified, a random matching assembly will be uninstalled. 
        internal static void UninstallAssembly(String assemblyName, FUSION_INSTALL_REFERENCE reference,
                                             out IASSEMBLYCACHE_UNINSTALL_DISPOSITION disp)
        {
            if (reference != null)
            {
                if (!InstallReferenceGuid.IsValidUninstallGuidScheme(reference.GuidScheme))
                    throw new ArgumentException("Invalid reference guid.", "guid");
            }

            //  Create an assembly cache objet.
            IAssemblyCache ac = null;
            int hr = FusionImports.CreateAssemblyCache(out ac, 0);
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);

            //  Uninstall the assembly.
            hr = ac.UninstallAssembly(0, assemblyName, reference, out disp);
            if (hr < 0)
                Marshal.ThrowExceptionForHR(hr);
        }

        // See comments in UninstallAssembly
        internal static String QueryAssemblyInfo(String assemblyName)
        {
            if (assemblyName == null)
            {
                throw new ArgumentException("Invalid name", "assemblyName");
            }

            var aInfo = new ASSEMBLY_INFO();

            aInfo.cchBuf = 1024;
            // Get a string with the desired length
            aInfo.currentAssemblyPath = new String('\0', aInfo.cchBuf);

            IAssemblyCache ac = null;
            int hr = FusionImports.CreateAssemblyCache(out ac, 0);
            if (hr >= 0)
            {
                hr = ac.QueryAssemblyInfo(0, assemblyName, ref aInfo);
            }
            if (hr < 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }

            return aInfo.currentAssemblyPath;
        }
    }
    internal class InstallReferenceEnumerator
    {
        internal InstallReferenceEnumerator(String assemblyName)
        {
            IAssemblyName fusionName = null;

            int hr = FusionImports.CreateAssemblyNameObject(
                out fusionName,
                assemblyName,
                CREATE_ASM_NAME_OBJ_FLAGS.CANOF_PARSE_DISPLAY_NAME,
                IntPtr.Zero);

            if (hr >= 0)
            {
                hr = FusionImports.CreateInstallReferenceEnum(out refEnum, fusionName, 0, IntPtr.Zero);
            }

            if (hr < 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }
        }

        internal InstallReferenceEnumerator(IAssemblyName assemblyName)
        {

            var hr = FusionImports.CreateInstallReferenceEnum(out refEnum, assemblyName, 0, IntPtr.Zero);

            if (hr < 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }
        }

        internal FUSION_INSTALL_REFERENCE GetNextReference()
        {
            IInstallReferenceItem item = null;
            int hr = refEnum.GetNextInstallReferenceItem(out item, 0, IntPtr.Zero);
            if ((uint)hr == 0x80070103)
            {
                // ERROR_NO_MORE_ITEMS
                return null;
            }

            if (hr < 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }

            IntPtr refData;
            FUSION_INSTALL_REFERENCE instRef = new FUSION_INSTALL_REFERENCE(Guid.Empty, String.Empty, String.Empty);

            hr = item.GetReference(out refData, 0, IntPtr.Zero);
            if (hr < 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }

            Marshal.PtrToStructure(refData, instRef);
            return instRef;
        }

        internal IInstallReferenceEnum refEnum;
    }

    internal class InstallReferenceGuid
    {
        internal static bool IsValidUninstallGuidScheme(Guid guid)
        {
            return (guid.Equals(UninstallSubkeyGuid) ||
                    guid.Equals(FilePathGuid) ||
                    guid.Equals(OpaqueGuid) ||
                    guid.Equals(MsiGuid) ||
                    guid.Equals(Guid.Empty));
        }
        internal static bool IsValidInstallGuidScheme(Guid guid)
        {
            return (guid.Equals(UninstallSubkeyGuid) ||
                    guid.Equals(FilePathGuid) ||
                    guid.Equals(OpaqueGuid) ||
                    guid.Equals(Guid.Empty));
        }

        internal static readonly Guid UninstallSubkeyGuid = new Guid("8cedc215-ac4b-488b-93c0-a50a49cb2fb8");
        internal static readonly Guid FilePathGuid = new Guid("b02f9d65-fb77-4f7a-afa5-b391309f11c9");
        internal static readonly Guid OpaqueGuid = new Guid("2ec93463-b0c3-45e1-8364-327e96aea856");
        // these GUID cannot be used for installing into GAC.
        internal static readonly Guid MsiGuid = new Guid("25df0fc1-7f97-4070-add7-4b13bbfd7cb8");
        internal static readonly Guid OsInstallGuid = new Guid("d16d444c-56d8-11d5-882d-0080c847b195");
    }

}
