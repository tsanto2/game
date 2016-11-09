using UnityEngine;
using System.Collections;

public class BasePauseAbility : MonoBehaviour {

	#region Variables

	private bool isPaused, canActivate, unlocked;

	protected KeyCode myKey;

	[SerializeField]
	protected TimeControlScript timeControl;

	#endregion

	#region MonoBehaviours
	
	private void Update ()
	{
		isPaused = timeControl.IsPaused;

		// Check to see if time is paused and we have not used an ability yet...
		CheckCanActivate();

		// Check to see if the player has activated an ability...
		CheckPlayerActivation();
	}

	#endregion

	#region Private Methods

	private void CheckCanActivate ()
	{
		if ( isPaused && !timeControl.abilityActivated )
		{
			canActivate = true;
		}
	}

	private void CheckPlayerActivation ()
	{
		if ( Input.GetKeyDown(myKey) && canActivate && unlocked )
		{
			timeControl.abilityActivated = true;
			canActivate = false;

			ActivateAbility();
		}
	}

	#endregion

	#region Protected Methods

	protected virtual void ActivateAbility () { }

	#endregion

}
