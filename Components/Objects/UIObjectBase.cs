using DG.Tweening;
using JetBrains.Annotations;
using Systems.SimpleUserInterface.Components.Animations.Abstract;
using Systems.SimpleUserInterface.Components.Objects.Markers;
using Systems.SimpleUserInterface.Components.Objects.Markers.Context;
using Systems.SimpleUserInterface.Components.Windows;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.SimpleUserInterface.Components.Objects
{
    /// <summary>
    ///     Represents a user interface object
    /// </summary>
    public abstract class UIObjectBase : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler
    {
        /// <summary>
        ///     Object show/hide animation
        /// </summary>
        private UIShowHideAnimationBase _showHideAnimation;

        private Sequence _currentShowHideAnimationSequence;

        private bool _isDragging;
        [CanBeNull] protected UIWindowBase windowContainerReference;
        [CanBeNull] protected RectTransform rectTransformReference;
        [CanBeNull] protected CanvasGroup canvasGroupReference;

        [CanBeNull] protected internal RectTransform RectTransformReference => rectTransformReference;

        /// <summary>
        ///     Check if element is not hidden
        /// </summary>
        public bool IsVisible { get; private set; }
        
        /// <summary>
        ///     Checks if the object is destroyed
        /// </summary>
        protected bool IsDestroyed { get; private set; }

        /// <summary>
        ///     Gets the window container handle
        /// </summary>
        [CanBeNull] public UIWindowBase WindowHandle => windowContainerReference;

        /// <summary>
        ///     Method used to assign components from the game object
        /// </summary>
        protected virtual void AssignComponents()
        {
            rectTransformReference = GetComponent<RectTransform>();
            _showHideAnimation = GetComponent<UIShowHideAnimationBase>();
            canvasGroupReference = GetComponent<CanvasGroup>();
        }

        /// <summary>
        ///     Method used to attach events of components to this object
        /// </summary>
        protected virtual void AttachEvents()
        {
        }

        /// <summary>
        ///     Method used to detach events of components from this object
        /// </summary>
        protected virtual void DetachEvents()
        {
        }

        /// <summary>
        ///     Called when the object is setup
        /// </summary>
        protected virtual void OnSetupComplete()
        {
        }

        /// <summary>
        ///     Called when the object is torn down
        ///     Warning: events are detached at this point
        /// </summary>
        protected virtual void OnTearDownComplete()
        {
        }

        /// <summary>
        ///     Tries to perform first render, executed after
        ///     setup was complete
        /// </summary>
        private void TryPerformFirstRender()
        {
            if (this is IRenderable renderable) renderable.Render();
        }

        /// <summary>
        ///     Executed when the object is refreshed
        /// </summary>
        protected virtual void OnRefresh()
        {
            
        }
        
        protected internal void Show()
        {
            IsVisible = true;
            
            // Ensure object is active
            gameObject.SetActive(true);

            // If no animation, just return
            if (_showHideAnimation is null) return;

            // Play nice animation
            //_currentShowHideAnimationSequence?.Kill();
            _currentShowHideAnimationSequence = _showHideAnimation.AnimateObjectShow().Play();
        }

        protected internal void Hide()
        {
            IsVisible = false;
            
            // If no animation, just disable and return
            if (_showHideAnimation is null)
            {
                gameObject.SetActive(false);
                return;
            }

            // Play nice animation
            //_currentShowHideAnimationSequence?.Kill();
            _currentShowHideAnimationSequence = _showHideAnimation.AnimateObjectHide()
                .OnComplete(() => gameObject.SetActive(false))
                .Play();
        }

        protected void Awake()
        {
            AssignComponents();

            // Access window container if this is not a window
            if (this is not UIWindowBase) windowContainerReference = GetComponentInParent<UIWindowBase>();

            OnSetupComplete();

            // Set visibility
            IsVisible = gameObject.activeSelf;
        }

        protected void OnEnable()
        {
            AttachEvents();
            TryPerformFirstRender();

            if (canvasGroupReference) canvasGroupReference.interactable = true;
        }

        private void OnDisable()
        {
            DetachEvents();

            if (canvasGroupReference) canvasGroupReference.interactable = false;
        }

        protected void OnDestroy()
        {
            IsDestroyed = true;
            OnTearDownComplete();
        }

        protected void Update()
        {
            IWithContext withContext = this as IWithContext;
            
            // Check if context is dirty
            if (!ReferenceEquals(withContext, null)) withContext.CheckIfContextIsDirty();
            
            // Skip if context is not dirty (only if context is available)
            if(this is IWithContext {IsDirty: false}) return;
   
            // Render object if necessary
            if (this is IRenderable renderable) 
                renderable.Render();

            // Call refresh event
            OnRefresh();
            
            // Reset dirty status
            if (!ReferenceEquals(withContext, null)) withContext.SetDirty(false);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            // Handle drag to be ignored to prevent focusing windows with dragging components...
            if (_isDragging) return;

            // Check if this is window and focus on it
            if (this is UIWindowBase window)
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
            // Focus on window container
            if (this is UIWindowBase window) window.Focus();
            _isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
        }
    }
}