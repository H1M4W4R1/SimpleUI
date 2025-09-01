using JetBrains.Annotations;
using Systems.SimpleUserInterface.Components.Abstract;
using Systems.SimpleUserInterface.Components.Abstract.Markers;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Components.Images
{
    [RequireComponent(typeof(Image))]
    public abstract class UISpriteObjectBase : UIObjectWithContextBase<Sprite>, IRenderable<Sprite>
    {
        [field: SerializeField, HideInInspector] protected Image ImageReference { get; private set; }

        public virtual void OnRender([CanBeNull] Sprite withContext)
        {
            ImageReference.sprite = withContext;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            ImageReference = GetComponent<Image>();
        }
    }
}