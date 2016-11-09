using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueScript : MonoBehaviour {

	#region Variables

	[SerializeField]
	private Text myText;

	#endregion

	#region MonoBehaviours

	void OnTriggerEnter2D(Collider2D col )
	{
		if (col.name == "CheckpointDialogue" )
		{
			StartCoroutine(HideText(col.gameObject, 3.0f));
			myText.text = "Press 'E' when the meter is full to drop a checkpoint.";
		}
		else if (col.name == "CheckpointDialogue2" )
		{
			StartCoroutine(HideText(col.gameObject, 3.0f));
			myText.text = "The checkpoint meter only fills if you are not in slo-mo";
		}
		else if (col.name == "TimeControlDialogue" )
		{
			StartCoroutine(HideText(col.gameObject, 5.0f));
			myText.text = "Hold the Lshift button to slow down objects in your environment.";
		}
	}

	#endregion

	#region Coroutines

	IEnumerator HideText (GameObject col, float waitTime)
	{
		yield return new WaitForSeconds(waitTime);

		Destroy(col);

		myText.text = "";
	}

	#endregion

}
