using Systems.SimpleUserInterface.Components.Scrolling;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples._00._Text_and_Input.Scripts.Scroll
{
    public sealed class ExampleScrollbar : UIScrollbar
    {
        protected override void OnScrollbarValueChanged(float value)
        {
            Debug.Log("Scrollbar value: " + value);
        }
    }
}