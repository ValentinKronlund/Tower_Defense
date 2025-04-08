using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class TileSorter : MonoBehaviour
{
	void Start()
	{
		// Find all tiles in the hierarchy (assuming they have a specific tag like "Tile")
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

		// Sort tiles by their names based on coordinates
		var sortedTiles = tiles
			.OrderBy(tile =>
			{
				string cleanedName = tile.name.Replace("(", "").Replace(")", "");
				string[] coords = cleanedName.Split(',');
				int x = int.Parse(coords[0]);
				int y = int.Parse(coords[1]);
				return (x * 10000) + y; // Sorting key: prioritize x over y
			})
			.ToArray();

		// Reorder tiles in the hierarchy based on their sorted order
		for (int i = 0; i < sortedTiles.Length; i++)
		{
			sortedTiles[i].transform.SetSiblingIndex(i);
		}

		Debug.Log("Tiles reordered in the hierarchy!");
	}
}
