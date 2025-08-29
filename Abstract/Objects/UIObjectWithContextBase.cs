using Systems.SimpleUserInterface.Abstract.Context;
using Systems.SimpleUserInterface.Abstract.Markers.Context;

namespace Systems.SimpleUserInterface.Abstract.Objects
{
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