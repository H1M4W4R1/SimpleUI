using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using Systems.SimpleCore.Automation.Attributes;
using Systems.SimpleUserInterface.Data;
using UnityEngine;

namespace Systems.SimpleUserInterface.Base.Windows
{
    /// <summary>
    ///     Represents a user interface window
    /// </summary>
    [AutoAddressableObject("UI Windows", "SimpleUI.Windows")] [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIWindow : UIPanel
    {
        protected CanvasGroup canvasGroupReference;

        /// <summary>
        ///     Order of regular windows, max 5K
        /// </summary>
        public const int WINDOW_SORTING_ORDER = 15000;

        /// <summary>
        ///     Order of popups (always on top), max 5K
        /// </summary>
        public const int POPUP_SORTING_ORDER = 20000;

        /// <summary>
        ///     Anything that should overlay windows (e.g. custom sprite-based mouse cursor)
        /// </summary>
        public const int WINDOW_OVERLAY_SORTING_ORDER = 25000;

        /// <summary>
        ///     If true, multiple instances of this window are allowed
        /// </summary>
        /// <remarks>
        ///     True overrides value of <see cref="AllowMultipleInstancesWithDifferentContext"/> to also
        ///     be true automatically
        /// </remarks>
        public virtual bool AllowMultipleInstancesWithSameContext => false;

        /// <summary>
        ///     If true, multiple instances of this window are allowed with different context
        /// </summary>
        public virtual bool AllowMultipleInstancesWithDifferentContext => true;

        /// <summary>
        ///     Context of this window
        /// </summary>
        protected object WindowContext { get; private set; }

        /// <summary>
        ///     List of all open windows
        /// </summary>
        [NotNull] [ItemNotNull] protected static List<UIWindow> OpenWindows { get; } = new();

        /// <summary>
        ///     List of closed windows that are hidden from user view but cached for re-use
        /// </summary>
        [NotNull] [ItemNotNull] protected static List<UIWindow> ClosedWindows { get; } = new();

        /// <summary>
        ///     List of all windows that are dependent on this window
        /// </summary>
        [NotNull] [ItemNotNull] protected List<UIWindow> Dependents { get; } = new();

        /// <summary>
        ///     Focuses this window
        /// </summary>
        public void Focus()
        {
            // Set window in OpenWindows to last (quick swap)
            OpenWindows.Remove(this);
            OpenWindows.Add(this);

            // Sort windows
            SortAll();
        }

        /// <summary>
        ///     Sorts all open windows
        /// </summary>
        public static void SortAll()
        {
            // Get popup and window index to sort windows sequentially based
            // on their type to make sure popups are always on top
            int popupIndex = POPUP_SORTING_ORDER;
            int windowIndex = WINDOW_SORTING_ORDER;

            // Sort windows automatically
            for (int i = 0; i < OpenWindows.Count; i++)
            {
                if (OpenWindows[i] is UIPopup)
                {
                    OpenWindows[i].SetSortingOrder(popupIndex);
                    popupIndex++;
                }
                else
                {
                    OpenWindows[i].SetSortingOrder(windowIndex);
                    windowIndex++;
                }
            }
        }

#region Opening and closing windows

        /// <summary>
        ///     Check if this window can be closed
        /// </summary>
        public virtual bool CanBeClosed => true;

        /// <summary>
        ///     Check if this window can be opened, should be instance-independent
        /// </summary>
        public virtual bool CanBeOpened => true;

        private void _DisableWindow()
        {
            gameObject.SetActive(false);
            canvasGroupReference.interactable = false; // Gamepad fix
        }

        private void _EnableWindow()
        {
            canvasGroupReference.interactable = true; // Gamepad fix
            gameObject.SetActive(true);
        }

        /// <summary>
        ///     Checks if a window can be closed (including dependents)
        /// </summary>
        private static bool _CanCloseWindow([NotNull] UIWindow window)
        {
            // Check if window can be closed
            if (!window.CanBeClosed) return false;

            // Check if all dependents can be closed
            for (int i = 0; i < window.Dependents.Count; i++)
            {
                if (!window.Dependents[i].CanBeClosed) return false;
            }

            return true;
        }

        /// <summary>
        ///     Closes this window
        /// </summary>
        /// <param name="force">Force close window</param>
        /// <returns>True if window was closed, false if it was not</returns>
        public virtual bool Close(bool force = false) => CloseWindow(this, force);

        /// <summary>
        ///     Closes all dependents of this window
        /// </summary>
        /// <param name="force">Force to close window</param>
        /// <returns>Count of windows closed, -1 if any window could not be closed</returns>
        public int CloseAllDependents(bool force = false) =>
            CloseAllDependents<UIWindow>(force);

        /// <summary>
        ///     Closes all dependents of this window of specified type
        /// </summary>
        /// <typeparam name="TWindowType">Type of window to close</typeparam>
        /// <param name="force">Force to close window</param>
        /// <returns>Count of windows closed, -1 if any window could not be closed</returns>
        public int CloseAllDependents<TWindowType>(bool force = false)
            where TWindowType : UIWindow
        {
            // Check if all dependents can be closed
            for (int i = 0; i < Dependents.Count; i++)
            {
                if (Dependents[i] is not TWindowType) continue;
                if (!_CanCloseWindow(Dependents[i]) && !force) return -1;
            }

            // Close all dependents
            int nWindowsClosed = 0;
            for (int i = 0; i < Dependents.Count; i++)
            {
                if (Dependents[i] is not TWindowType) continue;
                CloseWindow(Dependents[i], force);
                nWindowsClosed++;
            }

            return nWindowsClosed;
        }

        /// <summary>
        ///     Closes all open windows
        /// </summary>
        /// <param name="force">Force to close windows</param>
        /// <returns>Count of windows closed, -1 if any window could not be closed</returns>
        public static int CloseAll(bool force = false) => CloseAll<UIWindow>(force);

        /// <summary>
        ///     Closes all open windows
        /// </summary>
        /// <typeparam name="TWindowType">Type of window to close</typeparam>
        /// <param name="force">Force to close windows</param>
        /// <returns>Count of windows closed, -1 if any window could not be closed</returns>
        public static int CloseAll<TWindowType>(bool force = false)
            where TWindowType : UIWindow
        {
            int nWindowsClosed = 0;

            // Get all open windows
            for (int i = 0; i < OpenWindows.Count; i++)
            {
                if (OpenWindows[i] is not TWindowType) continue;
                if (!_CanCloseWindow(OpenWindows[i]) && !force) return -1;
            }

            // Close all windows
            for (int i = 0; i < OpenWindows.Count; i++)
            {
                if (OpenWindows[i] is not TWindowType) continue;
                CloseWindow(OpenWindows[i]);

                nWindowsClosed++;
            }

            return nWindowsClosed;
        }

        /// <summary>
        ///     Closes a window
        /// </summary>
        /// <param name="window">Window to close</param>
        /// <param name="force">Force close window</param>
        /// <returns>True if window was closed, false if it was not</returns>
        public static bool CloseWindow([NotNull] UIWindow window, bool force = false)
        {
            if (!_CanCloseWindow(window) && !force) return false;

            // Close all dependents
            for (int i = 0; i < window.Dependents.Count; i++)
            {
                UIWindow dependentWindow = window.Dependents[i];
                CloseWindow(dependentWindow, force);
            }

            // Close window (move to closed list)
            window._DisableWindow();
            OpenWindows.Remove(window);
            ClosedWindows.Add(window);
            return true;
        }

        /// <summary>
        ///     Opens a dependent window for this window
        /// </summary>
        /// <typeparam name="TWindowType">Window to open</typeparam>
        /// <param name="force">Force to open window</param>
        /// <param name="context">Context to pass to window</param>
        /// <returns>True if window was opened, false if it was not</returns>
        public bool OpenDependentWindow<TWindowType>(bool force = false, [CanBeNull] object context = null)
            where TWindowType : UIWindow => OpenWindow<TWindowType>(this, force, context);

        /// <summary>
        ///     Opens a window
        /// </summary>
        /// <param name="parentWindow">Parent window to open this window as dependent of</param>
        /// <param name="force">Force to open window</param>
        /// <param name="context">Context to pass to window</param>
        /// <typeparam name="TWindowType">Window type to open</typeparam>
        /// <returns>True if window was opened, false if it was not</returns>
        public static bool OpenWindow<TWindowType>(
            [CanBeNull] UIWindow parentWindow = null,
            bool force = false,
            [CanBeNull] object context = null)
            where TWindowType : UIWindow
        {
            TWindowType window = WindowsDatabase.GetFast<TWindowType>();
            Assert.IsNotNull(window, $"Window {typeof(TWindowType).Name} not found in database");
            return OpenWindow(window, parentWindow, force, context);
        }

        /// <summary>
        ///     Opens a window
        /// </summary>
        /// <param name="windowPrefab">Window prefab to open</param>
        /// <param name="parentWindow">Window to open this window as dependent of</param>
        /// <param name="force">Force to open window</param>
        /// <param name="context">Context to pass to window</param>
        /// <returns>True if window was opened, false if it was not</returns>
        public static bool OpenWindow(
            [NotNull] UIWindow windowPrefab,
            [CanBeNull] UIWindow parentWindow = null,
            bool force = false,
            [CanBeNull] object context = null)
        {
            // Check if window can be opened
            if (!windowPrefab.CanBeOpened && !force) return false;

            // Check if there is any instance with same context and forbid opening
            // This is to prevent opening multiple windows with same context
            if (!windowPrefab.AllowMultipleInstancesWithSameContext && !force)
            {
                for (int i = 0; i < OpenWindows.Count; i++)
                {
                    // Check if window is the same type with same context
                    UIWindow window = OpenWindows[i];
                    if (window.GetType() != windowPrefab.GetType()) continue;
                    if (window.WindowContext == context) return false;

                    // If window does not allow multiple instances with different context, forbid opening
                    // otherwise continue to next window
                    if (!windowPrefab.AllowMultipleInstancesWithDifferentContext) return false;
                }
            }

            // Find created disabled window instance
            UIWindow windowInstance = null;
            for (int closedWindowIndex = 0; closedWindowIndex < ClosedWindows.Count; closedWindowIndex++)
            {
                UIWindow window = ClosedWindows[closedWindowIndex];
                // Check if window is the same type
                if (window.GetType() != windowPrefab.GetType()) continue;
                windowInstance = window;

                // Remove from closed windows
                ClosedWindows.RemoveAt(closedWindowIndex);
                break;
            }

            // Create window instance if not found
            if (!windowInstance) windowInstance = Instantiate(windowPrefab);

            // Add window to open windows and enable to ensure GameObject is active
            OpenWindows.Add(windowInstance);
            windowInstance._EnableWindow();

            // Set window context
            windowInstance.WindowContext = context;

            // Set parent window
            if (parentWindow) parentWindow.Dependents.Add(windowInstance);

            // Return true
            return true;
        }

#endregion

#region Core Override

        protected override void AssignComponents()
        {
            base.AssignComponents();
            canvasGroupReference = GetComponent<CanvasGroup>();
        }

#endregion
    }
}