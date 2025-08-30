using Systems.SimpleUserInterface.Components.Objects.Markers.Context;
using Systems.SimpleUserInterface.Components.Progress;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples.Objects.Progress
{
    public sealed class UIProgressExample : UIProgressBase, IWithLocalContext<float>
    {
        [field: SerializeField] 
        [Range(0,1)]
        private float progressValue;
        
        public bool TryGetContext(out float context)
        {
            context = progressValue;
            return true;
        }
    }
}