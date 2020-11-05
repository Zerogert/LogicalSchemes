using Logical_cxem.Enumerations;

namespace Logical_cxem.Models
{
    public class ConnectionLine : BaseElement
    {
        public delegate void PropertyChanged();

        public ConnectionLine(int id) : base(ETypeOfElement.Line, id)
        {
            AddPin(ETypePin.TypeIn, 0);
            AddPin(ETypePin.TypeOut, 1);
        }

        public EState State
        {
            get => state;
            set
            {
                state = value;
                OnPropertyChangedModel();
                OnPropertyChanged();
            }
        }

        public event PropertyChanged OnPropertyChangedModel;

        protected override void MegaLogic()
        {
            var bufState = EState.False;
            foreach (var pin in GetAllInPins())
                if (pin.State == EState.True)
                {
                    bufState = EState.True;
                    break;
                }

            State = bufState;

            foreach (var pin in GetAllOutPins()) pin.State = State;
            base.MegaLogic();
        }
    }
}