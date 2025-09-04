<div align="center">
  <h1>Simple User Interface</h1>
</div>

# About

Simple User Interface provides a modular, composable UI toolkit built on Unity UI and TextMeshPro. It focuses on context-driven rendering, reusable base classes, and consistent interaction patterns.

- Core object and context abstractions
- Interactable controls (buttons, toggles, sliders, input fields, scrollbars)
- Lists and selectors (carousel, dropdown, spinner, tabs)
- Windows and canvases (root/window/popup)
- Animations for show/hide
- Tooltips and notifications
- Progress, text, images, and model viewport widgets
- Drag & drop and positioning helpers

*For requirements check .asmdef*

# Quick start

1. Add a root canvas (`UIRootCanvasBase` implementation) to your scene (e.g., `UIGenericCanvas`, `UIWindowCanvas`, or `UIPopupCanvas`).
2. Create UI objects inheriting from the provided base classes and bind data via contexts.
3. Optionally attach show/hide animations and features (drag, positioning, tooltips).

# Core concepts

## Object and context model

- `UIObjectBase`: Base for all UI objects.
- `UIObjectWithContextBase<TContextType>`: Base for UI elements that render a context. Provides lifecycle hooks for applying and updating context.
- `IRenderable<T>`: Contract for components that render a value or context.
- `IWithContext<TContextType>` / `IWithLocalContext<TContextType>`: Markers for objects that carry a shared/global or local context reference.
- `UIInteractableObjectBase`: Base for interactable elements (e.g., buttons, toggles, sliders) that standardizes binding to underlying Unity components.

## Context system

- `ContextProviderBase<TContextType>`: MonoBehaviour that supplies a typed context instance.
- `IContextProvider`: Common interface for context sources.
- `ListContext<TListObject>`: Base context describing a collection and selection for list-like UI.
- `SelectableContext<TListObject>`: List context with a notion of a selected/current item.
- `TabInfoSelectableContext`: A concrete context for tab systems using `UITab` items.

## Canvases and windows

- `UIRootCanvasBase`: Base root canvas. Concrete root canvases:
  - `UIGenericCanvas`: General-purpose root for regular UI.
  - `UIWindowCanvas`: Root for windows; typically one per UI layer.
  - `UIPopupCanvas`: Root dedicated to popups.
- `UIPanelBase` / `UIPanelBaseWithContext<T>`: Panel containers for grouping UI with or without context, should be used only in very complex windows to reduce lag spikes when part of UI is being updated.
- `UIWindowBase`: Base window with show/hide lifecycle; integrates with animations and features.
- `UIPopupBase`: Specialized window base for popups.

## Animations

- `IUIAnimation`: Common interface for UI animations.
- `IUIShowAnimation` / `IUIHideAnimation`: Specializations used during show/hide phases.
- `UIAnimationBase`: Base class for implementing animations.
- Built-ins:
  - `EnableShowAnimation`: Enables object on show.
  - `DisableHideAnimation`: Disables object on hide.
  - `ScaleShowHideAnimation`: Scales in/out on show/hide.

## Text, images, progress and models

- `UITextObject`: Renders `string` context; convenient base for TextMeshPro-powered labels.
- `UISpriteObjectBase`: Renders `Sprite` context into an `Image`.
- `UIProgressBase`: Renders `float` progress values; provides normalized output for visuals.
- `UIProgressImage`: Simple progress visualization using an `Image` fill.
- `UIModelViewportBase`: Displays a `GameObject` or model in a dedicated viewport with camera/render controls.

## Interactable controls

- `UIButtonBase`: Base for button actions; wires Unity `Button` to higher-level events.
- `UIToggleBase`: Base for binary toggles; integrates with `UIToggleGroupBase` to enforce grouping rules.
- `UIToggleGroupBase`: Manages mutual exclusivity and change events across toggles.
- `UISliderBase`: Base for value sliders; exposes normalized and raw values.
- `UIScrollbar`: Base for scrollbars using Unity `Scrollbar`.
- `UIInputFieldBase`: Base for TMP input fields; surfaces text commit/update events and validation hooks.

## Lists

- `UIListBase<TListObject>` / `UIListBase<TListContext, TListObject>`: Base for list containers that bind to a `ListContext` to spawn and update `UIListElementBase` children.
- `UIListElementBase<TListObject>`: Base for per-item UI elements that render the bound list object.

## Selectors

