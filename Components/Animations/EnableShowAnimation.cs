using DG.Tweening;
using Systems.SimpleUserInterface.Components.Animations.Abstract;

namespace Systems.SimpleUserInterface.Components.Animations
{
    public sealed class EnableShowAnimation : UIAnimationBase, IUIShowAnimation
    {
        public Sequence OnShow()
        {
            // Same as regular implementation, kept as fail-safe if it would get removed.
            return DOTween.Sequence().SetUpdate(true).OnComplete(Activate);
        }
    }
}