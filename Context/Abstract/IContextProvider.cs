using JetBrains.Annotations;

namespace Systems.SimpleUserInterface.Context.Abstract
{
    /// <summary>
    ///     Interface for context providers
    /// </summary>
    public interface IContextProvider
    {
        public bool TryProvideContext<TContextType>([CanBeNull] out TContextType context);
        
        public bool CanProvideContext<TContextType>();
    }
}