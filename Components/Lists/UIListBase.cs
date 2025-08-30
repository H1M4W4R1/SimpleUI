using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using Systems.SimpleUserInterface.Components.Objects;
using Systems.SimpleUserInterface.Components.Objects.Markers;
using Systems.SimpleUserInterface.Components.Objects.Markers.Context;
using Systems.SimpleUserInterface.Context.Wrappers;
using UnityEngine;

namespace Systems.SimpleUserInterface.Components.Lists
{
    /// <summary>
    ///     List object used to render list elements
    /// </summary>
    /// <typeparam name="TListObject">Object type in the list</typeparam>
    public abstract class UIListBase<TListObject> : UIListBase<ListContext<TListObject>, TListObject>
    {
        
    }
    
    /// <summary>
    ///     List object used to render list elements, used to inherit list properties for different
    ///     context types.
    /// </summary>
    /// <typeparam name="TListContext">Context type of the list</typeparam>
    /// <typeparam name="TListObject">Object type in the list</typeparam>
    public abstract class UIListBase<TListContext, TListObject> : UIObjectWithContextBase<TListContext>,
        IRenderable<TListContext>, IRefreshable
        where TListContext : ListContext<TListObject>
    {
        private IReadOnlyList<TListObject> _renderedList;
        private int _renderedCount;

        /// <summary>
        ///     List of all drawn elements
        /// </summary>
        protected readonly List<UIListElementBase<TListObject>> DrawnElements = new();

        /// <summary>
        ///     Reference to the prefab of the list element
        /// </summary>
        [field: SerializeReference] protected UIListElementBase<TListObject> ElementPrefab { get; private set; }

        public void OnRender([CanBeNull] TListContext withContext)
        {
            Assert.IsNotNull(withContext, "List context cannot be null.");

            // Handle elements that already exist
            for (int index = 0; index < DrawnElements.Count; index++)
            {
                // Show element if it is not hidden
                UIListElementBase<TListObject> element = DrawnElements[index];
                if (!withContext.IsValidIndex(index))
                    element.Hide();
                else if (element.IsVisible) element.Show();

                // Set element context
                element.Owner = withContext;
                element.Index = index;

                // Ensure element is re-rendered based on regular implementations
                element.RequestRefresh();
            }

            // Handle elements that need to be created
            for (int index = DrawnElements.Count; index < withContext.Count; index++)
            {
                // Create element
                UIListElementBase<TListObject> element = Instantiate(ElementPrefab, transform);

                // Set element context
                element.Owner = withContext;
                element.Index = index;

                // Add element to the list
                DrawnElements.Add(element);

                // Show element
                if (!element.IsVisible) element.Show();

                // Ensure element is re-rendered based on regular implementations
                // as first render will be called before element data is set to be valid
                element.RequestRefresh();
            }

            // Update rendered list
            _renderedList = withContext.DataArray;
            _renderedCount = withContext.Count;
            
            // Notify elements that list has been modified
            // but only if list is dirty to prevent multi-calls
            if(((IWithContext) this).IsDirty) 
                OnListElementsModified();
        }

        void IWithContext.CheckIfContextIsDirty()
        {
            // Check if context has changed (list data changed)
            if (!ReferenceEquals(_renderedList, Context?.DataArray))
            {
                SetDirty();
                return;
            }

            // Check if count has changed (element added/removed)
            if (_renderedCount == Context?.Count) return;
            SetDirty();

            // Single-element updates are handled within UIListElementBase
        }

        void IRefreshable.OnRefresh()
        {
            OnListElementsModified();
        }
        
        /// <summary>
        ///     Event called when list elements are modified
        /// </summary>
        protected internal void OnListElementsModified()
        {
            
        }
        
    }
}