## 📖 Documentation du Projet - Alone in Space (VR)

### 🧩 Structure Générale du Projet

Le projet **Alone in Space** est développé sous **Unity** (template VR de base).  
Le jeu se déroule à bord d'un vaisseau spatial et propose au joueur d'effectuer différentes tâches pour survivre au cours de plusieurs "jours" ingame.

Principaux dossiers :
- `Assets/ScriptsC#/` : Tous les scripts du projet.
- `Assets/Scenes/` : Scènes principales du jeu (ex: Décollage, Jour1...).
- `Assets/Builds/` : Objets et éléments interactifs réutilisables.

---

### ⚡ Parties Sensibles du Code

#### 🎯 `CheckMission.cs`
- **Rôle :** Ce script est **essentiel** car il gère le système de progression du jeu basé sur les jours ingame.
- **Fonctionnalité principale :** 
  - Vérifie si toutes les missions/tâches du jour sont complétées.
  - Déclenche la transition vers le jour suivant.
  - Sans ce script fonctionnel, **la boucle principale du jeu est cassée**.
---

### 🛠️ Bonnes pratiques d'implémentation

- **Scripts dans l'Inspecteur :**
  - Vérifiez systématiquement que tous les objets du jeu ayant des interactions (drag & drop, surbrillance, missions...) ont bien leurs **scripts correctement attachés** dans Unity.
  - Un script manquant ou mal lié pourrait empêcher la progression ou causer des bugs invisibles.
  - Pas seulement les scripts mais tout autre éléments utiles à l'action voulu --> toujours check son implémentation dans l'inspecteur
---

### 💬 Notes complémentaires

- Avant de build le projet, oubliez pas de build pour la plateforme Android
- Si un jour vous ajoutez de **nouvelles missions** ou modifiez le cycle des jours, **prévoyez de tester entièrement** `CheckMission.cs` avant tout build.
- Toujours tester en VR réel : certains bugs liés au drag & drop ou aux collisions n'apparaissent pas dans l'éditeur Unity mais seulement dans un casque et ses manettes.

---

