using Systems.SimpleUserInterface.Abstract.Markers.Context;

namespace Systems.SimpleUserInterface.Abstract.Markers
{
    /// <summary>
    ///     Informs object that it should be rendered with a specific context type
    /// </summary>
    /// <typeparam name="TContextType">Context type</typeparam>
    public interface IRenderable<TContextType> : IRenderable, IWithContext<TContextType>
    {
        /// <summary>
        ///     Event that is called when the object is rendered
        /// </summary>
        void OnRender(TContextType withContext);
        
        void IRenderable.Render() =>
            OnRender(ProvideContext());
    }
    
    /// <summary>
    ///     Informs object that it should be rendered.
    ///     Do not use directly, see <see cref="IRenderable{TContextType}"/>
    /// </summary>
    public interface IRenderable
    {
        /// <summary>
        ///     Renders the object
        /// </summary>
        protected internal void Render();
    }
}