using System.Collections.Generic;

namespace Logical_cxem
{
    public delegate void OnChanged();

    public class Pin
    {
        private readonly OnChanged _OnChanged;
        private EState _State;

        public Pin(ETypePin type, BaseElement master = null, string name = "default", int id = 0, int parentId = 0,
            OnChanged onChanged = null)
        {
            CoWorker = new List<Pin>();
            Type = type;
            Name = name;
            ParentId = parentId;
            Id = id;
            State = EState.False;
            _OnChanged = onChanged;
        }

        public List<Pin> CoWorker { get; set; }
        public ETypePin Type { get; }
        public BaseElement Master { get; }
        private string Name { get; }
        public int Id { get; set; }
        public int ParentId { get; set; }

        public EState State
        {
            get => _State;
            set
            {
                if (Type == ETypePin.TypeIn)
                {
                    _State = value;
                }
                else
                {
                    if (_State != value)
                    {
                        _State = value;
                        Exec();
                    }
                }
            }
        }

        public void Connect(Pin conectingPin)
        {
            CoWorker.Add(conectingPin);
            Exec();
            if (_OnChanged != null) _OnChanged();
        }

        public void DisConnect(Pin disConnectingPin)
        {
            CoWorker.Remove(disConnectingPin);
            Exec();
        }

        public void DisConnectAll()
        {
            foreach (var coWorkerPin in CoWorker) coWorkerPin.DisConnect(this);
            CoWorker = new List<Pin>();
        }

        public List<Pin> GetAllCoWorkerIn()
        {
            var pins = new List<Pin>();
            foreach (var pin in CoWorker)
                if (pin.Type == ETypePin.TypeIn)
                    pins.Add(pin);
            return pins;
        }

        public List<Pin> GetAllCoWorker()
        {
            var pins = new List<Pin>();
            foreach (var pin in CoWorker) pins.Add(pin);
            return pins;
        }

        public List<Pin> GetAllCoWorkerOut()
        {
            var pins = new List<Pin>();
            foreach (var pin in CoWorker)
                if (pin.Type == ETypePin.TypeOut)
                    pins.Add(pin);
            return pins;
        }

        public int GetParentId()
        {
            return ParentId;
        }

        public void Exec()
        {
            if (Type == ETypePin.TypeIn
            ) //Если пин входной то получить состояние у коллеги если коллеги нет то он равен нулю
            {
                var bufState = EState.False;
                foreach (var worker in CoWorker)
                    if (worker.State == EState.True)
                    {
                        bufState = worker.State;
                        break;
                    }

                if (State != bufState)
                {
                    State = bufState;
                    _OnChanged();
                }
            }
            else
            {
                if (CoWorker.Count != 0)
                    foreach (var pin in CoWorker)
                        if (pin.Type == ETypePin.TypeIn)
                            pin.Exec();
            }
        }

        public bool CheckHaveCoWorker(int parentId, int id)
        {
            foreach (var coPin in CoWorker)
                if (coPin.Id == id && coPin.ParentId == parentId)
                    return true;
            return false;
        }
    }
}