using Systems.SimpleUserInterface.Components.Interactable.DataInput;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples._00._Text_and_Input.Scripts.Input
{
    public sealed class ExampleInputField : UIInputFieldBase
    {
        protected override void OnFieldSubmitted(string withText)
        {
            base.OnFieldSubmitted(withText);
            Debug.Log($"Submitted text: {withText}");
        }

        protected override void OnFieldEndEdited(string currentText)
        {
            base.OnFieldEndEdited(currentText);
            Debug.Log($"Current field text: {currentText}");
        }
    }
}