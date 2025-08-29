using Systems.SimpleUserInterface.Abstract.Markers;
using UnityEngine;

namespace Systems.SimpleUserInterface.Abstract.Objects
{
    /// <summary>
    ///     Represents a user interface object
    /// </summary>
    public abstract class UIObjectBase : MonoBehaviour
    {
        /// <summary>
        ///     Method used to assign components from the game object
        /// </summary>
        protected virtual void AssignComponents(){}

        /// <summary>
        ///     Method used to attach events of components to this object
        /// </summary>
        protected virtual void AttachEvents(){}
        
        /// <summary>
        ///     Method used to detach events of components from this object
        /// </summary>
        protected virtual void DetachEvents(){}

        /// <summary>
        ///     Called when the object is setup
        /// </summary>
        protected virtual void OnSetupComplete(){}

        /// <summary>
        ///     Called when the object is tearing down
        /// </summary>
        protected virtual void OnTearDownBegin(){}
        
        /// <summary>
        ///     Called when the object is torn down
        ///     Warning: events are detached at this point
        /// </summary>
        protected virtual void OnTearDownComplete(){}

        /// <summary>
        ///     Tries to perform first render, executed after
        ///     setup was complete
        /// </summary>
        private void TryPerformFirstRender()
        {
            if (this is IRenderable renderable) renderable.Render();
        }
        
        protected void Awake()
        {
            AssignComponents();
            AttachEvents();
            OnSetupComplete();
            TryPerformFirstRender();
        }

        protected void OnDestroy()
        {
            OnTearDownBegin();
            DetachEvents();
            OnTearDownComplete();
        }

        protected void Update()
        {
            if (this is IRefreshable refreshable) refreshable.TryRefresh();
        }
    }
}