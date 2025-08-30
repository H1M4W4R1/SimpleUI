﻿using UnityEngine;

namespace Systems.SimpleUserInterface.Context.Abstract
{
    /// <summary>
    ///     Provides a context to this or other objects
    /// </summary>
    /// <typeparam name="TContextType">Context type to provide</typeparam>
    public abstract class ContextProviderBase<TContextType> : MonoBehaviour, IContextProvider
    {
        /// <summary>
        ///     Tries to provide the context to the object
        /// </summary>
        /// <param name="context">Context to provide</param>
        /// <typeparam name="TDesiredContextType">Desired context type</typeparam>
        /// <returns>True if the context was provided</returns>
        public bool TryProvideContext<TDesiredContextType>(out TDesiredContextType context)
        {
            // Provide core context
            TContextType foundContext = GetContext();

            // Check if context is of desired type
            if (foundContext is TDesiredContextType desiredContext)
            {
                context = desiredContext;
                return true;
            }

            // Context is not of desired type
            context = default;
            return false;
        }

        /// <summary>
        ///     Checks if the context can be provided
        /// </summary>
        /// <typeparam name="TDesiredContextType">Type of the context to check</typeparam>
        /// <returns>True if the context can be provided</returns>
        public bool CanProvideContext<TDesiredContextType>()
            => typeof(TDesiredContextType).IsAssignableFrom(typeof(TContextType));

        /// <summary>
        ///     Provides the context to objects
        /// </summary>
        /// <returns>Provided context</returns>
        public abstract TContextType GetContext();
    }
}