using Systems.SimpleUserInterface.Context.Abstract;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples.Objects.Text.Providers
{
    /// <summary>
    ///     Provides a simple string to be displayed
    /// </summary>
    public sealed class SimpleStringProvider : ContextProviderBase<string>
    {
        [SerializeField] private string stringToProvide;
        
        protected internal override string ProvideContext() => stringToProvide;
    }
}