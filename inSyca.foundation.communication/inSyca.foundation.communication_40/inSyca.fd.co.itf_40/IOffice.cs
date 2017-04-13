using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Linq;

namespace inSyca.foundation.communication.itf
{
    [ServiceContract(Namespace = "http://www.inSyca.com/IOffice")]
    public interface IOffice
    {
        [OperationContract(Name = "GetDataRibbonContainer")]
        DataRibbonContainer GetDataRibbonContainer(OfficeApplicationContainer officeApplicationContainer);

        [OperationContract(Name = "GetDataDialogContainer")]
        DataDialogContainer GetDataDialogContainer(OfficeApplicationContainer officeApplicationContainer);

        [OperationContract(Name = "UpdateDialogData")]
        XElement UpdateDialogData(OfficeApplicationContainer officeApplicationContainer, XElement xData);

        [OperationContract(Name = "UpdateDocumentData")]
        XElement UpdateDocumentData(OfficeApplicationContainer officeApplicationContainer, XElement xData);

        [OperationContract(Name = "SetDocument")]
        void SetDocument(OfficeApplicationContainer officeApplicationContainer, byte[] openXmlDocument);

        [OperationContract(Name = "GetDocument")]
        byte[] GetDocument(OfficeApplicationContainer officeApplicationContainer);

        [OperationContract(Name = "TransformDocumentDocument")]
        byte[] TransformDocument(OfficeApplicationContainer officeApplicationContainer, byte[] openXmlDocument);
    }

    [DataContract]
    [Serializable]
    public class DataRibbonContainer
    {
        [DataMember]
        public XElement ribbonData { get; set; }

        [DataMember]
        public XElement actionData { get; set; }

        [DataMember]
        public XElement documentData { get; set; }
    }

    [DataContract]
    [Serializable]
    public class DataDialogContainer
    {
        [DataMember]
        public byte[] xamlDialog;

        [DataMember]
        public XElement dataSource { get; set; }

        [DataMember]
        public string xmlDialogDataName;

        private string _dialogID;
        public string dialogID
        {
            get
            {
                return window.Uid;
            }
        }


        private XmlDataProvider xmlDataProvider
        {
            get
            {
                return window.FindResource(xmlDialogDataName) as XmlDataProvider;
            }
        }

        private Window _window;
        public Window window
        {
            get
            {
                if (_window == null)
                {
                    _window = (Window)XamlReader.Load(new MemoryStream(xamlDialog));
                    _window.Loaded += _window_Loaded;
                }
                return _window;
            }
        }

        private void _window_Loaded(object sender, RoutedEventArgs e)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(dataSource.CreateReader());

            xmlDataProvider.Document = xmlDocument;
        }

        public void UpdateDataProvider()
        {
            if (xmlDataProvider.Document == null)
                return;

            xmlDataProvider.Document.Load(dataSource.CreateReader());
            xmlDataProvider.Refresh();
        }

