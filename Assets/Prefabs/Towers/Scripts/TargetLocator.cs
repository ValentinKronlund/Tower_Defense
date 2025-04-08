using UnityEngine;

public class TargetLocator : MonoBehaviour
{
	[SerializeField]
	Transform weapon;

	[SerializeField]
	float towerRange = 30f;

	[SerializeField]
	float weaponRotationSpeed = 3f;

	[SerializeField]
	ParticleSystem projectileParticles;

	Transform target;

	void Update()
	{
		FindClosestTarget();
		AimWeapon();
	}

	void FindClosestTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		Transform closestTarget = null;
		float maxDistance = towerRange;

		foreach (GameObject enemy in enemies)
		{
			float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

			if (targetDistance < maxDistance)
			{
				closestTarget = enemy.transform;
				maxDistance = targetDistance;
			}
		}

		target = closestTarget;
	}

	void AimWeapon()
	{
		if (target == null)
		{
			Attack(false);
			return;
		}

		float targetDistance = Vector3.Distance(transform.position, target.position);
		if (targetDistance <= towerRange)
		{
			Quaternion startRotation = weapon.transform.rotation;
			Quaternion endRotation = Quaternion.LookRotation(
				target.position - weapon.transform.position
			);
			weapon.transform.rotation = Quaternion.Lerp(
				startRotation,
				endRotation,
				Time.deltaTime * weaponRotationSpeed
			);
			Attack(true);
		}
	}

	void Attack(bool isActive)
	{
		var emissionModule = projectileParticles.emission;
		emissionModule.enabled = isActive;
	}
}
