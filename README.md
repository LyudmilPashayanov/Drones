# Drone Job Simulation

A small simulation where drones are assigned jobs consisting of a **pickup location** and a **drop-off location**.  
Drones navigate a grid world using pathfinding and execute jobs step-by-step while avoiding collisions.

---

# How to Run

1. Unity version used for this assignment was 6000.3.8f1. Expected to work in **Unity 2022 LTS or newer**.
2. Open the "DronesSimulation" scene.
3. Press **Play** in the Unity Editor.
4. Use the **control panel UI** to:
   - Create new jobs - A job pick up and drop off location should be between -5 and 5 (inclusive). When creating a new job, keep that rule.
   - Assign Drones <-> Jobs (vice versa) - Once you have selected any Drone and any Job from the Drones & Jobs Scroll View on the Left of the screen - 
     you need to click the assign Drones <-> Jobs button. Then the selected drone will be ready to perform the selected job.
   - Start the simulation - All drones, which have been assigned a job will start executing their job and their status will change accordingly.
5. For the sake of easier testing I am pre-creating 3 drones and 2 jobs to test the assignment. Feel free to Create new jobs or modify the DroneFactory gameObject in the hierarchy to spawn more drones in the world.6
6. You can check/uncheck the "Random Blocks" in the 'WorldGenerator' script on the 'World' GameObject in the hierarchy.
7. Look around how the simulation unfolds with your mouse and use WASD to move.    
## High-Level Architecture

The project is structured into several layers to keep the simulation logic decoupled from the Unity presentation layer.
 ### UI Layer

Unity `MonoBehaviour` components responsible for **rendering and user interaction**.

Examples:

- `ControlPanelView`
- `DroneListView`
- `JobsListView`

These classes contain **no simulation logic** and only display data provided by the ViewModels.

---

### ViewModel Layer

Acts as the **bridge between the UI and the simulation layer**.

Responsibilities:

- Expose simulation data to the UI
- Translate UI actions into simulation commands

Examples:

- `DronesViewModel`
- `JobsViewModel`

---

### Simulation Layer

Contains the **core logic of the drone simulation**.

Main responsibilities:

- Job assignment
- Drone movement
- Pathfinding
- Step-based simulation control

Key classes include:

- `Drone`
- `Job`
- `StepCoordinator`
- `TrafficController`
- `IPathfinder` implementations

These classes are implemented as **pure C# classes without Unity dependencies**, making the simulation logic easier to test and maintain.

---

### World Layer

Represents the **grid environment** the drones operate in.

Key components:

- `WorldGrid`
- `WorldGenerator`
- `WorldCoordinates`
- `WorldBlock`

This layer provides **spatial data and world queries** used by the simulation systems.

---

### Dependency Injection

All systems are wired together using **VContainer**.

`GameLifetimeScope` registers core systems and installers:

- `SimulationInstaller`
- `GameplayInstaller`
- `UIInstaller`

This approach keeps dependencies **explicit**, improving maintainability and making systems easier to replace or extend.

## Key Architectural Decisions

First and foremost: 
Since this project is for **VRelax**, I prioritised **simulation performance and deterministic behaviour**, keeping in mind that the system may eventually run on **VR hardware where CPU resources are more limited**.  
The architecture therefore focuses on **predictable updates, low overhead systems, and clean separation between simulation and rendering**, which helps maintain stable performance.


### Separation of Logic and Presentation

Simulation objects such as `Drone` and `Job` are implemented as **pure C# classes** without Unity dependencies.

Unity `MonoBehaviour` objects are used only for:

- Rendering
- Animation
- UI

This separation keeps the **simulation layer testable and independent from the Unity engine**.

---

### Pathfinding

Drones navigate the grid using a **custom A(star) pathfinding implementation**.

The system operates on a 3D grid-based world and calculates paths between two coordinates.  
The pathfinder is implemented behind an `IPathfinder` interface so different movement strategies can be swapped easily.

For example, the current implementation supports:

- **Straight (orthogonal) movement**
- **Diagonal movement**

Because the pathfinding system is decoupled from the drone logic, it is easy to introduce additional pathfinding strategies or constraints in the
future (e.g., restricted movement, weighted terrain, or alternative algorithms).

---

### Dependency Injection

The project uses dependency injection via **VContainer**.

Core systems are registered inside a `GameLifetimeScope`.

This allows systems such as:

- Pathfinding
- Traffic control
- Simulation coordination

to remain **loosely coupled and easily replaceable**, improving maintainability and testability.

### Decoupling Simulation from Unity

Simulation logic was implemented using **pure C# classes** rather than `MonoBehaviour` components.

Benefits:

- Easier to test
- Reusable outside Unity
- Clearer separation of responsibilities

---

### Collision-Safe Movement

A `TrafficController` was introduced to manage **cell reservations**.

This prevents two drones from occupying the same grid cell during the same simulation step (Doesn't work always and needs fixing).

---

### Maintainable Architecture

The project was structured into logical layers:

- **Simulation**
- **UI**
- **ViewModels**
- **Infrastructure**

This makes the project easier to extend and maintain.

## What I Prioritised in the first 2 Hours (After which I've spent around 6 more

I initially approached the task by focusing on the **core simulation first**, before implementing the full assignment requirements.

During the **first ~2 hours**, I built a minimal prototype of the drone simulation:

- Implemented a simple grid world
- Added basic drone movement
- Implemented custom pathfinding
- Verified that drones could navigate the world correctly

At this stage, the system had **no job logic and no UI**. The goal was simply to validate the **core movement and pathfinding behaviour**.

Once the simulation foundation was working, I extended the project to meet the assignment requirements. In total, I spent approximately **6–8 hours** implementing the rest of the features, including:

- Job assignment logic
- Step-based simulation coordination
- Collision prevention via `TrafficController`
- UI and ViewModel layers
- Dependency injection setup
- Code refactoring and architecture cleanup
- Documentation

## What I Would Improve With More Time

### 1. Smarter Traffic Resolution

- Currently there are situations that a drone might fly through another drone. As in real life this will result in a crash, the Traffic system should be improved.
- If a job location is given to be a grid coordinate which is blocked, the UI should give an error.
- 

---

### 2. Unit Tests for Simulation Systems

Because the simulation layer is pure C#, it would be straightforward to add tests for:

- Pathfinding
- Job state transitions
- Traffic reservations

---

### 3. UI Improvements

Better way to:
- assign drones to jobs
- create jobs
Better way to observe the drone simulations.

Additional UI features could include: Path previews.

---

### 4. Data-Driven Configuration (ScriptableObjects)

Another improvement would be to make parts of the simulation **data-driven using ScriptableObjects**.

Currently, most system configuration (for example movement speed, simulation timing, and certain 
behavioural parameters) is defined directly in code. With more time, 
these values could be exposed through **ScriptableObject configuration assets** that control system behaviour.

Potential benefits:

- Easier tuning of simulation parameters without modifying code
- Cleaner separation between **configuration data and logic**
- Designers or non-programmers could tweak behaviour directly in the Unity Editor
- Multiple configurations could be created for different simulation scenarios

For example, ScriptableObjects could define settings for:

- Drone movement parameters
- Simulation step timing
- Pathfinding configuration
- Traffic system behaviour

## Summary

The main focus of the implementation was to create a **clean, extensible architecture for a drone job simulation**, with emphasis on:

- Performance for VR
- Separation of concerns
- Maintainability
- Scalable system design
- Deterministic simulation
