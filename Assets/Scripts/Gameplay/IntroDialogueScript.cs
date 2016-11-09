using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroDialogueScript : MonoBehaviour {

	[SerializeField]
	private float moveDist, startY, moveSpeed;

	private PlayerMovement pm;

	[SerializeField]
	private GameObject myTextObj;

	private Text txt;

	private bool speaking;

	private string[] dialogue;

	void Start () {
		txt = myTextObj.GetComponent<Text>();
		pm = GameObject.FindObjectOfType<PlayerMovement>();

		dialogue = new string[9];
		startY = transform.position.y;

		dialogue[0] = "Hello.";
		dialogue[1] = "You look confused... Do you not know what has happened?";
		dialogue[2] = "You are the last possible candidate on His list, and I have been sent to gather you for testing.";
		dialogue[3] = "If you pass this test, and you are indeed The Chosen One, He will spare your planet from the harvest.";
		dialogue[4] = "The harvest? You don't need to worry about that now. For now you must only focus on mastering the abilities which He has bestowed upon you.";
		dialogue[5] = "With this gift, you have been granted the ability to control the flow of time around you.";
		dialogue[6] = "Imagine that time as you perceive it, is a river. Your people are but leaves, drifting along, out of control, towards a definite end.";
		dialogue[7] = "But now, you can become an exception to this rule. You can choose to be a leaf, or you can become a boulder, defying the will of the river.";
		dialogue[8] = "You will come to understand this during your tests. But now I must go. I will see you again, over the course of your testing. Good luck.";

		speaking = true;
		StartCoroutine(FloatUp());
		StartCoroutine(ShowDialogue());
	}

	IEnumerator ShowDialogue ()
	{
		for (int x = 0; x < 9; x++ )
		{
			char[] temp = new char[dialogue[x].Length];
			for (int i = 0; i < dialogue[x].Length; i++ )
			{
				temp[i] = dialogue[x][i];
			}

			for (int y = 0; y < dialogue[x].Length; y++ )
			{
				txt.text += temp[y];
				transform.FindChild("Audio").GetComponent<AudioSource>().pitch = Random.Range(0.5f, 2.5f);
				transform.FindChild("Audio").GetComponent<AudioSource>().Play();
				yield return new WaitForSeconds(0.08f);
				transform.FindChild("Audio").GetComponent<AudioSource>().Stop();
			}

			yield return new WaitForSeconds(3.0f);

			txt.text = "";
		}

		speaking = false;

		while (transform.position.y < 100 )
		{
			transform.Translate(Vector2.up * 100 * Time.deltaTime);
			yield return new WaitForSeconds(0.02f);
		}

		txt.text = "Press 'X' to alter the flow of time.";

		yield return new WaitForSeconds(3.0f);

		txt.text = "";

		pm.canMove = true;

	}

	IEnumerator FloatUp ()
	{
		if ( speaking )
		{
			while ( transform.position.y < startY + moveDist )
			{
				transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
				yield return new WaitForSeconds(0.01f);
			}

			transform.position = new Vector3(transform.position.x, startY + moveDist);

			StartCoroutine(FloatDown());
		}
	}

	IEnumerator FloatDown ()
	{
		if ( speaking )
		{
			while ( transform.position.y > startY - moveDist )
			{
				transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
				yield return new WaitForSeconds(0.01f);
			}

			transform.position = new Vector3(transform.position.x, startY - moveDist);

			StartCoroutine(FloatUp());
		}
	}
}
