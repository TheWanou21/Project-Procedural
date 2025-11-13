Outils de Génération Procédurale
===
Les outils de générations procédurale sont des codes qui ont été travaillé sur une semaine théorique sur le sujet.
Ils sont aux nombres de 4 et sont à utiliser sur Unity dans une version postérieur à la version 6.

## Architecture du code
- [Cell](#Cell)
- [Grid](#Grid)
- [ProceduralGenerationMethod](#ProceduralGenerationMethod)

## Methodes de generations
- [Simple Room Placement](#Simple-Room-Placement)
- [BSP](#BSP)
- [Cellular Automata](#Cellular-Automata)
- [Noise](#Noise)
- [Ajouter une méthode](#Ajouter-une-méthode)


Cell
---
La `Cell` est un objet qui représente une case dans la grille de génération.

La `Cell` permet de stocker :
* La position grâce à des coordonées x et y dans la grille
* Un `GridObject` et un `GridObjectController` qui permettent de mettre en objet dans la cellule ainsi que de le contrôler
  
La `Cell` peut :
* Recevoir un objet grâce à `AddObject`
* Détruire leur objet grâce à `ClearGridObject`
* Donner sa position dans le monde grâce à `GetCenterPosition`
