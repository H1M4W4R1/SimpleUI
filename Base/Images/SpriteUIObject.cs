using JetBrains.Annotations;
using Systems.SimpleUserInterface.Abstract.Markers;
using Systems.SimpleUserInterface.Abstract.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Base.Images
{
    [RequireComponent(typeof(Image))]
    public abstract class SpriteUIObject : UIObjectWithContextBase<Sprite>, IRenderable<Sprite>
    {
        protected Image imageReference;

        protected override void AssignComponents()
        {
            base.AssignComponents();
            imageReference = GetComponent<Image>();
        }

        public virtual void OnRender([CanBeNull] Sprite withContext)
        {
            imageReference.sprite = withContext;
        }
    }
}