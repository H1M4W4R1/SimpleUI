using Systems.SimpleUserInterface.Components.Objects.Interactable;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Components.Interactable.Buttons
{
    [RequireComponent(typeof(Button))] public abstract class UIButtonBase : UIInteractableObjectBase
    {
        /// <summary>
        ///     Reference to the button component
        /// </summary>
        [field: SerializeField, HideInInspector] protected Button buttonReference;
        
        public sealed override bool IsInteractable => buttonReference.interactable;
        
        protected override void AttachEvents()
        {
            base.AttachEvents();
            buttonReference.onClick.AddListener(OnClick);
        }

        protected override void DetachEvents()
        {
            buttonReference.onClick.RemoveListener(OnClick);
            base.DetachEvents();
        }

        /// <summary>
        ///     Event that is called when the button is clicked
        /// </summary>
        protected abstract void OnClick();

        /// <summary>
        ///     Changes the interactable state of the button
        /// </summary>
        public override void SetInteractable(bool interactable)
        {
            buttonReference.interactable = interactable;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            buttonReference = GetComponent<Button>();
        }
    }
}