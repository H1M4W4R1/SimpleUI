using JetBrains.Annotations;
using Systems.SimpleUserInterface.Components.Selectors.Tabs;

namespace Systems.SimpleUserInterface.Context.Tabs
{
    /// <summary>
    ///     Tab without information for quick implementations
    /// </summary>
    public sealed class EmptyTabInfo : TabInfo
    {
        public EmptyTabInfo([NotNull] UITab tab) : base(tab)
        {
        }
    }
}