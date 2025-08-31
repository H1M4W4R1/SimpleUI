using System.Collections.Generic;
using JetBrains.Annotations;
using Systems.SimpleUserInterface.Components.Selectors.Tabs;
using Systems.SimpleUserInterface.Context.Wrappers;

namespace Systems.SimpleUserInterface.Context.Tabs
{
    /// <summary>
    ///     Basic tab info selector context
    /// </summary>
    public sealed class TabInfoSelectableContext : SelectableContext<UITab>
    {
        public TabInfoSelectableContext([NotNull] IReadOnlyList<UITab> data, int defaultIndex = 0) : 
            base(data, defaultIndex)
        {
        }
    }
}