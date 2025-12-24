# GameDevGPT-01

Petit projet Unity d'exemple (TappyTale) contenant des scripts de gameplay minimalistes.

## Ouverture
- Ouvrir le dossier racine dans Unity (fichier `GameDevGPT-01.slnx` présent).

## Scripts ajoutés
Les scripts créés se trouvent dans `Assets/Scripts` :
- Core: `Core/LayerMaskUtil.cs`, `Core/Health.cs`
- Player: `Player/PlayerMotor.cs`, `Player/PlayerAim.cs`, `Player/PlayerWeaponController.cs`
- Combat: `Combat/Weapon.cs`, `Combat/Projectile.cs`, `Combat/ProjectilePool.cs`
- Enemies: `Enemies/EnemyChaser.cs`, `Enemies/ContactDamage.cs`
- Game: `Game/EnemySpawner.cs`, `Game/GameManager.cs`

## Branche
- Branche source: `feature/add-scripts`
- Commit: "Add gameplay scripts (Core, Player, Combat, Enemies, Game)"

## Créer la PR (localement)
Si `gh` est installé et authentifié :

```bash
gh label create gameplay --color ffcc00 --description "Gameplay scripts" || true
gh pr create --base main --head feature/add-scripts \
  --title "Add gameplay scripts (Core, Player, Combat, Enemies, Game)" \
  --body-file pr_body.md \
  --label gameplay
```

---
Fichier généré automatiquement par l'assistant.
