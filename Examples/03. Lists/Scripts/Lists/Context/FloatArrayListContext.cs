using System.Collections.Generic;
using JetBrains.Annotations;
using Systems.SimpleUserInterface.Context.Wrappers;

namespace Systems.SimpleUserInterface.Examples._04._Lists.Scripts.Lists.Context
{
    public sealed class FloatArrayListContext : ListContext<float>
    {
        public FloatArrayListContext([NotNull] IReadOnlyList<float> data) : base(data)
        {
        }
    }
}