using Systems.SimpleUserInterface.Components.Objects;
using Systems.SimpleUserInterface.Components.Objects.Markers;
using Systems.SimpleUserInterface.Components.Objects.Markers.Context;
using UnityEngine;
using UnityEngine.Assertions;

namespace Systems.SimpleUserInterface.Components.Progress
{
    /// <summary>
    ///     Object that represents a progress "bar"
    /// </summary>
    public abstract class UIProgressBase : UIObjectWithContextBase<float>, IRenderable<float>
    {
        private float _drawnValue = -1f;
        
        /// <summary>
        ///     Collection of progress images
        /// </summary>
        private UIProgressImage[] progressImages;
        
        protected override void AssignComponents()
        {
            base.AssignComponents();
            progressImages = GetComponentsInChildren<UIProgressImage>();
            Assert.IsTrue(progressImages.Length > 0, "No progress images found");
        }

        public override void ValidateContext()
        {
            base.ValidateContext();
            
            // Check if progress has changed
            if (Mathf.Approximately(Context, _drawnValue)) return;
            SetDirty();
        }

        public void OnRender(float newProgress)
        {
            // Clamp progress to [0, 1]
            newProgress = Mathf.Clamp01(newProgress);
            
            // Update all progress images
            for (int index = 0; index < progressImages.Length; index++)
            {
                UIProgressImage progressImage = progressImages[index];
                progressImage.SetProgress(newProgress);
            }
            
            _drawnValue = newProgress;
        }
    }
}