using Systems.SimpleUserInterface.Components.Canvases;
using UnityEngine;

namespace Systems.SimpleUserInterface.Features.Positioning
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public sealed class LimitObjectToViewport : MonoBehaviour
    {
        /// <summary>
        ///     Viewport that the object is limited to
        /// </summary>
        private RectTransform _viewport;

        /// <summary>
        ///     RectTransform of the object
        /// </summary>
        private RectTransform _rectTransform;

        private void Awake()
        {
            UIRootCanvasBase rootCanvas = GetComponentInParent<UIRootCanvasBase>();
            if (!rootCanvas) return;
            
            _viewport = rootCanvas.GetComponent<RectTransform>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void LateUpdate()
        {
            // Ensure components are assigned
            if (!_viewport || !_rectTransform) return;
            KeepInsideViewport();
        }

        /// <summary>
        ///     Keeps the object inside the viewport
        /// </summary>
        private void KeepInsideViewport()
        {
            // Transform corners into world space
            Vector3[] worldCorners = new Vector3[4];
            _rectTransform.GetWorldCorners(worldCorners);

            Vector3[] viewportCorners = new Vector3[4];
            _viewport.GetWorldCorners(viewportCorners);

            Vector3 offset = Vector3.zero;

            // Left
            float delta = viewportCorners[0].x - worldCorners[0].x;
            if (delta > 0) offset.x += delta;

            // Right
            delta = viewportCorners[2].x - worldCorners[2].x;
            if (delta < 0) offset.x += delta;

            // Bottom
            delta = viewportCorners[0].y - worldCorners[0].y;
            if (delta > 0) offset.y += delta;

            // Top
            delta = viewportCorners[2].y - worldCorners[2].y;
            if (delta < 0) offset.y += delta;

            // Apply correction
            if (!Mathf.Approximately(offset.x, 0) || !Mathf.Approximately(offset.y, 0) ||
                !Mathf.Approximately(offset.z, 0))
            {
                _rectTransform.position += offset;
            }
        }
    }
}