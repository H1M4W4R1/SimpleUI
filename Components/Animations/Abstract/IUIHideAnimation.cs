﻿using DG.Tweening;

namespace Systems.SimpleUserInterface.Components.Animations.Abstract
{
    /// <summary>
    ///     Animation that hides the UI element
    /// </summary>
    public interface IUIHideAnimation : IUIAnimation
    {
        /// <summary>
        ///     Sequence to hide the UI element
        /// </summary>
        public Sequence OnHide();
    }
}