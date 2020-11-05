using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Logical_cxem.Commands;
using Logical_cxem.Enumerations;
using Logical_cxem.ViewModels.Component;

namespace Logical_cxem.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        #region Constructor

        public MainViewModel(ref Canvas surface)
        {
            _surface = surface;
            surface.Drop += Surface_Drop;
            surface.DragOver += Surface_DragOver;
            DrawBufLine = new ViewModelComponentLine(new PointPin(-100, -100, EDirection.East, ETypePin.TypeIn, 10, 10),
                new PointPin(-100, -100, EDirection.East, ETypePin.TypeIn, 10, 10), _surface, -1000);
            DrawBufLine.IsCheck = true;
        }

        #endregion

        #region Fields

        private readonly Canvas _surface;

        private readonly List<BaseViewModelComponent> _elements = new List<BaseViewModelComponent>();
        private bool _isOpenCxem;
        private bool _deleteChecked;
        private bool _isTest = true;
        private bool _isInTest = true;
        private string codeWork;
        private readonly string nameStudent = "";
        private float scale = 1;
        private int widthCanvas = 1920;
        private int heightCanvas = 1080;
        private ViewModelComponentLine DrawBufLine;
        private EStateApp stateApp = EStateApp.Editor;
        private ICommand _deleteCommand;
        private ICommand _clearAll;
        private ICommand _check;
        private ICommand _saveFileAs;
        private ICommand _saveFile;
        private ICommand _loadFile;
        private ICommand _loadTest;
        private ICommand _runTest;
        private ICommand _runControlTest;
        private ICommand _endTest;
        private ICommand _clickCanvas;
        private ICommand _newCxem;
        private ICommand enterCodeWorkCommand;

        #endregion

        #region Properties

        public bool IsOpenCxem
        {
            get => _isOpenCxem;
            set
            {
                _isOpenCxem = value;
                OnPropertyChanged();
            }
        }

        public bool DeleteChecked
        {
            get => _deleteChecked;
            set
            {
                _deleteChecked = value;
                base.OnPropertyChanged();
            }
        }

        public string CodeWork
        {
            get => codeWork;
            set
            {
                codeWork = value;
                OnPropertyChanged();
            }
        }

        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                if (scale >= 1)
                {
                    WidthCanvas = (int) (scale * 1920);
                    HeightCanvas = (int) (scale * 1080);
                }
                else
                {
                    WidthCanvas = 1920;
                    HeightCanvas = 1080;
                }

                OnPropertyChanged();
            }
        }

        public int WidthCanvas
        {
            get => widthCanvas;
            set
            {
                widthCanvas = value;
                OnPropertyChanged();
            }
        }

        public int HeightCanvas
        {
            get => heightCanvas;
            set
            {
                heightCanvas = value;
                OnPropertyChanged();
            }
        }

        private string PathCxem { get; set; } = "";

        public bool IsTest
        {
            get => _isTest;
            set
            {
                _isTest = value;
                OnPropertyChanged();
            }
        }

        public bool IsInTest
        {
            get => _isInTest;
            set
            {
                _isInTest = value;
                OnPropertyChanged();
            }
        }


        public EStateApp StateApp
        {
            get => stateApp;
            set
            {
                stateApp = value;
                OnPropertyChanged();
            }
        }


        public ICommand ClickCanvasCommand
        {
            get
            {
                _clickCanvas = _clickCanvas ?? new Command(ClickCanvas);
                return _clickCanvas;
            }
        }


        public ICommand EndTestCommand
        {
            get
            {
                _endTest = _endTest ?? new Command(EndTest);
                return _endTest;
            }
        }



        public ICommand RunControlTestCommand
        {
            get
            {
                _runControlTest = _runControlTest ?? new Command(RunControlTest);
                return _runControlTest;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                _deleteCommand = _deleteCommand ?? new Command(DeleteProccessCommand);
                return _deleteCommand;
            }
        }

        public ICommand SaveFileAs
        {
            get
            {
                _saveFileAs = _saveFileAs ?? new Command(SaveToFileAs);
                return _saveFileAs;
            }
        }

        public ICommand SaveFile
        {
            get
            {
                _saveFile = _saveFile ?? new Command(SaveToFile);
                return _saveFile;
            }
        }

        public ICommand LoadFile
        {
            get
            {
                _loadFile = _loadFile ?? new Command(LoadFromFile);
                return _loadFile;
            }
        }

        public ICommand LoadTest
        {
            get
            {
                _loadTest = _loadTest ?? new Command(LoadFromFileTest);
                return _loadTest;
            }
        }

        public ICommand NewCxemCommand
        {
            get
            {
                _newCxem = _newCxem ?? new Command(NewFile);
                return _newCxem;
            }
        }

        public ICommand ClearAll
        {
            get
            {
                _clearAll = _clearAll ?? new Command(ClearAllCommand);
                return _clearAll;
            }
        }

        public ICommand Check
        {
            get
            {
                _check = _check ?? new Command(CheckCommand);
                return _check;
            }
        }

        #endregion
    }
}