        public void UpdateDataSource()
        {
            if (xmlDataProvider.Document != null)
                dataSource = XElement.Load(new XmlNodeReader(xmlDataProvider.Document));
        }
    }

    [DataContract]
    [Serializable]
    public class OfficeApplicationContainer
    {
        public enum eOfficeApplication
        {
            Word,
            Excel,
            Unknown
        };

        private object _OfficeApplication;
        public object OfficeApplication
        {
            get
            {
                return _OfficeApplication;
            }
            set
            {
                _OfficeApplication = value;
                if (value is Microsoft.Office.Interop.Word.Application)
                    applicationType = eOfficeApplication.Word;
                else if (OfficeApplication is Microsoft.Office.Interop.Excel.Application)
                    applicationType = eOfficeApplication.Excel;
                else
                    applicationType = eOfficeApplication.Unknown;
            }

        }

        [DataMember]
        public eOfficeApplication applicationType { get; set; }

        public void ProcessDocumentData(string id, XElement documentData)
        {
            if (applicationType == eOfficeApplication.Word)
                ProcessDocumentData(documentData, ((Microsoft.Office.Interop.Word.Application)OfficeApplication).ActiveDocument);
            else if (applicationType == eOfficeApplication.Excel)
                ProcessDocumentData(documentData, ((Microsoft.Office.Interop.Excel.Application)OfficeApplication).ActiveWorkbook);
        }

        private void ProcessDocumentData(XElement documentData, Microsoft.Office.Interop.Word.Document activeDocument)
        {
            try
            {
                IEnumerable<XElement> items = from el in documentData.Descendants("Variables") select el;

                foreach (XElement xElement in items.Descendants())
                {
                    try
                    {
                        activeDocument.Variables[xElement.Name.ToString()].Value = xElement.Value.ToString();
                    }
                    catch { }
                }

                activeDocument.Fields.Update();

                foreach (Microsoft.Office.Interop.Word.Section currentsection in activeDocument.Sections)
                {
                    currentsection.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterFirstPage].Range.Fields.Update();
                    currentsection.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Fields.Update();
                    currentsection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterFirstPage].Range.Fields.Update();
                    currentsection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Fields.Update();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("FillWordDocumentVariables Error: {0}", ex.Message), System.Diagnostics.EventLogEntryType.Error);
            }
        }

        private void ProcessDocumentData(XElement documentData, Microsoft.Office.Interop.Excel.Workbook activeWorkbook)
        {
            try
            {
                IEnumerable<XElement> items = from el in documentData.Descendants("Variables") select el;
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("FillWordDocumentVariables Error: {0}", ex.Message), System.Diagnostics.EventLogEntryType.Error);
            }
        }

        public void ActivateDocument(string tempFile, bool closeDocument)
        {
            if (applicationType == eOfficeApplication.Word)
                ActivateDocument(((Microsoft.Office.Interop.Word.Application)OfficeApplication).ActiveDocument, tempFile, closeDocument);
            else if (applicationType == eOfficeApplication.Excel)
                ActivateDocument(((Microsoft.Office.Interop.Excel.Application)OfficeApplication).ActiveWorkbook, tempFile, closeDocument);
        }

        private void ActivateDocument(Microsoft.Office.Interop.Word._Document document, string tempFile, bool closeDocument)
        {
            object oMissing = System.Reflection.Missing.Value;

            object saveOptionsObject = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;

            if (closeDocument)
                document.Close(saveOptionsObject);

            Microsoft.Office.Interop.Word.Document oDoc = ((Microsoft.Office.Interop.Word.Application)OfficeApplication).Documents.Add(tempFile, ref oMissing, ref oMissing, ref oMissing);

            oDoc.Activate();
        }

        private void ActivateDocument(Microsoft.Office.Interop.Excel._Workbook workbook, string tempFile, bool closeDocument)
        {
            if (closeDocument)
                workbook.Close(false, Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel._Workbook _workbook = ((Microsoft.Office.Interop.Excel.Application)OfficeApplication).Workbooks.Add(tempFile);

            _workbook.Activate();
        }

        public string CreateTempFile()
        {
            if (applicationType == eOfficeApplication.Word)
                return CreateTempFile(((Microsoft.Office.Interop.Word.Application)OfficeApplication).ActiveDocument);
            else if (applicationType == eOfficeApplication.Excel)
                return CreateTempFile(((Microsoft.Office.Interop.Excel.Application)OfficeApplication).ActiveWorkbook);

            return string.Empty;
        }

        private string CreateTempFile(Microsoft.Office.Interop.Word.Document activeDocument)
        {
            var document = activeDocument;
            var ipersistfile = (System.Runtime.InteropServices.ComTypes.IPersistFile)document;
            string tempfile = Path.GetTempFileName();
            ipersistfile.Save(tempfile, false);
            return tempfile;
        }

        private string CreateTempFile(Microsoft.Office.Interop.Excel.Workbook activeWorkbook)
        {
            var document = activeWorkbook;
            var ipersistfile = (System.Runtime.InteropServices.ComTypes.IPersistFile)document;
            string tempfile = Path.GetTempFileName();
            ipersistfile.Save(tempfile, false);
            return tempfile;
        }
    }
}
