using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class TileSorter : MonoBehaviour
{
	void Start()
	{
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

		var sortedTiles = tiles
			.OrderBy(tile =>
			{
				string cleanedName = tile.name.Replace("(", "").Replace(")", "");
				string[] coords = cleanedName.Split(",");
				int x = int.Parse(coords[0]);
				int y = int.Parse(coords[1]);
				return (x * 10000) + y; // Sorying key: prioritize x over y
			})
			.ToArray();

		for (int i = 0; i < sortedTiles.Length; i++)
		{
			sortedTiles[i].transform.SetSiblingIndex(i); // If a GO shares a parent, their SiblingIndex is where they are positioned in the heirarchy
		}

		Debug.Log("TileSorter: Reordered tiles in the hierarchy!");
	}
}
