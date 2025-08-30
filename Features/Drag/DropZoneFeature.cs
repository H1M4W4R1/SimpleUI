using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.SimpleUserInterface.Features.Drag
{
    /// <summary>
    ///     Base drop zone feature.
    /// </summary>
    /// <typeparam name="TFeature">Type of draggable this zone accepts.</typeparam>
    public abstract class DropZoneFeature<TFeature> : MonoBehaviour
        where TFeature : DragFeature<TFeature>
    {
        /// <summary>
        ///     RectTransform of this zone.
        /// </summary>
        [field: SerializeField, HideInInspector] protected RectTransform rectTransform;
        
        /// <summary>
        ///     Collection of all drop zones.
        /// </summary>
        protected static readonly List<DropZoneFeature<TFeature>> zones = new();
        
        /// <summary>
        ///     Access to all drop zones.
        /// </summary>
        public static IReadOnlyList<DropZoneFeature<TFeature>> Zones => zones;

        protected virtual void OnEnable() => zones.Add(this);
        protected virtual void OnDisable() => zones.Remove(this);

        /// <summary>
        ///     Checks if the pointer is over this zone.
        /// </summary>
        /// <param name="eventData">Event data.</param>
        /// <returns>True if the pointer is over this zone, false otherwise.</returns>
        internal virtual bool IsPointerOverZone([NotNull] PointerEventData eventData)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, eventData.position,
                eventData.pressEventCamera);
        }

        /// <summary>
        ///     Override this to check if object can be dropped.
        /// </summary>
        public virtual bool CanDrop([NotNull] TFeature feature) => true;
        
        /// <summary>
        ///     Checks if the draggable can be picked up.
        /// </summary>
        /// <param name="feature">Feature to pick from zone.</param>
        /// <returns>True if the draggable can be picked up, false otherwise.</returns>
        public virtual bool CanPick([NotNull] TFeature feature) => true;

        /// <summary>
        ///     Called by DragFeature when dropped on this zone.
        /// </summary>
        protected internal virtual void OnDrop([NotNull] TFeature feature)
        {
            feature.transform.SetParent(transform);
        }

        /// <summary>
        ///     Called by DragFeature when drop fails and original zone was this one.
        /// </summary>
        /// <param name="feature">Feature that failed to drop.</param>
        protected internal virtual void OnFailedDrop([NotNull] TFeature feature)
        {
            
        }

        /// <summary>
        ///     Called by DragFeature when picked up from this zone.
        /// </summary>
        public virtual void OnPick([NotNull] TFeature dragFeature)
        {
            // Do nothing by default
            dragFeature.transform.SetParent(transform);
        }

        protected virtual void OnValidate()
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }
}