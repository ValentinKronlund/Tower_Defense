using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI displayHealth;

	[SerializeField]
	int startingHp = 5;

	[SerializeField]
	int currentHp;
	public int CurrentHp
	{
		get { return currentHp; }
	}

	void Awake()
	{
		currentHp = startingHp;
		UpdateDisplay();
	}

	void Update()
	{
		if (currentHp <= 0)
		{
			// Lose the game
			Debug.Log("You have lost the game");
			ReloadScene();
		}
	}

	public void DamagePlayer(int value)
	{
		if (currentHp > 0)
		{
			currentHp -= value;
		}
		UpdateDisplay();
	}

	public void HealPlayer(int value)
	{
		currentHp += value;
		UpdateDisplay();
	}

	void ReloadScene()
	{
		Scene currentScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(currentScene.buildIndex);
	}

	void UpdateDisplay()
	{
		var hearts = new List<String> { };

		for (int i = 0; i < currentHp; i++)
		{
			hearts.Add("<3");
		}

		displayHealth.text = $"Health: {string.Join(" ", hearts)}";
	}
}
