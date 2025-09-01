using Systems.SimpleUserInterface.Components.Abstract;

namespace Systems.SimpleUserInterface.Components.Selectors.Tabs
{
    /// <summary>
    ///     Example UI tab
    /// </summary>
    public abstract class UITab : UIObjectBase
    {
        protected internal virtual void OnTabSelected()
        {
            
        }
        
        protected internal virtual void OnTabDeselected()
        {
        }
    }
}