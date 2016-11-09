using UnityEngine;
using System.Collections;

public class BaseProjectileSpawnerScript : MonoBehaviour {

	#region Variables

	// The time control object
	private TimeControlScript timeControl;

	// How long to wait between each shot and how much slower !isFast is
	[SerializeField]
	private float baseWaitTime, waitTimeMultiplier;

	// How long we are actually waiting
	private float waitTime;

	// For keeping track when slowing down time
	private float timer;

	// What we are shooting
	[SerializeField]
	private GameObject projectilePrefab;

	//For setting direction of projectile from inspector
	private GameObject thisProjectile;
	private BaseProjectileScript thisProjScript;

	// Which direction is our projectile moving
	[SerializeField]
	private Vector2 direction;

	// Where will our projectile fire from
	[SerializeField]
	private GameObject spawnPos;

	[SerializeField]
	private bool isException;

	#endregion

	#region MonoBehaviours

	private void Start ()
	{
		timeControl = GameObject.FindObjectOfType<TimeControlScript>();

		StartCoroutine(FireProjectile());
	}

	#endregion

	#region Private Methods

	private void SetWaitTime ()
	{
		if ( !timeControl.IsFast && !timeControl.IsPaused )
		{
			waitTime = baseWaitTime;
		}
		else if ( timeControl.IsFast && !timeControl.IsPaused )
		{
			waitTime = baseWaitTime;
		}
	}

	#endregion

	#region Coroutines

	IEnumerator FireProjectile ()
	{
		if (!isException)
			SetWaitTime();

		while ( timer < waitTime )
		{
			if ( timeControl.IsFast && !timeControl.IsPaused )
				timer += 0.01f;
			else if ( !timeControl.IsFast && !timeControl.IsPaused )
				timer += 0.01f * waitTimeMultiplier;
			yield return new WaitForFixedUpdate();
		}

		if ( !timeControl.IsPaused ) {
			thisProjectile = (GameObject)Instantiate(projectilePrefab, spawnPos.transform.position, spawnPos.transform.rotation);
			thisProjScript = thisProjectile.GetComponent<BaseProjectileScript>();
			thisProjScript.direction = direction;
		}

		timer = 0;

		StartCoroutine(FireProjectile());
	}

	#endregion

}
