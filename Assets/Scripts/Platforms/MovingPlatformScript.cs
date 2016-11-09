using UnityEngine;
using System.Collections;

public class MovingPlatformScript : TimeControlledObject {

	#region Variables

	// Is this platform player activated...
	[SerializeField]
	private bool isPlayerActivated, isLoop;

	// Waypoints for platform to follow
	[SerializeField]
	private Transform[] waypoints;

	// Which direction should this platform move
	private Vector3 direction, target;

	// For getting x and y travel distance
	private float xDist, yDist;

	// The last position before translate
	private Vector3 lastPos;

	// Counter for waypoints
	private int i;

	#endregion

	#region MonoBehaviours

	private void Start ()
	{
		i = 0;

		if ( !isPlayerActivated )
		{
			StartCoroutine(MoveToWaypoint());
		}
	}

	#endregion

	#region Private Methods

	private void SetDirection ()
	{
		Vector3 next;

		if ( i == waypoints.Length - 1 )
			next = waypoints[0].position;
		else
			next = waypoints[i + 1].position;

		direction = next - waypoints[i].position;
		target = next;
	}

	#endregion

	#region Coroutines

	IEnumerator MoveToWaypoint ()
	{
		SetDirection();

		while ( Mathf.Abs(direction.x) > xDist || Mathf.Abs(direction.y) > yDist )
		{
			lastPos = transform.position;
			transform.Translate(direction.normalized * speed * Time.deltaTime);
			xDist += Mathf.Abs(transform.position.x - lastPos.x);
			yDist += Mathf.Abs(transform.position.y - lastPos.y);
			yield return new WaitForFixedUpdate();
		}

		xDist = 0;
		yDist = 0;
		transform.position = target;

		if ( i == waypoints.Length - 1 )
			i = 0;
		else
			i++;

		StartCoroutine(MoveToWaypoint());
	}

	#endregion

}
