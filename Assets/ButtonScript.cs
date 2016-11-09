using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	#region Variables



	#endregion

	#region MonoBehaviours



	#endregion

	#region Private Methods

	public void RestartLevel ()
	{
		Application.LoadLevel("Level1-2");
	}

	public void QuitGame ()
	{
		Application.Quit();
	}

	#endregion

}
