using System.Collections.Generic;
using Systems.SimpleUserInterface.Components.Selectors.Tabs;
using Systems.SimpleUserInterface.Context.Abstract;
using Systems.SimpleUserInterface.Context.Tabs;

namespace Systems.SimpleUserInterface.Examples._05._Tabs.Scripts.Tabs.Context
{
    public sealed class ExampleTabContextProvider : ContextProviderBase<TabInfoSelectableContext>
    {
        private readonly List<UITab> _tabs = new();
        private TabInfoSelectableContext _context;

        private void Awake()
        {
            ExampleTab[] tabs = GetComponentsInChildren<ExampleTab>(true);
            for (int tabIndex = 0; tabIndex < tabs.Length; tabIndex++)
            {
                UITab tab = tabs[tabIndex];
                _tabs.Add(tab);
            }
            
            _context = new TabInfoSelectableContext(_tabs, 1);
        }

        public override TabInfoSelectableContext GetContext() => _context;
    }
}