using JetBrains.Annotations;
using UnityEngine;

namespace Systems.SimpleUserInterface.Abstract.Context
{
    /// <summary>
    ///     Provides a context to this or other objects
    /// </summary>
    /// <typeparam name="TContextType">Context type to provide</typeparam>
    public abstract class ContextProviderBase<TContextType> : MonoBehaviour
    {
        /// <summary>
        ///     Provides the context to objects
        /// </summary>
        /// <returns>Provided context</returns>
        [CanBeNull] protected internal abstract TContextType ProvideContext();
    }
}