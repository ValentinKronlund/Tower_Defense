# Game Document - Draft

## Game Type
**Rogue Like Tower Defense**

---

## Game Loop

You are collecting a resource (rogue-like resource) from an extraction point/mine.  
Enemies surround you on all sides and try to stop you from gaining power through this resource.

Starting from a central point, build a defense around the extraction point by placing towers at strategic positions to obstruct enemies from destroying you.

The longer you hold out, the more resources you are able to extract.

Resources can be used to upgrade **perks**, which give you specific abilities depending on type.

---

## Game Mechanics

### Console
- PC
- Mac

### Controls
- Top-down, asymmetric RTS.
- Third-person "hand-of-god" view: You control the cursor.

### Player
- Has resources:
  - **Arcana**: Used to upgrade perks. Player maintains these resources between matches.
  - **Gold**: Used to build towers during matches. Not maintained between matches.
  - **Wood**: Used to build towers during matches. Not maintained between matches.
- Can place towers: Towers block the enemies' path and inflict damage.
- Wins the match by surviving all waves.
- Loses the match if enemies destroy the central base.

### Enemies
- Have a health bar.
- Spawn at random locations at the edge of the map.
- Path automatically toward the central base (not predetermined).
- Deal damage to the central base when within range.

### Base
- Has a health bar.
- Gathers Arcana periodically during combat.
- Debris spawns randomly in a circle around a central point.
- Cannot be built. Destroyed if health reaches 0.

---

## World / Environment / Map

- Tile-based.
- Spawns random debris outside of base camp range.
- Has a turn cycle: **Prepare for attack → Defend against attack**.
- Includes a wave counter.
- Includes an enemy spawn timer.

---

## Game Lore

You are a mage, extracting Arcana to further your power.

---

## Game Theme

- Dark, gritty, medieval feeling and ambience.
- Magic: Mages and wizards.
- You are an antagonist-protagonist.

---

## Assets

- Scenario GG (Concept Art)
- Blender AI (Model Generation)
