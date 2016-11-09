using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (PlatformScript))]
public class PlatformScriptEditor : Editor {

	public override void OnInspectorGUI ()
	{
		PlatformScript myPlatScript = (PlatformScript)target;

		int width, height;

		if ( GUILayout.Button("Build Platform") )
		{
			myPlatScript.BuildPlatform();
		}

		DrawDefaultInspector();
	}

}
