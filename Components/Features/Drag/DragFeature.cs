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
        protected virtual bool ChangeParent { get; set; } = false;

        /// <summary>
        ///     Checks if the draggable should snap to the mouse position.
        /// </summary>
        protected virtual bool SnapToMouse { get; set; } = true;

        /// <summary>
        ///     Checks if the draggable should snap back to its original position on failed drop.
        /// </summary>
        protected virtual bool SnapBackOnFailedDrop { get; set; } = true;

        /// <summary>
        ///     Current drop zone this draggable is in / over.
        /// </summary>
        public DropZoneFeature<TSelf> CurrentDropZone { get; private set; }

        /// <summary>
        ///     Checks if the draggable can be picked up from the given zone.
        /// </summary>
        protected internal virtual bool CanPickFrom([CanBeNull] DropZoneFeature<TSelf> zone) =>
            ReferenceEquals(zone, CurrentDropZone);

        /// <summary>
        ///     Checks if the draggable can be dropped into the given zone.
        /// </summary>
        protected internal virtual bool CanDropInto([CanBeNull] DropZoneFeature<TSelf> zone) =>
            zone is not null;

        /// <summary>
        ///     Called when the draggable is picked up from the given zone.
        /// </summary>
        protected internal virtual void OnPickFrom([CanBeNull] DropZoneFeature<TSelf> zone)
        {
            // Store original position and parent
            _originalWorldPosition = _rectTransform.position;
            _originalParent = _rectTransform.parent;
            _originalSiblingIndex = _rectTransform.GetSiblingIndex();

            // Move to top-level canvas so it's always visible
            if (ChangeParent)
                _rectTransform.SetParent(_rootCanvasTransform);
            else
            {
                // Ensure zone is parent of this object
                if (zone) _rectTransform.SetParent(zone.transform);
                _rectTransform.SetAsLastSibling();
            }
        }

        /// <summary>
        ///     Called when the draggable fails to drop.
        /// </summary>
        protected internal virtual void OnFailedDrop(
            [CanBeNull] DropZoneFeature<TSelf> originalZone,
            [CanBeNull] DropZoneFeature<TSelf> targetZone)
        {
            ResetToOriginalLocation();
        }

        /// <summary>
        ///     Called when the draggable successfully drops.
        /// </summary>
        /// <param name="newZone">New drop zone.</param>
        protected internal virtual void OnSuccessfulDropInto([CanBeNull] DropZoneFeature<TSelf> newZone)
        {
            CurrentDropZone = newZone;
            _rectTransform.SetParent(newZone ? newZone.transform : null);
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
            if (CurrentDropZone is null) return CanPickFrom(null);

            // Check if draggable can be picked up
            return CurrentDropZone.CanPick(self);
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            // Check if draggable can be dragged
            if (!CanBeDragged()) return;

            // Conversion because generics are weird :D
            TSelf self = this as TSelf;
            Assert.IsNotNull(self, "DragFeature must be of type TSelf, this should not happen.");

            // Handle pick events for zone (if any) or for object itself
            if (CurrentDropZone)
                CurrentDropZone.OnPick(self);
            else
                OnPickFrom(null);
        }

        public virtual void OnDrag([NotNull] PointerEventData eventData)
        {
            if (ChangeParent) _rectTransform.SetParent(_rootCanvasTransform);

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

            // Detect best drop zone
            DropZoneFeature<TSelf> bestZone = null;
            for (int index = 0; index < DropZoneFeature<TSelf>.Zones.Count; index++)
            {
                DropZoneFeature<TSelf> zone = DropZoneFeature<TSelf>.Zones[index];

                // Skip if not over zone or cannot drop
                if (!zone.IsPointerOverZone(eventData)) continue;

                // Set zone as best and break when can be dropped into
                bestZone = zone;
                if (zone.CanDrop(self)) break;
            }

            // If best zone found, try to drop it here
            if (bestZone is not null)
            {
                // Check if we can drop into best zone, if not, reset to original location
                // and fail the drop into target zone
                if (!bestZone.CanDrop(self))
                {
                    bestZone.OnFailedDrop(self);
                    return;
                }

                // Drop draggable into zone, automatically parents draggable to zone
                // and updates CurrentDropZone
                bestZone.OnDrop(self);
            }
            else
            {
                // Handle dropping into null zones (aka no zone)
                if (!CanDropInto(null))
                {
                    // Notify object of failed drop (if no drop zone found)
                    OnFailedDrop(self.CurrentDropZone, null);
                    return;
                }

                OnSuccessfulDropInto(null);
            }
        }

        protected virtual void ResetToOriginalLocation()
        {
            _rectTransform.SetParent(_originalParent, false);
            _rectTransform.SetSiblingIndex(_originalSiblingIndex);

            if (SnapBackOnFailedDrop) _rectTransform.position = _originalWorldPosition;
        }

        protected virtual void OnValidate()
        {
            _rectTransform = GetComponent<RectTransform>();
            Assert.IsNotNull(_rectTransform, "DragFeature requires a RectTransform component");

            // Optional drop zone assignment, can be null if none
            CurrentDropZone = GetComponentInParent<DropZoneFeature<TSelf>>();

            if (string.IsNullOrEmpty(gameObject.scene.name)) return;
            _rootCanvas = GetComponentInParent<Canvas>();
            Assert.IsNotNull(_rootCanvas,
                "DragFeature requires a Canvas component in parent or on object itself.");
            _rootCanvasTransform = _rootCanvas.GetComponent<RectTransform>();
            Assert.IsNotNull(_rootCanvasTransform,
                "DragFeature requires a RectTransform component on the Canvas.");
        }
    }
}