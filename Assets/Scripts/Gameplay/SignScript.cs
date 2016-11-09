using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SignScript : MonoBehaviour {

	#region Variables

	[SerializeField]
	private GameObject myBtn, myTxt;
	private Text txt;
	private PlayerMovement player;

	private bool inZone, triggered;

	#endregion

	#region Methods

	void Start ()
	{
		player = GameObject.FindObjectOfType<PlayerMovement>();
		txt = myTxt.GetComponent<Text>();
	}

	void Update ()
	{
		if ( (Input.GetKeyDown(KeyCode.Y) || Input.GetButtonDown("UpBombXbox")) && !triggered )
		{
			if ( inZone && !triggered )
			{
				triggered = true;
				player.canMove = false;
				myBtn.SetActive(false);
				StartCoroutine(ShowText());
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player" )
		{
			myBtn.SetActive(true);
			inZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D col )
	{
		if (col.tag == "Player" )
		{
			inZone = false;
			myBtn.SetActive(false);
		}
	}

	#endregion

	#region Coroutines

	IEnumerator ShowText ()
	{
		myTxt.SetActive(true);

		txt.text = "This is a test.";

		yield return new WaitForSeconds(3.0f);

		myTxt.SetActive(false);
		myBtn.SetActive(true);

		player.canMove = true;

		triggered = false;
	}

	#endregion

}
