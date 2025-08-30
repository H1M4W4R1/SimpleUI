using DG.Tweening;
using Systems.SimpleUserInterface.Components.Animations.Abstract;
using UnityEngine;

namespace Systems.SimpleUserInterface.Components.Animations
{
    /// <summary>
    ///     Basic animation that scales the object to show it and scales it back to hide it
    /// </summary>
    public sealed class ScaleShowHideAnimation : UIShowHideAnimationBase
    {
        /// <summary>
        ///     The duration of the transition in seconds
        /// </summary>
        [field: SerializeField] public float TransitionDuration { get; private set; } = 0.25f;

        private void OnEnable()
        {
            // Ensure the object is scaled down before showing
            transform.localScale = Vector3.zero;
        }

        protected internal override Sequence AnimateObjectHide()
        {
            return base.AnimateObjectHide().Append(transform.DOScale(Vector3.zero, TransitionDuration));
        }

        protected internal override Sequence AnimateObjectShow()
        {
            return base.AnimateObjectShow()
                .Append(transform.DOScale(Vector3.one, TransitionDuration));
        }
    }
}