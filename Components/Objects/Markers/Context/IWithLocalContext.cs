using JetBrains.Annotations;

namespace Systems.SimpleUserInterface.Components.Objects.Markers.Context
{
    /// <summary>
    ///     Represents an object that has a local context
    /// </summary>
    /// <typeparam name="TContextType">Context type</typeparam>
    public interface IWithLocalContext<TContextType> : IWithContext<TContextType>
    {
        /// <summary>
        ///     Gets the context of the object
        /// </summary>
        /// <returns>Context of the object or null if no context is set</returns>
        public bool TryGetContext([CanBeNull] out TContextType context);
        
    }
}