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

- `UIObjectBase`: Base for all UI objects. Implements pointer click and optional drag handlers.
- `UIObjectWithContextBase<TContextType>`: Base for UI elements that render a context. Provides lifecycle hooks for applying and updating context.
- `IRenderable<T>`: Contract for components that render a value or context locally.
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
- `UIPanelBase` / `UIPanelBaseWithContext<T>`: Panel containers for grouping UI with or without context.
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

- Windows derived from `UIWindowBase` support show/hide with optional animations via `IUIShowAnimation`/`IUIHideAnimation` components attached to the same GameObject or children.
- Popups derive from `UIPopupBase` and are typically spawned on a `UIPopupCanvas` root.

## Utility

- `Utility.UserInterface`: Static helpers for common UI tasks (e.g., safe set active, find components, or other misc helpers exposed by the package).

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


