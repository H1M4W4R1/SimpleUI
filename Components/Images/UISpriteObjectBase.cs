using JetBrains.Annotations;
using Systems.SimpleUserInterface.Components.Objects;
using Systems.SimpleUserInterface.Components.Objects.Markers;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Components.Images
{
    [RequireComponent(typeof(Image))]
    public abstract class UISpriteObjectBase : UIObjectWithContextBase<Sprite>, IRenderable<Sprite>
    {
        [field: SerializeField, HideInInspector] protected Image imageReference;

        public virtual void OnRender([CanBeNull] Sprite withContext)
        {
            imageReference.sprite = withContext;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            imageReference = GetComponent<Image>();
        }
    }
}