using Systems.SimpleUserInterface.Components.Selectors.Abstract;
using UnityEngine;

namespace Systems.SimpleUserInterface.Components.Selectors.Implementations
{
    /// <summary>
    ///     Toggle-group selector for UI, used to select single item from a list
    /// </summary>
    /// <typeparam name="TObjectType">Object type in the list</typeparam>
    [RequireComponent(typeof(UISelectorToggleGroup))]
    public abstract class UIToggleGroupSelectorBase<TObjectType> : UISelectorBase<TObjectType>
    {
        [field: SerializeField, HideInInspector] private UISelectorToggleGroup toggleGroup;

        /// <summary>
        ///     Selects a toggle
        /// </summary>
        /// <param name="toggleIndex">Index of the toggle to select</param>
        public bool SelectToggle(int toggleIndex) => toggleGroup.SelectToggle(toggleIndex);
     
        protected override void AttachEvents()
        {
            base.AttachEvents();
            toggleGroup.OnSelectionChanged += ToggleGroupSelectionChangedHandler;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            toggleGroup.OnSelectionChanged -= ToggleGroupSelectionChangedHandler;
        }

        /// <summary>
        ///     Handler for toggle group selection change
        /// </summary>
        private void ToggleGroupSelectionChangedHandler(int newIndex)
        {
            // Notify base implementation
            TrySelectIndex(newIndex);
        }

        protected override void OnLateSetupComplete()
        {
            base.OnLateSetupComplete();
            if (Context is null) return;
            
            // Notify of changes
            SelectToggle(Context.SelectedIndex);
            OnSelectedIndexChanged(-1, Context.SelectedIndex);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            toggleGroup = GetComponent<UISelectorToggleGroup>();
        }
    }
}