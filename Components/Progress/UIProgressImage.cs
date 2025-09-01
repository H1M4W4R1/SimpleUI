using Systems.SimpleUserInterface.Components.Abstract;
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
        [field: SerializeField, HideInInspector] private Image ImageReference { get; set; }

   /// <summary>
        ///     Sets the progress of the image
        /// </summary>
        internal void SetProgress(float progress)
        {
            if (ReferenceEquals(ImageReference, null)) ImageReference = GetComponent<Image>();
            ImageReference.fillAmount = progress;
        }


        protected override void OnValidate()
        {
            ImageReference = GetComponent<Image>();
            if (!ImageReference) return;
            ImageReference.type = Image.Type.Filled;
        }
    }
}