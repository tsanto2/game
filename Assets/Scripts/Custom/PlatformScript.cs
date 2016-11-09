using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {

	[SerializeField]
	private int width, height;

	[SerializeField]
	private GameObject parent;

	[SerializeField]
	private GameObject basePlatPrefab;

	private GameObject myPlat;

	public void BuildPlatform ()
	{
		myPlat = (GameObject)Instantiate(basePlatPrefab, transform.position, transform.rotation);
		myPlat.transform.parent = parent.transform;
		myPlat.transform.localScale = new Vector3(width, height, 0);
	}
}
