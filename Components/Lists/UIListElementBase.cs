using Systems.SimpleUserInterface.Components.Objects;
using Systems.SimpleUserInterface.Components.Objects.Markers;
using Systems.SimpleUserInterface.Components.Objects.Markers.Context;
using Systems.SimpleUserInterface.Context.Wrappers;

namespace Systems.SimpleUserInterface.Components.Lists
{
    /// <summary>
    ///     Element of a list, automatically provides context of the object it is linked to
    /// </summary>
    /// <typeparam name="TListObject">List object type</typeparam>
    public abstract class UIListElementBase<TListObject> : UIObjectWithContextBase<TListObject>, IWithLocalContext<TListObject>,
            IRefreshable
    {
        /// <summary>
        ///     List that this element is linked to
        /// </summary>
        protected internal UIListBase<TListObject> List { get; internal set; }
        
        /// <summary>
        ///     List context that this element is linked to
        /// </summary>
        protected internal ListContext<TListObject> Owner { get; internal set; }
        
        /// <summary>
        ///     Index of this element in the list
        /// </summary>
        protected internal int Index { get; internal set; }

        /// <summary>
        ///     Gets the context of this element
        /// </summary>
        TListObject IWithLocalContext<TListObject>.GetContext() => Owner[Index];

        void IRefreshable.OnRefresh()
        {
            // Notify list that elements have been modified
            List.OnListElementsModified();
        }
        
        void IWithContext.CheckIfContextIsDirty()
        {
            // Check if owner is null
            if (Owner == null) return;

            // Element is different from one in list, refresh this element
            if (Equals(Owner[Index], Context)) return;
            SetDirty();
        }
    }
}