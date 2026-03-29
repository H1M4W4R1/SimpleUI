using Systems.SimpleUI.Components.Abstract;
using Systems.SimpleUI.Components.Abstract.Markers;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Systems.SimpleUI.Components.Text
{
    /// <summary>
    ///     Common class for text UI objects
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class UITextObject : UIObjectWithContextBase<string>, IRenderable<string>
    {
        [field: SerializeField, HideInInspector] protected TextMeshProUGUI TextReference { get; private set; }

        public virtual void OnRender(string withContext)
        {
            TextReference.SetText(withContext);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            TextReference = GetComponent<TextMeshProUGUI>();
            Assert.IsNotNull(TextReference, "UITextObject requires a TextMeshProUGUI component");
        }
    }
}