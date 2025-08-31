using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Components.Selectors.Implementations.Carousel
{
    /// <summary>
    ///     Carousel scroll rect that blocks pointer dragging and clicking
    /// </summary>
    public sealed class UICarouselScrollRect : ScrollRect
    {
        public override void OnBeginDrag(PointerEventData eventData) { }
        public override void OnDrag(PointerEventData eventData) { }
        public override void OnEndDrag(PointerEventData eventData) { }

        protected override void OnValidate()
        {
            base.OnValidate();
            movementType = MovementType.Clamped;
            scrollSensitivity = 0;
            inertia = false;
            if(horizontalScrollbar)
            {
                Destroy(horizontalScrollbar);
                horizontalScrollbar = null;
            }

            if (verticalScrollbar)
            {
                Destroy(verticalScrollbar);
                verticalScrollbar = null;
            }
        }
    }
}