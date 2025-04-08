using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
	[SerializeField] Color defaultColor = Color.white;

	[SerializeField] Color blockedColor = Color.red;

	[SerializeField] Color exploredColor = Color.magenta;

	[SerializeField] Color pathColor = Color.yellow;

	TextMeshPro label;
	Vector2Int coordinates = new Vector2Int();

	void Awake()
	{
		label = GetComponent<TextMeshPro>();
		DisplayCoordinates();
	}

	void Update()
	{
		if (!Application.isPlaying)
		{
			DisplayCoordinates();
			UpdateObjectName();
		}

		ToggleLabels();
	}

	//
	// Cusom Functions
	//

	void DisplayCoordinates()
	{
		coordinates.x = Mathf.RoundToInt(
			transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x
		);
		coordinates.y = Mathf.RoundToInt(
			transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z
		);

		label.text = $"{coordinates.x},{coordinates.y}";
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
