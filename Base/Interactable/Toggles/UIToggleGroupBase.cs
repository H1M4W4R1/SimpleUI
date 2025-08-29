using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using Systems.SimpleUserInterface.Abstract.Objects.Interactable;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.SimpleUserInterface.Base.Interactable.Toggles
{
    [RequireComponent(typeof(ToggleGroup))] [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIToggleGroupBase : UIInteractableObjectBase
    {
        protected ToggleGroup toggleGroupReference;
        protected CanvasGroup canvasGroupReference;

        public bool IsInteractable => canvasGroupReference.interactable;

        /// <summary>
        ///     List of all toggles in this toggle group
        /// </summary>
        public List<UIToggleBase> Toggles { get; } = new();

        /// <summary>
        ///     Check if at least one toggle must be active
        /// </summary>
        public bool RequireAtLeastOneActive
        {
            get => !toggleGroupReference.allowSwitchOff;
            protected set => toggleGroupReference.allowSwitchOff = !value;
        }

        protected override void AssignComponents()
        {
            base.AssignComponents();
            toggleGroupReference = GetComponent<ToggleGroup>();
            canvasGroupReference = GetComponent<CanvasGroup>();

            // Register all toggles in this toggle group
            RefreshToggleArray();
        }

        /// <summary>
        ///     Sets the interactable state of the toggle group
        /// </summary>
        public override void SetInteractable(bool interactable) =>
            canvasGroupReference.interactable = interactable;

        /// <summary>
        ///     Method to update the toggle array and register all toggles in this toggle group
        /// </summary>
        internal void RefreshToggleArray()
        {
            // Clear toggle array
            Toggles.Clear();

            // Add all toggles in this toggle group to the toggle array
            Toggles.AddRange(GetComponentsInChildren<UIToggleBase>());

            // Register toggle to toggle group
            for (int i = 0; i < Toggles.Count; i++)
            {
                Toggle toggle = Toggles[i].ToggleReference;
                if (toggle.group != toggleGroupReference) toggle.group = toggleGroupReference;
            }
        }

        /// <summary>
        ///     Checks if a toggle is toggled
        /// </summary>
        /// <param name="index">Index of the toggle</param>
        /// <returns>True if the toggle is toggled, false otherwise (or when the index is out of range)</returns>
        public bool IsToggled(int index)
        {
            Assert.IsFalse(index < 0 || index >= Toggles.Count, "Toggle index out of range");
            if (index < 0 || index >= Toggles.Count) return false;
            return Toggles[index].IsToggled;
        }

        /// <summary>
        ///     Changes the state of a toggle
        /// </summary>
        /// <param name="index">Index of the toggle</param>
        /// <param name="value">Value to set </param>
        public void SetToggled(int index, bool value)
        {
            Assert.IsFalse(index < 0 || index >= Toggles.Count, "Toggle index out of range");
            if (index < 0 || index >= Toggles.Count) return;
            Toggles[index].IsToggled = value;
        }

        /// <summary>
        ///     Event that is called when a toggle value changes
        /// </summary>
        /// <param name="toggleIndex">Index of the toggle that changed</param>
        /// <param name="newValue">New value of the toggle</param>
        protected abstract void OnToggleValueChanged(int toggleIndex, bool newValue);

        /// <summary>
        ///     Handles the toggle changed event
        /// </summary>
        internal void OnToggleChanged([NotNull] UIToggleBase uiToggleBase, bool newValue)
        {
            Assert.Contains(uiToggleBase, Toggles, "Toggle is not registered in this toggle group");
            OnToggleValueChanged(Toggles.IndexOf(uiToggleBase), newValue);
        }
    }
}