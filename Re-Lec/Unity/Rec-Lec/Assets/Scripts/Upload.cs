using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Upload : MonoBehaviour {

    public bool debugLocalUpload = false;
    public string debugCookieString;
    public string debugUsername;

	public string localFilePath;
	public string url = "http://docker.fruumo.com/upload";

    public LoadingText debugText;
    public Slider progressSlider;

    // Use this for initialization
    void Start () {
        if (debugText == null) {
            debugText = FindObjectOfType<LoadingText>();
        }
        if (progressSlider == null) {
            progressSlider = FindObjectOfType<Slider>();
        }

        if (debugLocalUpload) {
            localFilePath = Application.dataPath + "/Resources/ScreenCaptureTrim.mp4";
            Debug.Log(localFilePath);
            Debug.Log("Debug Cookie String: " + debugCookieString);

            UploadFile("CSC301", "1", debugUsername, localFilePath, url);
        }
    }
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Q)) {
            localFilePath = ApplicationModel.path;
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
            //StartCoroutine(UploadFileCo(name, url));
            Debug.LogWarning("Alex: this feature has been disabled due to added parameters to UploadFile Coroutine!");
			//UploadFile("test-title", "CSC301", "UGH", localFilePath, url);
		}
	}

    public void UploadFile(string courseCode, string lectureNumber, string username, string localFileName, string uploadURL) {
        StartCoroutine(UploadFileCo(courseCode, lectureNumber, username, localFileName, uploadURL));
    }

	IEnumerator UploadFileCo(string courseCode, string lectureNumber, string username, string localFilePath, string uploadURL) {

        debugText.SetStaticText("Upload (" + localFilePath + ") starting: " + courseCode + " - " + lectureNumber + " by " + username);

        WWW localFile = new WWW ("file:///" + localFilePath);
		yield return localFile;
		if (localFile.error == null) {
			Debug.Log ("Loaded file successfully");
            debugText.SetStaticText(debugText.mainText + "\nLoaded file successfully.");

        } else {
			Debug.Log ("Open file error: " + localFile.error);
            debugText.SetStaticText(debugText.mainText + "\nError opening file.");
            yield break;
		}

        WWWForm postForm = new WWWForm ();
        //postForm.AddField("title", videoTitle);
        postForm.AddField("lecture", lectureNumber);
        postForm.AddField("course", courseCode);
        postForm.AddField("username", username);
        //postForm.AddField("ETag", ApplicationModel.dicodac["ETag"]);
        //postForm.AddField("user", "W/6d7-Vjo1bxUcQ9W1bGBizOhPKeaxtfA");
        //postForm.AddBinaryData ("video", localFile.bytes, Path.GetFileName(localFileName), "video/mp4");
        postForm.AddBinaryData("video", localFile.bytes);
        WWW upload = new WWW (uploadURL, postForm);

        ///*

        StartCoroutine(ShowProgress(upload));

        yield return upload;

		if (upload.error == null) {
			Debug.Log ("upload done : " + upload.text);
            debugText.SetStaticText("Upload Completed!");
        } else {
			Debug.Log ("Error during upload: " + upload.error);
            debugText.SetStaticText("Error during upload: " + upload.error);
        }
        //*/

        /*
        string data;
        using (UnityWebRequest request = UnityWebRequest.Post(url, postForm)) {
            if (debugLocalUpload) {
                request.SetRequestHeader("ETag", debugCookieString);
            } else {
                request.SetRequestHeader("ETag", ApplicationModel.dicodac["ETag"]);
            }
            request.timeout = 9999999;
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError) {
                Debug.Log(request.error
                    + " : NetworkError=" + request.isNetworkError.ToString()
                    + " : HttpError=" + request.isHttpError.ToString());

                Debug.Log("uploadedBytes=" + request.uploadedBytes + "\n"
                    + "responseCode=" + request.responseCode + "\n"
                    + "timeout=" + request.timeout + "\n"
                    + "uploadProgress=" + request.uploadProgress + "\n"
                    + "useHttpContinue=" + request.useHttpContinue + "\n"
                    + "url=" + request.url + "\n"
                    + "redirectLimit=" + request.redirectLimit + "\n"
                    + "method=" + request.method + "\n"
                    + "GetType()=" + request.GetType() + "\n"
                    );
            } else {
                // Show results as text
                //Debug.Log(www.downloadHandler.text);
                data = request.downloadHandler.text;
                // Or retrieve results as binary data
                byte[] results = request.downloadHandler.data;
            }
        }
        */


        /*
        byte[] localFile2 = File.ReadAllBytes(localFilePath);
        Debug.Log(localFile2.Length);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormFileSection("1", "lecture"));
        formData.Add(new MultipartFormFileSection(courseName, "course"));
        formData.Add(new MultipartFormFileSection("video", localFile2));
        //formData.

        UnityWebRequest www = UnityWebRequest.Post(uploadURL, formData);

        StartCoroutine(ShowProgress(www));

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            debugText.SetStaticText(www.error);
        } else {
            debugText.SetStaticText("Upload complete!");
        }
        */
    }

    private IEnumerator ShowProgress(WWW www) {

        progressSlider.gameObject.SetActive(true);

        while (!www.isDone) {
            progressSlider.value = www.progress;

            if (www.error != null) {
                debugText.SetStaticText("Error during upload: " + www.error);
            } else {
                debugText.SetStaticText("Upload progress: " + (www.uploadProgress * 100).ToString());
            }

            yield return new WaitForSeconds(.1f);
        }
        progressSlider.value = 1f;
    }

    private IEnumerator ShowProgress(UnityWebRequest www) {

        progressSlider.gameObject.SetActive(true);

        while (!www.isDone) {
            progressSlider.value = www.uploadProgress;

            if (www.error != null) {
                debugText.SetStaticText("Error during upload: " + www.error);
            } else {
                debugText.SetStaticText("Upload progress: " + (www.uploadProgress * 100).ToString());
            }

            yield return new WaitForSeconds(.1f);
        }
        progressSlider.value = 1f;
    }
}