- `UISelectorBase<TObjectType>`: List-derived selector with selection state, navigation, and change events.
- `UIAnimatedSelectorBase<TObjectType>`: Selector that supports animated transitions between items.
- `UIPreviousNextAnimatedSelectorBase<TObjectType>`: Adds previous/next navigation with animation hooks.
- Implementations:
  - `UICarouselSelectorBase<TObjectType>`: Cycles through items horizontally/vertically.
  - `UIDropdownSelectorBase<TObjectType>`: Dropdown-style selection; binds display/selected value.
  - `UISpinnerSelectorBase<TObjectType>`: Previous/next spinner with optional axis setting.
  - `UIToggleGroupSelectorBase<TObjectType>`: Selection driven by a `UIToggleGroupBase`.
  - `UITabSelectorBase`: Tabbed selector using `UITab` items with `UITabSelectorToggle` buttons.

## Tabs

- `UITabSelectorBase`: Tabbing controller built on toggle groups.
- `UITabSelectorToggle`: Toggle button specialized for driving tab selection.
- `TabInfoSelectableContext`: Context describing tab metadata and selection.

## Tooltips

- `UITooltipBase<TObject>`: Base for tooltip views that render a context when shown.
- `UITooltipFeature<TTooltipBase, TTooltipContext>`: Feature component that shows a tooltip when the pointer hovers or via explicit calls.

## Notifications

- `NotificationBase`: Abstract definition of a notification payload.
- `UINotificationDisplayBase`: Base for displaying collections of notifications (queueing, limits, rendering strategy).

## Features

### Drag & drop

- `DragFeature<TSelf>`: Adds drag behavior to an object; emits drag lifecycle callbacks.
- `DraggableWindowFeature`: Ready-to-use drag feature for windows.
- `DropZoneFeature<TFeature>`: Area capable of accepting drops of a specific drag feature type.
- `SlotFeature<TDragFeature>`: Specialization of drop zone for slot-based UIs (e.g., inventory slot).

### Positioning

- `LimitObjectToParent`: Keeps an object within the bounds of its parent rect.
- `LimitObjectToViewport`: Keeps an object within the UI viewport bounds.

## Scrolling

- `UIScrollbar`: Base for scrollbars with value events and binding helpers.

## Windows lifecycle

- Windows derived from `UIWindowBase` support show/hide with optional animations via `IUIShowAnimation`/`IUIHideAnimation`.
- Popups derive from `UIPopupBase` and are typically spawned on a `UIPopupCanvas` root.

## Utility

- `Utility.UserInterface`: Static helpers for common UI tasks (e.g., safe set active, find components, or other misc helpers exposed by the package).

# Using built-in objects

Below are minimal steps to drop common controls into a scene. Add the relevant component to a GameObject that has the expected Unity UI component and wire your logic via overrides or context.

## Button (`UIButtonBase`)

```csharp
// Derive and override click
using Systems.SimpleUserInterface.Components.Buttons;
using UnityEngine;

public sealed class LogHelloWorldButton : UIButtonBase
{
    protected override void OnClick()
    {
        Debug.Log("Hello World!");
    }
}
```

## Progress (`UIProgressBase` / `UIProgressImage`)

```csharp
// Provide progress via local context [0..1]
using Systems.SimpleUserInterface.Components.Abstract.Markers.Context;
using Systems.SimpleUserInterface.Components.Progress;
using UnityEngine;

public sealed class UIProgressExample : UIProgressBase, IWithLocalContext<float>
{
    [SerializeField, Range(0,1)] private float progressValue = 0.5f;
    public bool TryGetContext(out float context)
    {
        context = progressValue; // normalize 0..1
        return true;
    }
}
```

Note: Attach to the GameObject that drives your progress visuals (images, text). For simple image fills, add `UIProgressImage` to child `Image` objects and set their Image Type to `Filled`.

## Lists (`UIListBase<>` + `UIListElementBase<>`)

```csharp
// Context describing list contents
using System.Collections.Generic;
using Systems.SimpleUserInterface.Context.Lists;

public sealed class FloatArrayListContext : ListContext<float>
{
    public FloatArrayListContext(IReadOnlyList<float> data) : base(data) { }
}

// Per-item renderer
using Systems.SimpleUserInterface.Components.Abstract.Markers;
using Systems.SimpleUserInterface.Components.Lists;
using TMPro;
using UnityEngine;

public sealed class ExampleFloatListElement : UIListElementBase<float>, IRenderable<float>
{
    [SerializeField] private TextMeshProUGUI _text;
    public void OnRender(float withContext)
    {
        _text.text = withContext.ToString();
    }
}

// List
public sealed class ExampleFloatList : UIListBase<float> // or UIListBase<FloatArrayListContext, float>
{
        
    
}
```

Note: Create a `UIListBase<float>`-derived controller, assign an element prefab that derives from `UIListElementBase<T>`, and provide a `FloatArrayListContext` (via `ContextProviderBase<FloatArrayListContext>` or directly). The list will spawn and render items from the context.

Tip: You can change the list's container to control where elements are instantiated in the hierarchy.

