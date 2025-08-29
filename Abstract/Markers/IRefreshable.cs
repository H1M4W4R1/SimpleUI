using Systems.SimpleUserInterface.Abstract.Markers.Context;

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
        ///     Refreshes the object
        /// </summary>
        protected internal void TryRefresh()
        {
            // Skip if context is not dirty (only if context is available)
            if(this is IWithContext {IsDirty: false}) return;
   
            // Render object if necessary
            if (this is IRenderable renderable) 
                renderable.Render();

            // Call refresh event
            OnRefresh();
            
            // Reset dirty status
            if (this is IWithContext withContext) withContext.SetDirty(false);
        }
    }
}