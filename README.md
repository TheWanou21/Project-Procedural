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

Grid
---
La `Grid` est la grille contenant toutes les `Cells` .

La `Grid` permet :
* D'accéder au différentes `Cell` à partir de coordonnées ou de position dans le monde
* D'interagir avec plusieurs `Cell` rapidement pour placer des `Tiles` ou autre
* De servir de "pont" entre les méthodes et l'interaction sur les `Cells`

ProceduralGenerationMethod
---
Cette méthode est un "modèle" qui sert de base à toutes les méhodes de générations.

La `ProceduralGenerationMethod` permet :
* D'avoir un accès à la `Grid`
* D'utiliser une méthode `Async` c'est à dire dont on contrôle le temps
* Plusieurs fonctions permettant de faciliter certaines méthodes de constructions
