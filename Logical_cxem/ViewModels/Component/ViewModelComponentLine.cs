using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Logical_cxem.Enumerations;
using Logical_cxem.Models;
using Logical_cxem.ViewElements;

namespace Logical_cxem.ViewModels.Component
{
    public class ViewModelComponentLine : BaseViewModelComponent
    {
        private bool isCheck;
        private PointCollection points = new PointCollection();

        public ViewModelComponentLine(PointPin point0, PointPin point1, Canvas _surface, int id = -1)
        {
            BaseComponentUI componentUi;
            if (id == -1)
            {
                componentUi = new ComponentUIConnectionLine(BaseElement.IdCounter);
                element = new ConnectionLine(BaseElement.IdCounter);
            }
            else
            {
                componentUi = new ComponentUIConnectionLine(id);
                element = new ConnectionLine(id);
            }

            Panel.SetZIndex(componentUi, -1000000);
            ((ConnectionLine) element).OnPropertyChangedModel += PropertyChanged;
            componentUi.DataContext = this;
            FirstPin = point0;
            LastPin = point1;
            BuildLine(point0, point1);
            componentUi.OnDeleteElement += OnDelete;
            _surface.Children.Add(componentUi);
        }

        public PointCollection Points
        {
            get => points;
            set
            {
                points = value;
                OnPropertyChanged();
            }
        }

        public bool IsCheck
        {
            get => isCheck;
            set
            {
                isCheck = value;
                OnPropertyChanged("State");
            }
        }

        public EState State
        {
            get
            {
                if (!IsCheck) return ((ConnectionLine) element).State;
                return EState.OK;
            }
            set => OnPropertyChanged();
        }

        public PointPin FirstPin { get; set; }
        public PointPin LastPin { get; set; }

        public int MinDistation { get; } = 15 + App.random.Next(-5, 5);

        private void PropertyChanged()
        {
            OnPropertyChanged("State");
        }

        public void ReBuild(PointPin point0, PointPin point1)
        {
            Points = new PointCollection();
            BuildLine(point0, point1);
            FirstPin = point0;
            LastPin = point1;
        }

        public void ReBuild()
        {
            Points = new PointCollection();
        }

        private void BuildLineStandart(PointPin point0, PointPin point1)
        {
            Points.Add(new Point(point0.X, point0.Y));
            var bufPoint = new Point();
            bufPoint.X = (point1.X + point0.X) / 2;
            bufPoint.Y = point0.Y;
            Points.Add(bufPoint);
            bufPoint.Y = point1.Y;
            Points.Add(bufPoint);
            Points.Add(new Point(point1.X, point1.Y));
        }

        private void BuildLineNotStandart(PointPin point0, PointPin point1)
        {
            Points.Add(new Point(point0.X, point0.Y));
            var bufPoint = new Point();
            bufPoint.X = point0.X + MinDistation;
            bufPoint.Y = point0.Y;
            Points.Add(bufPoint);
            bufPoint.Y = (point0.Y + point1.Y) / 2;
            Points.Add(bufPoint);
            bufPoint.X = point1.X - MinDistation;
            Points.Add(bufPoint);
            bufPoint.Y = point1.Y;
            Points.Add(bufPoint);
            Points.Add(new Point(point1.X, point1.Y));
        }

        private void BuildLine(PointPin point0, PointPin point1)
        {
            if (point0.Direction == EDirection.West)
                if (point1.Direction == EDirection.East)
                {
                    if (point0.X + MinDistation * 2 <= point1.X) BuildLineStandart(point0, point1);

                    if (point0.X + MinDistation * 2 > point1.X) BuildLineNotStandart(point0, point1);
                }

            if (point0.Direction == EDirection.East)
                if (point1.Direction == EDirection.West)
                {
                    if (point1.X + MinDistation * 2 <= point0.X) BuildLineStandart(point0, point1);

                    if (point1.X + MinDistation * 2 > point0.X) BuildLineNotStandart(point1, point0);
                }

            if (point0.Direction == EDirection.East)
                if (point1.Direction == EDirection.East)
                {
                    if (point1.X > point0.X)
                    {
                        Points.Add(new Point(point0.X, point0.Y));
                        var bufPoint = new Point();
                        bufPoint.X = point0.X - MinDistation;
                        bufPoint.Y = point0.Y;
                        Points.Add(bufPoint);
                        bufPoint.Y = point1.Y;
                        Points.Add(bufPoint);
                        bufPoint.X = point1.X;
                        Points.Add(bufPoint);
                        Points.Add(new Point(point1.X, point1.Y));
                    }

                    if (point1.X <= point0.X)
                    {
                        Points.Add(new Point(point1.X, point1.Y));
                        var bufPoint = new Point();
                        bufPoint.X = point1.X - MinDistation;
                        bufPoint.Y = point1.Y;
                        Points.Add(bufPoint);
                        bufPoint.Y = point0.Y;
                        Points.Add(bufPoint);
                        bufPoint.X = point0.X;
                        Points.Add(bufPoint);
                        Points.Add(new Point(point0.X, point0.Y));
                    }
                }

            if (point0.Direction == EDirection.West)
                if (point1.Direction == EDirection.West)
                {
                    if (point1.X <= point0.X)
                    {
                        Points.Add(new Point(point0.X, point0.Y));
                        var bufPoint = new Point();
                        bufPoint.X = point0.X + MinDistation;
                        bufPoint.Y = point0.Y;
                        Points.Add(bufPoint);
                        bufPoint.Y = point1.Y;
                        Points.Add(bufPoint);
                        bufPoint.X = point1.X;
                        Points.Add(bufPoint);
                        Points.Add(new Point(point1.X, point1.Y));
                    }

                    if (point1.X > point0.X)
                    {
                        Points.Add(new Point(point1.X, point1.Y));
                        var bufPoint = new Point();
                        bufPoint.X = point1.X + MinDistation;
                        bufPoint.Y = point1.Y;
                        Points.Add(bufPoint);
                        bufPoint.Y = point0.Y;
                        Points.Add(bufPoint);
                        bufPoint.X = point0.X;
                        Points.Add(bufPoint);
                        Points.Add(new Point(point0.X, point0.Y));
                    }
                }
        }
    }
}