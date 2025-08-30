using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Systems.SimpleUserInterface.Features.Drag
{
    /// <summary>
    ///     Base draggable component.
    /// </summary>
    /// <typeparam name="TSelf">Concrete draggable type.</typeparam>
    public abstract class DragFeature<TSelf> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
        where TSelf : DragFeature<TSelf>
    {
        private Canvas _rootCanvas;
        private RectTransform _rootCanvasTransform;

        private RectTransform _rectTransform;
        private Transform _originalParent;
        private int _originalSiblingIndex;
        private Vector3 _originalLocalPosition;

        /// <summary>
        ///     Checks if the draggable should snap to the mouse position.
        /// </summary>
        [field: SerializeField] protected virtual bool SnapToMouse { get; private set; } = true;
        
        /// <summary>
        ///     Checks if the draggable should snap back to its original position on failed drop.
        /// </summary>
        [field: SerializeField] protected virtual bool SnapBackOnFailedDrop { get; private set; } = true;
        
        /// <summary>
        ///     Current drop zone this draggable is in / over.
        /// </summary>
        protected DropZoneFeature<TSelf> CurrentDropZone { get; private set; }

        /// <summary>
        ///     Checks if the draggable can be dragged.
        /// </summary>
        protected bool CanBeDragged()
        {
            // Get self as TSelf
            TSelf self = this as TSelf;
            Assert.IsNotNull(self, "DragFeature must be of type TSelf, this should not happen.");
            
            // If no drop zone, can be dragged
            if (CurrentDropZone is null) return true;
            
            // Check if draggable can be picked up
            return CurrentDropZone.CanPick(self);
        }
        
        protected virtual void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
            _rootCanvasTransform = _rootCanvas.GetComponent<RectTransform>();
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            // Check if draggable can be dragged
            if (!CanBeDragged()) return;
    
            _originalLocalPosition = _rectTransform.localPosition;
            _originalParent = _rectTransform.parent;
            _originalSiblingIndex = transform.GetSiblingIndex();

            // Move to top-level canvas so it's always visible
            transform.SetParent(_rootCanvasTransform, true);
        }

        public virtual void OnDrag([NotNull] PointerEventData eventData)
        {
            // Handle position updates
            if (SnapToMouse)
            {
                _rectTransform.position = eventData.position;
            }
            else
            {
                // Keep offset between pointer and pivot point
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _rectTransform.parent as RectTransform,
                    eventData.position,
                    eventData.pressEventCamera,
                    out Vector2 localPoint
                );
                _rectTransform.localPosition = localPoint;
            }
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            // Get self as TSelf
            TSelf self = this as TSelf;
            Assert.IsNotNull(self, "DragFeature must be of type TSelf, this should not happen.");
            
            // Detect "best" drop zone
            DropZoneFeature<TSelf> bestZone = null;
            foreach (DropZoneFeature<TSelf> zone in DropZoneFeature<TSelf>.Zones)
            {
                // Skip if not over zone or cannot drop
                if (!zone.IsPointerOverZone(eventData) || !zone.CanDrop(self)) continue;
                bestZone = zone;
                break;
            }

            // If best zone found, drop it there
            if (bestZone is not null)
            {
                CurrentDropZone = bestZone;
                
                // Drop draggable into zone
                // automatically parents draggable to zone
                bestZone.OnDrop(self);
            }
            else
            {
                // Snap back to original position if requested
                if (SnapBackOnFailedDrop) _rectTransform.localPosition = _originalLocalPosition;
                ResetToOriginalParent();
            }
        }

        protected virtual void ResetToOriginalParent()
        {
            _rectTransform.SetParent(_originalParent, false);
            _rectTransform.SetSiblingIndex(_originalSiblingIndex);
        }
    }
}