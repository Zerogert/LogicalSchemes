using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Logical_cxem.Enumerations;
using Logical_cxem.Models;
using Logical_cxem.Service;
using Logical_cxem.Structs;
using Logical_cxem.ViewElements;
using Logical_cxem.ViewModels.Component;
using Microsoft.Win32;

namespace Logical_cxem.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        #region Methods

        private void MakeTest()
        {
            LoadFromFile();
            OnOfCheck(false);
        }

        public void MakeControl(string work)
        {
            var fileData = ManageFileSaveLoad.LoadFileFromDB(work);
            ClearAllCommand();
            MakeFromFileDataCxem(fileData);
            IsOpenCxem = true;
            OnOfCheck(false);
        }

        private void DeleteProccessCommand()
        {
        }

        private void ClearAllCommand()
        {
            _elements.Clear();
            _surface.Children.Clear();
            DrawBufLine = new ViewModelComponentLine(new PointPin(0, 0, EDirection.East, ETypePin.TypeIn, 10, 10),
                new PointPin(0, 0, EDirection.East, ETypePin.TypeIn, 10, 10), _surface, -1000);
            DrawBufLine.IsCheck = true;
        }

        public virtual void CheckCommand() //Проверка
        {
            var CountIsTrue = 0;
            Grouping.StartGroup(_elements);
            foreach (var element in _elements)
            {
                element.GetElementModel().RunCheck();
                if (element.GetElementModel().TrueStateCheck == EState.False) CountIsTrue++;
            }
        }

        public Result CheckGroup() //переписать куче блоков а лучше полностью приложение
        {
            var errors = new Dictionary<Guid, bool>();
            var elements = from element in _elements
                where element.GetElementModel().Name != ETypeOfElement.Input &&
                      element.GetElementModel().Name != ETypeOfElement.Output &&
                      element.GetElementModel().Name != ETypeOfElement.Line &&
                      element.GetElementModel().Name != ETypeOfElement.Label
                select element.GetElementModel();
            foreach (var element in elements)
                if (!errors.ContainsKey(element.GroupID))
                    if (element.TrueStateCheck == EState.False)
                        errors.Add(element.GroupID, false);

            var query = from ip in elements
                group ip by ip.GroupID
                into groups
                let count = groups.Count()
                orderby count descending
                select groups;
            Result result = new Result(){Count = query.Count(), Error = errors.Count };
            return result;
        }

        private void AddComponent()
        {
        }

        public void AddToCanvas(UIElement element)
        {
            _surface.Children.Add(element);
        }


        private BaseElement output;
        private BaseElement input;

        private void OnComponentClicked(object sender)
        {
            if (output == null)
                output = (BaseElement) sender;
            else
                input = (BaseElement) sender;

            //DrawLine(output, input);
            output = null;
            input = null;
        }

        private void CreateInput(Point point, Canvas surface, int id = -1, string text="")
        {
	        ComponentUIInput componentUi;
	        ElementInput element;
	        ElementLabel elementLabel;
	        
            var labelVM = new ViewModelLabel();
            if (id < 0) {
		        componentUi = new ComponentUIInput(point, BaseElement.IdCounter);
		        element = new ElementInput(BaseElement.IdCounter);
		        elementLabel = new ElementLabel(BaseElement.IdCounter);
		        text = "Подпись";

            } else {
		        componentUi = new ComponentUIInput(point, id);
		        element = new ElementInput(id);
		        elementLabel = new ElementLabel(id);
            }
            BaseViewModelComponent baseViewModelComponent = new BaseViewModelComponent { element = element };
            baseViewModelComponent.strength = text;
            componentUi.DataContext = element;
            surface.Children.Add(componentUi);
            componentUi.OnPinDropped += baseViewModelComponent.OnPinDrop;
            componentUi.OnDeleteElement += baseViewModelComponent.OnDelete;
            labelVM.element = elementLabel;
            componentUi.Label.DataContext = baseViewModelComponent;
            componentUi.Label.OnEdit += baseViewModelComponent.OnEdit;
            _elements.Add(baseViewModelComponent);
            _elements[_elements.Count - 1].OnPinElementDropped += OnElementPinDropped;
            _elements[_elements.Count - 1].OnElementDelete += Delete;
        }

        private void CreateComponent(ETypeOfElement name, Point point)
        {
            BaseViewModelComponent newComponent;
            if (name== ETypeOfElement.Input) {
	            CreateInput(point, _surface);
            }
            switch (name)
            {
                case ETypeOfElement.And:
                    newComponent = new GenericViewModelComponent<ComponentUIAnd, ElementAnd>(point, _surface);
                    break;
                case ETypeOfElement.AndNot:
                    newComponent = new GenericViewModelComponent<ComponentUIAndNot, ElementAndNot>(point, _surface);
                    break;
                case ETypeOfElement.Output:
                    newComponent = new GenericViewModelComponent<ComponentUIOutput, ElementOutput>(point, _surface);
                    break;
                case ETypeOfElement.Or:
                    newComponent = new GenericViewModelComponent<ComponentUIOr, ElementOr>(point, _surface);
                    break;
                case ETypeOfElement.OrNot:
                    newComponent = new GenericViewModelComponent<ComponentUIOrNot, ElementOrNot>(point, _surface);
                    break;
                case ETypeOfElement.Xor:
                    newComponent = new GenericViewModelComponent<ComponentUIXor, ElementXor>(point, _surface);
                    break;
                case ETypeOfElement.XorNot:
                    newComponent = new GenericViewModelComponent<ComponentUIXorNot, ElementXorNot>(point, _surface);
                    break;
                case ETypeOfElement.Check:
                    newComponent = new GenericViewModelComponent<ComponentUICheck, ElementCheck>(point, _surface);
                    break;
                case ETypeOfElement.Not:
                    newComponent = new GenericViewModelComponent<ComponentUINot, ElementNot>(point, _surface);
                    break;
                case ETypeOfElement.Label:
                    newComponent = new ViewModelLabel(point, _surface);
                    break;
                default:
                    return;
            }
            _elements.Add(newComponent);
            _elements[_elements.Count - 1].OnPinElementDropped += OnElementPinDropped;
            _elements[_elements.Count - 1].OnElementDelete += Delete;
        }

        private void CreateComponent(ETypeOfElement name, Point point, int id, string text)
        {
            BaseViewModelComponent newComponent;
            if (name == ETypeOfElement.Input)
            {
	            CreateInput(point, _surface, id, text);
            }
            switch (name)
            {
                case ETypeOfElement.And:
                    newComponent = new GenericViewModelComponent<ComponentUIAnd, ElementAnd>(point, _surface, id);
                    break;
                case ETypeOfElement.AndNot:
                    newComponent = new GenericViewModelComponent<ComponentUIAndNot, ElementAndNot>(point, _surface, id);
                    break;
                case ETypeOfElement.Output:
                    newComponent = new GenericViewModelComponent<ComponentUIOutput, ElementOutput>(point, _surface, id);
                    break;
                case ETypeOfElement.Or:
                    newComponent = new GenericViewModelComponent<ComponentUIOr, ElementOr>(point, _surface, id);
                    break;
                case ETypeOfElement.OrNot:
                    newComponent = new GenericViewModelComponent<ComponentUIOrNot, ElementOrNot>(point, _surface, id);
                    break;
                case ETypeOfElement.Xor:
                    newComponent = new GenericViewModelComponent<ComponentUIXor, ElementXor>(point, _surface, id);
                    break;
                case ETypeOfElement.XorNot:
                    newComponent = new GenericViewModelComponent<ComponentUIXorNot, ElementXorNot>(point, _surface, id);
                    break;
                case ETypeOfElement.Check:
                    newComponent = new GenericViewModelComponent<ComponentUICheck, ElementCheck>(point, _surface, id);
                    break;
                case ETypeOfElement.Not:
                    newComponent = new GenericViewModelComponent<ComponentUINot, ElementNot>(point, _surface, id);
                    break;
                case ETypeOfElement.Label:
                    newComponent = new ViewModelLabel(point, _surface, id);
                    break;
                default:
                    return;
            }
            newComponent.strength = text;
            _elements.Add(newComponent);
            _elements[_elements.Count - 1].OnPinElementDropped += OnElementPinDropped;
            _elements[_elements.Count - 1].OnElementDelete += Delete;
        }

        public List<ViewModelComponentLine> GetAllLineWithConnectionOfLines(BaseViewModelComponent element)
        {
            var lines = new List<ViewModelComponentLine>();
            foreach (var pin in element.GetElementModel().GetAllPins())
            foreach (var coPin in pin.GetAllCoWorker())
                if (FindElementId(coPin.GetParentId()).Name == ETypeOfElement.Line)
                    lines.Add((ViewModelComponentLine) FindViewModelId(coPin.GetParentId()));
            return lines;
        }

        private void Delete(int id, UserControl UIElement) //Не глубокое удаление
        {
            if (DeleteChecked)
            {
                foreach (var line in GetAllLineWithConnectionOfLines(FindViewModelId(id)))
                {
                    line.Delete();
                    _elements.Remove(line);
                    for (var i = _surface.Children.Count - 1; i >= 0; i--)
                    {
                        var element = _surface.Children[i];
                        if (element is ComponentUIConnectionLine)
                            if (((ComponentUIConnectionLine) element).ID == line.Id)
                            {
                                _surface.Children.Remove(element);
                                break;
                            }
                    }
                }

                FindViewModelId(id).Delete();
                _elements.Remove(FindViewModelId(id));
                _surface.Children.Remove(UIElement);
            }
        }

        private void DeleteAllLineConnection(int id)
        {
        }

        private BaseElement FindElementId(int id)
        {
            foreach (var element in _elements)
            {
                Console.WriteLine(element.Id);
                if (element.Id == id) return element.GetElementModel();
            }

            return null;
        }

        private BaseViewModelComponent FindViewModelId(int id)
        {
            foreach (var element in _elements)
                if (element.Id == id)
                    return element;
            return null;
        }

        private void MoveLine(ViewModelComponentLine bufLine, Point truePosition, BaseComponentUI element)
        {
            var bufPointPin0 = bufLine.FirstPin;
            var bufPointPin1 = bufLine.LastPin;
            if (element.ID == bufLine.FirstPin.IdParent)
            {
                bufPointPin0.X = bufLine.FirstPin.X - truePosition.X;
                bufPointPin0.Y = bufLine.FirstPin.Y - truePosition.Y;
                bufPointPin1.X = bufLine.LastPin.X;
                bufPointPin1.Y = bufLine.LastPin.Y;
            }
            else
            {
                bufPointPin0.X = bufLine.FirstPin.X;
                bufPointPin0.Y = bufLine.FirstPin.Y;
                bufPointPin1.X = bufLine.LastPin.X - truePosition.X;
                bufPointPin1.Y = bufLine.LastPin.Y - truePosition.Y;
            }

            bufLine.ReBuild(bufPointPin0, bufPointPin1);
        }

        private void MoveAllLine(BaseComponentUI elementUi)
        {
            var oldPosition = elementUi.oldPosition;
            var newPosition = new Point(elementUi.Margin.Left, elementUi.Margin.Top);
            var truePosition = new Point(oldPosition.X - newPosition.X, oldPosition.Y - newPosition.Y);
            ViewModelComponentLine bufLine;
            var element = _elements.SingleOrDefault(e => e.Id == elementUi.ID);
            if (element == null) return;

            foreach (var pin in element.GetElementModel().GetAllPins())
            foreach (var coPin in pin.GetAllCoWorker())
                if (FindElementId(coPin.GetParentId()).Name == ETypeOfElement.Line)
                {
                    bufLine = (ViewModelComponentLine) FindViewModelId(coPin.GetParentId());
                    MoveLine(bufLine, truePosition, elementUi);
                }
        }

        private void Surface_Drop(object sender, DragEventArgs e)
        {
            DrawBufLine.ReBuild();
            var data = e;
            if (data.Handled == false)
            {
                var element = (BaseComponentUI) data.Data.GetData("ComponentUI");
                if (element != null)
                {
                    if (!element.Construct)
                    {
                        if (data.AllowedEffects.HasFlag(DragDropEffects.Move))
                            data.Effects = DragDropEffects.Move;
                        //MoveAllLine(element);
                    }
                    else
                    {
                        if (data.AllowedEffects.HasFlag(DragDropEffects.Copy))
                            CreateComponent(element.NameElement,
                                e.GetPosition(sender as Canvas ?? throw new InvalidOperationException()));
                    }
                }
            }
        }

        private void Surface_DragOver(object sender, DragEventArgs e)
        {
            var data = e;
            if (data.Handled == false)
            {
                var element = (BaseComponentUI) data.Data.GetData("ComponentUI");
                if (data.Data.GetData("DataLine") != null)
                {
                    var elementLine = (DataLine) data.Data.GetData("DataLine");
                    var bufPointPin = DrawBufLine.LastPin;
                    var position = e.GetPosition(sender as Canvas ?? throw new InvalidOperationException());
                    bufPointPin.X = position.X - 2;
                    bufPointPin.Y = position.Y - 2;
                    if (elementLine.point0.Direction == EDirection.East)
                        bufPointPin.Direction = EDirection.West;
                    else
                        bufPointPin.Direction = EDirection.East;
                    DrawBufLine.ReBuild(elementLine.point0, bufPointPin);
                }

                if (element != null)
                {
                    if (data.AllowedEffects.HasFlag(DragDropEffects.Move))
                    {
                        var position = e.GetPosition(sender as Canvas ?? throw new InvalidOperationException());
                        element.oldPosition = new Point(element.Margin.Left, element.Margin.Top);
                        element.Margin = new Thickness(Math.Max(0, position.X - element.relativeMouse.X),
                            Math.Max(0, position.Y - element.relativeMouse.Y), 0, 0);
                        data.Effects = DragDropEffects.Move;
                        MoveAllLine(element);
                    }

                    var i = 0;
                }
            }
        }

        private void SaveToFileAs()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".lcx";
            saveFileDialog.Filter = "Logical cxem files (*.lcx)|*.lcx";
            saveFileDialog.AddExtension = true;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                PathCxem = saveFileDialog.FileName;
                IsOpenCxem = true;
                ManageFileSaveLoad.SaveFile(PathCxem, MakeFileData());
            }
        }

        private void SaveToFile()
        {
            if (PathCxem != "") ManageFileSaveLoad.SaveFile(PathCxem, MakeFileData());
        }

        private void EnabledTestInput(bool b)
        {
            foreach (var element in _elements)
                if (element.GetElementModel().Name == ETypeOfElement.Input)
                {
                    ((ElementInput) element.GetElementModel()).GenerateNewValue();
                    ((ElementInput) element.GetElementModel()).OnTest(b);
                }
        }

        private void OnOfCheck(bool on)
        {
            if (on)
                StateApp = EStateApp.Editor;
            else
                StateApp = EStateApp.Test;
            IsTest = !on;
            IsInTest = on;
            DeleteChecked = false;
            EnabledTestInput(!on);
            ShowCheckElement(!on);
            ShowLineState(!on);
        }

        private void ShowLineState(bool b)
        {
            foreach (var line in _elements)
                if (line.GetElementModel().Name == ETypeOfElement.Line)
                    ((ViewModelComponentLine) line).IsCheck = b;
        }

        private void LoadFromFile()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Logical cxem files (*.lcx)|*.lcx";
            openFileDialog.ShowDialog();
            PathCxem = openFileDialog.FileName;
            if (PathCxem != "")
                try
                {
                    var logicalfile = ManageFileSaveLoad.LoadFile(PathCxem);
                    OnOfCheck(true);
                    ClearAllCommand();
                    MakeFromFileDataCxem(logicalfile);
                    IsOpenCxem = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
        }

        private void LoadFromFileTest()
        {
            MakeTest();
        }

        private void AddCheckElements()
        {
            foreach (var element in _elements)
            {
                var isIgnoredElement = (element.GetElementModel().Name == ETypeOfElement.Check) |
                                       (element.GetElementModel().Name == ETypeOfElement.Line) |
                                       (element.GetElementModel().Name == ETypeOfElement.Input) |
                                       (element.GetElementModel().Name == ETypeOfElement.Output);
                if (!isIgnoredElement)
                {
                    var positionElement = new Point(FindUIElement(element.Id).Margin.Left,
                        FindUIElement(element.Id).Margin.Top);
                    var position = positionElement;
                    position.X += 50;
                    CreateComponent(ETypeOfElement.Check, position);
                }
            }
        }

        private FileData MakeFileData()
        {
            FileData bufFileData;
            bufFileData.Name = "NameElement";
            bufFileData.Elements = new List<DataElement>();
            bufFileData.TypeWork = ETypeWork.Cxem;
            foreach (var viewModelComponent in _elements)
                if (viewModelComponent.GetElementModel().Name != ETypeOfElement.Line)
                {
                    DataElement bufDataElement;
                    bufDataElement.ID = viewModelComponent.Id;
                    bufDataElement.Type = viewModelComponent.GetElementModel().Name;
                    bufDataElement.Position = new Point(FindUIElement(viewModelComponent.Id).Margin.Left,
                        FindUIElement(viewModelComponent.Id).Margin.Top);
                    bufDataElement.Connections = new List<DataConnection>();
                    bufDataElement.Text = viewModelComponent.strength;
                    foreach (var pin in viewModelComponent.GetElementModel().GetAllOutPins())
                    foreach (var targetPin in pin.GetAllCoWorkerIn())
                    {
                        DataConnection bufDataConnection;
                        PointPin bufPointPin0;
                        PointPin bufPointPin1;
                        bufPointPin0 = ((ViewModelComponentLine) FindViewModelId(targetPin.ParentId)).FirstPin;
                        bufPointPin1 = ((ViewModelComponentLine) FindViewModelId(targetPin.ParentId)).LastPin;
                        bufDataConnection.PointPin0 = bufPointPin0;
                        bufDataConnection.PointPin1 = bufPointPin1;
                        bufDataConnection.ID = ((ViewModelComponentLine) FindViewModelId(targetPin.ParentId)).Id;
                        bufDataElement.Connections.Add(bufDataConnection);
                    }

                    bufFileData.Elements.Add(bufDataElement);
                }

            return bufFileData;
        }

        private void MakeFromFileDataCxem(FileData fileData)
        {
            ClearAllCommand();
            foreach (var element in fileData.Elements)
            {
                CreateComponent(element.Type, element.Position, element.ID, element.Text);
            }

            foreach (var element in fileData.Elements)
            foreach (var connection in element.Connections)
                if (CheckIsConnection(connection.PointPin0, connection.PointPin1))
                    CreateLine(connection);
        }

        private BaseComponentUI FindUIElement(int id)
        {
	        foreach (BaseComponentUI component in _surface.Children)
	        {
		        if (component.ID == id)
			        return component;
	        }
	        return null;
        }

        private void
            CreateLine(PointPin point0, PointPin point1) //Два соединение элемент-линия-линия-элемент и элемент-элемент 
        {
            var CheckCreateLine = (point0.IdParent != point1.IdParent) & (point0.Type != point1.Type);
            if (CheckCreateLine)
            {
                var pin0 = FindElementId(point0.IdParent).GetPinIndex(point0.Id);
                var pin1 = FindElementId(point1.IdParent).GetPinIndex(point1.Id);
                //BaseElement.Connect(pin0, pin1);
                //BaseElement.Connect(pin1, pin0);
                var line = new ViewModelComponentLine(point0, point1, _surface);
                _elements.Add(line);
                line.OnElementDelete += Delete;
                if (pin0.Type == ETypePin.TypeOut)
                {
                    //FindElementId(point1.IdParent).GroupID = FindElementId(point0.IdParent).GroupID;
                    pin1 = line.GetElementModel().GetPinId(0);
                    BaseElement.Connect(pin0, pin1);
                    pin0 = FindElementId(point1.IdParent).GetPinIndex(point1.Id);
                    pin1 = line.GetElementModel().GetPinId(1);
                }
                else
                {
                    //FindElementId(point0.IdParent).GroupID = FindElementId(point1.IdParent).GroupID;
                    pin1 = line.GetElementModel().GetPinId(1);
                    BaseElement.Connect(pin0, pin1);
                    pin0 = FindElementId(point1.IdParent).GetPinIndex(point1.Id);
                    pin1 = line.GetElementModel().GetPinId(0);
                }

                BaseElement.Connect(pin0, pin1);
                //BaseElement.Connect(pin1, pin0);
            }
        }

        private void CreateLine(DataConnection data) //Два соединение элемент-линия-линия-элемент и элемент-элемент 
        {
            var point0 = data.PointPin0;
            var point1 = data.PointPin1;
            var CheckCreateLine = (point0.IdParent != point1.IdParent) & (point0.Type != point1.Type);
            if (CheckCreateLine)
            {
                var pin0 = FindElementId(point0.IdParent).GetPinIndex(point0.Id);
                var pin1 = FindElementId(point1.IdParent).GetPinIndex(point1.Id);
                //BaseElement.Connect(pin0, pin1);
                //BaseElement.Connect(pin1, pin0);
                var line = new ViewModelComponentLine(point0, point1, _surface, data.ID);
                _elements.Add(line);
                line.OnElementDelete += Delete;
                if (pin0.Type == ETypePin.TypeOut)
                {
                    pin1 = line.GetElementModel().GetPinId(0);
                    BaseElement.Connect(pin0, pin1);
                    pin0 = FindElementId(point1.IdParent).GetPinIndex(point1.Id);
                    pin1 = line.GetElementModel().GetPinId(1);
                }
                else
                {
                    pin1 = line.GetElementModel().GetPinId(1);
                    BaseElement.Connect(pin0, pin1);
                    pin0 = FindElementId(point1.IdParent).GetPinIndex(point1.Id);
                    pin1 = line.GetElementModel().GetPinId(0);
                }

                BaseElement.Connect(pin0, pin1);
                //BaseElement.Connect(pin1, pin0);
            }
        }

        private void OnElementPinDropped(DataLine dataLine)
        {
            if (CheckIsConnection(dataLine.point0, dataLine.point1)) CreateLine(dataLine.point0, dataLine.point1);
        }

        private void ClickCanvas()
        {
        }

        private void ShowCheckElement(bool b)
        {
            foreach (var component in _elements) component.GetElementModel().ShowCheckElement(b);
        }

        private void NewFile()
        {
            ClearAllCommand();
            PathCxem = "";
            OnOfCheck(true);
            IsOpenCxem = false;
        }



        private void RunControlTest()
        {
            OnOfCheck(false);
        }

        private void EndTest()
        {
            OnOfCheck(true);
        }

        private List<T> FindAllElementsOfType<T>() //Дописать\узнать
        {
            var elementList = new List<T>();
            foreach (var element in _elements)
                if (element is T)
                {
                }

            return elementList;
        }

        public List<ViewModelComponentLine> FindAllLines()
        {
            var lines = new List<ViewModelComponentLine>();
            foreach (var line in _elements)
                if (line.GetElementModel().Name == ETypeOfElement.Line)
                    lines.Add((ViewModelComponentLine) line);
            return lines;
        }

        private bool CheckIsConnection(PointPin pin0, PointPin pin1)
        {
            var IsAllowConnection = true;
            foreach (var line in FindAllLines())
            {
                if ((line.FirstPin.Id == pin0.Id) & (line.FirstPin.IdParent == pin0.IdParent) &
                    (line.LastPin.IdParent == pin1.IdParent) & (line.LastPin.Id == pin1.Id)) IsAllowConnection = false;
                if ((line.FirstPin.Id == pin1.Id) & (line.FirstPin.IdParent == pin1.IdParent) &
                    (line.LastPin.IdParent == pin0.IdParent) & (line.LastPin.Id == pin0.Id)) IsAllowConnection = false;
            }

            return IsAllowConnection;
        }

        #endregion
    }
}