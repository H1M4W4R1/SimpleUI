using JetBrains.Annotations;
using Systems.SimpleUserInterface.Components.Selectors.Tabs;

namespace Systems.SimpleUserInterface.Context.Tabs
{
    /// <summary>
    ///     Information about specific tab, used to store things such as tab name, icon, etc.
    /// </summary>
    public abstract class TabInfo
    {
        protected TabInfo([NotNull] UITab tab)
        {
            Tab = tab;
        }

        public UITab Tab { get; private set; } }
}