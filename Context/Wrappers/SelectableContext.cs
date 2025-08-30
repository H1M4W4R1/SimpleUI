using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Systems.SimpleUserInterface.Context.Wrappers
{
    /// <summary>
    ///     Selector context for UI
    /// </summary>
    public abstract class SelectableContext<TListObject> : ListContext<TListObject>
    {
        /// <summary>
        ///     Index of selected item
        /// </summary>
        public int SelectedIndex { get; private set; }

        /// <summary>
        ///     Checks if an item is selected
        /// </summary>
        public bool IsSelected => IsValidIndex(SelectedIndex);

        /// <summary>
        ///     Checks if there is a next item
        /// </summary>
        public bool HasNext => IsValidIndex(SelectedIndex + 1);
        
        /// <summary>
        ///     Checks if there is a previous item
        /// </summary>
        public bool HasPrevious => IsValidIndex(SelectedIndex - 1);
        
        /// <summary>
        ///     Gets the selected item
        /// </summary>
        /// <returns>The selected item, or null/default if the index is out of range</returns>
        [CanBeNull] public TListObject SelectedItem =>
            IsValidIndex(SelectedIndex) ? DataArray[SelectedIndex] : default;

        /// <summary>
        ///     Selects an item
        /// </summary>
        /// <param name="index">Index of item to select</param>
        public void SelectIndex(int index)
        {
            Assert.IsTrue(TrySelectIndex(index), "Index out of range");
        }

        /// <summary>
        ///     Tries to select an item
        /// </summary>
        /// <param name="index">Item to select</param>
        /// <returns>True if the item was selected, false otherwise</returns>
        public bool TrySelectIndex(int index)
        {
            int oldIndex = SelectedIndex;
            if (!IsValidIndex(index)) return false;
            SelectedIndex = index;
            OnSelectionChanged(oldIndex, index);
            return true;
        }

        public bool TrySelectObject([CanBeNull] TListObject item)
        {
            // Find first index of item
            int itemIndex = -1;
            for (int iDataIndex = 0; iDataIndex < DataArray.Count; iDataIndex++)
            {
                TListObject dataObj = DataArray[iDataIndex];
                if (!Equals(dataObj, item)) continue;
                itemIndex = iDataIndex;
                break;
            }
            
            // Change selection
            if (itemIndex == -1) return false;
            
            int oldIndex = SelectedIndex;
            SelectedIndex = itemIndex;
            
            OnSelectionChanged(oldIndex, itemIndex);
            return true;
        }

        /// <summary>
        ///     Event called when selection changes
        /// </summary>
        public virtual void OnSelectionChanged(int oldIndex, int newIndex)
        {
            
        }

        public SelectableContext([NotNull] IReadOnlyList<TListObject> data, int selectedIndex = -1) : base(data)
        {
            SelectedIndex = selectedIndex;
        }
    }
}