using System.Globalization;
using Systems.SimpleUserInterface.Components.Abstract.Markers;
using Systems.SimpleUserInterface.Components.Lists;
using TMPro;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples._03._Lists.Scripts.Lists
{
    public sealed class ExampleFloatListElement : UIListElementBase<float>, IRenderable<float>
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void OnRender(float withContext)
        {
            _text.text = withContext.ToString(CultureInfo.InvariantCulture);
        }
    }
}