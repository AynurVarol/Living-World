# Social Network Simulator - Living World (Mobil Prototype)
 A mobile-oriented social simulation/idle game prototype developed in Unity 2D using C# for mobile platforms, featuring NPCs that interact dynamically and relationships evolve over time.
 This project originally started as a small "Living World" prototype focused on autonomous NPC behavior and gradually evolved into a more complete simulation game featuring relationship systems, goals, system design, progression mechanics, upgrades save system and mobile interaction support. 
 
 The main focus of the projcet is:
 - gameplay system architecture
 - real-time feedback systems
 - modular programming 
 - mobile-friendly interaction design
 - scalable simulation mechanics
# Demo
Prototype Verison:
  * https://www.instagram.com/reel/DXMp54sDIK5/?igsh=MzdleGJpOGczMmJm
    
# Features

- NPCs interact autonomously over time
- Dynamic relationship/trust system
- NPC memories created from interactions
- Multiple interaction types:
   - Talk
   - Help
   - Gossip
   - Insult
 ## Relationship Visulization
  - Real-time relationship visulization
  - Line color and thickness change dynamically based on trust values
  
## Gameplay Systems
 - Coin reward loop
 - Combo system
 - Goal/progression system
 - Upgrade system with scaling costs
 - Win condition system
   
## Mobile Support
 - Touch input sopport
 - Mobile-oriented UI flow
 - Responsive UI interactions

## Feedback Systems
 - Floating text effects
 - Button press animations
 - Combo animations
 - Camera shake affects
 - Sound effects

## Save System
 - Save/Load system using JSON serialization with PlayerPrefs
 - Reset progress support

# Gameplay Overview
- NPCs automatically interact with each other over time
- Every interaction changes relationship values dynamically
- The player can select NPCs and trigger manual interactions
- Coins are earned through interactions
- Coins can be spent on upgrades to speed up the simulation
- Goals provide progression and rewards
- The player aims to increase overall relationship/trust levels

# Tech Stack
  - Unity(C#)
  - Unity UI
  - Object-Oriented Programming
  - Event-driven system design
  - JSON serialization(PlayerPrefs)
  - Coroutine-Based Animation System

# System Architecture
  ## GameManager
  Handles:
  - gameflow
  - NPC setup
  - combo system
  - win conditions
  - global game data
  ## IdleSystem
  Handles:
  - automatic Npc interactions
  - tick-based simulation flow
  ## InteractionSystem
  Handles:
  - interaction processing
  - relationship changes
  - rewards
  - memories
  - gameplay feedback systems
 ## RealtionshipVisualizer
 Handles:
 - dynamic relationship line rendering
 - trust-based visualization
  ## UIManager
  Handles:
  - player interface
  - animations
  - combo UI
  - goal UI
  - upgrade UI
  ## SaveManager
  Handles:
  - save/load functionality
  - reset progress system
   ## GoalManager
   Handles:
   - gameplay goals
   - progression tasks
   - reward generation

#  Design Goals
  - Mobile friendly interaction flow
  - Clear visual feedback for player actions
  - Readable and modular code structure
  - Scalable gameplay systems
  - Lightweight but expandable simulation architecture 
  
# Setup
  1. Clone this repostory to your pc.
  2. Open the project in UnityHub.
  3. Click the "Play" button in Scenes/Main.
   
# Notes
 This project was developed as a portfolio project to improve and demonstrate:
 - Gameplay system architecture
 - Simulation programming
 - Event-driven programming
 - Mobile game development fundamentals
 - Scable system design

The project also demonstrates the evulation of a simple prototype into a more feature-copmlete gameplay experience.
