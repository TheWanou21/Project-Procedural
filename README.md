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

Simple Room Placement
---
Cette méthode permet de placer plusieurs salles au fur et à mesure en testant des placements aléatoires plusieurs fois jusqu'à ce qu'il trouve un emplacement de libre

Point positifs de cette méthode :
* La  méthode est relativement simple à coder
* Les paramètres de placement restent très contrôlables

Point négatifs de cette méthode :
* Si les paramètres sont mal gérés, les espaces peuvent vite se recouper ou sortir de la carte
* Cette méthode peut ne pas donner le nombre maximal ou idéal d'espaces souhaités

BSP
---
Cette méthode permet de placer plusieurs salles en coupant de manière semi-aléatoires un espace plusieurs fois

Point positifs de cette méthode :
* Les paramètres de placement restent très contrôlables
* Cette méthode permet d'avoir le nombre d'espaces souhaités
* Cette méthode permet de facilement avoir les salles voisines entre elles

Point négatifs de cette méthode :
* Cette méthode est un peu plus compliquée que la précédente
* Cette méthode peut occasionnellement donné un côté trop " organisé " des espaces

Cellular Automata
---
Cette méthode permet de faire des cartes plus générales en initialisant aléatoirement les tiles à un type et en changeant leur états selon les tyles voisines

Point positifs de cette méthode :
* Write your text here :) ...

Point négatifs de cette méthode :
* Cette méthode n'est pas particulièrement simple
* Cette méthode est assez peu contrôlable
* En fonction du rendement cette méthode peut être lente et avoir un rendu peu réaliste

Noise
---
Cette méthode permet de faire des cartes à grand échelles en utilisant des noises c'est à dire des irrégularités naturelles et contrôlables

Point positifs de cette méthode :
* La  méthode est plus simple à coder que le Cellular Automata
* Les paramètres de la carte sont très contrôlables
* La carte est lisse

Point négatifs de cette méthode :
* Il faut importer une bibliothèque pour ça
* La méthode étant très précise, elle peut demander du temps pour être bien maîtrisée

Ajouter une méthode
---
Pour ajouter une méthode, il faut que celle-ci dérive de la classe `ProceduralGenerationMethode` et, du coup, qu'il aient la méthode `ApplyGeneration`.
La base nouvelle méthode se présente comme ça :
```csharp
//Entrez vos imports

namespace Components.ProceduralGeneration.nom_de_la_methode
{
  [CreateAssetMenu(menuName = "Procedural Generation Method/nom_de_la_methode" )]
  public class nom_de_la_methode : ProceduralGenerationMethod
  {
    protected override async UniTask ApplyGeneration(CancellationToken cancellationToken)
    {
    await UniTask.Delay(GridGenerator.StepDelay, cancellationToken: cancellationToken);
    cancellationToken.ThrowIfCancellationRequested();
    }
  }
}
```
Vous n'avez plus qu'à l'utiliser !
