# GameDevGPT-01

Petit projet Unity d'exemple (TappyTale) contenant des scripts de gameplay minimalistes.

## Daily Unity Game/Project Summary

Création d'un jeu de tir simple avec un joueur, des ennemis, et un système de gestion de jeu, pour démontrer les bonnes pratiques de développement de jeux sur Unity et démontrer la simplicité d'utilisation de GitHub Copilot. Chaque projet est accompagné d'un fichier `pr_body.md` pour faciliter la création de Pull Requests.

Le projet est structuré avec les dossiers suivants :

- `Assets/Scripts/Core` : Scripts utilitaires et de base (ex: gestion de la santé, masques de calque).
- `Assets/Scripts/Player` : Scripts liés au joueur (mouvement, visée, contrôle des armes).
- `Assets/Scripts/Combat` : Scripts liés au combat (armes, projectiles, pool de projectiles).
- `Assets/Scripts/Enemies` : Scripts liés aux ennemis (IA de poursuite, dégâts de contact).
- `Assets/Scripts/Game` : Scripts de gestion du jeu (génération d'ennemis, gestionnaire de jeu).

Le projet inclut également un fichier `CHANGELOG.md` pour suivre les modifications apportées.

# GameDevGPT-01

## Project Summary

This repository contains a concise set of readable C# components under `Assets/Scripts` designed for learning and rapid prototyping:

- Core: `Core/LayerMaskUtil.cs`, `Core/Health.cs`
- Player: `Player/PlayerMotor.cs`, `Player/PlayerAim.cs`, `Player/PlayerWeaponController.cs`
- Combat: `Combat/Weapon.cs`, `Combat/Projectile.cs`, `Combat/ProjectilePool.cs`
- Enemies: `Enemies/EnemyChaser.cs`, `Enemies/ContactDamage.cs`
- Game: `Game/EnemySpawner.cs`, `Game/GameManager.cs`

These scripts implement a straightforward top-down shooter flow: player movement and aiming, projectile spawning and pooling, simple enemy chasing, and a scene-level game manager that reloads the scene on player death.

## Recommended Unity Version

Open the project with the Unity Editor version referenced in `ProjectSettings/ProjectVersion.txt`. The project currently lists `m_EditorVersion: 6000.3.0f1` (Unity 2024.3 family). Use Unity Hub to install or open the matching editor.

## Quick Start — Open and Run

1. Install the matching Unity Editor and open the repository root with Unity Hub.
2. Allow Unity to import assets. If prompted about the Input System package, follow project guidance (the repo includes Input System assets and settings).
3. Open `Assets/Scenes/SampleScene.unity`.
4. Press Play. If the scene is fully configured, the player, enemies and spawners should be present and interact.

If the scene lacks configuration, perform the manual setup below.

### Manual Scene Setup

Player
- Create a `Player` GameObject (Capsule or custom model).
- Add a `Rigidbody` component (enable interpolation for smoother movement) and a `Health` component. Set the GameObject tag to `Player`.
- Add the `PlayerMotor`, `PlayerAim`, and `PlayerWeaponController` components.
- Create a child `Muzzle` transform at the weapon barrel and assign it to `PlayerWeaponController` → `muzzle`.
- Ensure a `Camera` exists and is assigned to `PlayerAim` or is `Camera.main`.

Projectile prefab and pool
- Create a prefab with a Collider and `Rigidbody` (disable gravity) and add the `Projectile` script.
- Add a `ProjectilePool` GameObject, assign the prefab and configure `prewarm` as desired.
- Assign the `ProjectilePool` reference on the player's `PlayerWeaponController`.

Enemy prefab and spawner
- Create an enemy prefab with a `Rigidbody`, add `EnemyChaser` and `ContactDamage` components, and tag the prefab `Enemy`.
- Place an `EnemySpawner` in the scene and assign the player `Transform` and enemy prefab.

Game manager
- Add a `GameManager` to an empty GameObject and assign the player's `Health` component (or leave blank to auto-find at Awake).

Layers and masks
- Configure scene layers (e.g., `Floor`) and set `PlayerAim` → `aimMask` to target the appropriate layers so aim raycasts hit the ground.

## Input

The scripts currently use the legacy input API (`Input.GetAxis`, `Input.GetMouseButton`, `Input.mousePosition`). To use the new Input System, enable the compatibility layer or refactor the input reads.

## Design Notes

- `PlayerMotor`: reads input in `Update` and applies smoothed velocity in `FixedUpdate`.
- `PlayerAim`: raycasts from the camera to compute an aim point and smoothly rotates the player.
- `PlayerWeaponController`: pulls projectiles from `ProjectilePool` and applies spread and rate logic.
- `Projectile`: pool-friendly `Launch` API, lifetime tracking, and damage-on-collision.
- `EnemyChaser`: physics-based chase with rotation.
- `ContactDamage`: continuous damage while colliding with the player.

## Contribution & Branches

- Current development branch: `feature/add-scripts` (contains added scripts and docs).

To create a PR locally using GitHub CLI (`gh`):

```bash
gh label create gameplay --color ffcc00 --description "Gameplay scripts" || true
gh pr create --base main --head feature/add-scripts \
  --title "Add gameplay scripts (Core, Player, Combat, Enemies, Game)" \
  --body-file pr_body.md \
  --label gameplay
```

## CHANGELOG

See `CHANGELOG.md` at the repository root for initial entries.

---
Maintenance: this repository keeps `README.md` and `CHANGELOG.md` at the root. Please keep them updated when making notable changes.

## CHANGELOG

See `CHANGELOG.md` at the project root for the initial entries.

---
Maintenance: this repository keeps `README.md` and `CHANGELOG.md` at the root. Please keep them updated when making notable changes.
