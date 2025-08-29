using JetBrains.Annotations;
using Systems.SimpleCore.Storage;
using Systems.SimpleUserInterface.Base.Windows;

namespace Systems.SimpleUserInterface.Data
{
    /// <summary>
    ///     Database with all known User Interface Windows
    /// </summary>
    public sealed class WindowsDatabase : AddressableDatabase<WindowsDatabase, UIWindow>
    {
        [NotNull] protected override string AddressableLabel => "SimpleUI.Windows";
    }
}