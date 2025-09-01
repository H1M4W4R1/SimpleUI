using Systems.SimpleUserInterface.Components.Buttons;
using Systems.SimpleUserInterface.Examples._02._Windows.Scripts.Windows;
using Systems.SimpleUserInterface.Utility;

namespace Systems.SimpleUserInterface.Examples._02._Windows.Scripts.Button
{
    public sealed class OpenExampleStaticWindowButton : UIButtonBase
    {
        protected override void OnClick()
        {
            // Show Example Window
            UserInterface.OpenWindow<ExampleStaticWindow>();
        }
    }
}