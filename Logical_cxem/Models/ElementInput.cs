using Logical_cxem.Enumerations;
using Logical_cxem.View;

namespace Logical_cxem.Models
{
    internal class ElementInput : BaseElement
    {
        private bool isTest;
        private bool isEditing;

        public ElementInput(int id) : base(ETypeOfElement.Input, id)
        {
            AddPin(ETypePin.TypeOut, 0);
            State = EState.False;
        }

        private bool IsTest
        {
            get => isTest;
            set
            {
                isTest = value;
                State = EState.False;
            }
        }


        public bool IsEditing
        {
	        get => isEditing;
	        set
	        {
		        if (isEditing == value)
			        return;
		        isEditing = value;
		        OnPropertyChanged("IsEditing");
	        }
        }


        public EState State
        {
            get => state;
            set
            {
                if ((state != value) & !IsTest)
                {
                    state = value;
                    MegaLogic();
                }

                OnPropertyChanged();
            }
        }

        protected override void MegaLogic()
        {
            foreach (var pin in GetAllOutPins())
                if (pin.State != State)
                    pin.State = State;
            base.MegaLogic();
        }

        public override void ChangeState()
        {
            State = EState.True == State ? EState.False : EState.True;
        }

        public void GenerateNewValue()
        {
            IsTest = false;
            if (App.random.Next(2) == 0)
                State = EState.False;
            else
                State = EState.True;
            IsTest = true;
        }

        public void OnTest(bool b)
        {
            if (b) GenerateNewValue();
            IsTest = b;
        }

        private void OnEdit(bool b) {
	        var form = new EnterLabel();
	        form.Label.Text = Strength;
	        form.ShowDialog();
	        Strength = form.Label.Text;
        }
    }
}