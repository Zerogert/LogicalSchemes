using Logical_cxem.Enumerations;

namespace Logical_cxem.Models
{
    internal class ElementNot : BaseElement
    {
        public ElementNot(int id) : base(ETypeOfElement.Not, id)
        {
            CreatePins();
        }

        public ElementNot() : base(ETypeOfElement.Not)
        {
            CreatePins();
        }

        private void CreatePins()
        {
            AddPin(ETypePin.TypeIn, 0);
            AddPin(ETypePin.TypeOut, 1);
            GetPinId(1).State = EState.True;
        }

        protected override void MegaLogic()
        {
            Pins[1].State = Pins[0].State == EState.True ? EState.False : EState.True;
        }
    }
}