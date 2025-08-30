using Systems.SimpleUserInterface.Components.Interactable.Toggles;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples._00._Text_and_Input.Scripts.Toggles
{
    public sealed class ExampleToggle : UIToggleBase
    {
        protected override void OnToggleValueChanged(bool newValue)
        {
            Debug.Log("Toggle value: " + newValue);
        }
    }
}