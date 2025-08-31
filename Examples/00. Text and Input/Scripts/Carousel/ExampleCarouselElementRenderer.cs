using Systems.SimpleUserInterface.Components.Lists;
using Systems.SimpleUserInterface.Components.Objects.Markers;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Examples._00._Text_and_Input.Scripts.Carousel
{
    [RequireComponent(typeof(Image))]
    public sealed class ExampleCarouselElementRenderer : UIListElementBase<Color>, IRenderable<Color>
    {
        [field: SerializeField, HideInInspector] private Image ImageReference { get; set; }
       
        protected override void OnValidate()
        {
            base.OnValidate();
            ImageReference = GetComponent<Image>();
        }

        public void OnRender(Color withContext)
        {
            ImageReference.color = withContext;
        }
    }
}