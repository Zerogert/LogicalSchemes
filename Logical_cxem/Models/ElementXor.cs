using Logical_cxem.Enumerations;

namespace Logical_cxem.Models
{
    internal class ElementXor : BaseElement
    {
        public ElementXor(int id) : base(ETypeOfElement.Xor, id)
        {
            CreatePins();
        }

        public ElementXor() : base(ETypeOfElement.Xor)
        {
            CreatePins();
        }

        private void CreatePins()
        {
            AddPin(ETypePin.TypeIn, 0);
            AddPin(ETypePin.TypeIn, 1);
            AddPin(ETypePin.TypeOut, 2);
        }

        protected override void MegaLogic()
        {
            var bufState = EState.False;
            if (Pins[0].State != Pins[1].State) bufState = EState.True;
            Pins[2].State = bufState;
        }
    }
}