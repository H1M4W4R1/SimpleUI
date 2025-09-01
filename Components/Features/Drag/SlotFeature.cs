using UnityEngine;
using UnityEngine.Assertions;

namespace Systems.SimpleUserInterface.Components.Features.Drag
{
    /// <summary>
    ///     Slot that accepts exactly one draggable.
    /// </summary>
    /// <typeparam name="TDragFeature">Type of draggable it can hold.</typeparam>
    public abstract class SlotFeature<TDragFeature> : DropZoneFeature<TDragFeature>
        where TDragFeature : DragFeature<TDragFeature>
    {
        /// <summary>
        ///     Gets the draggable that is currently in the slot.
        /// </summary>
        public TDragFeature Occupant { get; protected set; }

        /// <summary>
        ///     Checks if the draggable can be picked up.
        /// </summary>
        /// <param name="feature">Feature to pick up.</param>
        /// <returns>True if the draggable can be picked up, false otherwise.</returns>
        public override bool CanPick(TDragFeature feature)
        {
            Assert.AreEqual(Occupant, feature, "Occupant must be the same as feature. This should not happen.");
            return true;
        }

        public override void OnPick(TDragFeature dragFeature)
        {
            Occupant = null;
            dragFeature.transform.SetParent(transform);
        }

        /// <summary>
        ///     Checks if the draggable can be dropped into the slot.
        /// </summary>
        /// <param name="feature">Draggable to be dropped.</param>
        /// <returns>True if the draggable can be dropped into the slot, false otherwise.</returns>
        public override bool CanDrop(TDragFeature feature)
        {
            return Occupant is null; // default: only if empty
        }

        /// <summary>
        ///     Performs actions when the draggable is dropped into the slot.
        /// </summary>
        /// <param name="feature">Draggable to be dropped.</param>
        protected internal override void OnDrop(TDragFeature feature)
        {
            base.OnDrop(feature);
            Occupant = feature;
            feature.transform.localPosition = Vector3.zero; // snap to center
        }

        /// <summary>
        ///     Performs actions when the draggable fails to be dropped out of this slot
        /// </summary>
        /// <param name="feature">Feature that failed to drop.</param>
        protected internal override void OnFailedDrop(TDragFeature feature)
        {
            base.OnFailedDrop(feature);
            Occupant = feature;
            feature.transform.localPosition = Vector3.zero; // snap to center
        }

        /// <summary>
        ///     Clears the slot.
        /// </summary>
        public virtual void ClearSlot()
        {
            Occupant = null;
        }
    }
}