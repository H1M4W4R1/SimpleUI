using Systems.SimpleUserInterface.Components.Selectors.Abstract;
using UnityEngine;

namespace Systems.SimpleUserInterface.Components.Selectors.Implementations
{
    /// <summary>
    ///     Selector that can select next/previous item
    /// </summary>
    /// <typeparam name="TObjectType">Object type in the list</typeparam>
    public abstract class UIPreviousNextSelectorBase<TObjectType> : UISelectorBase<TObjectType>
    {
        /// <summary>
        ///     Previous/Next selectors will be able to loop
        /// </summary>
        [field: SerializeField] public bool IsLooping { get; private set; }
        
        /// <summary>
        ///     True if there is a next item
        /// </summary>
        public bool HasNext => Context?.HasNext ?? false;
        
        /// <summary>
        ///     True if there is a previous item
        /// </summary>
        public bool HasPrevious => Context?.HasPrevious ?? false;
   
        /// <summary>
        ///     Selects the next item
        /// </summary>
        /// <returns>True if the item was selected, false otherwise</returns>
        public virtual bool TrySelectNext()
        {
            if (Context is null) return false;

            int oldIndex = Context.SelectedIndex;
            Context.TrySelectNext(IsLooping);

            // Ensure index has changed
            if (oldIndex == Context.SelectedIndex) return false;

            // Raise event
            OnSelectedIndexChanged(oldIndex, Context.SelectedIndex);
            return true;
        }

        /// <summary>
        ///     Selects the previous item
        /// </summary>
        /// <returns>True if the item was selected, false otherwise</returns>
        public virtual bool TrySelectPrevious()
        {
            if (Context is null) return false;
            
            int oldIndex = Context.SelectedIndex;
            Context.TrySelectPrevious(IsLooping);
            
            // Ensure index has changed
            if (oldIndex == Context.SelectedIndex) return false;

            // Raise event
            OnSelectedIndexChanged(oldIndex, Context.SelectedIndex);
            return true;
        }
    }
}