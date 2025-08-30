using Systems.SimpleUserInterface.Components.Interactable.Buttons;
using Systems.SimpleUserInterface.Components.Windows;
using Systems.SimpleUserInterface.Utility;

namespace Systems.SimpleUserInterface.Examples.Objects.Windows.Button
{
    public sealed class OpenExampleWindowButton : UIButtonBase
    {
        protected override void OnClick()
        {
            // Show Example Window
            UserInterface.OpenWindow<ExampleWindow>();
        }
    }
}