NPC Social Simulation (Mobil Prototype)

** A mobile-oriented simulation game prototype developed in Unity 2D using C# for mobile platforms, featuring NPCs that interact dynamically and relationships evolve over time.
 This project focuses on system design, real-time feedback, and player-driven progression mechanics.
 
**Demo
 -instagram count ekle
 
* Features
  * Dynamic NPC interaction system
  * Realtionship system with evolving trust values
  * Real-time relationship visulation(color and thickness bassed)
  * Interactive UI for selecting NPCs and viewing relationships
  * Upgrade system with scaling cost and progression
  * Coin-based reward loop
  * Save/Load system using JSON serialization
  * Responsive UI feedback (button press)

* Gameplay Overview
  * NPCs interact autonomously over time
  * Each interaction affects relationship values
  * Player can observe and trigger interactions
  * Coins are earned through interactions
  * Coins can be spent on upgrades to speed up the simulation
   
* Tech Stack
  * Unity(C#)
  * Object-Oriented Programming
  * Event-driven system design
  * JSON serialization(PlayerPrefs)
  * Real-time UI system
   
* System Architecture
  * Gam Manager- Core game flow and data
  * IdleSystem- Handles automatic NPC interactions
  * InteractionSystem- Processes relationship changes
  * RelationshipVisualizer- Draws connections dynamically
  * UIManager- Manages player interface and feedback
  * SaveManager- Handles save/load functionality
   
* Design Goals
  * Mobile friendly UI and interaction flow
  * Clear visual feedback for player actions
  * Simple but scalable game loop
  * Focus on redability and modular system design
   
* Setup
  * Clone this repostory to your pc.
  * Open the project in UnityHub.
  * Click the "Play" button in Scenes/Main.
   
* Notes
  * This project was developed as a portfolio piece to demonstrate:
  * System design skills
  * Gameplay loop understanding
  * Mobile game development fundamentals
