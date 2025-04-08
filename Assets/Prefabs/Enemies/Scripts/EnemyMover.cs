using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
	[Tooltip("The movement speed (1-5f) of the enemy.")]
	[SerializeField]
	[Range(0f, 5f)]
	List<Node> path = new List<Node>();

	Enemy enemy;
	GridManager gridManager;
	Pathfinder pathfinder;
	PlayerHealth playerHp;
	float movementSpeed = 1f;

	// --------------------------------------------------
	// --------------------------------------------------
	// ---------------------- Method Declarations Below--
	// --------------------------------------------------
	// --------------------------------------------------

	void Awake()
	{
		enemy = GetComponent<Enemy>();
		gridManager = FindFirstObjectByType<GridManager>();
		pathfinder = FindFirstObjectByType<Pathfinder>();
		playerHp = FindFirstObjectByType<PlayerHealth>();
	}

	void OnEnable()
	{
		ReturnToStart();
		RecalculatePath(true);
	}

	void FinishPath()
	{
		enemy.StealGold();
		playerHp.DamagePlayer(1);
		gameObject.SetActive(false);
	}

	IEnumerator FollowPath()
	{
		for (int i = 1; i < path.Count; i++)
		{
			Vector3 startPosition = transform.position;
			Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
			float travelPercent = 0f;

			transform.LookAt(endPosition);

			while (travelPercent < 1)
			{
				travelPercent += Time.deltaTime * movementSpeed;
				transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
				yield return new WaitForEndOfFrame();
			}
		}

		FinishPath();
	}

	public void IncreaseMovementSpeed(float value)
	{
		movementSpeed += Mathf.Abs(value);
	}

	void RecalculatePath(bool resetPath)
	{
		Vector2Int coordinates = new Vector2Int();

		if (resetPath)
		{
			coordinates = pathfinder.StartCoordinates;
		}
		else
		{
			coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
		}
		StopAllCoroutines();
		path.Clear();
		path = pathfinder.GetNewPath(coordinates);
		StartCoroutine(FollowPath());
	}

	void ReturnToStart()
	{
		transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
	}
}
