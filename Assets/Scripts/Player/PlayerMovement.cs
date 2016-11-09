using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

	#region Variables

	public Rigidbody2D rb;

	public SpriteRenderer sr;

	private Animator anim;

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private float moveSpeed, moveForce, jumpSpd, jumpShortSpd;

	[SerializeField]
	private bool isGrounded, jump, jumpCancel, isMoving, leftReset, rightReset;

	public bool canMove;

	#endregion

	#region MonoBehaviours

	void Start ()
	{
		if (!Application.isEditor)
			Cursor.visible = false;

		sr = gameObject.GetComponent<SpriteRenderer>();
		anim = gameObject.GetComponent<Animator>();

		// Player's rigidbody
		rb = gameObject.GetComponent<Rigidbody2D>();

		canMove = true;
	}

	void Update ()
	{
		CheckInput();
		CheckJump();

		if (rb.velocity.y < 0 )
		{
			anim.SetBool("isFalling", true);
		}
	}

	#endregion

	#region Private Methods

	private void CheckInput ()
	{
		// Left/Right movement...
		//if ( (Input.GetButton("Left") || (Input.GetAxis("Horizontal") < -0.2f)) && canMove )
		if (Input.GetKey(KeyCode.LeftArrow) && canMove)
		{
			leftReset = false;
			rightReset = false;
			anim.SetBool("isIdle", false);
			anim.SetBool("isRunning", true);
			MoveLeft();
		}
		//else if ( (Input.GetButton("Right") || (Input.GetAxis("Horizontal") > 0.2f)) && canMove )
		else if (Input.GetKey(KeyCode.RightArrow) && canMove)
		{
			leftReset = false;
			rightReset = false;
			anim.SetBool("isIdle", false);
			anim.SetBool("isRunning", true);
			MoveRight();
		}
		// For a bit of a slide at the end of movement...
		else if ( (((Input.GetAxis("Horizontal") > -0.2f) || !Input.GetButton("Left")) && rb.velocity.x < -0.5f && !leftReset) || (Input.GetButtonUp("Left") && !leftReset) )
		{
			leftReset = true;
			anim.SetBool("isIdle", true);
			anim.SetBool("isRunning", false);
			rb.velocity = new Vector2(0, rb.velocity.y);
			rb.AddForce(new Vector2(-moveForce, 0));
		}
		else if ( (((Input.GetAxis("Horizontal") < 0.2f) || !Input.GetButton("Right")) && rb.velocity.x > 0.5f && !rightReset) || (Input.GetButtonUp("Right") && !rightReset) )
		{
			rightReset = true;
			anim.SetBool("isIdle", true);
			anim.SetBool("isRunning", false);
			rb.velocity = new Vector2(0, rb.velocity.y);
			rb.AddForce(new Vector2(moveForce, 0));
		}

		// Jumping...
		if ( Input.GetKeyDown(KeyCode.UpArrow) && canMove )
		{
			if ( isGrounded )
			{
				anim.SetBool("isIdle", false);
				anim.SetBool("isJumpingUp", true);
				isGrounded = false;
				jump = true;
			}
		}
		if ( Input.GetKeyUp(KeyCode.UpArrow) )
		{
			if ( !isGrounded )
			{
				anim.SetBool("isIdle", false);
				anim.SetBool("isJumpingUp", false);
				anim.SetBool("isFalling", true);
				jumpCancel = true;
			}
		}
	}

	// Translate the player to the left
	private void MoveLeft ()
	{
		sr.flipX = true;
		//transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);

		if ( rb.velocity.x > -moveSpeed )
		{
			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
		}
		if ( rb.velocity.x <= -moveSpeed )
			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
	}

	// Translate the player to the left
	private void MoveRight ()
	{
		sr.flipX = false;
		//transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);

		if ( rb.velocity.x < moveSpeed )
		{
			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
		}
		if ( rb.velocity.x >= moveSpeed )
		{
			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
		}
	}

	// Make the player jump
	private void CheckJump ()
	{
		//rb.AddForce(Vector3.up * jumpPower);
		// Normal jump (full speed)
		if ( jump )
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpSpd);
			jump = false;
		}
		// Cancel the jump when the button is no longer pressed
		if ( jumpCancel )
		{
			if ( rb.velocity.y > jumpShortSpd )
				rb.velocity = new Vector2(rb.velocity.x, jumpShortSpd);
			jumpCancel = false;
		}
	}

	#endregion

	#region Public Methods

	// Used to set move/jump speed externally
	public void SetMoveJump ( float speed, float power )
	{
		moveSpeed = speed;
		jumpSpd = power;
	}

	// Check to see if we are on the ground
	public bool GetGrounded ()
	{
		return isGrounded;
	}

	#endregion

	#region Collisions

	void OnCollisionEnter2D ( Collision2D col )
	{
		string colTag = col.collider.tag;

		// Reset jump counter
		if ( colTag == "Ground" || colTag == "MovingPlatform" )
		{
			anim.SetBool("isFalling", false);
			anim.SetBool("isJumpingUp", false);
			isGrounded = true;
		}
	}

	void OnCollisionStay2D ( Collision2D col )
	{
		string colTag = col.collider.tag;

		// Reset jump counter
		if ( colTag == "Ground" || colTag == "MovingPlatform" )
		{
			isGrounded = true;
			if (colTag == "MovingPlatform")
				transform.parent = col.gameObject.transform;
		}
	}

	void OnCollisionExit2D (Collision2D col)
	{
		string colTag = col.collider.tag;

		if ( colTag == "Ground" || colTag == "MovingPlatform" )
		{
			isGrounded = false;
			transform.parent = null;
		}
	}

	#endregion

	#region Coroutines

	#endregion

}
