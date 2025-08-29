using Systems.SimpleUserInterface.Abstract.Objects.Interactable;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Base.Interactable.Toggles
{
    [RequireComponent(typeof(Toggle))]
    public abstract class UIToggleBase : UIInteractableObjectBase 
    {
        protected Toggle toggleReference;

        protected bool IsInteractable => toggleReference.interactable;
        
        /// <summary>
        ///     Returns the current state of the toggle
        /// </summary>
        public bool IsToggled
        {
            get => toggleReference.isOn;
            protected set => toggleReference.isOn = value;
        }
        
        protected override void AssignComponents()
        {
            base.AssignComponents();
            toggleReference = GetComponent<Toggle>();
        }

        protected override void AttachEvents()
        {
            toggleReference.onValueChanged.AddListener(OnToggleValueChanged);
        }

        protected override void DetachEvents()
        {
            toggleReference.onValueChanged.RemoveListener(OnToggleValueChanged);
        }

        /// <summary>
        ///     Event that is called when the toggle value changes
        /// </summary>
        protected abstract void OnToggleValueChanged(bool newValue);
        
        /// <summary>
        ///     Changes the interactable state of the toggle
        /// </summary>
        public override void SetInteractable(bool interactable) =>
            toggleReference.interactable = interactable;
    }
}