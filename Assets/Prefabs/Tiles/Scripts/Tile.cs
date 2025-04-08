using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField]
	private Material placeableOutlineMaterial;

	[SerializeField]
	private Material notPlaceableOutlineMaterial;

	[SerializeField]
	Tower towerPrefab;

	[SerializeField]
	private bool isPlaceable;
	public bool IsPlaceable
	{
		get { return isPlaceable; }
	}

	private Material originalMaterial;
	private Renderer rend; // Reference to the Renderer
	private GridManager gridManager;
	Pathfinder pathfinder;
	private Vector2Int coordinates = new Vector2Int();

	// --------------------------------------------------
	// --------------------------------------------------
	// ---------------------- Method Declarations Below--
	// --------------------------------------------------
	// --------------------------------------------------



	void Awake()
	{
		gridManager = FindFirstObjectByType<GridManager>();
		pathfinder = FindFirstObjectByType<Pathfinder>();
	}

	void Start()
	{
		// Get the Renderer component from the child
		rend = GetComponentInChildren<Renderer>();
		if (rend != null)
		{
			originalMaterial = rend.material;
		}
		else
		{
			Debug.LogError("Renderer component not found on any child of " + gameObject.name);
		}

		if (gridManager != null)
		{
			coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

			if (!isPlaceable)
			{
				gridManager.BlockNode(coordinates);
			}
		}
	}

	void OnMouseOver()
	{
		if (rend != null)
		{
			if (
				gridManager.GetNode(coordinates).isWalkable
				&& !pathfinder.WillBlockPath(coordinates)
			)
			{
				rend.material = placeableOutlineMaterial;

				if (Input.GetMouseButtonDown(0))
				{
					bool successfulPlacement = towerPrefab.CreateTower(
						towerPrefab,
						transform.position
					);

					if (successfulPlacement)
					{
						gridManager.BlockNode(coordinates);
						pathfinder.NotifyReceivers();
					}
				}
			}
			else if (!isPlaceable)
			{
				rend.material = notPlaceableOutlineMaterial;
			}
		}
	}

	void OnMouseExit()
	{
		if (rend != null)
		{
			rend.material = originalMaterial;
		}
	}
}
