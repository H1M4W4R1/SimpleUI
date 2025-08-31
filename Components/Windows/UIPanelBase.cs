using Systems.SimpleUserInterface.Components.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Components.Windows
{
    /// <summary>
    ///     Base panel for User Interface, can be used to store UI Context
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public abstract class UIPanelBase : UIObjectBase
    {
        [field: SerializeField, HideInInspector] protected Canvas CanvasReference { get; private set; }

        /// <summary>
        ///     Sets the sorting order of the panel, used mostly to handle windows z-index
        /// </summary>
        /// <param name="sortingOrder">Sorting order of the panel</param>
        protected internal void SetSortingOrder(int sortingOrder)
        {
            CanvasReference.overrideSorting = true;
            CanvasReference.sortingOrder = sortingOrder;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            CanvasReference = GetComponent<Canvas>();
        }
    }
}