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
public class onClick : MonoBehaviour {


	//public string[] urlL;


	public void ProceedQuery(string Query){
		//GameObject cell = new GameObject ("image");
		string[] urlL = new string[10];
		StartCoroutine (ProceedingRequest (Query));
		//getImageUrl (Query, urlL);
		PlayerPrefs.SetString("query_name", Query);
		SceneManager.LoadScene("MainScene 2");
		//StartCoroutine (getImage());

	}
		
	public void getImageUrl(string query, string[] urlL){
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://fypwebsearch.firebaseio.com/");

		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
		/*string firebaseUrl = "https://fypwebsearch.firebaseio.com/query/" + query + "/url.json";
		WWW w = new WWW (firebaseUrl);
		StartCoroutine (WaitForRequest (w));*/

		FirebaseDatabase.DefaultInstance.GetReference ("/query/" + query).GetValueAsync ().ContinueWith (task => {
			if (task.IsFaulted) {
				Debug.Log ("No found");
			} else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				string urlArray = snapshot.GetRawJsonValue();
				urls urlList = new urls ();
				urlList = JsonUtility.FromJson<urls>(urlArray);
				urlL = urlList.url;
			}
		});
		//DataSnapshot.
	}
	IEnumerator ProceedingRequest(string Query){

		//WWWForm form = new WWWForm ();
		Dictionary<string, string> postHeader = new Dictionary<string, string> ();
		postHeader.Add ("Content-Type", "application/json");
		string json = "{\"query\":\"" + Query + "\"}"; 
		//form.AddField("query", Queryname);
		Debug.Log (json);
		byte[] pData = System.Text.Encoding.ASCII.GetBytes(json.ToCharArray());
		WWW api = new WWW ("localhost:8000/search", pData, postHeader);
		yield return api;

		string txt = "";
		if (string.IsNullOrEmpty (api.error)) {
			txt = api.text;
		} else {
			txt = api.error;
		}
		Debug.Log (txt);
	}

	/* getImage(){
		WWW www = new WWW("https://www.w3schools.com/css/trolltunga.jpg");
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
	}*/
}
