using UnityEngine;
using Components.ProceduralGeneration;
using Cysharp.Threading.Tasks;
using System.Threading;
using VTools.Grid;

namespace Components.ProceduralGeneration.CellularAutomata
{
    [CreateAssetMenu(menuName = "Procedural Generation Method / Cellular Automata")]
    public class CellularAutomata : ProceduralGenerationMethod
    {
        [SerializeField] float noiseDensity = 0.6f;
        [SerializeField] float blocksToGetEarth = 4;
        protected override async UniTask ApplyGeneration(CancellationToken cancellationToken)
        {
            //Génération de la grid
            GridGeneration();

            for (int i = 0; i < _maxSteps; i++)
            {
                //Pas touche
                cancellationToken.ThrowIfCancellationRequested();
                await UniTask.Delay(GridGenerator.StepDelay, cancellationToken: cancellationToken);
            }
        }

        void GridGeneration()
        {
            for(int i = 0; i < Grid.Width; i++)
            {
                for(int j = 0; j < Grid.Lenght; j++)
                {
                    if (!Grid.TryGetCellByCoordinates(i, j, out var cell))
                    {
                        continue;
                    }
                    if (RandomService.Chance(noiseDensity))
                    {

                        AddTileToCell(cell, GRASS_TILE_NAME, true);
                    }
                    else
                    {
                        AddTileToCell(cell, WATER_TILE_NAME, true);
                    }
                }
            }
        }

        void NextGeneration()
        {
            for (int i = 0; i < Grid.Width; i++)
            {
                for (int j = 0; j < Grid.Lenght; j++)
                {
                    if (!Grid.TryGetCellByCoordinates(i, j, out var cell))
                    {
                        continue;
                    }
                    if (RandomService.Chance(noiseDensity))
                    {

                        AddTileToCell(cell, GRASS_TILE_NAME, true);
                    }
                    else
                    {
                        AddTileToCell(cell, WATER_TILE_NAME, true);
                    }
                }
            }
        }
    }
}