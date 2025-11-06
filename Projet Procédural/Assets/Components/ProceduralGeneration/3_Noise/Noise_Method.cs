using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace Components.ProceduralGeneration.Noise_Method
{
    [CreateAssetMenu(menuName = "Procedural Generation Method /Noice Method")]
    public class Noise_Method : ProceduralGenerationMethod
    {
        FastNoiseLite noise;

        [Header("General")]
        [SerializeField, Tooltip("Défini la fréquence d'apparition de pics, donne une sensation de dézoom"), Range(0.01f, 0.1f)] float frequency;
        [SerializeField] FastNoiseLite.NoiseType noiseType;

        [Header("Fractal")]
        [SerializeField] FastNoiseLite.FractalType fractalType;
        [SerializeField, Range(1, 8)] int octave = 3;
        [SerializeField, Range(-5f, 5f)] float lacunarity = 2.0f;
        [SerializeField, Range(-5f, 5f)] float gain = 0.5f;
        [SerializeField, Range(-5f, 5f)] float weightedStrength = 0.0f;
        [SerializeField, Range(-5f, 5f)] float pingPongStrength = 2.0f;

        [Header("Cellular")]
        [SerializeField] FastNoiseLite.CellularDistanceFunction CellularDistanceFunction = FastNoiseLite.CellularDistanceFunction.EuclideanSq;
        [SerializeField] FastNoiseLite.CellularReturnType cellularReturnType = FastNoiseLite.CellularReturnType.Distance;
        [SerializeField, Range(-5f, 5f)] float cellularJitterModifier = 1.0f;

        [Header("Material Height")]
        [SerializeField, Range(-1f, 1f)] float waterHeight = 0.3f;
        [SerializeField, Range(-1f, 1f)] float sandHeight = 0.35f;
        [SerializeField, Range(-1f, 1f)] float grassHeight = 0.5f;

        protected override async UniTask ApplyGeneration(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            noise = new FastNoiseLite(GridGenerator.Seed);
            float height = 0f;

            noise.SetFrequency(frequency);
            noise.SetNoiseType(noiseType);
            noise.SetFractalType(fractalType);
            noise.SetFractalOctaves(octave);
            noise.SetFractalLacunarity(lacunarity);
            noise.SetFractalGain(gain);
            noise.SetFractalWeightedStrength(weightedStrength);
            noise.SetFractalPingPongStrength(pingPongStrength);
            noise.SetCellularDistanceFunction(CellularDistanceFunction);
            noise.SetCellularReturnType(cellularReturnType);
            noise.SetCellularJitter(cellularJitterModifier);

            for (int x = 0; x < Grid.Width; x++)
            {
                for (int y = 0; y < Grid.Lenght; y++)
                {
                    height = noise.GetNoise(x, y);
                    AssignTypeToCell(x,y, height);
                    
                }
            }


            await UniTask.Delay(GridGenerator.StepDelay, cancellationToken: cancellationToken);
        }

        void AssignTypeToCell(int x, int y, float height)
        {
            if (!Grid.TryGetCellByCoordinates(x, y, out var chosenCell))
            {
                Debug.LogError($"Unable to get cell on coordinates : ({x}, {y})");
            }
            else if (height <= waterHeight)
            {
                AddTileToCell(chosenCell, WATER_TILE_NAME, true);
            }
            else if (height > waterHeight && height <= sandHeight) 
            { 
                AddTileToCell(chosenCell, SAND_TILE_NAME, true);
            }
            else if (height > sandHeight && height <= grassHeight)
            {
                AddTileToCell(chosenCell, GRASS_TILE_NAME, true);
            }
            else if (height > grassHeight)
            {
                AddTileToCell(chosenCell, ROCK_TILE_NAME, true);
            }
        }
    }
}