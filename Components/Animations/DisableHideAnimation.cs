using DG.Tweening;
using Systems.SimpleUserInterface.Components.Animations.Abstract;

namespace Systems.SimpleUserInterface.Components.Animations
{
    public sealed class DisableHideAnimation : UIAnimationBase, IUIHideAnimation
    {
        public Sequence OnHide()
        {
            return DOTween.Sequence().SetUpdate(true).OnComplete(Deactivate);
        }
    }
}