using JetBrains.Annotations;
using UnityEngine;

namespace Systems.SimpleUserInterface.Context.Abstract
{
    /// <summary>
    ///     Provides a context to this or other objects
    /// </summary>
    /// <typeparam name="TContextType">Context type to provide</typeparam>
    public abstract class ContextProviderBase<TContextType> : MonoBehaviour, IContextProvider<TContextType>
    {
        /// <summary>
        ///     Event that is called when the context changes
        /// </summary>
        public event IContextProvider<TContextType>.ContextChangedHandler OnContextChanged;

        /// <summary>
        ///     Provides the context to objects
        /// </summary>
        /// <returns>Provided context</returns>
        public abstract TContextType ProvideContext();
        
        /// <summary>
        ///     Notifies all objects that use this context provider that the context has changed
        /// </summary>
        public void NotifyContextChanged() => OnContextChanged?.Invoke();

        
    }
}