using Systems.SimpleUserInterface.Components.Interactable.Buttons;

namespace Systems.SimpleUserInterface.Examples._02._Windows.Scripts.Button
{
    /// <summary>
    ///     Button used to close current window
    /// </summary>
    public sealed class CloseCurrentWindowButton : UIButtonBase
    {
        protected override void OnClick()
        {
            // Close current window if any
            if (!WindowHandle) return;
            WindowHandle.Close();
        }
    }
}