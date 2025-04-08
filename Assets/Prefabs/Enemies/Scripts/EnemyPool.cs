using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[Tooltip("The enemy prefab used in the pool.")]
	[SerializeField]
	GameObject enemyPrefab;

	[Tooltip("The size of the pool containing all enemies. Can be used as a pseudo enemy wave.")]
	[SerializeField]
	[Range(0, 50)]
	int poolSize = 5;

	[Tooltip("The time it takes an enemy in the pool to spawn.")]
	[SerializeField]
	[Range(0.5f, 30f)]
	float spawnTimer = 1f;

	GameObject[] pool;

	void Awake()
	{
		PopulatePool();
	}

	void Start()
	{
		StartCoroutine(SpawnEnemy());
	}

	void PopulatePool()
	{
		pool = new GameObject[poolSize];

		for (int i = 0; i < pool.Length; i++)
		{
			pool[i] = Instantiate(enemyPrefab, transform);
			pool[i].SetActive(false);
		}
	}

	void EnableObjectInPool()
	{
		for (int i = 0; i < pool.Length; i++)
		{
			if (pool[i].activeInHierarchy == false)
			{
				pool[i].SetActive(true);
				return;
			}
		}
	}

	IEnumerator SpawnEnemy()
	{
		while (true)
		{
			EnableObjectInPool();
			yield return new WaitForSeconds(spawnTimer);
		}
	}
}
