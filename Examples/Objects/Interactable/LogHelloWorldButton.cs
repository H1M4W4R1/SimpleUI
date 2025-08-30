using Systems.SimpleUserInterface.Components.Interactable.Buttons;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples.Objects.Interactable
{
    /// <summary>
    ///     Button that logs "Hello World!" to the console
    /// </summary>
    public sealed class LogHelloWorldButton : UIButtonBase
    {
        protected override void OnClick()
        {
            Debug.Log("Hello World!");
        }
    }
}