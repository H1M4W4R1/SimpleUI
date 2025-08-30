using Systems.SimpleUserInterface.Components.Objects;
using UnityEngine;
using UnityEngine.Assertions;
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
        private Image imageReference;

        protected override void AssignComponents()
        {
            base.AssignComponents();
            imageReference = GetComponent<Image>();
            Assert.IsTrue(imageReference.type == Image.Type.Filled,
                "Image type must be filled for progress image to work correctly");
        }

        /// <summary>
        ///     Sets the progress of the image
        /// </summary>
        internal void SetProgress(float progress)
        {
            if (ReferenceEquals(imageReference, null)) imageReference = GetComponent<Image>();
            imageReference.fillAmount = progress;
        }
    }
}