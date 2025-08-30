using Systems.SimpleUserInterface.Components.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Components.Progress
{
    /// <summary>
    ///     UI image to display progress nicely
    /// </summary>
    [RequireComponent(typeof(Image))] public sealed class UIProgressImage : UIObjectBase
    {
        /// <summary>
        ///     Reference to the image component
        /// </summary>
        [field: SerializeField, HideInInspector] private Image imageReference;

   /// <summary>
        ///     Sets the progress of the image
        /// </summary>
        internal void SetProgress(float progress)
        {
            if (ReferenceEquals(imageReference, null)) imageReference = GetComponent<Image>();
            imageReference.fillAmount = progress;
        }


        protected override void OnValidate()
        {
            imageReference = GetComponent<Image>();
            if (!imageReference) return;
            imageReference.type = Image.Type.Filled;
        }
    }
}