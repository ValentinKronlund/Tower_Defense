using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
	[SerializeField]
	Vector2Int startCoordinates;
	public Vector2Int StartCoordinates
	{
		get { return startCoordinates; }
	}

	[SerializeField]
	Vector2Int destinationCoordinates;
	public Vector2Int DestinationCoordinates
	{
		get { return destinationCoordinates; }
	}

	[SerializeField]
	Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

	Node startNode;
	Node destinationNode;
	Node currentSearchNode;

	GridManager gridManager;
	Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
	Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
	Queue<Node> frontier = new Queue<Node>();

	// --------------------------------------------------
	// --------------------------------------------------
	// ---------------------- Method Declarations Below--
	// --------------------------------------------------
	// --------------------------------------------------

	void Awake()
	{
		gridManager = FindFirstObjectByType<GridManager>();

		if (gridManager != null)
		{
			grid = gridManager.Grid;
			startNode = grid[startCoordinates];
			destinationNode = grid[destinationCoordinates];
		}
	}

	void Start()
	{
		GetNewPath();
	}

	void BreadthFirstSearch(Vector2Int coord)
	{
		startNode.isWalkable = true;
		destinationNode.isWalkable = true;

		gridManager.ResetNodes();
		frontier.Clear();
		reached.Clear();

		bool isRunning = true;

		frontier.Enqueue(grid[coord]);
		reached.Add(coord, grid[coord]);

		while (frontier.Count > 0 && isRunning)
		{
			currentSearchNode = frontier.Dequeue();
			currentSearchNode.isExplored = true;
			ExploreNeighbours();
			if (currentSearchNode.coordinates == destinationCoordinates)
			{
				isRunning = false;
			}
		}
	}

	List<Node> BuildPath()
	{
		List<Node> path = new List<Node>();
		Node currentNode = destinationNode;

		path.Add(currentNode);
		currentNode.isPath = true;

		while (currentNode.connectedTo != null)
		{
			currentNode = currentNode.connectedTo;
			path.Add(currentNode);
			currentNode.isPath = true;
		}

		path.Reverse();

		return path;
	}

	public List<Node> GetNewPath()
	{
		return GetNewPath(startCoordinates);
	}

	public List<Node> GetNewPath(Vector2Int coord)
	{
		gridManager.ResetNodes();
		BreadthFirstSearch(coord);
		return BuildPath();
	}

	void ExploreNeighbours()
	{
		List<Node> neighbours = new List<Node>();

		foreach (Vector2Int direction in directions)
		{
			Vector2Int n_coords = currentSearchNode.coordinates + direction;

			if (grid.ContainsKey(n_coords))
			{
				neighbours.Add(grid[n_coords]);
			}
		}

		foreach (Node neighbour in neighbours)
		{
			if (!reached.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
			{
				neighbour.connectedTo = currentSearchNode;
				reached.Add(neighbour.coordinates, neighbour);
				frontier.Enqueue(neighbour);
			}
			;
		}
	}

	public void NotifyReceivers()
	{
		BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
	}

	public bool WillBlockPath(Vector2Int coord)
	{
		if (grid.ContainsKey(coord))
		{
			bool previousState = grid[coord].isWalkable;
			grid[coord].isWalkable = false;

			List<Node> newPath = GetNewPath();
			grid[coord].isWalkable = previousState;

			if (newPath.Count <= 1)
			{
				GetNewPath();
				return true;
			}
		}

		return false;
	}
}