Tip: Context sub-objects are recognized automatically, so `FloatArrayListContext` is will be also provided if
`ListContext<float>` is requested.

## Toggles (`UIToggleBase`, `UIToggleGroupBase`)

Requirements: GameObjects with `Toggle` for each item; use `UIToggleBase` on items and optionally `UIToggleGroupBase` on a parent to manage exclusivity.

```csharp
using Systems.SimpleUserInterface.Components.Toggles;
using UnityEngine;

public sealed class ExampleToggleGroup : UIToggleGroupBase
{
    protected override void OnToggleValueChanged(int toggleIndex, bool newValue)
    {
        if (!newValue) return; // handle only selection
        Debug.Log("Selected toggle index: " + toggleIndex);
    }
}
```

Note: Place `ExampleToggleGroup` on a container; child toggles derive from `UIToggleBase`. For a single toggle, you can omit a group and use a standalone `UIToggleBase`.

## Input field (`UIInputFieldBase`)

Requirements: GameObject with `TMP_InputField` and a `UIInputFieldBase`-derived component.

```csharp
using Systems.SimpleUserInterface.Components.InputFields;
using UnityEngine;

public sealed class ExampleInputField : UIInputFieldBase
{
    protected override void OnFieldSubmitted(string withText)
    {
        Debug.Log($"Submitted text: {withText}");
    }

    protected override void OnFieldEndEdited(string currentText)
    {
        Debug.Log($"Current field text: {currentText}");
    }
}
```

See more examples in `Examples/*X*/Scripts/`.

# Creating custom objects

Derive from the appropriate base class and implement minimal hooks:

- Display-only: derive from `UIObjectWithContextBase<T>` and implement `IRenderable<T>` to render the context.
- Interactable: derive from `UIInteractableObjectBase` subclasses and override event methods (e.g., `OnClick`, `OnValueChanged`).
- Lists/Selectors: implement an element type (`UIListElementBase<T>` + `IRenderable<T>`) and bind a `ListContext<T>` or `SelectableContext<T>`.

# Creating complex rendering objects

For composite views that render multiple values, implement `IRenderable.Render()` explicitly. In many cases you can implement strongly typed `IRenderable<T>` interfaces (for each type you render) and avoid manual plumbing, but the explicit approach gives you full control when combining contexts.

Example: a labeled sprite view

```csharp
using UnityEngine;

public struct SpriteWithLabel
{
    public Sprite Sprite { get; set; }
    public string Label { get;  set; }
}
```


```csharp
using Systems.SimpleUserInterface.Components.Abstract.Markers;
using Systems.SimpleUserInterface.Components.Images;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class IconWithLabel : UIObjectWithContextBase<SpriteWithLabel>,
    IWithLocalContext<string>, IWithLocalContext<Sprite>,
    IRenderable<string>, IRenderable<Sprite>
{
    IContextProvider IWithContext<string>.CachedContextProvider { get; set; }
    IContextProvider IWithContext<Sprite>.CachedContextProvider { get; set; }

    public void OnRender(string withContext) { /* render label */ }
    
    public void OnRender(Sprite withContext) { /* render sprite */ }

    void IRenderable.Render()
    {
        IWithContext withContext = this;
        OnRender(withContext.ProvideContext<string>());
        OnRender(withContext.ProvideContext<Sprite>());
    }

    public bool TryGetContext(out string context)
    {
        IWithContext withContext = this;
        context = withContext.ProvideContext<SpriteWithLabel>().Label;
        return !string.IsNullOrEmpty(context);
    }

    public bool TryGetContext(out Sprite context)
    {
        IWithContext withContext = this;
        context = withContext.ProvideContext<SpriteWithLabel>().Sprite;
        return context is not null;
    }
}
```

# Common usage patterns

- **Bind data via contexts**: Prefer `UIObjectWithContextBase<T>` and `ContextProviderBase<T>` to decouple data from views.
- **Compose behavior with features**: Add tooltip, drag, and positioning features as separate components to keep views focused.
- **Prefer base classes**: Derive from the appropriate base (e.g., `UIButtonBase`, `UIListElementBase<T>`) to inherit consistent wiring and events.
- **Use selectors for choice UIs**: Build selection UIs on top of selector bases to get navigation, change events, and animations.
- **Attach animations**: Add one or more `IUIShowAnimation`/`IUIHideAnimation` components to control the entry/exit of panels and windows.

# Notes

- Base classes encapsulate Unity component dependencies through `[RequireComponent]` where applicable to reduce setup errors.
- Context-driven APIs minimize direct references between scripts and UI prefabs, improving reusability.
- List and selector abstractions are designed to handle dynamic content and selection changes efficiently.
- Keep custom components small and single-purpose. Favor composition over complex inheritance chains.


