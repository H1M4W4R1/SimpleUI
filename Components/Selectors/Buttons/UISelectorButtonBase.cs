using NUnit.Framework;
using Systems.SimpleUserInterface.Components.Buttons;
using Systems.SimpleUserInterface.Components.Selectors.Abstract;
using UnityEngine;

namespace Systems.SimpleUserInterface.Components.Selectors.Buttons
{
    public abstract class UISelectorButtonBase : UIButtonBase
    {
        protected IPreviousNextSelector Selector { get; private set; }

        protected override void AssignComponents()
        {
            base.AssignComponents();
            Transform parent = transform.parent;
            Assert.IsNotNull(parent, "SelectorNextButton must be a sibling of a selector");
            Selector = parent.GetComponentInChildren<IPreviousNextSelector>();
            Assert.IsNotNull(Selector, "SelectorNextButton must be a sibling of a selector");
        }
    }
}