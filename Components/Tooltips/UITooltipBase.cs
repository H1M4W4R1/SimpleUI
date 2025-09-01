using JetBrains.Annotations;
using Systems.SimpleUserInterface.Components.Abstract;
using Systems.SimpleUserInterface.Components.Abstract.Markers.Context;
using Systems.SimpleUserInterface.Components.Animations;
using Systems.SimpleUserInterface.Components.Animations.Abstract;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Components.Tooltips
{
    /// <summary>
    ///     Tooltip window drawn on overlay layer
    /// </summary>
    /// <remarks>
    ///     Tooltips should be placed on scene.
    /// </remarks>
    public abstract class UITooltipBase<TObject> : UIObjectWithContextBase<TObject>, IWithLocalContext<TObject>,
        IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
    {
        protected bool _shouldBeVisible;

        /// <summary>
        ///     Cached context of this tooltip
        /// </summary>
        protected internal TObject CachedContext { get; set; }

        /// <summary>
        ///     Checks if tooltip should be visible
        /// </summary>
        protected internal bool ShouldBeVisible
        {
            get => _shouldBeVisible;
            set
            {
                // Skip if value is the same
                if (_shouldBeVisible == value) return;
                _shouldBeVisible = value;

                // Show if should be visible
                if (_shouldBeVisible) Show();
            }
        }

        protected internal void SetPosition(Vector2 mousePosition)
        {
            if (!RectTransformReference) return;
            RectTransformReference.anchoredPosition = mousePosition;
        }

        public void OnPointerEnter([NotNull] PointerEventData eventData)
        {
            ShouldBeVisible = true;
            RequestRefresh();
        }

        public void OnPointerExit([NotNull] PointerEventData eventData)
        {
            ShouldBeVisible = false;
            RequestRefresh();
        }

        public void OnPointerMove([NotNull] PointerEventData eventData)
        {
            SetPosition(eventData.position);
        }

        protected override void OnLateSetupComplete()
        {
            base.OnLateSetupComplete();

            // Ensure object is hidden after loading, design
            // tends to do shitty job at disabling objects
            if (!ShouldBeVisible) Hide();
        }


        protected override void OnValidate()
        {
            base.OnValidate();

            // Add DisableHideAnimation if none is present
            // this is to make tooltips work correctly as intended
            if (GetComponent<IUIHideAnimation>() == null) gameObject.AddComponent<DisableHideAnimation>();

            // Anchor to left bottom corner
            if (RectTransformReference)
            {
                RectTransformReference.anchorMin = Vector2.zero;
                RectTransformReference.anchorMax = Vector2.zero;

                // Check if parent is the same as root canvas (tooltip should be on overlay layer)
                if (!RootCanvasReference) return;
                if (!ReferenceEquals(RectTransformReference.parent,
                        RootCanvasReference.GetComponent<RectTransform>()))
                    Debug.LogError("Tooltip parent is not the same as root canvas. This will cause issues!");
            }

            // Disable ray-casting graphics
            Graphic[] rayGraphic = GetComponentsInChildren<Graphic>();
            int nChanged = 0;

            for (int graphicIndex = 0; graphicIndex < rayGraphic.Length; graphicIndex++)
            {
                Graphic graphic = rayGraphic[graphicIndex];
                if (!graphic.raycastTarget) continue;
                nChanged++;
                graphic.raycastTarget = false;
            }

            if (nChanged > 0)
                Debug.LogWarning($"Changed {nChanged} ray-casting graphics on tooltip {name} to prevent issues.");
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();

            // Check if state is valid
            if (ShouldBeVisible == IsVisible) return;

            // If not refresh
            if (ShouldBeVisible)
                Show();
            else
                Hide();

            // Make dirty again :)
            RequestRefresh();
        }

        public bool TryGetContext(out TObject context)
        {
            context = CachedContext;
            return true;
        }
    }
}