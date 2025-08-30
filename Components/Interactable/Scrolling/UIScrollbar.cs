using Systems.SimpleUserInterface.Components.Objects.Interactable;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Components.Interactable.Scrolling
{
    [RequireComponent(typeof(Scrollbar))] public abstract class UIScrollbar : UIInteractableObjectBase
    {
        [field: SerializeField, HideInInspector] protected Scrollbar scrollbarReference;

        protected override void AttachEvents()
        {
            base.AttachEvents();
            scrollbarReference.onValueChanged.AddListener(OnScrollbarValueChanged);
        }
        
        protected override void DetachEvents()
        {
            scrollbarReference.onValueChanged.RemoveListener(OnScrollbarValueChanged);
            base.DetachEvents();
        }

        /// <summary>
        ///     Raises when the scrollbar value changes
        /// </summary>
        /// <param name="value">New scrollbar value (0-1)</param>
        protected abstract void OnScrollbarValueChanged(float value);
        
        /// <summary>
        ///     Checks if the scrollbar is interactable
        /// </summary>
        public sealed override bool IsInteractable => scrollbarReference.interactable;
        
        /// <summary>
        ///     Changes the interactable state of the scrollbar
        /// </summary>
        public override void SetInteractable(bool interactable) =>
            scrollbarReference.interactable = interactable;

        protected override void OnValidate()
        {
            base.OnValidate();
            scrollbarReference = GetComponent<Scrollbar>();
        }
    }
}