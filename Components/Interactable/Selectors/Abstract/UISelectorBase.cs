using JetBrains.Annotations;
using Systems.SimpleUserInterface.Components.Lists;
using Systems.SimpleUserInterface.Context.Wrappers;

namespace Systems.SimpleUserInterface.Components.Interactable.Selectors.Abstract
{
    /// <summary>
    ///     Selector for UI, used to select single item from a list
    /// </summary>
    /// <typeparam name="TObjectType">Object type in the list</typeparam>
    public abstract class UISelectorBase<TObjectType> : UIListBase<SelectableContext<TObjectType>, TObjectType>
    {
        /// <summary>
        ///     Gets the selected item
        /// </summary>
        /// <returns>The selected item or null if no item is selected</returns>
        [CanBeNull] public TObjectType SelectedItem => Context != null ? Context.SelectedItem : default;

        /// <summary>
        ///     Checks if an item is selected
        /// </summary>
        public bool IsSelected => Context is {IsSelected: true};

        /// <summary>
        ///     Select given object if it is in the list
        /// </summary>
        /// <param name="item">Object to select</param>
        /// <returns>True if the object was selected, false otherwise</returns>
        public bool TrySelectObject([CanBeNull] TObjectType item)
        {
            if (Context is null) return false;

            // Get old index and try to select new object
            int oldIndex = Context.SelectedIndex;
            if (!Context.TrySelectObject(item)) return false;

            // Ensure that index has changed
            if (oldIndex == Context.SelectedIndex) return false;

            // Raise event
            OnSelectedIndexChanged(oldIndex, Context.SelectedIndex);
            return true;
        }

        /// <summary>
        ///     Changes the selected index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool TrySelectIndex(int index)
        {
            if (Context is null) return false;

            // Get old index and try to select new index
            int oldIndex = Context.SelectedIndex;
            if (!Context.TrySelectIndex(index)) return false;

            // Ensure index has changed
            if (oldIndex == Context.SelectedIndex) return false;

            OnSelectedIndexChanged(oldIndex, Context.SelectedIndex);
            return true;
        }

        /// <summary>
        ///     Event called when selection changes
        /// </summary>
        protected virtual void OnSelectedIndexChanged(int from, int to)
        {
            // Request to refresh element if index has changed
            // to redraw the renderable
            RequestRefresh();
        }

        protected override void OnRefresh()
        {
            // Ensure base implementation is called
            base.OnRefresh();

            // Update selected element
            TrySelectIndex(Context?.SelectedIndex ?? -1);
        }
    }
}