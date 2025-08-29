using Systems.SimpleUserInterface.Abstract.Objects.Interactable;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Base.Interactable.Sliders
{
    /// <summary>
    ///     Slider for UI
    /// </summary>
    public abstract class UISliderBase : UIInteractableObjectBase
    {
        protected Slider sliderReference;

        /// <summary>
        ///     Default value of the slider
        /// </summary>
        protected virtual float DefaultValue { get; private set; } = float.NaN;
        
        /// <summary>
        ///     Minimum value of the slider
        /// </summary>
        protected virtual float MinValue
        {
            get => sliderReference.minValue;
            set => sliderReference.minValue = value;
        }

        /// <summary>
        ///     Maximum value of the slider
        /// </summary>
        protected virtual float MaxValue
        {
            get => sliderReference.maxValue;
            set => sliderReference.maxValue = value;
        }

        /// <summary>
        ///     Current value of the slider
        /// </summary>
        protected virtual float CurrentValue
        {
            get => sliderReference.value;
            set => sliderReference.value = value;
        }

        protected override void AssignComponents()
        {
            base.AssignComponents();
            sliderReference = GetComponent<Slider>();
            
            // Update current value of slider on creation
            if (float.IsNaN(DefaultValue))
                DefaultValue = sliderReference.value;
            
            CurrentValue = DefaultValue;
        }

        protected override void AttachEvents()
        {
            sliderReference.onValueChanged.AddListener(OnSliderValueChanged);
        }

        protected override void DetachEvents()
        {
            sliderReference.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        /// <summary>
        ///     Event that is called when the slider value changes
        /// </summary>
        protected abstract void OnSliderValueChanged(float newValue);

        /// <summary>
        ///     Sets the interactable state of the slider
        /// </summary>
        public override void SetInteractable(bool interactable) =>
            sliderReference.interactable = interactable;
    }
}