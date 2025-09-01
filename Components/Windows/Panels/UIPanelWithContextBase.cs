﻿using Systems.SimpleUserInterface.Components.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Components.Windows.Panels
{
    /// <summary>
    ///     Base panel for User Interface, can be used to store UI Context
    /// </summary>
    [RequireComponent(typeof(Canvas))] [RequireComponent(typeof(GraphicRaycaster))]
    public abstract class UIPanelBaseWithContext<TObject> : UIObjectWithContextBase<TObject>
    {
        /// <summary>
        ///     Sets the sorting order of the panel, used mostly to handle windows z-index
        /// </summary>
        /// <param name="sortingOrder">Sorting order of the panel</param>
        protected internal void SetSortingOrder(int sortingOrder)
        {
            if (!ClosestCanvasReference) return;
            ClosestCanvasReference.overrideSorting = true;
            ClosestCanvasReference.sortingOrder = sortingOrder;
        }
    }
}