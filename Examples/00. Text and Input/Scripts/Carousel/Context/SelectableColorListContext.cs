using System.Collections.Generic;
using JetBrains.Annotations;
using Systems.SimpleUserInterface.Context.Wrappers;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples._00._Text_and_Input.Scripts.Carousel.Context
{
    public sealed class SelectableColorListContext : SelectableContext<Color>
    {
        public SelectableColorListContext([NotNull] IReadOnlyList<Color> data, int defaultIndex = -1) : base(data,
            defaultIndex)
        {
        }
    }
}