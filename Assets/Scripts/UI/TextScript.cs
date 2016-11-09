using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextScript : MonoBehaviour {

	#region Variables

	private Text myText;

	[SerializeField]
	private TimeControlScript timeControl;

	#endregion

	#region MonoBehaviours

	private void Start ()
	{
		myText = GetComponent<Text>();
	}

	private void Update ()
	{
		if (name == "PauseCharge" )
		{
			myText.text = timeControl.PauseCharge.ToString();
		}
	}

	#endregion

}
