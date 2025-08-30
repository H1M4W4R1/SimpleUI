using JetBrains.Annotations;
using Systems.SimpleUserInterface.Components.Lists;
using Systems.SimpleUserInterface.Context.Wrappers;

namespace Systems.SimpleUserInterface.Components.Interactable.Selectors
{
    /// <summary>
    ///     Selector for UI, used to select single item from a list
    /// </summary>
    /// <typeparam name="TListObject">Object type in the list</typeparam>
    public abstract class UISelectorBase<TListObject> : UIListBase<SelectableContext<TListObject>, TListObject>
    {
        /// <summary>
        ///     Tries to select an item
        /// </summary>
        /// <param name="index">Index of item to select</param>
        /// <returns>True if the item was selected, false otherwise</returns>
        public bool TrySelect(int index) => Context?.TrySelect(index) ?? false;
        
        /// <summary>
        ///     Gets the selected item
        /// </summary>
        /// <returns>The selected item or null if no item is selected</returns>
        [CanBeNull] public TListObject SelectedItem => Context != null ? Context.SelectedItem : default;
        
        /// <summary>
        ///     Checks if an item is selected
        /// </summary>
        public bool IsSelected => Context is {IsSelected: true};

        protected override void OnRefresh()
        {
            // Ensure base implementation is called
            base.OnRefresh();
            
            // Update selected element
            TrySelect(Context?.SelectedIndex ?? -1);
        }
    }
}