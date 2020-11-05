using Logical_cxem.Enumerations;

namespace Logical_cxem.Models
{
    internal class ElementLabel : BaseElement
    {
        public ElementLabel(int id) : base(ETypeOfElement.Label)
        {
            Id = id;
        }
    }
}