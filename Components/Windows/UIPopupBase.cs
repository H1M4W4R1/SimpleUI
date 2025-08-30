using System.Collections.Generic;
using JetBrains.Annotations;
using Systems.SimpleUserInterface.Data;
using Systems.SimpleUserInterface.Utility;
using UnityEngine.Assertions;

namespace Systems.SimpleUserInterface.Components.Windows
{
    /// <summary>
    ///     Popup window to handle notifications and other weird stuff
    /// </summary>
    public abstract class UIPopupBase : UIWindowBase
    {
        public sealed override bool AllowMultipleInstancesWithDifferentContext => false;

        public sealed override bool AllowMultipleInstancesWithSameContext => false;

        protected internal override void OnWindowClosed()
        {
            base.OnWindowClosed();
            
            // Open next popup in queue if any
            UserInterface.TryOpenNextPopup();
        }
        
    }
}