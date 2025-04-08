using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
	[SerializeField]
	int cost = 75;

	[SerializeField]
	[Range(0, 5)]
	float buildSpeed = 1f;

	void Start()
	{
		StartCoroutine(Build());
	}

	IEnumerator Build()
	{
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(false);
			foreach (Transform grandchild in child)
			{
				grandchild.gameObject.SetActive(false);
			}
		}

		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(true);
			yield return new WaitForSeconds(buildSpeed);
			foreach (Transform grandchild in child)
			{
				grandchild.gameObject.SetActive(true);
			}
		}
	}

	public bool CreateTower(Tower tower, Vector3 position)
	{
		Bank bank = FindFirstObjectByType<Bank>();

		if (bank == null)
		{
			return false;
		}

		if (bank.CurrentBalance >= cost)
		{
			bank.Withdraw(cost);
			Instantiate(tower.gameObject, position, Quaternion.identity);
			return true;
		}

		return false;
	}
}
