using Systems.SimpleUserInterface.Components.Interactable.Toggles;

namespace Systems.SimpleUserInterface.Components.Selectors.Implementations.Tabbing
{
    /// <summary>
    ///     Helper toggle group to select single toggle from a list
    ///     and notify about selection change to current selector
    /// </summary>
    public sealed class UISelectorToggleGroup : UIToggleGroupBase
    {
        internal delegate void SelectionChangedHandler(int newIndex);
        
        internal SelectionChangedHandler OnSelectionChanged;
        
        protected override void OnToggleValueChanged(int toggleIndex, bool newValue)
        {
            OnSelectionChanged?.Invoke(toggleIndex);
        }
    }
}