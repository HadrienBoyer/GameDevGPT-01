# GameDevGPT-01

Small example Unity project (TappyTale) containing minimal gameplay scripts.

## Project summary

A simple shooter with a player, enemies, and a game management system. The project demonstrates good Unity development practices and showcases how easy it is to use GitHub Copilot. This repository includes a `pr_body.md` file to help create pull requests.

The project is organized with the following folders:

- `Assets/Scripts/Core`: Core and utility scripts (e.g., health management, layer masks).
- `Assets/Scripts/Player`: Player-related scripts (movement, aiming, weapon control).
- `Assets/Scripts/Combat`: Combat-related scripts (weapons, projectiles, projectile pool).
- `Assets/Scripts/Enemies`: Enemy-related scripts (chase AI, contact damage).
- `Assets/Scripts/Game`: Game management scripts (enemy spawning, game manager).

## Installing and running the project

1. Clone the repository to your local machine.
2. Install the matching Unity Editor, then open the repository root with Unity Hub.
3. Allow Unity to import assets. If prompted about the Input System package, follow the project guidance (the repo includes Input System assets and settings).
4. Open `Assets/Scenes/SampleScene.unity`.
5. Press Play. If the scene is configured, the player, enemies, and spawners should be present and interact.

If the scene isn't configured, perform the manual setup below.

### Manual Scene Setup

- Player

  - Create a `Player` GameObject (Capsule or custom model).
  - Add a `Rigidbody` component (enable interpolation for smoother movement) and a `Health` component. Set the GameObject tag to `Player`.
  - Add the `PlayerMotor`, `PlayerAim`, and `PlayerWeaponController` components.
  - Create a child `Muzzle` transform at the weapon barrel and assign it to `PlayerWeaponController` → `muzzle`.
  - Ensure a `Camera` exists and is assigned to `PlayerAim` or is `Camera.main`.

- Projectile prefab and pool

  - Create a prefab with a Collider and a `Rigidbody` (disable gravity),  and add the `Projectile` script.
  - Add a `ProjectilePool` GameObject, assign the prefab, and configure  `prewarm` as desired.
  - Assign the `ProjectilePool` reference in the player's `PlayerWeaponController`.

- Enemy prefab and spawner

  - Create an enemy prefab with a `Rigidbody`, add `EnemyChaser` and `ContactDamage` components, and tag the prefab as `Enemy`.
  - Place an `EnemySpawner` in the scene, and assign the player's `Transform` and the enemy prefab.

Game manager

- Add a `GameManager` to an empty GameObject and assign the player's `Health` component (or leave it blank to auto-find at Awake).

Layers and masks

- Configure scene layers (e.g., `Floor`) and set `PlayerAim` → `aimMask` to target the appropriate layers so aim raycasts hit the ground.

## Input

The scripts currently use the legacy input API (`Input.GetAxis`, `Input.GetMouseButton`, `Input.mousePosition`). To use the new Input System, enable the compatibility layer or refactor input handling.

## Design notes

- `PlayerMotor`: reads input in `Update` and applies smoothed velocity in `FixedUpdate`.
- `PlayerAim`: raycasts from the camera to compute an aim point and smoothly rotates the player.
- `PlayerWeaponController`: pulls projectiles from `ProjectilePool` and applies spread and rate logic.
- `Projectile`: pool-friendly `Launch` API, lifetime tracking, and damage-on-collision.
- `EnemyChaser`: physics-based chase with rotation.
- `ContactDamage`: continuous damage while colliding with the player.

## Contribution & branches

- Current development branch: `feature/add-scripts` (contains added scripts and documentation).

## Tappy Tale Framework

This project is built using the Tappy Tale framework, which provides a set of reusable components and systems for rapid game development in Unity. The framework emphasizes simplicity, modularity, and ease of use, making it ideal for prototyping and learning.

To create a PR locally using GitHub CLI (`gh`):

```bash
gh label create gameplay --color ffcc00 --description "Gameplay scripts" || true
gh pr create --base main --head feature/add-scripts \
  --title "Add gameplay scripts (Core, Player, Combat, Enemies, Game)" \
  --body-file pr_body.md \
  --label gameplay
```

## CHANGELOG

All changes should be recorded in the root `CHANGELOG.md` file.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Why this project?

I use LLMs daily and my workflow supports "vibe-coding" (automatic code generation, refactoring, and documentation). I often don't need to open Unity to create a working project. This repository contains game and app prototypes in C# for Unity 6.2 (Built-in RP; builds tested on macOS), usually produced within a couple of hours (including documentation) using GitHub Codex. — Hadrien Boyer

Do not hesitate to reach out on GitHub with questions or suggestions.
