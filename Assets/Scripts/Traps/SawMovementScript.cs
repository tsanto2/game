using UnityEngine;
using System.Collections;

public class SawMovementScript : TimeControlledObject {

	#region Variables

	// Does this saw move vertically or horizontally
	[SerializeField]
	private bool movesHorizontally;

	// Does this saw start at the right/top side of its loop
	[SerializeField]
	private bool startFinished;

	// Starting x or y value of this saw
	private float startPos;

	// Distance this saw will travel
	[SerializeField]
	private float moveDistance;

	#endregion

	#region MonoBehaviours

	void Start ()
	{
		timeControl = GameObject.FindObjectOfType<TimeControlScript>();

		if ( movesHorizontally )
		{
			startPos = transform.position.x;

			if ( !startFinished )
				StartCoroutine(MoveRight());
			else
				StartCoroutine(MoveLeft());
		}
		else
		{
			startPos = transform.position.y;

			if ( !startFinished )
				StartCoroutine(MoveUp());
			else
				StartCoroutine(MoveDown());
		}
	}

	#endregion

	#region Coroutines

	IEnumerator MoveRight ()
	{
		float target;

		if ( !startFinished )
			target = startPos + moveDistance;
		else
			target = startPos;

		while ( x < target )
		{
			transform.Translate(Vector2.right * speed * Time.deltaTime);
			yield return new WaitForFixedUpdate();
			//yield return new WaitForSeconds(0.01f);
		}

		transform.position = new Vector2(target, y);

		StartCoroutine(MoveLeft());
	}

	IEnumerator MoveLeft ()
	{
		float target;

		if ( !startFinished )
			target = startPos;
		else
			target = startPos - moveDistance;

		while ( x > target )
		{
			transform.Translate(Vector2.left * speed * Time.deltaTime);
			yield return new WaitForFixedUpdate();
			//yield return new WaitForSeconds(0.01f);
		}

		transform.position = new Vector2(target, y);

		StartCoroutine(MoveRight());
	}

	IEnumerator MoveUp ()
	{
		float target;

		if ( !startFinished )
			target = startPos + moveDistance;
		else
			target = startPos;

		while ( y < target )
		{
			transform.Translate(Vector2.up * speed * Time.deltaTime);
			yield return new WaitForFixedUpdate();
		}

		transform.position = new Vector2(x, target);

		StartCoroutine(MoveDown());
	}

	IEnumerator MoveDown ()
	{
		float target;

		if ( !startFinished )
			target = startPos;
		else
			target = startPos - moveDistance;

		while (y > target )
		{
			transform.Translate(Vector2.down * speed * Time.deltaTime);
			yield return new WaitForFixedUpdate();
		}

		transform.position = new Vector2(x, target);

		StartCoroutine(MoveUp());
	}

	#endregion

}
