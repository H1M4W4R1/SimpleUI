using System.Collections.Generic;
using JetBrains.Annotations;
using Systems.SimpleUserInterface.Data;
using UnityEngine.Assertions;

namespace Systems.SimpleUserInterface.Base.Windows
{
    /// <summary>
    ///     Popup window to handle notifications and other weird stuff
    /// </summary>
    public abstract class UIPopup : UIWindow
    {
        /// <summary>
        ///     Queue of popups to open
        /// </summary>
        protected static readonly Queue<UIPopup> popupQueue = new();

        public sealed override bool AllowMultipleInstancesWithDifferentContext => false;

        public sealed override bool AllowMultipleInstancesWithSameContext => false;

        protected override void OnWindowClosed()
        {
            base.OnWindowClosed();
            
            // Open next popup in queue if any
            UIPopup popup = popupQueue.Dequeue();
            OpenPopup(popup);
        }

        /// <summary>
        ///     Opens a popup
        /// </summary>
        /// <typeparam name="TPopupType">Type of popup to open</typeparam>
        /// <returns>True if popup was opened (or queued), false if it was not</returns>
        public static bool OpenPopup<TPopupType>()
            where TPopupType : UIPopup, new()
        {
            // Get popup from database
            UIPopup popup = WindowsDatabase.GetExact<TPopupType>();
            Assert.IsNotNull(popup, $"Popup {typeof(TPopupType).Name} not found in database");
            
            return OpenPopup(popup);
        }

        /// <summary>
        ///     Opens a popup
        /// </summary>
        /// <param name="popup">Popup to open</param>
        /// <returns>True if popup was opened (or queued), false if it was not</returns>
        private static bool OpenPopup([NotNull] UIPopup popup)
        {
            // Check if any popup is open and forbid opening
            for (int nWindow = 0; nWindow < OpenWindows.Count; nWindow++)
            {
                // Skip if not popup
                if (OpenWindows[nWindow] is not UIPopup) continue;
                
                // If popup is open, add to queue
                popupQueue.Enqueue(popup);
                return true;
            }
            
            // No popups are open, show popup
            return OpenWindow(popup);
        }
        
        
        
    }
}