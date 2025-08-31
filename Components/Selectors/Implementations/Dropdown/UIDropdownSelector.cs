using System.Collections.Generic;
using JetBrains.Annotations;
using Systems.SimpleUserInterface.Components.Selectors.Abstract;
using TMPro;
using UnityEngine;

namespace Systems.SimpleUserInterface.Components.Selectors.Implementations.Dropdown
{
    /// <summary>
    ///     Dropdown selector for UI, used to select single item from a list
    /// </summary>
    /// <typeparam name="TObjectType">Object type in the list</typeparam>
    [RequireComponent(typeof(TMP_Dropdown))]
    public abstract class UIDropdownSelectorBase<TObjectType> : UISelectorBase<TObjectType>
    {
        private TObjectType _lastKnownSelectedValue;

        [field: SerializeField, HideInInspector] private TMP_Dropdown DropdownComponent { get; set; }

        /// <summary>
        ///     Selects a dropdown option by index
        /// </summary>
        /// <param name="index">Index of option</param>
        public bool SelectOption(int index)
        {
            if (!DropdownComponent || Context == null) return false;
            if (!Context.IsValidIndex(index)) return false;

            DropdownComponent.value = index; // this triggers onValueChanged
            return true;
        }

        protected override void AttachEvents()
        {
            base.AttachEvents();
            DropdownComponent.onValueChanged.AddListener(DropdownSelectionChangedHandler);
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            DropdownComponent.onValueChanged.RemoveListener(DropdownSelectionChangedHandler);
        }

        /// <summary>
        ///     Handler for dropdown selection change
        /// </summary>
        private void DropdownSelectionChangedHandler(int newIndex)
        {
            // Check if same index
            if (ReferenceEquals(Context, null)) return;

            // Sync with base Context
            TrySelectIndex(newIndex);
        }

        protected override void OnSelectedIndexChanged(int from, int to)
        {
            base.OnSelectedIndexChanged(from, to);
            
            // Update selected cached value to new one to prevent issues
            if (ReferenceEquals(Context, null)) return;
            _lastKnownSelectedValue = Context.IsSelected ? Context.SelectedItem : default;
        }

        protected override void OnLateSetupComplete()
        {
            base.OnLateSetupComplete();

            if (Context is null) return;

            // Rebuild dropdown options from context
            RefreshDropdownOptions(Context.DataArray);

            // Select default / current index
            SelectOption(Context.SelectedIndex >= 0 ? Context.SelectedIndex : Context.DefaultIndex);
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            if (Context is null) return;

            // Re-sync options if list changed
            RefreshDropdownOptions(Context.DataArray);

            // Ensure dropdown selection matches context
            if (Context.IsSelected)
            {
                // Handle case when selection changed
                if (DropdownComponent.value != Context.SelectedIndex)
                    DropdownComponent.value = Context.SelectedIndex;
                else if (!Equals(_lastKnownSelectedValue, Context.SelectedItem))
                {
                    // Notify that selection has changed, but index keeps being the same
                    // for god know why user deletes items from list reason
                    OnSelectedIndexChanged(DropdownComponent.value, Context.SelectedIndex);
                }
            }
            else 
            {
                // Fallback to last value in array as we probably deleted last element from list
                TrySelectIndex(Context.DataArray.Count - 1); 
            }
        }

        /// <summary>
        ///     Refresh dropdown options from given data
        /// </summary>
        /// <param name="data">List of data objects</param>
        private void RefreshDropdownOptions([NotNull] IReadOnlyList<TObjectType> data)
        {
            DropdownComponent.options.Clear();
            for (int index = 0; index < data.Count; index++)
            {
                TObjectType obj = data[index];
                DropdownComponent.options.Add(new TMP_Dropdown.OptionData(GetOptionLabel(obj)));
            }

            DropdownComponent.RefreshShownValue();
        }

        /// <summary>
        ///     Converts an object into a dropdown label
        ///     Override to customize how objects are displayed
        /// </summary>
        protected abstract string GetOptionLabel(TObjectType obj);

        protected override void OnValidate()
        {
            base.OnValidate();
            DropdownComponent = GetComponent<TMP_Dropdown>();
        }
    }
}