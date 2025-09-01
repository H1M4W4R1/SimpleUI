using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Systems.SimpleUserInterface.Components.Features.Drag
{
    /// <summary>
    ///     Base draggable component.
    /// </summary>
    /// <typeparam name="TSelf">Concrete draggable type.</typeparam>
    public abstract class DragFeature<TSelf> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
        where TSelf : DragFeature<TSelf>
    {
        [field: SerializeField, HideInInspector] private Canvas _rootCanvas;
        [field: SerializeField, HideInInspector] private RectTransform _rootCanvasTransform;
        [field: SerializeField, HideInInspector] private RectTransform _rectTransform;

        private Transform _originalParent;
        private int _originalSiblingIndex;
        private Vector3 _originalWorldPosition;

        /// <summary>
        ///     If true, the draggable will be parented to canvas when moving
        /// </summary>
        protected virtual bool ChangeParent { get; private set; } = false;
        
        /// <summary>
        ///     Checks if the draggable should snap to the mouse position.
        /// </summary>
         protected virtual bool SnapToMouse { get; private set; } = true;

        /// <summary>
        ///     Checks if the draggable should snap back to its original position on failed drop.
        /// </summary>
        protected virtual bool SnapBackOnFailedDrop { get; private set; } = true;

        /// <summary>
        ///     Current drop zone this draggable is in / over.
        /// </summary>
        protected DropZoneFeature<TSelf> CurrentDropZone { get; private set; }

        /// <summary>
        ///     Checks if the draggable can be dropped into the given zone.
        /// </summary>
        protected virtual bool CanDropInto([NotNull] DropZoneFeature<TSelf> zone) => true;

        /// <summary>
        ///     Called when the draggable fails to drop.
        /// </summary>
        protected virtual void OnFailedDrop([CanBeNull] DropZoneFeature<TSelf> originalZone)
        {
        }

        /// <summary>
        ///     Called when the draggable successfully drops.
        /// </summary>
        /// <param name="originalZone">Original drop zone.</param>
        /// <param name="newZone">New drop zone.</param>
        protected virtual void OnSuccessfulDrop(
            [CanBeNull] DropZoneFeature<TSelf> originalZone,
            [NotNull] DropZoneFeature<TSelf> newZone)
        {
        }

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

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            // Check if draggable can be dragged
            if (!CanBeDragged()) return;

            _originalWorldPosition = _rectTransform.position;
            _originalParent = _rectTransform.parent;
            _originalSiblingIndex = _rectTransform.GetSiblingIndex();

            // Move to top-level canvas so it's always visible
            if(ChangeParent) _rectTransform.SetParent(_rootCanvasTransform);
            else _rectTransform.SetAsLastSibling();

            TSelf self = this as TSelf;
            Assert.IsNotNull(self, "DragFeature must be of type TSelf, this should not happen.");

            if (CurrentDropZone) CurrentDropZone.OnPick(self);
        }

        public virtual void OnDrag([NotNull] PointerEventData eventData)
        {
            if(ChangeParent) _rectTransform.SetParent(_rootCanvasTransform);

            // Handle position updates
            if (SnapToMouse)
            {
                _rectTransform.position = eventData.position;
            }
            else
            {
                // Keep offset between pointer and pivot point
                _rectTransform.anchoredPosition += eventData.delta;
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
                if (!zone.IsPointerOverZone(eventData) || !zone.CanDrop(self) ||
                    !CanDropInto(zone))
                    continue;
                bestZone = zone;
                break;
            }

            // If best zone found, drop it there
            if (bestZone is not null)
            {
                DropZoneFeature<TSelf> oldZone = CurrentDropZone;
                CurrentDropZone = bestZone;

                // Notify draggable of successful drop
                OnSuccessfulDrop(oldZone, bestZone);

                // Drop draggable into zone
                // automatically parents draggable to zone
                bestZone.OnDrop(self);
            }
            else
            {
                // Snap back to original position if requested
                ResetToOriginalParent();
                if (SnapBackOnFailedDrop) _rectTransform.position = _originalWorldPosition;

                // Notify draggable of failed drop
                OnFailedDrop(CurrentDropZone);

                // Notify drop zone of failed drop, if such zone exists
                if (CurrentDropZone) CurrentDropZone.OnFailedDrop(self);
            }
        }

        protected virtual void ResetToOriginalParent()
        {
            _rectTransform.SetParent(_originalParent, false);
            _rectTransform.SetSiblingIndex(_originalSiblingIndex);
        }

        protected virtual void OnValidate()
        {
            _rectTransform = GetComponent<RectTransform>();
            Assert.IsNotNull(_rectTransform, "DragFeature requires a RectTransform component");
            _rootCanvas = GetComponentInParent<Canvas>();
            Assert.IsNotNull(_rootCanvas, "DragFeature requires a Canvas component in parent or on object itself.");
            _rootCanvasTransform = _rootCanvas.GetComponent<RectTransform>();
            Assert.IsNotNull(_rootCanvasTransform, "DragFeature requires a RectTransform component on the Canvas.");
        }
    }
}