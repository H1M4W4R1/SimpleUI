using Systems.SimpleUserInterface.Abstract.Markers;
using Systems.SimpleUserInterface.Abstract.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Base.Images
{
    [RequireComponent(typeof(Image))]
    public abstract class SpriteUIObject : UIObjectWithContextBase<Sprite>, IRenderable<Sprite>
    {
        private Image _imageReference;

        /// <summary>
        ///     Assigns image component to display sprite
        /// </summary>
        protected override void AssignComponents()
        {
            base.AssignComponents();
            _imageReference = GetComponent<Image>();
        }
        
        /// <summary>
        ///     Renders the sprite
        /// </summary>
        public void OnRender(Sprite withContext)
        {
            _imageReference.sprite = withContext;
        }
    }
}