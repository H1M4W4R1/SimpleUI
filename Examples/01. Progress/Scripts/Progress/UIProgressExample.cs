using Systems.SimpleUserInterface.Components.Abstract.Markers.Context;
using Systems.SimpleUserInterface.Components.Progress;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples._01._Progress.Scripts.Progress
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