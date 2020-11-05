using Logical_cxem.Enumerations;

namespace Logical_cxem.Models
{
    internal class ElementCheck : BaseElement
    {
        private EState trueState;

        public ElementCheck(int id) : base(ETypeOfElement.Check, id)
        {
            CreatePins();
        }

        public ElementCheck() : base(ETypeOfElement.Check)
        {
            CreatePins();
        }

        public EState TrueState
        {
            get => trueState;
            set
            {
                trueState = value;
                OnPropertyChanged();
            }
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

        private void CreatePins()
        {
            AddPin(ETypePin.TypeIn, 0);
        }

        protected override void MegaLogic()
        {
        }

        public void Check()
        {
            var bufState = EState.False;
            foreach (var item in GetAllInPins())
                if (item.State == EState.True)
                {
                    bufState = EState.True;
                    break;
                }

            if (bufState == State)
                TrueState = EState.True;
            else
                TrueState = EState.False;
        }
    }
}