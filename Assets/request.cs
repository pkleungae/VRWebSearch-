using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class request : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Upload());
	}
	
	// Update is called once per frame
	IEnumerator Upload(){
		byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");
		UnityWebRequest www = UnityWebRequest.Put("http://www.my-server.com/upload", myData);
		yield return www.Send();

		if(www.isNetworkError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log("Upload complete!");
		}
	}
}
