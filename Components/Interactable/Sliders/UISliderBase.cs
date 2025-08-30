using Systems.SimpleUserInterface.Components.Objects.Interactable;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Components.Interactable.Sliders
{
    /// <summary>
    ///     Slider for UI
    /// </summary>
    [RequireComponent(typeof(Slider))] public abstract class UISliderBase : UIInteractableObjectBase
    {
        [field: SerializeField, HideInInspector] protected Slider sliderReference;

        protected bool IsInteractable => sliderReference.interactable;

        /// <summary>
        ///     Default value of the slider
        /// </summary>
        protected virtual float DefaultValue { get; private set; } = float.NaN;

        /// <summary>
        ///     Minimum value of the slider
        /// </summary>
        public float MinValue
        {
            get => sliderReference.minValue;
            protected set => sliderReference.minValue = value;
        }

        /// <summary>
        ///     Maximum value of the slider
        /// </summary>
        public float MaxValue
        {
            get => sliderReference.maxValue;
            protected set => sliderReference.maxValue = value;
        }

        /// <summary>
        ///     Current value of the slider
        /// </summary>
        public float CurrentValue
        {
            get => sliderReference.value;
            protected set => sliderReference.value = value;
        }

        protected override void AssignComponents()
        {
            base.AssignComponents();

            // Update current value of slider on creation
            if (float.IsNaN(DefaultValue)) DefaultValue = sliderReference.value;

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

        protected override void OnValidate()
        {
            base.OnValidate();
            sliderReference = GetComponent<Slider>();
        }
    }
}