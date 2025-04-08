using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
	[SerializeField]
	Color defaultColor = Color.white;

	[SerializeField]
	Color blockedColor = Color.red;

	[SerializeField]
	Color exploredColor = Color.magenta;

	[SerializeField]
	Color pathColor = Color.yellow;

	TextMeshPro label;
	Vector2Int coordinates = new Vector2Int();
	GridManager gridManager;

	void Awake()
	{
		label = GetComponent<TextMeshPro>();
		gridManager = FindFirstObjectByType<GridManager>();
		DisplayCoordinates();
	}

	void Update()
	{
		if (!Application.isPlaying)
		{
			DisplayCoordinates();
			UpdateObjectName();
		}

		SetLabelColor();
		ToggleLabels();
	}

	void SetLabelColor()
	{
		if (gridManager == null)
			return;

		Node node = gridManager.GetNode(coordinates);

		if (node == null)
			return;

		if (!node.isWalkable)
		{
			label.color = blockedColor;
		}
		else if (node.isPath)
		{
			label.color = pathColor;
		}
		else if (node.isExplored)
		{
			label.color = exploredColor;
		}
		else
		{
			label.color = defaultColor;
		}
	}

	void DisplayCoordinates()
	{
		if (gridManager == null)
			return;

		coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
		coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

		label.text = coordinates.x + "," + coordinates.y;
	}

	void ToggleLabels()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			label.enabled = !label.IsActive();
		}
	}

	void UpdateObjectName()
	{
		transform.parent.name = coordinates.ToString();
	}
}
