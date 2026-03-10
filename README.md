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
5. For the sake of easier testing I am pre-creating 3 drones and 2 jobs to test the assignment. Feel free to Create new jobs or modify the DroneFactory gameObject in the hierarchy to spawn more drones in the world.

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

## What I Prioritised in the 6-8 Hours

Given the time constraint, I focused on **architecture and correctness** rather than ease of use in the UI.

### 1. Clean Simulation Architecture

The priority was building a system where:

- Drones can be added dynamically
- Jobs can be assigned at runtime
- The simulation remains deterministic

This resulted in the **StepCoordinator pattern**, where the simulation progresses step-by-step and synchronizes drone movement.

---

### 2. Decoupling Simulation from Unity

Simulation logic was implemented using **pure C# classes** rather than `MonoBehaviour` components.

Benefits:

- Easier to test
- Reusable outside Unity
- Clearer separation of responsibilities

---

### 3. Collision-Safe Movement

A `TrafficController` was introduced to manage **cell reservations**.

This prevents two drones from occupying the same grid cell during the same simulation step (Doesn't work always and needs fixing).

---

### 4. Maintainable Architecture

The project was structured into logical layers:

- **Simulation**
- **UI**
- **ViewModels**
- **Infrastructure**

This makes the project easier to extend and maintain.

## What I Would Improve With More Time

### 1. Smarter Traffic Resolution

Currently there are situations that a drone might fly through another drone. As in real life this will result in a crash, the Traffic system should be improved.

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
