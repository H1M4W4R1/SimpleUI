using System.Collections.Generic;
using JetBrains.Annotations;
using Systems.SimpleUserInterface.Components.Selectors.Abstract;
using Systems.SimpleUserInterface.Context.Wrappers;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Examples._00._Text_and_Input.Scripts.Dropdown.Context
{
    public sealed class FloatArrayListSelectableContext : SelectableContext<float>
    {
        public FloatArrayListSelectableContext([NotNull] IReadOnlyList<float> data, int defaultIndex = -1) : base(data, defaultIndex)
        {
        }
    }
}