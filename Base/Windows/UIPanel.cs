using Systems.SimpleUserInterface.Abstract.Objects;
using UnityEngine;

namespace Systems.SimpleUserInterface.Base.Windows
{
    /// <summary>
    ///     Base panel for User Interface, can be used to store UI Context
    /// </summary>
    [RequireComponent(typeof(Canvas))] public abstract class UIPanel : UIObjectBase
    {
        protected Canvas canvasReference;

        protected override void AssignComponents()
        {
            base.AssignComponents();
            canvasReference = GetComponent<Canvas>();
        }

        /// <summary>
        ///     Sets the sorting order of the panel, used mostly to handle windows z-index
        /// </summary>
        /// <param name="sortingOrder">Sorting order of the panel</param>
        protected void SetSortingOrder(int sortingOrder) => canvasReference.sortingOrder = sortingOrder;
    }
}