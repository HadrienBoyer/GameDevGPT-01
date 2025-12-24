# Copilot instructions — GameDevGPT-01

Project snapshot
- Unity3D project using the `TappyTale` namespace. Core code lives under `Assets/Scripts/`.
- Open the project with the Unity Editor; solution file: `GameDevGPT-01.slnx`.

Quick architecture (big picture)
- Player systems: `Assets/Scripts/Player/` (see `PlayerWeaponController.cs`, `PlayerAim.cs`, `PlayerMotor.cs`).
- Combat systems: `Assets/Scripts/Combat/` (`ProjectilePool.cs`, `Projectile.cs`, `Weapon.cs`). Pooling is used to avoid runtime spikes.
- Game flow: `Assets/Scripts/Game/GameManager.cs` (scene reload on player death) and `EnemySpawner.cs` for simple spawn logic.
- Core utilities: `Assets/Scripts/Core/` (`Health.cs`, `LayerMaskUtil.cs`) — `Health` exposes `Changed` and `Died` events.

Development and runtime conventions
- Scene-first: many dependencies are assigned in the Inspector; prefer editing serialized fields in the Unity Editor rather than hard-coding refs.
- Tags: objects rely on `Player` and `Enemy` tags (see `EnemySpawner.CountAliveEnemies()` which uses `FindGameObjectsWithTag`).
- Reset methods: components use `Reset()` to auto-wire local references (e.g., `PlayerWeaponController.Reset()` sets `aim`). Mimic this pattern when adding new components.
- Time-based logic: cooldowns and rates use `Time.time` and `SecondsPerShot` helpers (see `Weapon.SecondsPerShot`).
- Events: `Health` uses `Action` events — prefer subscribing/unsubscribing in `OnEnable/OnDisable`.

Code patterns to follow (examples)
- Projectile pooling: call `ProjectilePool.Get()` and set `transform.position` then `Launch(...)` on the returned `Projectile` (see `PlayerWeaponController.Fire()`).
- Weapon config: use the serializable `Weapon` class to hold `fireRate`, `projectileSpeed`, `damage`, `spreadDegrees`. Compute cadence via `SecondsPerShot`.
- Spawning: `EnemySpawner.SpawnOne()` instantiates `enemyPrefab` and names instances; caps are enforced via counting tag matches.
- Health: modify health only via `Health.ApplyDamage()` and restore with `RestoreFull()`; react to death via `Died` event.

Build / run / debug notes
- Primary workflow is the Unity Editor. Open the provided solution (`GameDevGPT-01.slnx`) in Visual Studio / Rider for code debugging.
- There are no custom build scripts in the repo; use Unity's standard build pipeline or add an Editor build script if CI automation is needed.
- To reproduce runtime behavior quickly: open `Assets/Scenes/SampleScene.unity` in the Editor and press Play.

CI and Editor settings
- Unity version: the project targets `m_EditorVersion: 6000.3.0f1` (see `ProjectSettings/ProjectVersion.txt`). Use the same editor version in CI and local development to avoid asset/serialization diffs.
- No CI currently configured in this repo. For quick GitHub Actions integration, use the community `game-ci/unity-builder` or `webbertakken/unity-actions` builders. Minimal example snippet (copy into `.github/workflows/unity-ci.yml`):

```yaml
name: Unity CI
on: [push, pull_request]
jobs:
	build:
		runs-on: ubuntu-latest
		steps:
			- uses: actions/checkout@v4
			- name: Cache Library
				uses: actions/cache@v4
				with:
					path: Library
					key: ${{ runner.os }}-library-${{ hashFiles('**/*.csproj') }}
			- name: Unity Builder
				uses: game-ci/unity-builder@v2
				with:
					unityVersion: 6000.3.0f1
```

- Keep `ProjectSettings` and `Assets` consistent when running CI — serialized scene/prefab changes will appear when Unity versions differ. If you add CI, ensure the runner uses the exact `m_EditorVersion` above.

Editor conventions
- Prefer opening the project in the Unity Editor and using the Inspector to wire serialized fields. After changing serialized fields or prefabs, run the Editor and verify `SampleScene` to catch missing references.
- Recommended editor extensions (optional): Visual Studio / Rider for C# editing; enable code-formatting and Rider/VS integration if used.

What to watch for when changing code or prefabs
- If you add serialized fields, update scene prefab instances in the Editor; prefabs and scenes track `.meta` files — keep them consistent in commits.
- Avoid relying on global Find/FindGameObject calls in hot loops; the project already uses simple caps (tag counts) — prefer maintaining references when performance matters.
- Keep `Prewarm()` style pooling for frequently spawned objects (see `ProjectilePool.cs`).

Where to look first (key files)
- `Assets/Scripts/Game/GameManager.cs` — main loop / restart on death
- `Assets/Scripts/Game/EnemySpawner.cs` — spawn logic
- `Assets/Scripts/Combat/ProjectilePool.cs` — pooling pattern
- `Assets/Scripts/Player/PlayerWeaponController.cs` — firing flow example
- `Assets/Scripts/Core/Health.cs` — event-based health

Checklist for changes
- Update Inspector defaults and check scenes after edits.
- Run the SampleScene and verify no missing references.
- Update `README.md` and `CHANGELOG.md` for notable changes.

If something's unclear
- Ask for the scene and prefab to open, or which platform/target to build for. Provide the script and the GameObject you expect it to attach to and I will produce precise code edits or a small Editor helper.

— End
