using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	[SerializeField]
	Vector2Int gridSize;

	[Tooltip("World Grid Size - Should match the UnityEditor snap settings.")]
	[SerializeField]
	int unityGridSize = 10;
	public int UnityGridSize
	{
		get { return unityGridSize; }
	}

	Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
	public Dictionary<Vector2Int, Node> Grid
	{
		get { return grid; }
	}

	// --------------------------------------------------
	// --------------------------------------------------
	// ---------------------- Method Declarations Below--
	// --------------------------------------------------
	// --------------------------------------------------

	void Awake()
	{
		CreateGrid();
	}

	public void BlockNode(Vector2Int coord)
	{
		if (grid.ContainsKey(coord))
		{
			grid[coord].isWalkable = false;
		}
	}

	void CreateGrid()
	{
		for (int x = 0; x < gridSize.x; x++)
		{
			for (int y = 0; y < gridSize.y; y++)
			{
				Vector2Int coordinates = new Vector2Int(x, y);
				grid.Add(coordinates, new Node(coordinates, true));
			}
		}
	}

	public Vector2Int GetCoordinatesFromPosition(Vector3 position)
	{
		Vector2Int coordinates = new Vector2Int();

		coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
		coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

		return coordinates;
	}

	public Vector3 GetPositionFromCoordinates(Vector2Int coord)
	{
		Vector3 position = new Vector3();

		position.x = coord.x * unityGridSize;
		position.z = coord.y * unityGridSize;

		return position;
	}

	public Node GetNode(Vector2Int coorKey)
	{
		if (grid.ContainsKey(coorKey))
		{
			return grid[coorKey];
		}

		return null;
	}

	public void ResetNodes()
	{
		foreach (KeyValuePair<Vector2Int, Node> entry in grid)
		{
			entry.Value.connectedTo = null;
			entry.Value.isExplored = false;
			entry.Value.isPath = false;
		}
	}
}
