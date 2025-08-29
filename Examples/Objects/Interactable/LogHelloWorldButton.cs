using Systems.SimpleUserInterface.Base.Interactable.Buttons;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples.Objects.Interactable
{
    /// <summary>
    ///     Button that logs "Hello World!" to the console
    /// </summary>
    public sealed class LogHelloWorldButton : UIButton
    {
        protected override void OnClick()
        {
            Debug.Log("Hello World!");
        }
    }
}