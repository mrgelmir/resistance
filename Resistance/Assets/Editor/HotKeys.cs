using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public static class HotKeys
{
	[MenuItem("Hotkeys/Run from first scene %#r")]
	public static void RunFromFirstScene()
	{
		if(EditorApplication.isPlaying)
		{
			// TODO: stop playing
		}
		else
		{
			// TODO: Save current Scene
			EditorSceneManager.OpenScene(EditorBuildSettings.scenes[0].path);
			EditorApplication.isPlaying = true;
		}
	}
}
