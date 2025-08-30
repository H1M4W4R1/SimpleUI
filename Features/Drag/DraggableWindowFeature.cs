using Systems.SimpleUserInterface.Features.Positioning;
using UnityEngine;

namespace Systems.SimpleUserInterface.Features.Drag
{
    /// <summary>
    ///     Feature that allows dragging windows
    /// </summary>
    [RequireComponent(typeof(LimitObjectToViewport))]
    public sealed class DraggableWindowFeature : DragFeature<DraggableWindowFeature>
    {
        /// <summary>
        ///     We don't want to snap back on failed drop as window should be draggable across the screen
        /// </summary>
        protected override bool SnapBackOnFailedDrop => false;
    }
}