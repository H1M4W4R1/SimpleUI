using Systems.SimpleUserInterface.Abstract.Markers.Context;
using Systems.SimpleUserInterface.Base.Text;
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
        string IWithLocalContext<string>.GetContext() => Application.version;
    }
}