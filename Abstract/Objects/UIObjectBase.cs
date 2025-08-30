using JetBrains.Annotations;
using Systems.SimpleUserInterface.Abstract.Markers;
using Systems.SimpleUserInterface.Abstract.Markers.Context;
using Systems.SimpleUserInterface.Base.Windows;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.SimpleUserInterface.Abstract.Objects
{
    /// <summary>
    ///     Represents a user interface object
    /// </summary>
    public abstract class UIObjectBase : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler
    {
        private bool _isDragging;
        [CanBeNull] protected UIWindow windowContainerReference;
        
        /// <summary>
        ///     Gets the window container handle
        /// </summary>
        [CanBeNull] public UIWindow WindowHandle => windowContainerReference;
        
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
            
            // Access window container if this is not a window
            if (this is not UIWindow)
                windowContainerReference = GetComponentInParent<UIWindow>();
            
            OnSetupComplete();
        }
        
        protected void OnEnable()
        {
            AttachEvents();
            TryPerformFirstRender();
        }

        private void OnDisable()
        {
            DetachEvents();
        }

        protected void OnDestroy()
        {
            OnTearDownComplete();
            
            // Detach context provider events
            if (this is IWithContext withContext) withContext.TryClearContextProvider();
        }

        protected void Update()
        {
            if (this is IRefreshable refreshable) refreshable.TryRefresh();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            // Handle drag to be ignored to prevent focusing windows with dragging components...
            if(_isDragging) return;
         
            // Check if this is window and focus on it
            if (this is UIWindow window)
            {
                window.Focus();
                return;
            }
            
            // Focus on window container
            if (!windowContainerReference) return;
            windowContainerReference.Focus();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
        }
    }
}