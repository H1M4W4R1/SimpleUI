using DG.Tweening;

namespace Systems.SimpleUserInterface.Components.Animations.Abstract
{
    /// <summary>
    ///     Animation that shows the UI element
    /// </summary>
    public interface IUIShowAnimation : IUIAnimation
    {
        /// <summary>
        ///     Sequence to show the UI element
        /// </summary>
        /// <returns>Animation sequence that will be played</returns>
        public Sequence OnShow();
        
    }
}