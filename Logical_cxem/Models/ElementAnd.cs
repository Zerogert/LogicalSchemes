using Logical_cxem.Enumerations;

namespace Logical_cxem
{
    internal class ElementAnd : BaseElement
    {
        public ElementAnd(int id) : base(ETypeOfElement.And, id)
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

            foreach (var outputPin in GetAllOutPins()) outputPin.State = State;
            base.MegaLogic();
        }
    }
}