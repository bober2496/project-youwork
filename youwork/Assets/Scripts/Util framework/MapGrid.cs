using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Bober.UnityTools
{
    //Make a 2D grid on the map
    [System.Serializable]
    public class MapGrid<TGridArray>
    {
        //Grid basic attribute variables
        [SerializeField]
        protected int width, height;
        [SerializeField]
        protected float cellSize;
        protected TGridArray[,] gridArray;
        [SerializeField]
        protected Vector3 startingPosition;

        //Debug variables
        protected TextMeshPro[,] debugTextArray;        

        protected MapGrid()
        {

        }
        public MapGrid(int width, int height, float cellSize, Vector3 startingPosition = default)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.startingPosition = startingPosition;

            gridArray = new TGridArray[width, height];
        }

        protected bool IsGridCellExist(int x, int y)
        {
            bool exist = (x >= 0 && y >= 0 && x < width && y < height);
            return exist;
        }
        protected bool IsGridCellExist(Vector2Int coordinates)
        {
            return IsGridCellExist(coordinates.x, coordinates.y);
        }
        protected Vector3 GetWorldPosition(int x, int y)      //Grid cell to world position coordinates
        {
            Vector3 worldPosition = new Vector3(x, y) * cellSize + startingPosition;
            return worldPosition;
        }
        protected Vector3 GetWorldPosition(Vector2Int coordinates)
        {
            return GetWorldPosition(coordinates.x, coordinates.y);
        }
        protected Vector3 GetCenterPosition(int x, int y)
        {
            Vector3 offset = new Vector3(cellSize, cellSize) / 2f;
            Vector3 center = GetWorldPosition(x, y) + offset;
            return center;
        }
        protected Vector3 GetCenterPosition(Vector2Int coordinates)
        {
            return GetCenterPosition(coordinates.x, coordinates.y);
        }
        protected Vector2Int GetCoordinates(Vector3 worldPosition)    //World position to grid cell coordinates
        {
            Vector2Int coordinates = default;
            coordinates.x = Mathf.FloorToInt((worldPosition - startingPosition).x / cellSize);
            coordinates.y = Mathf.FloorToInt((worldPosition - startingPosition).y / cellSize);
            return coordinates;
        }

        protected virtual void SetDebugText(int x, int y, dynamic value)
        {
            if (debugTextArray != null)
                debugTextArray[x, y].text = value.ToString();
        }

        //Draw the weight numbers and cell border lines for every cell
        public virtual void DrawDebug(int fontSize, Color color, float time = 10f, bool withValues = true)
        {
            debugTextArray = new TextMeshPro[width, height];
            Transform debugParent = new GameObject("NavigationGrid debug parent").transform;
            debugParent.position = Vector3.zero;

            for (int x = 0; x < gridArray.GetLength(0); x++)
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    if (withValues)
                        debugTextArray[x, y] = BobTools.CreateTextMeshPro(gridArray[x, y].ToString(), GetCenterPosition(x, y), debugParent, fontSize, color, TextAlignmentOptions.Center, new Vector2(cellSize, cellSize));
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), color, int.MaxValue);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), color, int.MaxValue);
                }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), color, int.MaxValue);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), color, int.MaxValue);
        }

        public void SetValue(int x, int y, TGridArray value)
        {
            if (IsGridCellExist(x, y))
            {
                gridArray[x, y] = value;
                SetDebugText(x, y, value);
            }
            else
            {
                Debug.LogError($"Cannot set the the value to cell {x} : {y}\nGrid cell does not exist.");
            }
        }
        public void SetValue(Vector2Int coordinates, TGridArray value)
        {
            SetValue(coordinates.x, coordinates.y, value);
        }
        public void SetValue(Vector3 worldPosition, TGridArray value)
        {
            SetValue(GetCoordinates(worldPosition), value);
        }

        public TGridArray GetValue(int x, int y)
        {
            if (IsGridCellExist(x, y))
                return gridArray[x, y];
            else
            {
                Debug.LogError($"Cannot get the the value of cell {x} : {y}\nGrid cell does not exist.");
                return default;
            }
        }
        public TGridArray GetValue(Vector2Int coordinates)
        {
            return GetValue(coordinates.x, coordinates.y);
        }
        public TGridArray GetValue(Vector3 worldPosition)
        {
            return GetValue(GetCoordinates(worldPosition));
        }

    }

    public class PathNode
    {
        //Constants
        public const int MOVE_STRAIGHT_COST = 10;
        public const int MOVE_DIAGONAL_COST = 14;

        public Vector2Int coordinate;
        public int gCost, hCost, fCost;
        public PathNode parent;
        public int weightFactor;

        public PathNode()
        {

        }
        public PathNode(Vector2Int coordinate)
        {
            this.coordinate = coordinate;
            weightFactor = 1;
        }
        public PathNode(int x, int y)
        {
            this.coordinate.x = x;
            this.coordinate.y = y;
            weightFactor = 1;
        }
        
        public static int CalculateCost(Vector2Int a, Vector2Int b)
        {
            int xDistance = Mathf.Abs(a.x - b.x);
            int yDistance = Mathf.Abs(a.y - b.y);
            int difference = Mathf.Abs(xDistance - yDistance);
            int cost = Mathf.Min(xDistance, yDistance) * MOVE_DIAGONAL_COST + difference * MOVE_STRAIGHT_COST;
            return cost;
        }
        public void SetGCost(int coefficient = 1)
        {
            gCost = parent != null ? parent.gCost + CalculateCost(coordinate, parent.coordinate) : 0;
            gCost *= coefficient;
        }
        public void SetHCost(Vector2Int destination, bool applyWeight = true, int coefficient = 1)
        {
            hCost = CalculateCost(coordinate, destination);
            if (applyWeight)
                hCost += weightFactor * 10;
            hCost *= coefficient;
        }
        public void SetFCost()
        {
            fCost = gCost + hCost;
        }
        public void SetAllCosts(Vector2Int destination, bool applyWeight = true, int gCoefficient = 1, int hCoefficient = 1)
        {
            SetGCost(gCoefficient);
            SetHCost(destination, applyWeight, hCoefficient);
            SetFCost();
        }
        public void ResetAllCosts()
        {
            gCost = hCost = fCost = 0;
            parent = null;
        }
    }
    public class NavigationGrid : MapGrid<PathNode>
    {
        //Working variables

        //Constructors
        public NavigationGrid(int width, int height, float cellSize, Vector3 startingPosition = default)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.startingPosition = startingPosition;

            gridArray = new PathNode[width, height];
            for (int x = 0; x < gridArray.GetLength(0); x++)
                for (int y = 0; y < gridArray.GetLength(1); y++)
                    gridArray[x, y] = new PathNode() { coordinate = new Vector2Int(x, y), weightFactor = 0, gCost = 0, hCost = 0, fCost = 0, parent = null };
        }
        public NavigationGrid(int width, int height, float cellSize, Dictionary<LayerMask, int> weightDictionary, Vector3 startingPosition = default)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.startingPosition = startingPosition;

            gridArray = new PathNode[width, height];
            for (int x = 0; x < gridArray.GetLength(0); x++)
                for (int y = 0; y < gridArray.GetLength(1); y++)
                    gridArray[x, y] = new PathNode() {coordinate = new Vector2Int(x, y), weightFactor = 0, gCost = 0, hCost = 0, fCost = 0, parent = null };

            SetWalkingLayers(weightDictionary);
        }

        protected override void SetDebugText(int x, int y, dynamic value)
        {
            if (((PathNode)value).weightFactor == int.MaxValue)
                base.SetDebugText(x, y, "N");
            else base.SetDebugText(x, y, ((PathNode)value).weightFactor);
            
        }
        public override void DrawDebug(int fontSize, Color color, float time = 10f, bool withValues = true)
        {
            debugTextArray = new TextMeshPro[width, height];
            Transform debugParent = new GameObject("NavigationGrid debug parent").transform;
            debugParent.position = Vector3.zero;

            for (int x = 0; x < gridArray.GetLength(0); x++)
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    if(withValues)
                        debugTextArray[x, y] = BobTools.CreateTextMeshPro(gridArray[x, y].weightFactor == int.MaxValue ? "N" : gridArray[x, y].weightFactor.ToString(), GetCenterPosition(x, y), debugParent, fontSize, color, TextAlignmentOptions.Center, new Vector2(cellSize, cellSize));
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), color, time);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), color, time);
                }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), color, time);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), color, time);
        }
        
        public void SetWalkingLayers(Dictionary<LayerMask, int> weightDictionary)
        {
            foreach(PathNode node in gridArray)
            {
                PathNode setNew = new PathNode 
                {
                    coordinate = node.coordinate,
                    gCost = node.gCost,
                    hCost = node.hCost,
                    fCost = node.fCost,
                    parent = node.parent,
                    weightFactor = 0
                };
                foreach(Collider2D obj in Physics2D.OverlapBoxAll(GetCenterPosition(node.coordinate), Vector2.one * cellSize, 0f))
                {
                    if (weightDictionary.ContainsKey(obj.gameObject.layer))
                        if(weightDictionary[obj.gameObject.layer] > setNew.weightFactor)
                            setNew.weightFactor = weightDictionary[obj.gameObject.layer];
                }
                SetValue(node.coordinate, setNew);
            }
        }
        private List<PathNode> GetNeighbourList(PathNode node, PathNode[,] pool)
        {
            Vector2Int offset = new Vector2Int();
            List<PathNode> neigbourList = new List<PathNode>();

            for (int i = 0; i < 8; i++)
            {
                switch (i)
                {
                    case 0:     //Up                        
                        offset = new Vector2Int(0, 1);
                        break;
                    case 1:     //Up-Left
                        offset = new Vector2Int(-1, 1);
                        break;
                    case 2:     //Left
                        offset = new Vector2Int(-1, 0);
                        break;
                    case 3:     //Down-Left
                        offset = new Vector2Int(-1, -1);
                        break;
                    case 4:     //Down
                        offset = new Vector2Int(0, -1);
                        break;
                    case 5:     //Down-Right
                        offset = new Vector2Int(1, -1);
                        break;
                    case 6:     //Right
                        offset = new Vector2Int(1, 0);
                        break;
                    case 7:     //Up-Right
                        offset = new Vector2Int(1, 1);
                        break;
                }
                Vector2Int neighbourCoordinate = node.coordinate + offset;
                if (IsGridCellExist(neighbourCoordinate) && pool[neighbourCoordinate.x, neighbourCoordinate.y].weightFactor != int.MaxValue)
                {
                    neigbourList.Add(pool[neighbourCoordinate.x, neighbourCoordinate.y]);
                }
            }
            return neigbourList;
        }
        private List<Vector3> CalculatePath(PathNode destination)
        {
            List<Vector3> path = new List<Vector3>();
            PathNode currentNode = destination;
            do
            {
                path.Insert(0, GetCenterPosition(currentNode.coordinate));
                currentNode = currentNode.parent;
            } while (currentNode.parent != null);
            return path;
        }
        //Find a path between two cells
        public List<Vector3> FindPath(Vector3 startPosition, Vector3 destinationPosition, out bool pathFound, bool applyWeight = true, int gCoefficient = 1, int hCoefficient = 1)
        {
            pathFound = false;
            Vector2Int startCell = GetCoordinates(startPosition);
            Vector2Int destinationCell = GetCoordinates(destinationPosition);

            if( !(IsGridCellExist(startCell) && IsGridCellExist(destinationCell)) || startCell == destinationCell)
            {
                //Invalid path
                return null;
            }

            PathNode[,] tempGridArray = new PathNode[width, height];
            Array.Copy(gridArray, tempGridArray, gridArray.Length);

            bool haveFirst = false;
            Vector3 firstCoordinate = startPosition;

            /*
            foreach (PathNode node in tempGridArray)        //Safety reset the costs    //Yet no needed
                node.ResetAllCosts();
            */

            List<PathNode> openList = new List<PathNode>();
            List<PathNode> closedList = new List<PathNode>();

            PathNode currentNode = new PathNode(startCell);

            currentNode.parent = null;
            currentNode.SetAllCosts(destinationCell, applyWeight, gCoefficient, hCoefficient);

            do
            {
                closedList.Add(currentNode);
                foreach (PathNode neighbour in GetNeighbourList(currentNode, tempGridArray))
                {                    
                    if (!closedList.Contains(neighbour))
                    {
                        if (openList.Contains(neighbour))
                            openList.Remove(neighbour);
                        if (neighbour.parent == null)
                        {
                            neighbour.parent = currentNode;
                            neighbour.SetAllCosts(destinationCell, applyWeight, gCoefficient, hCoefficient);
                        }
                        else if(currentNode.gCost + PathNode.CalculateCost(neighbour.coordinate, currentNode.coordinate) < neighbour.gCost)
                        {
                            neighbour.parent = currentNode;
                            neighbour.SetAllCosts(destinationCell, applyWeight, gCoefficient, hCoefficient);
                        }
                        openList.Add(neighbour);
                    }
                }
                if (openList.Count == 0)
                    break;

                PathNode smallest = openList[0];
                foreach (PathNode openNode in openList)
                {
                    if (openNode.fCost < smallest.fCost || openNode.fCost == smallest.fCost && openNode.hCost < smallest.hCost)
                        smallest = openNode;
                }
                if (!haveFirst)
                {
                    firstCoordinate = GetWorldPosition(smallest.coordinate);
                    haveFirst = true;
                }
                currentNode = smallest;
                openList.Remove(smallest);
                if (currentNode.coordinate == destinationCell)
                {
                    pathFound = true;
                    return CalculatePath(currentNode);
                }
            } while (openList.Count != 0);

            return new List<Vector3>() { firstCoordinate };
        }

        public List<Vector3> FindNextStep(Vector3 startPosition, Vector3 destinationPosition)
        {
            Vector2Int startCell = GetCoordinates(startPosition);
            Vector2Int destinationCell = GetCoordinates(destinationPosition);

            if (!(IsGridCellExist(startCell) && IsGridCellExist(destinationCell)) || startCell == destinationCell)
            {
                //Invalid path
                return null;
            }

            Vector3 nextStep = startPosition;
            int lowestCost = int.MaxValue;

            foreach(PathNode neighbour in GetNeighbourList(gridArray[startCell.x, startCell.y], gridArray))
            {
                neighbour.SetAllCosts(destinationCell);
                if(neighbour.fCost < lowestCost)
                {
                    lowestCost = neighbour.fCost;
                    nextStep = GetCenterPosition(neighbour.coordinate);
                }
                neighbour.ResetAllCosts();
            }

            return new List<Vector3>() { nextStep };
        } 

        
    }
}