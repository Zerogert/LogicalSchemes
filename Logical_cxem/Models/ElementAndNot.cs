using Logical_cxem.Enumerations;

namespace Logical_cxem.Models
{
    internal class ElementAndNot : BaseElement
    {
        public ElementAndNot(int id) : base(ETypeOfElement.AndNot, id)
        {
            AddPin(ETypePin.TypeIn, 0);
            AddPin(ETypePin.TypeIn, 1);
            AddPin(ETypePin.TypeOut, 2);
        }

        private EState State { get; set; }

        protected override void MegaLogic()
        {
            State = EState.True;
            foreach (var inputPin in GetAllInPins())
                if (inputPin.State == EState.False)
                {
                    State = EState.False;
                    break;
                }

            State = State == EState.True ? EState.False : EState.True;
            foreach (var outputPin in GetAllOutPins()) outputPin.State = State;
            base.MegaLogic();
        }
    }
}