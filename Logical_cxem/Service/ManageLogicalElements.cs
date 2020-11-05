using System;
using System.Windows.Controls;

namespace Logical_cxem
{
    internal class ManageLogicalElements
    {
        private static Canvas _canvas;

        private static void Init(Canvas canvas)
        {
            _canvas = canvas;
        }

        private static void AddElementToCanvas(BaseComponentUI componentUi)
        {
            if (_canvas != null)
                _canvas?.Children.Add(componentUi);
            else
                throw new Exception("Холст не инициализирован");
        }

        public void AddElementToCanvas(Canvas canvas, BaseComponentUI componentUi)
        {
            canvas.Children.Add(componentUi);
        }
    }
}