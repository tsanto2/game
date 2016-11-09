using UnityEngine;
using System.Collections;

public class BaseProjectileScript : TimeControlledObject {

	#region Variables

	// Has this object hit anything
	private bool hasCollided;

	// Which direction is this going
	public Vector2 direction;

	// This object's sprite renderer
	private SpriteRenderer sRenderer;

	#endregion

	#region MonoBehaviours

	private void Start ()
	{
		timeControl = GameObject.FindObjectOfType<TimeControlScript>();
		sRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
	}

	private void FixedUpdate ()
	{
		if ( !sRenderer.isVisible )
		{
			Destroy(gameObject);
		}
		transform.Translate(direction * speed * Time.deltaTime);
	}

	#endregion

}
