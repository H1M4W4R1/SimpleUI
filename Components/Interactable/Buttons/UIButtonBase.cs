using Systems.SimpleUserInterface.Abstract.Objects.Interactable;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Components.Interactable.Buttons
{
    [RequireComponent(typeof(Button))] public abstract class UIButtonBase : UIInteractableObjectBase
    {
        /// <summary>
        ///     Reference to the button component
        /// </summary>
        protected Button buttonReference;

        protected bool IsInteractable => buttonReference.interactable;
        
        protected override void AssignComponents()
        {
            base.AssignComponents();
            buttonReference = GetComponent<Button>();
        }

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

    }
}