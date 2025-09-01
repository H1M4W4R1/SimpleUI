using Systems.SimpleUserInterface.Features.Tooltips;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples._06._Tooltips.Scripts
{
    public sealed class ExampleTooltipFeature : UITooltipFeature<TooltipExample, string>
    {
        [field: SerializeField] private string TooltipText { get; set; }
        
        protected override string GetNewTooltipContext() => TooltipText;
    }
}