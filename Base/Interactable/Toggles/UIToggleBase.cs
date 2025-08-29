using Systems.SimpleUserInterface.Abstract.Objects.Interactable;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Base.Interactable.Toggles
{
    [RequireComponent(typeof(Toggle))]
    public abstract class UIToggleBase : UIInteractableObjectBase 
    {
        protected Toggle toggleReference;
        protected UIToggleGroupBase toggleGroupReference;

        /// <summary>
        ///     Access to the toggle component
        ///     If component is not assigned it gets the component from the game object
        ///     to ensure everything will work properly
        /// </summary>
        internal Toggle ToggleReference => toggleReference ? toggleReference : GetComponent<Toggle>();
        
        protected bool IsInteractable => toggleReference.interactable;
        
        /// <summary>
        ///     Returns the current state of the toggle
        /// </summary>
        public bool IsToggled
        {
            get => toggleReference.isOn;
            protected internal set => toggleReference.isOn = value;
        }
        
        protected override void AssignComponents()
        {
            base.AssignComponents();
            toggleReference = GetComponent<Toggle>();
            toggleGroupReference = GetComponentInParent<UIToggleGroupBase>(true);
        }

        protected override void AttachEvents()
        {
            toggleReference.onValueChanged.AddListener(_OnToggleValueChanged);
        }

        protected override void DetachEvents()
        {
            toggleReference.onValueChanged.RemoveListener(_OnToggleValueChanged);
        }

        protected override void OnTearDownComplete()
        {
            base.OnTearDownComplete();
            if (toggleGroupReference)
                toggleGroupReference.RefreshToggleArray();
        }

        /// <summary>
        ///     Internal event that is called when the toggle value changes
        ///     Proceeds to the <see cref="OnToggleValueChanged"/> event
        /// </summary>
        private void _OnToggleValueChanged(bool newValue)
        {
            if (toggleGroupReference) toggleGroupReference.OnToggleChanged(this, newValue);
            
            OnToggleValueChanged(newValue);
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