using Logical_cxem.Enumerations;

namespace Logical_cxem.Models
{
    internal class ElementOutput : BaseElement
    {
        public ElementOutput(int id) : base(ETypeOfElement.Output, id)
        {
            AddPin(ETypePin.TypeIn, 0);
            State = EState.False;
        }

        public EState State
        {
            get => state;
            set
            {
                if (state != value) state = value;
                OnPropertyChanged();
            }
        }

        protected override void MegaLogic()
        {
            foreach (var pin in GetAllInPins())
                if (pin.State == EState.True)
                {
                    if (State != pin.State)
                    {
                        State = pin.State;
                        break;
                    }
                }
                else
                {
                    if (State != pin.State) State = pin.State;
                }

            base.MegaLogic();
        }
    }
}