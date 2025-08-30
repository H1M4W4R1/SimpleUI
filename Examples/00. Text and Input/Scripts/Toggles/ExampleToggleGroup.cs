using Systems.SimpleUserInterface.Components.Interactable.Toggles;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples._00._Text_and_Input.Scripts.Toggles
{
    public sealed class ExampleToggleGroup : UIToggleGroupBase
    {
        private int _selectedToggleIndex = 0;
        
        protected override void OnToggleValueChanged(int toggleIndex, bool newValue)
        {
            if (!newValue) return;
            _selectedToggleIndex = toggleIndex;
            Debug.Log("Selected toggle index: " + FirstToggleIndex);
        }
    }
}