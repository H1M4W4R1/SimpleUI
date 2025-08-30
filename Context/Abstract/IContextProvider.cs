using JetBrains.Annotations;

namespace Systems.SimpleUserInterface.Context.Abstract
{
    /// <summary>
    ///     Represents an object that provides a context to other objects
    /// </summary>
    public interface IContextProvider<out TContextType> : IContextProvider
    {
        /// <summary>
        ///     Provides the context to objects
        /// </summary>
        /// <returns>The context of the object or null if context is not available</returns>
        [CanBeNull] TContextType ProvideContext();

        /// <summary>
        ///     Internal handler for providing context to other objects
        /// </summary>
        TContextTypeOther IContextProvider.ProvideContext<TContextTypeOther>()
        {
            // Check if context can be provided
            if (!CanProvideContext<TContextTypeOther>()) return default;

            // Provide core context
            TContextType context = ProvideContext();
            return context is TContextTypeOther other ? other : default;
        }

        bool IContextProvider.CanProvideContext<TContextTypeOther>()
        {
            // Same types
            if (typeof(TContextTypeOther) == typeof(TContextType)) return true;

            // TContextTypeOther is a base type of TContextType
            if (typeof(TContextTypeOther).IsAssignableFrom(typeof(TContextType))) return true;

            return false;
        }
    }

    /// <summary>
    ///     Internal interface for context providers
    ///     Do not use this interface directly <see cref="IContextProvider{TContextType}"/>
    /// </summary>
    public interface IContextProvider
    {
        public delegate void ContextChangedHandler();

        public event ContextChangedHandler OnContextChanged;

        [CanBeNull] TContextType ProvideContext<TContextType>();

        public bool CanProvideContext<TContextType>();
    }
}