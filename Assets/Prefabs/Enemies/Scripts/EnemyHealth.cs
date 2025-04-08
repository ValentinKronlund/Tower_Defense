using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyMover))]
public class EnemyHealth : MonoBehaviour
{
	[Tooltip("The maximum (starting) health for the enemy.")]
	[SerializeField]
	int maxHitPoints = 5;

	[Tooltip("Adds amount to maxHitPoints when enemy dies.")]
	[SerializeField]
	int healthIncreaseRamp = 1;
	int currentHitPoints = 0;

	Enemy enemy;
	EnemyMover enemyMover;

	void Start()
	{
		enemy = GetComponent<Enemy>();
		enemyMover = GetComponent<EnemyMover>();
	}

	void OnEnable()
	{
		currentHitPoints = maxHitPoints;
	}

	void OnParticleCollision(GameObject other)
	{
		ProcessHit();
	}

	void ProcessHit()
	{
		currentHitPoints--;

		if (currentHitPoints <= 0)
		{
			gameObject.SetActive(false);
			enemy.RewardsGold();
			maxHitPoints += healthIncreaseRamp;
			enemyMover.IncreaseMovementSpeed(0.1f);
		}
	}
}
