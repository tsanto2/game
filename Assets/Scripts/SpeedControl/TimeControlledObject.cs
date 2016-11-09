using UnityEngine;
using System.Collections;

public class TimeControlledObject : MonoBehaviour {

	#region Variables

	// The time control object
	[SerializeField]
	protected TimeControlScript timeControl;

	// Is this object an exception to time control
	[SerializeField]
	private bool isException;

	// Our object's speed multiplier when isFast
	[SerializeField]
	private float speedMultiplier;

	// Our object's base speed when !isFast
	[SerializeField]
	private float baseSpeed;

	// Our object's actual current speed
	protected float speed;

	// Our object's current x and y position values
	protected float x, y;

	#endregion

	#region MonoBehaviours

	private void Update ()
	{
		SetXY();

		if (!isException)
			SetSpeed();
	}

	#endregion

	#region Private Methods

	// So that we dont have to type out transform.position every time...
	private void SetXY ()
	{
		x = transform.position.x;
		y = transform.position.y;
	}


	#endregion

	#region Inherited Methods

	// Set speed of time controlled objects...
	protected void SetSpeed ()
	{
		if ( timeControl.IsFast && !timeControl.IsPaused && !isException )
		{
			speed = baseSpeed * speedMultiplier;
		}
		else if ( !timeControl.IsFast && !timeControl.IsPaused )
		{
			speed = baseSpeed;
		}
		else if ( timeControl.IsPaused && !isException )
		{
			speed = 0;
		}
	}

	#endregion

}
