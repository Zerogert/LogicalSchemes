using System.Collections.Generic;
using System.Windows;
using Logical_cxem.Enumerations;

namespace Logical_cxem.Structs
{
    public struct DataElement
    {
        public ETypeOfElement Type;
        public int ID;
        public Point Position;
        public string Text;
        public List<DataConnection> Connections;
    }
}