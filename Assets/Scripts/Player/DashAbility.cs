using UnityEngine;
using System.Collections;

public class DashAbility : BasePauseAbility {

	#region Variables

	// So we can use flipX to see which direction we are facing
	private SpriteRenderer sRenderer;

	// So we can control the effect of gravity on our player
	private Rigidbody2D rBody;

	// Which way are we dashing
	private Vector2 direction;

	// Where are we going and where we started from
	private float target, startPos;

	// How far are are we dashing and how fast
	[SerializeField]
	private float dashDistance, dashSpeed;

	#endregion

	#region MonoBehaviours

	private void Start ()
	{
		myKey = KeyCode.D;

		sRenderer = GetComponent<SpriteRenderer>();
		rBody = GetComponent<Rigidbody2D>();
	}

	#endregion

	#region Override Methods

	protected override void ActivateAbility ()
	{
		CheckDirection();

		StartCoroutine(Dash(direction));
	}

	#endregion

	#region Private Methods

	private void CheckDirection ()
	{
		if ( Input.GetKey(KeyCode.UpArrow) )
		{
			direction = Vector2.up;
			target = transform.position.y + dashDistance;
			startPos = transform.position.y;
		}
		/*else if ( Input.GetKey(KeyCode.DownArrow) )
		{
			direction = Vector2.down;
			target = transform.position.y - dashDistance;
			startPos = transform.position.y;
		}*/
		else if ( !sRenderer.flipX )
		{
			direction = Vector2.right;
			target = transform.position.x + dashDistance;
			startPos = transform.position.x;
		}
		else if ( sRenderer.flipX )
		{
			direction = Vector2.left;
			target = transform.position.x - dashDistance;
			startPos = transform.position.x;
		}
	}

	private float CheckDist (Vector2 dir)
	{
		float distTravelled;

		if ( dir == Vector2.right || dir == Vector2.left )
			distTravelled = Mathf.Abs(transform.position.x - startPos);
		else
			distTravelled = Mathf.Abs(transform.position.y - startPos);

		return distTravelled;
	}

	#endregion

	#region Coroutines

	IEnumerator Dash (Vector2 dir)
	{
		float distTravelled = 0;

		rBody.gravityScale = 0;

		while (distTravelled < dashDistance )
		{
			transform.Translate(dir * dashSpeed * Time.deltaTime);

			yield return new WaitForFixedUpdate();

			distTravelled = CheckDist(dir);
		}

		rBody.gravityScale = 5;
	}

	#endregion

}
