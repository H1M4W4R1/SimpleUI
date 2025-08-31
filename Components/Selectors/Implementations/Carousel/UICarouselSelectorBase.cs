using DG.Tweening;
using Systems.SimpleUserInterface.Components.Selectors.Abstract;
using UnityEngine;

namespace Systems.SimpleUserInterface.Components.Selectors.Implementations.Carousel
{
    /// <summary>
    ///     Carousel selector using Unity ScrollRect for horizontal/vertical snapping
    /// </summary>
    /// <typeparam name="TObjectType">Object type in the list</typeparam>
    [RequireComponent(typeof(UICarouselScrollRect))]
    public abstract class UICarouselSelectorBase<TObjectType> : UIAnimatedSelectorBase<TObjectType>,
        IPreviousNextSelector
    {
        [SerializeField, HideInInspector] private UICarouselScrollRect scrollRect;

        /// <summary>
        ///     Time of transition between items
        /// </summary>
        [field: SerializeField] protected float TransitionDuration = 0.35f;
        
        private bool IsHorizontal => scrollRect.horizontal;

        /// <summary>
        ///     True if there is a next item
        /// </summary>
        public bool HasNext => Context?.HasNext ?? false;

        /// <summary>
        ///     True if there is a previous item
        /// </summary>
        public bool HasPrevious => Context?.HasPrevious ?? false;

        /// <summary>
        ///     Selects the next item
        /// </summary>
        /// <returns>True if the item was selected, false otherwise</returns>
        public virtual bool TrySelectNext()
        {
            if (Context is null) return false;

            int oldIndex = Context.SelectedIndex;
            Context.TrySelectNext();

            // Ensure index has changed
            if (oldIndex == Context.SelectedIndex) return false;

            // Raise event
            OnSelectedIndexChanged(oldIndex, Context.SelectedIndex);
            return true;
        }

        /// <summary>
        ///     Selects the previous item
        /// </summary>
        /// <returns>True if the item was selected, false otherwise</returns>
        public virtual bool TrySelectPrevious()
        {
            if (Context is null) return false;

            int oldIndex = Context.SelectedIndex;
            Context.TrySelectPrevious();

            // Ensure index has changed
            if (oldIndex == Context.SelectedIndex) return false;

            // Raise event
            OnSelectedIndexChanged(oldIndex, Context.SelectedIndex);
            return true;
        }

        protected override void OnLateSetupComplete()
        {
            base.OnLateSetupComplete();
            if (Context is null) return;

            SnapToIndex(Context.SelectedIndex);
        }

        /// <summary>
        ///     Snap instantly to an index (no animation)
        /// </summary>
        protected void SnapToIndex(int index)
        {
            if (Context is null || !Context.IsValidIndex(index)) return;

            float normalized = GetNormalizedPositionForIndex(index);
            SetNormalizedPosition(normalized);
        }

        /// <summary>
        ///     Convert index to normalized position [0..1]
        /// </summary>
        private float GetNormalizedPositionForIndex(int index)
        {
            if (Context is null) return 0f;
            if (Context.Count <= 1) return 0f;
            float step = 1f / (Context.Count - 1);
            return Mathf.Clamp01(step * index);
        }

        /// <summary>
        ///     Direct setter for ScrollRect position
        /// </summary>
        private void SetNormalizedPosition(float value)
        {
            if (IsHorizontal)
                scrollRect.horizontalNormalizedPosition = value;
            else
                scrollRect.verticalNormalizedPosition = 1f - value; // vertical is inverted
        }

        /// <summary>
        ///     Animate selection change via DoTween
        /// </summary>
        protected override Sequence AnimateSelectionChange(int from, int to)
        {
            if (Context is null || !Context.IsValidIndex(to)) return null;

            float targetNormalized = GetNormalizedPositionForIndex(to);

            Sequence seq = DOTween.Sequence().SetUpdate(true);
            if (IsHorizontal)
            {
                seq.Append(scrollRect
                    .DOHorizontalNormalizedPos(targetNormalized, TransitionDuration)
                    .SetEase(Ease.OutCubic));
            }
            else
            {
                seq.Append(scrollRect
                    .DOVerticalNormalizedPos(1f - targetNormalized, TransitionDuration)
                    .SetEase(Ease.OutCubic));
            }

            return seq;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            scrollRect = GetComponent<UICarouselScrollRect>();
        }
    }
}