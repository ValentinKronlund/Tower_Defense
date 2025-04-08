using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	int goldReward = 25;

	[SerializeField]
	int goldPenalty = 50;

	Bank bank;

	void Start()
	{
		bank = FindObjectOfType<Bank>();
	}

	public void RewardsGold()
	{
		if (bank == null)
		{
			return;
		}

		bank.Deposit(goldReward);
	}

	public void StealGold()
	{
		if (bank == null)
		{
			return;
		}

		bank.Withdraw(goldPenalty);
	}
}
