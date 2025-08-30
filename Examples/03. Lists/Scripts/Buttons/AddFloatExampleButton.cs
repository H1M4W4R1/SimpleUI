using Systems.SimpleUserInterface.Components.Interactable.Buttons;
using Systems.SimpleUserInterface.Examples._04._Lists.Scripts.Lists.Context;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples._03._Lists.Scripts.Buttons
{
    public sealed class AddFloatExampleButton : UIButtonBase
    {
        [SerializeField] private FloatArrayContextProvider _floatArrayContextProvider;
        
        protected override void OnClick()
        {
            _floatArrayContextProvider.AddFloat();
            
        }
    }
}