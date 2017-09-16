using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;


[System.Serializable]
public class urls{
	public string[] url;
}


[System.Serializable]
public class displayImage : MonoBehaviour {

	public int index;

	void Start(){
		string[] urlL = new string[10];
		string query = PlayerPrefs.GetString ("query_name");
		getImageUrl (query, urlL);
		//Debug.Log (query);

	}

	public void getImageUrl(string query, string[] urlL){
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://fypwebsearch.firebaseio.com/");

		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
		/*string firebaseUrl = "https://fypwebsearch.firebaseio.com/query/" + query + "/url.json";
		WWW w = new WWW (firebaseUrl);
		StartCoroutine (WaitForRequest (w));*/

		urls urlList = new urls ();

		FirebaseDatabase.DefaultInstance.GetReference ("/query/" + query).GetValueAsync ().ContinueWith (task => {
			if (task.IsFaulted) {
				Debug.Log ("No found");
			} else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				string urlArray = snapshot.GetRawJsonValue();
				//Debug.Log(urlArray);
				urlList = JsonUtility.FromJson<urls>(urlArray);
				urlL = urlList.url;


				StartCoroutine (getImage (urlL));

			}
		});
		//DataSnapshot.
	}


	IEnumerator getImage(string[] url){
		Debug.Log (url [index]);
		//WWW www = new WWW("http://kids.nationalgeographic.com/content/dam/kids/photos/animals/Mammals/H-P/pig-full-body.adapt.945.1.jpg");
		WWW www = new WWW(url[index]);
		yield return www;
		 
		if (string.IsNullOrEmpty (www.text)) {
			Debug.Log ("Download failed");
		} else {
			Debug.Log ("Download Success");
		}

		Texture2D texture = new Texture2D (1, 1);
		www.LoadImageIntoTexture (texture);
		Sprite sprite = Sprite.Create (www.texture, 
			new Rect (0, 0, texture.width, texture.height), 
			Vector2.one/2);


		GetComponent<SpriteRenderer> ().sprite = sprite;
	}
}
