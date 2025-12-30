# Mobile Puzzle Game - Monetization Study

A logic-based mobile puzzle game developed in Unity to investigate the impact of monetization strategies on player experience. This project features two distinct versions: a monetized version with reward-based video ads and a non-monetized version with limited hints.

## Project Overview

This Unity-based mobile game presents players with 10 progressively challenging puzzle levels, each testing different cognitive skills including pattern recognition, logic deduction, and spatial reasoning. The game was developed as part of a research study to analyze how optional monetization affects player engagement and satisfaction.

**Key Features:**
- 10 unique puzzle levels with escalating difficulty
- Dual-version architecture (monetized vs. non-monetized)
- Voluntary reward-based ad system
- Hint and level skip mechanics
- Comprehensive scene management system

## Technical Stack

- **Engine:** Unity 3.10.0
- **Language:** C#
- **Platform:** iOS (via Unity Remote 5)
- **UI Framework:** Unity UI with TextMeshPro
- **Video Integration:** Unity Video Player

## Repository Structure

This repository contains the `Assets` folder from the Unity project, including all scripts, scenes, prefabs, and resources.

**Folder Organization:**
- **Scenes/** - 10 puzzle levels + UI scenes
- **Scripts/** - All C# gameplay and system scripts
  - HintManagement/ - HintManager.cs, HintManager1.cs, etc.
  - PuzzleMechanics/ - Drag, touch, input interaction scripts
  - Utility/ - Scene loading, video playback
- **Prefabs/** - UI elements, game objects
- **Resources/** - Video ads, images, assets

**To run this project:**
1. Create a new Unity project (Unity 2021.3+ recommended)
2. Clone this repository
3. Copy the `Assets` folder into your Unity project directory
4. Open the project in Unity
5. Navigate to `Assets/Scenes/` and load the first level scene
6. Press Play to test in the Unity Editor

**Note:** The `Library`, `Temp`, and other auto-generated Unity folders are excluded from this repository. Unity will regenerate these when you open the project.

## Project Structure

### Core Systems

#### **Hint Management System**
- `HintManager.cs` - Non-monetized version with fixed hint allocation (6 hints)
- `HintManager1.cs` - Monetized version with ad-reward functionality (unlimited hints via ads)
- `HintInOtherScenes.cs` / `HintInOtherScenes1.cs` - Scene-specific hint UI synchronization
- `HintManagerOtherScenes.cs` - Advanced scene persistence with full ad integration

**Key Features:**
- Singleton pattern for persistent hint tracking across scenes
- Scene-independent hint counter with DontDestroyOnLoad
- Confirmation dialogs for hint usage and level skipping
- Video ad integration with reward validation

#### **Monetization System**
The monetized version implements a voluntary reward-based ad system:
- Players watch 17-second unskippable video ads
- Each ad completion grants +1 hint
- Ad prompt only appears when hints are depleted
- VideoPlayer integration with callback handling
```csharp
// Example: Ad completion reward flow
private void OnVideoFinished(VideoPlayer vp)
{
    hintsAvailable++; 
    UpdateHintText();
    videoPanel.SetActive(false);
}
```

### Puzzle Mechanics

The game features diverse interaction patterns across 10 levels:

| Level | Mechanic | Script |
|-------|----------|--------|
| 1-2 | Basic Tapping | `Level1.cs`, `ButtonHandler.cs` |
| 3-4 | Drag and Drop | `ImageDragCollide.cs`, `DragAndReturnImage.cs` |
| 5 | Rapid Clicking | `RapidClicksLoad.cs` |
| 6 | Press and Hold | `ObjectHold.cs` |
| 7 | Multi-Touch | `TwoObjectHold.cs` |
| 8 | Pinch to Zoom | `ResizeAndLoad.cs`, `DragAndResize.cs` |
| 9 | Logic + Input | `CounterControl.cs` |
| 10 | Text Input Puzzle | `InputSubmit.cs` |

#### **Interaction Scripts**

**Drag & Drop System:**
- `ImageDrag.cs` - Basic UI element dragging
- `ImageDragCollide.cs` - Drag-to-target with collision detection and scene transition
- `DragAndReturnImage.cs` - Drag with automatic return to original position
- `DraggedObjectDisappears.cs` - Drag-to-hide mechanic with overlap detection

**Touch Gestures:**
- `TwoObjectHold.cs` - Simultaneous two-object press detection
- `ObjectHold.cs` - Timed hold-to-progress mechanic
- `RapidClicksLoad.cs` - Rapid click detection with time interval validation
- `ResizeAndLoad.cs` - Pinch-to-zoom with scale threshold triggering

**Input Validation:**
- `InputSubmit.cs` - Text input validation with mobile keyboard support
- `CounterControl.cs` - Counter-based puzzle with increment/decrement logic

### Utility Systems

**Scene Management:**
- `LoadNextScene.cs` - Sequential scene progression
- `LoadHomepage.cs` / `LoadLevel11.cs` / `LoadEnd.cs` - Specific scene loaders
- `ReloadScene.cs` - Current scene reload functionality

**Video System:**
- `CanvasVideoPlayer.cs` - Canvas-based video rendering
- `VideoController.cs` - Video playback state management

**Level Reset:**
- `ResetLevel.cs` - Tag-based object position reset system

## Core Technical Implementations

### 1. Persistent Data Management
Uses Unity's `DontDestroyOnLoad` to maintain hint count across scene transitions:
```csharp
void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);
    }
}
```

### 2. Touch Input Handling
Implements Unity's Event System for mobile-optimized interactions:
```csharp
public class ImageDrag : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public void OnPointerDown(PointerEventData eventData) { }
    public void OnDrag(PointerEventData eventData) { }
}
```

### 3. Gesture Recognition
Custom pinch-to-zoom implementation for multi-touch devices:
```csharp
if (Input.touchCount == 2)
{
    Touch touch1 = Input.GetTouch(0);
    Touch touch2 = Input.GetTouch(1);
    float currentDistance = Vector2.Distance(touch1.position, touch2.position);
    // Scale calculation logic
}
```

### 4. Ad Integration Flow
Player needs hint → Check hint availability → Show ad prompt
→ Player confirms → Play 17s video → Video completes → Award hint

## How to Run

1. **Open Project:**
   - Clone the repository
   - Create a new Unity project (Unity 2021.3+ recommended)
   - Copy the cloned `Assets` folder into your Unity project directory
   - Open the project in Unity

2. **Build Settings:**
   - Platform: iOS
   - Scenes: All scenes should be automatically detected from Assets/Scenes/

3. **Testing:**
   - **Option A (iOS Device):** Use Unity Remote 5 for real-time mobile testing
   - **Option B (Editor):** Test basic mechanics in Unity Editor (limited touch simulation)

4. **Version Selection:**
   - Non-monetized version: Scenes using `HintManager.cs` (6 fixed hints)
   - Monetized version: Scenes using `HintManager1.cs` (ad-based hint system)

## Design Highlights

- **Progressive Difficulty:** Each level introduces new mechanics while building on previous concepts
- **User-Centric Monetization:** Optional, reward-based system that preserves player agency
- **Clean UI/UX:** Intuitive button placement, clear feedback, and confirmation dialogs
- **Cross-Scene Persistence:** Seamless hint tracking across all levels
- **Mobile-First Design:** Touch-optimized controls with gesture support

## Technical Achievements

- Implemented dual-version architecture for A/B research testing
- Created 10+ unique puzzle interaction mechanics
- Developed persistent game state management system
- Integrated video ad rewards with validation
- Built scalable scene management framework
- Optimized for mobile touch input across various screen sizes

## Development Notes

- **Architecture:** Modular script design for easy puzzle level creation
- **Scalability:** Each puzzle mechanic is self-contained and reusable
- **Testing:** Iterative playtesting with pilot study refinement
- **Performance:** Optimized for mobile devices with efficient event handling

## Future Enhancements

- Analytics integration for detailed player behavior tracking
- Additional puzzle mechanics (rotation, color matching, physics-based)
- Difficulty adjustment based on player performance
- Sound effects and background music
- Tutorial system for first-time players

---

**Developed by:** Muhammad Zulhanif Bin Zairudin  
**Project Type:** Final Year Project - Computer Science  

**Focus:** Game Development, Monetization Research, Mobile UX
