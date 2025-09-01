using Systems.SimpleUserInterface.Components.Features.Positioning;
using UnityEngine;

namespace Systems.SimpleUserInterface.Components.Features.Drag
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

        /// <summary>
        ///     Don't snap to mouse as it will look terribly
        /// </summary>
        protected override bool SnapToMouse =>  false;

        /// <summary>
        ///     Do not change parent as we want to keep window in its original parent
        /// </summary>
        protected override bool ChangeParent => false;
    }
}