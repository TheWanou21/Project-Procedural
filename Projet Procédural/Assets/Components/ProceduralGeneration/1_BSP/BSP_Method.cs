using Components.ProceduralGeneration;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.XR;
using VTools.RandomService;
using System.Collections.Generic;

namespace Components.ProceduralGeneration.BSP_Method
{
    [CreateAssetMenu(menuName = "Procedural Generation Method / BSP Method Placement")]

    public class BSP_Method : ProceduralGenerationMethod
    {
        [Header("Room Parameters")]
        [SerializeField] private int _maxCuts = 4;
        [SerializeField] private Vector2Int _roomMinSize = new(3, 3);
        [SerializeField] private Vector2Int _roomMaxSize = new(12, 12);
        protected override async UniTask ApplyGeneration(CancellationToken cancellationToken)
        {
            //Instantiations
            RectInt gridSpace = new RectInt(0, 0, Grid.Width, Grid.Lenght);
            Node world = new Node(gridSpace, Grid , RandomService);

            List<Node> nodes = new List<Node>();
            nodes.Add(world);

            //Création d'emplacements
            world.SplitXGen(_maxCuts, world, nodes);
            Debug.Log($"Nodes contient {nodes.Count} salles.");

            //Création des salles
            foreach (Node node in nodes)
            {
                Debug.Log($"{node.nodeSpace}");
                for (int i = 0; i < _maxSteps; i++)
                {

                    int width = node.nodeSpace.width;
                    int lenght = node.nodeSpace.height;
                    int x = node.nodeSpace.xMin;
                    int y = node.nodeSpace.yMin;

                    RectInt newRoom = new RectInt(x, y, width, lenght);

                    if (CanPlaceRoom(newRoom, 0))
                    {
                        await UniTask.Delay(GridGenerator.StepDelay, cancellationToken: cancellationToken);
                        PlaceRoom(newRoom);
                        break;
                    }
                }
            } 
        }
    }

    public class Node
    {
        public Node Child1, Child2;
        public RectInt nodeSpace;

        private readonly VTools.Grid.Grid _grid;
        private readonly RandomService _randomService;

        public Node(RectInt nodeSpace, VTools.Grid.Grid grid, RandomService randomService)
        {
            this.nodeSpace = nodeSpace;
            _grid = grid;
            _randomService = randomService;
        }
        public void SplitXGen(int generations, Node node, List<Node> nodes)
        {
            if (generations == 0) return;
            node.Split();
            nodes.Add(node.Child1);
            nodes.Add(node.Child2);
            nodes.Remove(node);
            SplitXGen(generations - 1, node.Child1, nodes);
            SplitXGen(generations - 1, node.Child2, nodes);
        }

        public void Split()
        {
            
            //Une chance sur 2 de couper verticalement
            if (_randomService.Chance(0.5f))
            {
                int allowedSpace = _randomService.Range(nodeSpace.width / 4, nodeSpace.width * 3 / 4);
                RectInt firstChild = new RectInt(nodeSpace.xMin, nodeSpace.yMin, allowedSpace, nodeSpace.height); 
                RectInt secondChild = new RectInt(firstChild.xMax, nodeSpace.yMin, nodeSpace.width - allowedSpace, nodeSpace.height);
                Child1 = new Node(firstChild, _grid,_randomService);
                Child2 = new Node(secondChild, _grid, _randomService);
                
            }
            //Sinon coupure horizontale du parent
            else
            {
                int allowedSpace = _randomService.Range(nodeSpace.height / 4, nodeSpace.height * 3 / 4);
                RectInt firstChild = new RectInt(nodeSpace.xMin, nodeSpace.yMin, nodeSpace.width, allowedSpace);
                RectInt secondChild = new RectInt(nodeSpace.xMin, firstChild.yMax, nodeSpace.width, nodeSpace.height - allowedSpace);
                Child1 = new Node(firstChild, _grid, _randomService);
                Child2 = new Node(secondChild, _grid, _randomService);
            }
            
        }
    }
}