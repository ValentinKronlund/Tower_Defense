using System;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI displayBalance;

	[SerializeField]
	int startingBalance = 150;

	[SerializeField]
	int currentBalance;
	public int CurrentBalance
	{
		get { return currentBalance; }
	}

	void Awake()
	{
		Deposit(startingBalance);
		UpdateDisplay();
	}

	public void Deposit(int value)
	{
		currentBalance += Mathf.Abs(value);
		UpdateDisplay();
	}

	void UpdateDisplay()
	{
		displayBalance.text = $"Gold: {currentBalance}";
	}

	public void Withdraw(int value)
	{
		if (currentBalance - Mathf.Abs(value) <= 0)
		{
			currentBalance = 0;
		}
		else
		{
			currentBalance -= Mathf.Abs(value);
		}

		UpdateDisplay();
	}
}
