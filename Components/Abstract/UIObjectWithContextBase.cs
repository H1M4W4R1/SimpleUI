using JetBrains.Annotations;
using Systems.SimpleUserInterface.Components.Abstract.Markers.Context;
using Systems.SimpleUserInterface.Context.Abstract;

namespace Systems.SimpleUserInterface.Components.Abstract
{
    /// <summary>
    ///     Object that has a context
    /// </summary>
    /// <typeparam name="TContextType">Type of the context</typeparam>
    /// <remarks>
    ///     Do not use this for checking if object has context. Use <see cref="IWithContext"/>
    ///     interface instead as many object will implement this interface directly rather than
    ///     going through this utility class.
    /// </remarks>
    public abstract class UIObjectWithContextBase<TContextType> :
        UIObjectBase, IWithContext<TContextType>
    {
        /// <summary>
        ///     Gets the context of the object
        /// </summary>
        /// <returns>Context of the object or null if no context is set</returns>
        [CanBeNull] public TContextType Context
        {
            get
            {
                if (!((IWithContext<TContextType>) this).TryProvideContext(out TContextType context))
                    return default;
                
                return context;
            }
        }

        /// <summary>
        ///     The dirty status of the object
        /// </summary>
        bool IWithContext.IsDirty { get; set; }

        /// <summary>
        ///     Cached context provider
        /// </summary>
        IContextProvider IWithContext<TContextType>.CachedContextProvider { get; set; }

        /// <summary>
        ///     Changes the dirty status of the object
        /// </summary>
        public bool SetDirty(bool newStatus = true) => ((IWithContext<TContextType>) this).IsDirty = newStatus;

        /// <summary>
        ///     Requests redraw of the object, executed only if object supports IRefreshable
        ///     or IRenderable
        /// </summary>
        public void RequestRefresh()
        {
            // Ensure of correct type implementation
            IWithContext<TContextType> withContext = this;
            withContext.SetDirty();
        }

        public virtual void ValidateContext()
        {
            // Do nothing by default
        }
    }
}