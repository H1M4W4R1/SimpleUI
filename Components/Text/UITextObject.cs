using Systems.SimpleUserInterface.Abstract.Markers;
using Systems.SimpleUserInterface.Abstract.Objects;
using TMPro;
using UnityEngine;

namespace Systems.SimpleUserInterface.Components.Text
{
    /// <summary>
    ///     Common class for text UI objects
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class UITextObject : UIObjectWithContextBase<string>, IRenderable<string>
    {
        protected TextMeshProUGUI textReference;

        protected override void AssignComponents()
        {
            base.AssignComponents();
            textReference = GetComponent<TextMeshProUGUI>();
        }

        public virtual void OnRender(string withContext)
        {
            textReference.SetText(withContext);
        }
    }
}