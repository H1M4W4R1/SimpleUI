using Systems.SimpleUserInterface.Components.Objects.Markers.Context;
using Systems.SimpleUserInterface.Components.Text;
using UnityEngine;

namespace Systems.SimpleUserInterface.Examples.Objects.Text
{
    /// <summary>
    ///     Component used to display build version of the application
    /// </summary>
    public sealed class StaticBuildVersionText : UITextObject, IWithLocalContext<string>
    {
        /// <summary>
        ///     Gets the build version
        /// </summary>
        public bool TryGetContext(out string context)
        {
            context = Application.version;
            return true;
        }
    }
}