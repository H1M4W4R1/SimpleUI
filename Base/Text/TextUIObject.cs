using Systems.SimpleUserInterface.Abstract.Markers;
using Systems.SimpleUserInterface.Abstract.Objects;
using TMPro;
using UnityEngine;

namespace Systems.SimpleUserInterface.Base.Text
{
    /// <summary>
    ///     Common class for text UI objects
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class TextUIObject : UIObjectWithContextBase<string>, IRenderable<string>
    {
        /// <summary>
        ///     Reference to the text component
        /// </summary>
        private TextMeshProUGUI _textReference;

        /// <summary>
        ///     Assigns text component
        /// </summary>
        protected override void AssignComponents()
        {
            base.AssignComponents();
            _textReference = GetComponent<TextMeshProUGUI>();
        }

        /// <summary>
        ///     Renders the text
        /// </summary>
        public void OnRender(string withContext)
        {
            _textReference.SetText(withContext);
        }
    }
}