using Systems.SimpleUserInterface.Components.Windows;

namespace Systems.SimpleUserInterface.Examples.Objects.Windows
{
    public sealed class ExampleWindow : UIWindowBase
    {
        public override bool AllowMultipleInstancesWithSameContext => true;
    }
}