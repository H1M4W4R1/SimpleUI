using DG.Tweening;

namespace Systems.SimpleUserInterface.Components.Animations.Abstract
{
    /// <summary>
    ///     Base class for show/hide animations
    /// </summary>
    public abstract class UIShowHideAnimationBase : UIAnimationBase
    {
        /// <summary>
        ///     Plays the show animation
        /// </summary>
        protected internal virtual Sequence AnimateObjectShow() => DOTween.Sequence();

        /// <summary>
        ///     Plays the hide animation
        /// </summary>
        protected internal virtual Sequence AnimateObjectHide() => DOTween.Sequence();
    }
}