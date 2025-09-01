using Systems.SimpleUserInterface.Components.Sliders;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples._00._Text_and_Input.Scripts.Sliders
{
    public sealed class ExampleSlider : UISliderBase
    {
        protected override void OnSliderValueChanged(float newValue)
        {
            Debug.Log("Slider value: " + newValue);
        }
    }
}