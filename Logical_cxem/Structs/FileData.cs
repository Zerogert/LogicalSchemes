using System;
using System.Collections.Generic;
using Logical_cxem.Enumerations;
using Logical_cxem.Structs;

namespace Logical_cxem
{
    [Serializable]
    public struct FileData
    {
        public string Name;
        public ETypeWork TypeWork;
        public List<DataElement> Elements;

        public FileData(string name = "", List<DataElement> elements = null, ETypeWork typeWork = ETypeWork.Cxem)
        {
            Name = name;
            TypeWork = typeWork;
            Elements = elements;
        }
    }
}