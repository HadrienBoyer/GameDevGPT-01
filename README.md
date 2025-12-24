# GameDevGPT-01

Petit projet Unity d'exemple (TappyTale) contenant des scripts de gameplay minimalistes.
Création d'un jeu de tir simple avec un joueur, des ennemis, et un système de gestion de jeu, pour démontrer les bonnes pratiques de développement de jeux sur Unity et démontrer la simplicité d'utilisation de GitHub Copilot. Chaque projet est accompagné d'un fichier `pr_body.md` pour faciliter la création de Pull Requests.

Le projet est structuré avec les dossiers suivants :
- `Assets/Scripts/Core` : Scripts utilitaires et de base (ex: gestion de la santé, masques de calque).
- `Assets/Scripts/Player` : Scripts liés au joueur (mouvement, visée, contrôle des armes).
- `Assets/Scripts/Combat` : Scripts liés au combat (armes, projectiles, pool de projectiles).
- `Assets/Scripts/Enemies` : Scripts liés aux ennemis (IA de poursuite, dégâts de contact).
- `Assets/Scripts/Game` : Scripts de gestion du jeu (génération d'ennemis, gestionnaire de jeu).

Le projet inclut également un fichier `CHANGELOG.md` pour suivre les modifications apportées.

# GameDevGPT-01

TappyTale — a compact Unity example project containing focused gameplay scripts for player movement, aiming, projectile firing, simple enemy AI, and a minimal game loop.

## Project Summary

This repository provides a small set of easy-to-read C# components under `Assets/Scripts` intended for learning and fast prototyping:

- Core: `Core/LayerMaskUtil.cs`, `Core/Health.cs`
- Player: `Player/PlayerMotor.cs`, `Player/PlayerAim.cs`, `Player/PlayerWeaponController.cs`
- Combat: `Combat/Weapon.cs`, `Combat/Projectile.cs`, `Combat/ProjectilePool.cs`
- Enemies: `Enemies/EnemyChaser.cs`, `Enemies/ContactDamage.cs`
- Game: `Game/EnemySpawner.cs`, `Game/GameManager.cs`

These scripts implement a straightforward top-down shooter flow: player movement and aiming, projectile spawning and pooling, simple enemy chasing logic, and a scene-level game manager that reloads the scene on player death.

## Recommended Unity Version

Open the project with the Unity Editor version referenced in `ProjectSettings/ProjectVersion.txt`. This repository currently lists `m_EditorVersion: 6000.3.0f1` (Unity 2024.3 family). Use Unity Hub to install/open the matching editor if possible.

## Quick Start — Open and Run

1. Install the matching Unity Editor and open the repository root with Unity Hub.
2. Let Unity import assets. If prompted about the Input System package, follow the project guidance (the repo includes Input System assets and settings).
3. Open `Assets/Scenes/SampleScene.unity`.
4. Press Play — if the scene is preconfigured, the player, enemies and spawners should be present and interact.

If the scene is not preconfigured, follow the manual setup below.

## Manual Scene Setup (step-by-step)

1. Player
  - Create a `Player` GameObject (Capsule or custom model).
  - Add a `Rigidbody` component (use interpolation for smoother movement), add a `Health` component, and set the GameObject tag to `Player`.
  - Add the `PlayerMotor`, `PlayerAim`, and `PlayerWeaponController` components.
  - Create an empty child `Muzzle` transform at the weapon's barrel point and assign it to `PlayerWeaponController` → `muzzle`.
  - Create or assign a `Camera` in the scene and set it as `Camera.main` or assign it to `PlayerAim`.

2. Projectile prefab and pool
  - Create a prefab with a collider and `Rigidbody` (set gravity off), add the `Projectile` script.
  - Create a `ProjectilePool` GameObject and assign the projectile prefab and the desired prewarm amount.
  - Assign the `ProjectilePool` reference to the player's `PlayerWeaponController`.

3. Enemy prefab and spawner
  - Create an enemy prefab with a `Rigidbody`, add `EnemyChaser` and `ContactDamage` components, and tag the prefab with `Enemy`.
  - Add an `EnemySpawner` in the scene and point it to the `Player` transform and the enemy prefab.

4. Game manager
  - Add a `GameManager` to an empty GameObject and assign the player's `Health` component (or leave it blank to auto-find the player at Awake).

5. Layers and masks
  - Configure layers (e.g., Floor) and set `PlayerAim` → `aimMask` to the appropriate layers so aiming raycasts hit the ground.

6. Play
  - Press Play in the Editor. The player should be controllable (WASD/arrow keys), aim with the mouse, and fire with left mouse button.

## Input

These scripts use the legacy input API (`Input.GetAxis`, `Input.GetMouseButton`, `Input.mousePosition`). If you prefer the new Input System, either enable the compatibility layer or adapt the input reads.

## Design Notes

- `PlayerMotor`: reads input in `Update` and applies velocity smoothing in `FixedUpdate`.
- `PlayerAim`: uses a camera raycast to obtain a world aim point and smoothly rotates the player.
- `PlayerWeaponController`: uses `ProjectilePool` to spawn projectiles and applies spread and firing rate logic.
- `Projectile`: pool-friendly launch API, lifetime management, and damage-on-collision.
- `EnemyChaser`: physics-based chase logic and rotating towards the player.
- `ContactDamage`: deals continuous damage while colliding with the player.

## Contribution & Branches

- Current development branch: `feature/add-scripts` (contains the added scripts and docs).
- Commit example: `Add gameplay scripts (Core, Player, Combat, Enemies, Game)`.

To create a PR locally using GitHub CLI (`gh`):

```bash
gh label create gameplay --color ffcc00 --description "Gameplay scripts" || true
gh pr create --base main --head feature/add-scripts \
  --title "Add gameplay scripts (Core, Player, Combat, Enemies, Game)" \
  --body-file pr_body.md \
  --label gameplay
```

## CHANGELOG

See `CHANGELOG.md` at the project root for the initial entries.

---
Maintenance: this repository keeps `README.md` and `CHANGELOG.md` at the root. Please keep them updated when making notable changes.
