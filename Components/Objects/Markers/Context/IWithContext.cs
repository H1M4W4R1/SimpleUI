using JetBrains.Annotations;
using Systems.SimpleUserInterface.Context.Abstract;
using UnityEngine;
using UnityEngine.Assertions;

namespace Systems.SimpleUserInterface.Components.Objects.Markers.Context
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
        [CanBeNull] protected IContextProvider CachedContextProvider { get; set; }

        /// <summary>
        ///     Provides the context of the object
        /// </summary>
        /// <returns>The context of the object or null if context is not available</returns>
        protected internal bool TryProvideContext([CanBeNull] out TContextType context)
        {
            // Provide fallback to local context if called in weird way
            if (this is IWithLocalContext<TContextType> withLocalContext)
                return withLocalContext.TryGetContext(out context);

            // Acquire unity object and validate if correct
            Component thisComponent = this as Component;
            Assert.IsNotNull(thisComponent, "Object is not a unity component");

            // Provide cached context if available
            if (CachedContextProvider != null && CachedContextProvider.CanProvideContext<TContextType>())
                return CachedContextProvider.TryProvideContext(out context);

            // Get context provider and cache it to avoid multiple GetComponentInParent calls
            TryClearContextProvider();

            // Get context provider and attach event
            Assert.IsTrue(ReferenceEquals(CachedContextProvider, null),
                "Object was destroyed, but event was not cleared for some reason.");
            
            // Get all context providers and acquire first that can provide context
            IContextProvider[] contextProviders = thisComponent.GetComponentsInParent<IContextProvider>();
            for (int providerIndex = 0; providerIndex < contextProviders.Length; providerIndex++)
            {
                if (!contextProviders[providerIndex].CanProvideContext<TContextType>()) continue;
                CachedContextProvider = contextProviders[providerIndex];
                break;
            }
            
            // Validate context provider and attach event
            Assert.IsNotNull(CachedContextProvider, "Context provider was not found.");

            // Provide context if any provider is available
            return CachedContextProvider.TryProvideContext(out context);
        }

        /// <summary>
        ///     Clears the context provider
        /// </summary>
        void TryClearContextProvider()
        {
            // If object is null skip
            if (ReferenceEquals(CachedContextProvider, null)) return;

            // Detach event and clear provider
            CachedContextProvider = null;
        }

        /// <summary>
        ///     Called when the context changes - marks the object as dirty
        /// </summary>
        internal void OnContextChanged()
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
        /// </summary>
        protected internal bool IsDirty { get; set; }

        /// <summary>
        ///     Acquires the context of the object
        /// </summary>
        /// <typeparam name="TContextType">Context type</typeparam>
        /// <returns>The context of the object or default if context is not available / supported</returns>
        [CanBeNull] public TContextType ProvideContext<TContextType>()
        {
            if (this is not IWithContext<TContextType> context) return default;
            return context.ProvideContext<TContextType>();
        }

        /// <summary>
        ///     Used to update the dirty status of the object if context has changed
        ///     does nothing if returns false.
        /// </summary>
        public void ValidateContext();
    }
}