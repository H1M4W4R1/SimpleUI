using JetBrains.Annotations;
using Systems.SimpleUserInterface.Abstract.Markers.Context;
using UnityEngine;

namespace Systems.SimpleUserInterface.Abstract.Context
{
    /// <summary>
    ///     Provides a context to this or other objects
    /// </summary>
    /// <typeparam name="TContextType">Context type to provide</typeparam>
    public abstract class ContextProviderBase<TContextType> : MonoBehaviour
    {
        internal delegate void ContextChangedHandler();
        
        /// <summary>
        ///     Event that is called when the context changes
        /// </summary>
        internal event ContextChangedHandler OnContextChanged;
        
        /// <summary>
        ///     Notifies all refreshable objects that use this context provider
        ///     that the context has changed
        /// </summary>
        public void NotifyContextChanged() => OnContextChanged?.Invoke();
        
        /// <summary>
        ///     Provides the context to objects
        /// </summary>
        /// <returns>Provided context</returns>
        [CanBeNull] protected internal abstract TContextType ProvideContext();
    }
}