# SimpleUI

A lightweight, modular UI framework for Unity that provides reusable components and patterns for building user interfaces with minimal boilerplate. Built on top of Unity's Canvas and EventSystem, SimpleUI streamlines common UI tasks like window management, list rendering, animations, and interactive elements.

## Requirements

### Dependencies
- **Unity 2022.3+** (uses TextMeshPro)
- **SimpleCore** (internal dependency)
- **DOTween** (for UI animations)
- **Unity.Addressables** (for asset management)
- **TextMeshPro** (for text rendering)
- **Burst & Collections** (optional, for performance)

### Assembly Definition
- `SimpleUserInterface.asmdef` - Main assembly with unsafe code enabled

## Usage

### Basic Components

#### Creating Custom Buttons
```csharp
using Systems.SimpleUI.Components.Buttons;
using UnityEngine;

public sealed class MyCustomButton : UIButtonBase
{
    protected override void OnClick()
    {
        Debug.Log("Button clicked!");
    }
}
```
Simply inherit from `UIButtonBase`, implement `OnClick()`, add a `Button` component to your GameObject, and you're ready to go.

#### Working with Windows
```csharp
using Systems.SimpleUI.Components.Windows;
using Systems.SimpleUI.Utility;

// Open a window
UserInterface.OpenWindow<MyWindowType>();

// Open with context
UserInterface.OpenWindow<MyWindowType>(context: myData);

// Open as dependent of another window
UserInterface.OpenWindow<MyWindowType>(parentWindow: parentWindow);
```
Windows are cached after first creation and can be configured to allow single or multiple instances.

#### Creating Custom Text Elements
```csharp
using Systems.SimpleUI.Components.Text;

public sealed class DynamicLabel : UITextObject
{
    // Inherits from UIObjectWithContextBase<string>
    // Automatically renders text from string context
    public override void OnRender(string withContext)
    {
        base.OnRender(withContext);
        // Additional custom rendering logic here
    }
}
```

### Advanced Components

#### Lists and Dynamic Content
```csharp
using Systems.SimpleUI.Components.Lists;
using Systems.SimpleUI.Context.Lists;

public sealed class MyList : UIListBase<MyDataType>
{
    // Automatically handles element pooling and rendering
    // Inherits IRenderable to receive context updates
}
```
Lists automatically pool and recycle elements for efficient rendering of large datasets.

#### Context-Driven UI
```csharp
using Systems.SimpleUI.Context.Abstract;

public sealed class MyContextProvider : ContextProviderBase
{
    public override bool TryProvideContext<TContextType>(out TContextType context)
    {
        // Provide context to UI elements that request it
    }
}
```
Use context providers to pass data to UI elements without tight coupling.

### Animations

#### Adding Show/Hide Animations
```csharp
using Systems.SimpleUI.Components.Animations;

// Built-in animations:
// - ScaleShowHideAnimation: Scales element in/out
// - EnableShowAnimation: Simply activates GameObject
// - DisableHideAnimation: Simply deactivates GameObject
```
Attach animation components to your UI objects. They automatically integrate with the show/hide lifecycle.

#### Custom Animations
```csharp
using Systems.SimpleUI.Components.Animations.Abstract;
using DG.Tweening;

public sealed class MyCustomAnimation : UIAnimationBase, IUIShowAnimation, IUIHideAnimation
{
    public Sequence OnShow()
    {
        // Return a DOTween Sequence for show animation
    }

    public Sequence OnHide()
    {
        // Return a DOTween Sequence for hide animation
    }
}
```

### Interactive Features

#### Drag and Drop
```csharp
using Systems.SimpleUI.Components.Features.Drag;

// Attach DragFeature to make objects draggable
// Attach DropZoneFeature to define drop targets
// Use SlotFeature for inventory-style slot systems
```

#### Positioning Constraints
```csharp
using Systems.SimpleUI.Components.Features.Positioning;

// LimitObjectToParent: Keeps UI within parent bounds
// LimitObjectToViewport: Keeps UI within screen bounds
```

### Common Patterns

#### Managing Multiple Windows
Windows are automatically managed with sorting orders:
- Regular windows: `UI_WINDOW_SORTING_ORDER = 15000`
- Popups: `UI_POPUP_SORTING_ORDER = 20000`
- Overlays: `UI_OVERLAY_SORTING_ORDER = 25000`
- Tooltips: `UI_TOOLTIP_SORTING_ORDER = 30000`

#### Responsive Rendering
```csharp
using Systems.SimpleUI.Components.Abstract.Markers;

// Implement IRenderable<TContext> to respond to context changes
public class MyElement : UIObjectWithContextBase<MyContext>, IRenderable<MyContext>
{
    public void OnRender(MyContext withContext)
    {
        // Update UI based on context
    }
}
```

## Examples

The `Examples/` directory includes demonstrations of:
- Text and input fields (00)
- Progress bars (01)
- Window systems (02)
- List rendering (03)
- Drag and drop (04)
- Tab systems (05)
- Tooltips (06)
- 3D model viewports (07)

## Architecture

SimpleUI follows a component-based architecture:
- **Abstract Base Classes**: `UIObjectBase`, `UIInteractableObjectBase`, `UIObjectWithContextBase` provide core functionality
- **Markers/Interfaces**: `IRenderable`, `IWithContext` define behavior contracts
- **Context System**: Decouples data from presentation through context providers
- **Animation System**: Extensible animation framework using DOTween sequences
- **Window Management**: Centralized window lifecycle and focus management via `UserInterface` utility class

## License

Copyright 2025 H1M4W4R1 - MIT License
