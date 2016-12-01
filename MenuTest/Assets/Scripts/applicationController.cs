using UnityEngine;
using System.Collections;

public class applicationController : MonoBehaviour {

	/// <summary>
	/// Quit this instance.
	/// </summary>
	public void Quit () {
		// Check if we're running in the Unity Editor
		#if UNITY_EDITOR
			// If we are, stop playing
			UnityEditor.EditorApplication.isPlaying = false;
		// If we aren't, exit the application.
		#else
			Application.Quit ();
		#endif
	}

	/// <summary>
	/// Takes the screenshot.
	/// </summary>
	public void TakeScreenshot () {
		// Construct our filename based on the current date and time
		string filename = (System.DateTime.Now + ".png").Replace ("/", "-").Replace (":", "-");
		// Capture our screenshot
		Application.CaptureScreenshot(Application.persistentDataPath + "/" + filename);
		// Log where our screenshot was saved
		Debug.Log ("Screenshot saved at: " + Application.persistentDataPath + "/" + filename);
	}

}
