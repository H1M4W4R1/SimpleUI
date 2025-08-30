using Systems.SimpleUserInterface.Components.Selectors.Implementations;
using Systems.SimpleUserInterface.Context.Tabs;

namespace Systems.SimpleUserInterface.Components.Selectors.Tabs
{
    /// <summary>
    ///     Tab selector for UI
    /// </summary>
    public abstract class UITabSelectorBase : UIToggleGroupSelectorBase<TabInfo>
    {
        /// <summary>
        ///     Currently selected tab
        /// </summary>
        protected int SelectedTab => Context?.SelectedIndex ?? -1;
        
        /// <summary>
        ///     Use this method to handle tab selection - play animations etc.
        /// </summary>
        protected virtual void OnTabSelected(int from, int to)
        {
            if (Context is null) return;
            
            if(Context.IsValidIndex(from))
            {
                TabInfo info = Context[from];
                info.Tab.OnTabDeselected();
            }

            if (Context.IsValidIndex(to))
            {
                TabInfo info = Context[to];
                info.Tab.OnTabSelected();
            }
        }
        
        /// <summary>
        ///     Handles the selection change event
        /// </summary>
        protected override void OnSelectedIndexChanged(int from, int to)
        {
            base.OnSelectedIndexChanged(from, to);
            OnTabSelected(from, to);
            SelectToggle(to);
        }
    }
}