using System;
using System.Collections.Generic;
using System.Linq;
using Logical_cxem.Enumerations;
using Logical_cxem.ViewModels.Component;

namespace Logical_cxem.Service
{
    public class Grouping
    {
        public static void StartGroup(List<BaseViewModelComponent> elements)
        {
            foreach (var element in elements) element.GetElementModel().GroupID = default;
            var logicElements = from element in elements
                where element.GetElementModel().Name != ETypeOfElement.Input &&
                      element.GetElementModel().Name != ETypeOfElement.Output &&
                      element.GetElementModel().Name != ETypeOfElement.Line
                select element.GetElementModel();
            foreach (var element in elements)
                if (element.GetElementModel().GroupID == default)
                {
                    var id = Guid.NewGuid();
                    element.GetElementModel().GroupID = id;
                    Group(element.GetElementModel(), id, elements);
                }
        }

        public static void Group(BaseElement element, Guid id, List<BaseViewModelComponent> elements)
        {
            foreach (var pin in element.GetAllPins())
            foreach (var coPin in pin.CoWorker)
            {
                var targetElement = elements.Find(x => x.GetElementModel().Id == coPin.ParentId).GetElementModel();
                if (targetElement.GroupID == default || element.GroupID != targetElement.GroupID)
                {
                    targetElement.GroupID = id;
                    Group(targetElement, targetElement.GroupID, elements);
                }
            }
        }
    }
}