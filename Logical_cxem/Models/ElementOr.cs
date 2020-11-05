using Logical_cxem.Enumerations;

namespace Logical_cxem.Models
{
    internal class ElementOr : BaseElement
    {
        public ElementOr() : base(ETypeOfElement.Or)
        {
            CreatePins();
        }

        public ElementOr(int id) : base(ETypeOfElement.Or, id)
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
            foreach (var item in GetAllInPins())
                if (item.State == EState.True)
                    bufState = item.State;
            GetPinId(2).State = bufState;
        }
    }
}