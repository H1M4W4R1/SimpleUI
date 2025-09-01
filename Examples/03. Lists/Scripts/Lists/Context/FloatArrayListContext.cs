using System.Collections.Generic;
using JetBrains.Annotations;
using Systems.SimpleUserInterface.Context.Lists;

namespace Systems.SimpleUserInterface.Examples._03._Lists.Scripts.Lists.Context
{
    public sealed class FloatArrayListContext : ListContext<float>
    {
        public FloatArrayListContext([NotNull] IReadOnlyList<float> data) : base(data)
        {
        }
    }
}