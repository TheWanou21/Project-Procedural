using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VTools.Grid;
using VTools.ScriptableObjectDatabase;
using VTools.Utility;

namespace Components.ProceduralGeneration.SimpleRoomPlacement
{
    [CreateAssetMenu(menuName = "Procedural Generation Method/Simple Room Placement")]
    public class SimpleRoomPlacement : ProceduralGenerationMethod
    {
        [Header("Room Parameters")]
        [SerializeField] private int _maxRooms = 10;
        [SerializeField] private Vector2Int _roomMinSize = new(5, 5);
        [SerializeField] private Vector2Int _roomMaxSize = new(12, 8);

        protected override async UniTask ApplyGeneration(CancellationToken cancellationToken)
        {
            // ROOM CREATIONS
            List<RectInt> placedRooms = new();
            int roomsPlacedCount = 0;
            int attempts = 0;

            for (int i = 0; i < _maxSteps; i++)
            {
                // Check for cancellation
                cancellationToken.ThrowIfCancellationRequested();

                if (roomsPlacedCount >= _maxRooms)
                {
                    break;
                }

                attempts++;

                // choose a random size
                int width = RandomService.Range(_roomMinSize.x, _roomMaxSize.x + 1);
                int lenght = RandomService.Range(_roomMinSize.y, _roomMaxSize.y + 1);

                // choose random position so entire room fits into grid
                int x = RandomService.Range(0, Grid.Width - width);
                int y = RandomService.Range(0, Grid.Lenght - lenght);

                RectInt newRoom = new RectInt(x, y, width, lenght);

                if (!CanPlaceRoom(newRoom, 1))
                    continue;

                PlaceRoom(newRoom);
                placedRooms.Add(newRoom);

                roomsPlacedCount++;

                await UniTask.Delay(GridGenerator.StepDelay, cancellationToken: cancellationToken);
            }

            if (roomsPlacedCount < _maxRooms)
            {
                Debug.LogWarning($"RoomPlacer Only placed {roomsPlacedCount}/{_maxRooms} rooms after {attempts} attempts.");
            }

            if (placedRooms.Count < 2)
            {
                Debug.Log("Not enough rooms to connect.");
                return;
            }

            // CORRIDOR CREATIONS
            for (int i = 0; i < placedRooms.Count - 1; i++)
            {
                // Check for cancellation
                cancellationToken.ThrowIfCancellationRequested();

                Vector2Int start = placedRooms[i].GetCenter();
                Vector2Int end = placedRooms[i + 1].GetCenter();

                CreateDogLegCorridor(start, end);

                await UniTask.Delay(GridGenerator.StepDelay, cancellationToken: cancellationToken);
            }

            BuildGround();
        }
    }
}