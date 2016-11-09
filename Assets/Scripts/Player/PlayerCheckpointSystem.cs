using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCheckpointSystem : MonoBehaviour {

	#region Variables

	// The time control object
	[SerializeField]
	private TimeControlScript timeControl;

	// The UI bar for displaying checkpoint meter
	[SerializeField]
	private Image myCheckpointMeter;

	// For resetting the camera
	[SerializeField]
	private Camera cam;

	// Initial position of player
	private Vector3 initPos;

	// Furthest x position the player has been located
	private float curPos;

	// How much of the meter is filled
	private float meterCharge;

	// How much to subtract in slo-mo
	[SerializeField]
	private float subAmount;

	// Can the player drop a checkpoint
	private bool canDropCheckpoint;

	// Position of player's last checkpoint drop
	private Vector3 checkpointPos;

	// Checkpoint prefab for spawning visual representation of checkpoint
	[SerializeField]
	private GameObject checkpointPrefab;

	// For destroying our checkpoint object after a new one is spawned
	private GameObject lastCheckpoint;

	#endregion

	#region MonoBehaviours

	void Start ()
	{
		checkpointPos = transform.position;
		initPos = transform.position;
		curPos = transform.position.x;
		meterCharge = 0;
	}

	void Update ()
	{
		UpdateMeterCharge();

		if ( canDropCheckpoint )
			CheckCheckpointDropped();
	}

	#endregion

	#region Private Methods
	
	private void UpdateMeterCharge ()
	{
		if ( !timeControl.IsFast )
		{
			meterCharge -= subAmount;

			if ( transform.position.x > curPos )
			{
				curPos = transform.position.x;
			}

			if (meterCharge < 0 )
			{
				meterCharge = 0;
			}

			if ( meterCharge < 1 )
			{
				canDropCheckpoint = false;
			}

			myCheckpointMeter.fillAmount = meterCharge;
		}
		// If we are further than our furthest progress...
		else if ( transform.position.x > curPos )
		{
			// We only increase the meter if the player is not in slo-mo
			meterCharge += (transform.position.x - curPos) / 50;

			curPos = transform.position.x;

			if ( meterCharge > 1 )
			{
				meterCharge = 1;
				canDropCheckpoint = true;
			}

			myCheckpointMeter.fillAmount = meterCharge;
		}
	}

	// If the player's checkpoint meter is full, check to see if he drops a checkpoint
	private void CheckCheckpointDropped ()
	{
		if ( Input.GetKeyDown(KeyCode.E) )
		{
			canDropCheckpoint = false;

			checkpointPos = transform.position;

			if ( lastCheckpoint != null )
				Destroy(lastCheckpoint);

			lastCheckpoint = (GameObject)Instantiate(checkpointPrefab, transform.position, transform.rotation);

			// Reset meter so that we can start charging again
			ResetMeter();
		}
	}

	#endregion

	#region Public Methods

	public void ResetMeter ()
	{
		curPos = transform.position.x;
		meterCharge = 0;
		myCheckpointMeter.fillAmount = 0;
	}

	#endregion

	#region Collisions

	void OnTriggerEnter2D( Collider2D col )
	{
		string colTag = col.tag;

		if ( colTag == "KillPlayer" )
		{
			transform.position = checkpointPos;

			cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, cam.transform.position.z);

			ResetMeter();
		}

		if (colTag == "Finish" )
		{
			Application.LoadLevel("LevelComplete");
		}
	}

	void OnTriggerStay2D(Collider2D col )
	{
		// Pick up checkpoint...
		if (col.tag == "Checkpoint" && Input.GetKeyDown(KeyCode.E))
		{
			Destroy(col.gameObject);
			meterCharge = 0.66f;
			myCheckpointMeter.fillAmount = 0.66f;
			curPos = transform.position.x;
			checkpointPos = initPos;
		}
	}

	#endregion

	#region Properties

	public float CheckpointMeterCharge
	{
		get
		{
			return meterCharge;
		}
	}

	#endregion

}
