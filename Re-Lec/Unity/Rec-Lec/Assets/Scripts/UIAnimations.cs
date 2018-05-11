using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UIAnimations : MonoBehaviour {

	public Image img;
	public Image img2;

	public GameObject logo;
	public GameObject loginObject;
	public GameObject classObject;
    public GameObject lectureNumberObject;

	public Text id;
	public Text password;

	// Use this for initialization
	void Start () {
		StartCoroutine (FadeOut ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator FadeOut () {
		while (img.color.a > 0) {
			Color temp = img.color;
			temp.a -= 0.1f;
			img.color = temp;
			yield return new WaitForSeconds (0.1f);
		}

		yield return new WaitForSeconds (0.25f);

		while (img2.color.a > 0) {
			Color temp = img2.color;
			temp.a -= 0.1f;
			img2.color = temp;
			yield return new WaitForSeconds (0.1f);
		}

		Destroy (img.gameObject);
		Destroy (img2.gameObject);

		logo.GetComponent<Animator> ().SetTrigger ("Move");

		yield return new WaitForSeconds (2.5f);
		loginObject.SetActive (true);
	}

	public void login () {

        // send up id.text and password.text and get okay

        // do below and save username somewhere
		//we can save username in the ApplicationModel Script
		if (id.text == "test" && password.text == "test") {
			loginObject.SetActive (false);

			classObject.SetActive (true);

            lectureNumberObject.SetActive(false);

        }

		StartCoroutine (loginCo ());
	}

	IEnumerator loginCo() {
		
		string url = "http://docker.fruumo.com/login";


		//List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
		//formData.Add(new MultipartFormFileSection(id.text, "username"));
		//formData.Add(new MultipartFormFileSection(password.text, "password"));
		//
		//UnityWebRequest www = UnityWebRequest.Post(url, formData);
		//
		//yield return www.SendWebRequest ();
		//
		//Debug.Log (www.downloadHandler.text);

		///*
		WWWForm postForm = new WWWForm ();
		postForm.AddField ("username", id.text);
		postForm.AddField ("password", password.text);

		WWW upload = new WWW (url, postForm);

		if (upload.error != null) {
			Debug.Log("Error: " + upload.error);
			yield break;
		}

		yield return upload;

		if (upload.error == null) {
			Debug.Log ("upload done : " + upload.text);
			Dictionary<string, string> dico = upload.responseHeaders;
			Debug.Log ("upload done : " + upload.responseHeaders);
			foreach (string key in dico.Keys) {
				Debug.Log (key + " : " + dico [key]);

			}
			loginObject.SetActive (false);

			classObject.SetActive (true);

			lectureNumberObject.SetActive(false);

			ApplicationModel.dicodac = upload.responseHeaders;
            ApplicationModel.username = id.text;

		} else {
			Debug.Log ("Error during upload: " + upload.error);
			//wrong login
			id.transform.parent.GetComponent<Animator> ().SetTrigger ("Invalid");
			password.transform.parent.GetComponent<Animator> ().SetTrigger ("Invalid");
		}
		//*/
	}
}
