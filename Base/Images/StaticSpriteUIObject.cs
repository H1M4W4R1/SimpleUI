using Systems.SimpleUserInterface.Abstract.Markers;
using Systems.SimpleUserInterface.Abstract.Markers.Context;
using Systems.SimpleUserInterface.Abstract.Objects;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Base.Images
{
    /// <summary>
    ///     Static sprite UI object
    /// </summary>
    [RequireComponent(typeof(Image))]
    public sealed class StaticSpriteUIObject : UIObjectWithContextBase<Sprite>, IRenderable<Sprite>,
        IWithLocalContext<Sprite>
    {
        [SerializeField] private Sprite spriteToDisplay;
        private Image _imageReference;

        /// <summary>
        ///     Assigns image component to display sprite
        /// </summary>
        protected override void AssignComponents()
        {
            // Assign image reference
            _imageReference = GetComponent<Image>();
        }
        
        /// <summary>
        ///     Gets the sprite to display
        /// </summary>
        Sprite IWithLocalContext<Sprite>.GetContext() => spriteToDisplay;

        /// <summary>
        ///     Renders the sprite
        /// </summary>
        public void OnRender(Sprite withContext)
        {
            _imageReference.sprite = withContext;
        }
    }
}