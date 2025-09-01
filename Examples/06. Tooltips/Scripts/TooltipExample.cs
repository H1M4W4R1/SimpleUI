using Systems.SimpleUserInterface.Components.Abstract.Markers;
using Systems.SimpleUserInterface.Components.Tooltips;
using TMPro;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples._06._Tooltips.Scripts
{
    public sealed class TooltipExample : UITooltipBase<string>, IRenderable<string>
    {
        [field: SerializeField, HideInInspector] private TextMeshProUGUI TextReference { get; set; }

        protected override void OnValidate()
        {
            base.OnValidate();
            TextReference = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void OnRender(string withContext)
        {
            TextReference.text = withContext;
        }
    }
}