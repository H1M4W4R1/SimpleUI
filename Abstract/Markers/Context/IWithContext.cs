using JetBrains.Annotations;
using Systems.SimpleUserInterface.Abstract.Context;
using UnityEngine;
using UnityEngine.Assertions;

namespace Systems.SimpleUserInterface.Abstract.Markers.Context
{
    /// <summary>
    ///     Represents an object that has a context
    /// </summary>
    /// <typeparam name="TContextType">Context type</typeparam>
    public interface IWithContext<TContextType> : IWithContext
    {
        /// <summary>
        ///     Cached context provider
        /// </summary>
        [CanBeNull] protected ContextProviderBase<TContextType> CachedContextProvider { get; set; }

        /// <summary>
        ///     Provides the context of the object
        /// </summary>
        /// <returns>The context of the object or null if context is not available</returns>
        [CanBeNull] protected internal TContextType ProvideContext()
        {
            // Provide fallback to local context if called in weird way
            if (this is IWithLocalContext<TContextType> withLocalContext) return withLocalContext.ProvideContext();

            // Acquire unity object and validate if correct
            Component thisComponent = this as Component;
            Assert.IsNotNull(thisComponent, "Object is not a unity component");

            // Provide cached context if available
            if (CachedContextProvider) return CachedContextProvider.ProvideContext();
            
            // Get context provider and cache it to avoid multiple GetComponentInParent calls
            // Detach event when object is destroyed
            if (!ReferenceEquals(CachedContextProvider, null))
            {
                CachedContextProvider.OnContextChanged -= OnContextChanged;
                CachedContextProvider = null;
            }

            // Get context provider and attach event
            Assert.IsTrue(ReferenceEquals(CachedContextProvider, null),
                "Object was destroyed, but event was not cleared for some reason.");
            CachedContextProvider = thisComponent.GetComponentInParent<ContextProviderBase<TContextType>>();

            // Validate context provider and attach event
            Assert.IsNotNull(CachedContextProvider, "Context provider was not found.");
            CachedContextProvider.OnContextChanged += OnContextChanged;

            // Provide context if any provider is available
            return CachedContextProvider.ProvideContext();
        }

        /// <summary>
        ///     Called when the context changes - marks the object as dirty
        /// </summary>
        private void OnContextChanged()
        {
            SetDirty();
        }
    }

    /// <summary>
    ///     Marker interface for objects that have a context
    ///     Do not use directly, see <see cref="IWithContext{TContextType}"/>
    /// </summary>
    public interface IWithContext
    {
        /// <summary>
        ///     Changes the dirty status of the object
        /// </summary>
        public bool SetDirty(bool isNowDirty = true) => IsDirty = isNowDirty;

        /// <summary>
        ///     Indicates if the context is dirty, should be triggered
        ///     each time context changed (recommended to use events if provided)
        ///     Used to trigger <see cref="IRefreshable.TryRefresh"/> event.
        /// </summary>
        protected internal bool IsDirty { get; set; }

        /// <summary>
        ///     Acquires the context of the object
        /// </summary>
        /// <typeparam name="TContextType">Context type</typeparam>
        /// <returns>The context of the object or default if context is not available / supported</returns>
        [CanBeNull] protected internal TContextType ProvideContext<TContextType>()
        {
            if (this is not IWithContext<TContextType> context) return default;
            return context.ProvideContext();
        }
    }
}