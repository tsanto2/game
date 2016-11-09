using UnityEngine;
using System.Collections;

public class TimeControlScript : MonoBehaviour {

	#region Variables

	private bool isFast, isPaused;

	public bool abilityActivated;

	[SerializeField]
	private float pauseLength;

	// Amount of times we can use the pause ability without recharging...
	[SerializeField]
	private int pauseCharge;

	// So we can see how much charge the player has...
	[SerializeField]
	private PlayerCheckpointSystem meter;

	#endregion

	#region MonoBehaviours

	void Awake ()
	{
		isFast = true;
	}

	void Update ()
	{
		CheckTimeSwitch();
		if (pauseCharge > 0)
			CheckPaused();
	}

	#endregion

	#region Private Methods

	// Check to see if player is slowing down time...
	private void CheckTimeSwitch ()
	{
		if ( Input.GetKeyDown(KeyCode.LeftShift) && !isPaused && meter.CheckpointMeterCharge > 0 )
		{
			isFast = false;
		}
		if ( !(meter.CheckpointMeterCharge > 0) )
			IsFast = true;
		if ( Input.GetKeyUp(KeyCode.LeftShift) )
		{
			isFast = true;
		}
	}

	//Check to see if player has paused time...
	private void CheckPaused ()
	{
		if ( Input.GetKeyDown(KeyCode.F) )
		{
			isPaused = true;
			pauseCharge--;

			StartCoroutine(PauseTime());
		}
	}

	#endregion

	#region Properties

	public bool IsFast
	{
		get
		{
			return isFast;
		}
		set
		{
			isFast = value;
		}

	}

	public bool IsPaused
	{
		get
		{
			return isPaused;
		}
		set
		{
			isPaused = value;
		}
	}

	public int PauseCharge
	{
		get
		{
			return pauseCharge;
		}
		set
		{
			pauseCharge = value;
		}
	}

	#endregion

	#region Coroutines

	// Allow isPaused to be true for pauseLength seconds...
	IEnumerator PauseTime ()
	{
		yield return new WaitForSeconds(pauseLength);

		isPaused = false;
		abilityActivated = false;
	}

	#endregion

}
