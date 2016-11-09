using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour {

	#region Variables

	// Our scene camera
	[SerializeField]
	private GameObject cam;

	// For checking which collider we are running into
	private BoxCollider2D rightCol, leftCol;

	// The offset and size of the colliders
	[SerializeField]
	private float rightOffset, leftOffset, xSize, ySize;

	// Camera movement speed
	[SerializeField]
	private float moveSpeed;

	#endregion

	#region MonoBehaviours

	void Start ()
	{
		SetupColliders();
	}

	void OnTriggerStay2D (Collider2D col)
	{
		if (col == rightCol )
		{
			cam.transform.Translate(moveSpeed * Vector2.right * Time.deltaTime);
		}
		else if ( col == leftCol )
		{
			cam.transform.Translate(moveSpeed * Vector2.left * Time.deltaTime);
		}
	}

	#endregion

	#region Private Methods

	private void SetupColliders ()
	{
		rightCol = cam.AddComponent<BoxCollider2D>();
		rightCol.offset = new Vector2(rightOffset, 0);
		rightCol.size = new Vector2(xSize, ySize);
		rightCol.isTrigger = true;

		leftCol = cam.AddComponent<BoxCollider2D>();
		leftCol.offset = new Vector2(leftOffset, 0);
		leftCol.size = new Vector2(xSize, ySize);
		leftCol.isTrigger = true;
	}

	#endregion

}
