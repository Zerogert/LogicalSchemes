using System;
using System.Collections.Generic;
using Logical_cxem.Enumerations;
using Logical_cxem.Models;

namespace Logical_cxem
{
    public abstract class BaseElement : BaseModel
    {
        public static int IdCounter;
        public readonly ETypeOfElement Name;
        private bool checkVisibility;
        private Guid groupID = Guid.NewGuid();
        protected List<Pin> Pins;
        protected EState state;
        private EState stateCheck = EState.False;
        private EState trueStateCheck = EState.False;
        private string strength = "Подпись";
        public string Strength {
	        get => strength;
	        set {
		        strength = value;
		        OnPropertyChanged();
	        }
        }

        protected BaseElement(ETypeOfElement name)
        {
            IdCounter++;
            Name = name;
            Pins = new List<Pin>();
        }

        protected BaseElement(ETypeOfElement name, int id) : this(name)
        {
            Id = id;
        }

        public int Id { get; set; }

        public EState StateCheck
        {
            get => stateCheck;
            set
            {
                stateCheck = value;
                OnPropertyChanged();
            }
        }

        public EState TrueStateCheck
        {
            get => trueStateCheck;
            set
            {
                trueStateCheck = value;
                OnPropertyChanged();
            }
        }

        public bool CheckVisibility
        {
            get => checkVisibility;
            set
            {
                checkVisibility = value;
                OnPropertyChanged();
            }
        }

        public Guid GroupID
        {
            get => groupID;
            set
            {
                groupID = value;
                OnPropertyChanged();
            }
        }

        protected virtual void MegaLogic()
        {
            //OutputPin[0].State=EState.True;
        }

        public Pin GetPinIndex(int index)
        {
            return Pins[index];
        }

        public Pin GetPinId(int id)
        {
            foreach (var pin in Pins)
                if (pin.Id == id)
                    return pin;
            return null;
        }

        public void DeleteAllLink()
        {
            foreach (var pin in Pins) pin.DisConnectAll();
        }

        public List<Pin> GetAllInPins()
        {
            return Pins.FindAll(pin => pin.Type == ETypePin.TypeIn);
        }

        public List<Pin> GetAllOutPins()
        {
            return Pins.FindAll(pin => pin.Type == ETypePin.TypeOut);
        }

        public List<Pin> GetAllPins()
        {
            return Pins;
        }

        public void AddPin(ETypePin type, int idPin)
        {
            Pins.Add(type == ETypePin.TypeIn
                ? new Pin(type, onChanged: MegaLogic, id: idPin, parentId: Id)
                : new Pin(type, id: idPin, parentId: Id));
        }

        public virtual void ChangeState()
        {
        }

        public static void Connect(Pin inputPin, Pin outputPin)
        {
            inputPin.Connect(outputPin);
            outputPin.Connect(inputPin);
            inputPin.Exec();
            outputPin.Exec();
        }

        public static void DisConnect(Pin inputPin, Pin outputPin)
        {
            inputPin.DisConnect(outputPin);
            outputPin.DisConnect(inputPin);
        }

        public void Delete()
        {
            DeleteAllLink();
        }

        public void RunCheck()
        {
            if (GetAllOutPins().Count <= 0) return;

            TrueStateCheck = StateCheck == GetAllOutPins()[0].State ? EState.True : EState.False;
        }

        public void ShowCheckElement(bool b)
        {
            CheckVisibility = b;
        }

        public bool CheckIsConnectionToPin(int id, int coIdParentOfPin, int coId)
        {
            return GetPinId(id).CheckHaveCoWorker(coIdParentOfPin, coId);
        }
    }
}