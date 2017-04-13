using System;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;

namespace inSyca.foundation.integration.ssis.components
{
    ///
    /// <summary>
    /// Caches data about a single column in an input.
    /// </summary>
    /// 
    internal class XmlColumnInfo
    {
        string name_;
        DataType type_;
        int bufferIndex_;

        public XmlColumnInfo(string name, DataType type, int bufferIndex)
        {
            name_ = name;
            type_ = type;
            bufferIndex_ = bufferIndex;
        }

        /// <summary>
        /// Returns the name of the attribute or element
        /// that this column produces in te XML.
        /// </summary>
        public string Name
        {
            get { return name_; }
        }

        /// <summary>
        /// Returns the SSIS data type for this column.
        /// </summary>
        public DataType Type
        {
            get { return type_; }
        }

        /// <summary>
        /// Returns the index of data for this column in buffers passed
        /// to ProcessInput.
        /// </summary>
        public int BufferIndex
        {
            get { return bufferIndex_; }
        }
    }
}
