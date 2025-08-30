using System.Collections.Generic;
using JetBrains.Annotations;
using Systems.SimpleUserInterface.Context.Wrappers;

namespace Systems.SimpleUserInterface.Context.Tabs
{
    /// <summary>
    ///     Basic tab info selector context
    /// </summary>
    public sealed class TabInfoSelectableContext : SelectableContext<TabInfo>
    {
        public TabInfoSelectableContext([NotNull] IReadOnlyList<TabInfo> data) : 
            base(data, 0)
        {
        }
    }
}