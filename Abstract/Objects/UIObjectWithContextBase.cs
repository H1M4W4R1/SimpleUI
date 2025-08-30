using Systems.SimpleUserInterface.Abstract.Context;
using Systems.SimpleUserInterface.Abstract.Markers.Context;

namespace Systems.SimpleUserInterface.Abstract.Objects
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
        ///     The dirty status of the object
        /// </summary>
        bool IWithContext.IsDirty { get; set; }
        
        /// <summary>
        ///     Cached context provider
        /// </summary>
        ContextProviderBase<TContextType> IWithContext<TContextType>.CachedContextProvider { get; set; }
    }
}