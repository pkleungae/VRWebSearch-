using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;


public class onClick : MonoBehaviour {
	public string Queryname;

	public void ProceedQuery(string Query){
		Queryname = Query;
		StartCoroutine (ProceedingRequest ());
	}
	IEnumerator ProceedingRequest(){

		WWWForm form = new WWWForm ();
		Dictionary<string, string> postHeader = new Dictionary<string, string> ();
		postHeader.Add ("Content-Type", "application/json");
		string json = "{\"query\":\"" + Queryname + "\"}"; 
		//form.AddField("query", Queryname);
		Debug.Log (json);
		byte[] pData = System.Text.Encoding.ASCII.GetBytes(json.ToCharArray());
		WWW api = new WWW ("localhost:8000/search", pData, postHeader);
		yield return api;

		string txt = "";
		if (string.IsNullOrEmpty (api.error))
			txt = api.text;
		else
			txt = api.error;
		Debug.Log (txt);
		/*UnityWebRequest www = UnityWebRequest.Post("localhost:8000/search", json);
		yield return www.Send();

		if(www.isNetworkError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log("Upload complete!");
		}*/
	}		
}
