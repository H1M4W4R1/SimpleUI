namespace Systems.SimpleUserInterface.Abstract.Markers
{
    /// <summary>
    ///     Automates the refresh process of a user interface object
    /// </summary>
    public interface IRefreshable
    {
        /// <summary>
        ///     Method called when the object is refreshed
        /// </summary>
        protected void OnRefresh();
        
        /// <summary>
        ///     Marks the object as dirty
        /// </summary>
        protected bool IsDirty { get; set; }
        
        /// <summary>
        ///     Sets the dirty status of the object
        /// </summary>
        /// <param name="newStatus">New dirty status</param>
        public void SetDirty(bool newStatus = true) => IsDirty = newStatus;
        
        /// <summary>
        ///     Refreshes the object
        /// </summary>
        protected internal void TryRefresh()
        {
            // Skip if not dirty
            if (!IsDirty) return;
            
            // Render object if necessary
            if (this is IRenderable renderable) 
                renderable.Render();

            // Call refresh event
            OnRefresh();
            
            // Reset dirty status
            SetDirty(false);
        }
    }
}