using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CourseData {
    public string code;
    public string fullName;

    public CourseData(string _code, string _fullName) {
        code = _code;
        fullName = _fullName;
    }
}

public class CourseFinder : MonoBehaviour {

    public Text buildingText;
    public RectTransform contentPanel;
    public GameObject courseButton;

	public Text checkBuilding;
	public GameObject invalidId;

	public float lat = 43.65966794914353f;
	public float lng = -79.397374250679f;
    // Use this for initialization

    public float uncertainty = 0.000000001f;

    public string data;
    
    private List<CourseData> courses;

	void Start () {

        courses = new List<CourseData>();

        if (Application.isMobilePlatform) {
            StartCoroutine(GetLocation());
            StartCoroutine(GetCourse());
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {

			Debug.Log (lat);
			StartCoroutine (GetLocation ());
			StartCoroutine (GetCourse ());
		}
	}

    void LoadCourses() {
		foreach (Transform child in contentPanel.transform) {
			Destroy (child.gameObject);
		}
        foreach (CourseData course in courses) {
            Transform newButton = (Instantiate(courseButton) as GameObject).transform;
            newButton.SetParent(contentPanel, false);
            newButton.GetComponentInChildren<Text>().text = course.code;
            GetClass classButton = newButton.GetComponent<GetClass>();
            if (classButton != null) {
                classButton.course = course;
            }
        }
    }

	public void Check() {
		StartCoroutine (CheckCourse ());
	}

	IEnumerator CheckCourse() {
		bool error = false;
		invalidId.SetActive (false);
		//using (UnityWebRequest www = UnityWebRequest.Get("https://cobalt.qas.im/api/1.0/buildings/search?q=SF"))
		using (UnityWebRequest www = UnityWebRequest.Get("https://cobalt.qas.im/api/1.0/buildings/search?q=\"" + checkBuilding.text + "\""))
		{
			www.SetRequestHeader ("Authorization", "0jefdSlRNqys1AJNosfagSeOBIEnUVME");
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error);
				error = true;
			}
			else
			{
				// Show results as text
				//Debug.Log(www.downloadHandler.text);
				data = www.downloadHandler.text;
				// Or retrieve results as binary data
				byte[] results = www.downloadHandler.data;
			}
		}

		//Debug.Log (data);
		if (error) {
			//shake the box
			checkBuilding.transform.parent.GetComponent<Animator> ().SetTrigger ("Invalid");
			invalidId.SetActive (true);

		} else {
			JSONNode json = JSON.Parse (data);
			Debug.Log (json);
			string building = json [0] [1];
			string buildingLong = json [0] [2];
			Debug.Log (building);

			buildingText.text = "You are in: " + buildingLong;

			if (buildingLong == null) {
				checkBuilding.transform.parent.GetComponent<Animator> ().SetTrigger ("Invalid");
				invalidId.SetActive (true);

			} else {

				int AM = 0;
				int PM = 12;

				string day = System.DateTime.Now.DayOfWeek.ToString ().ToUpper (); 
				int startTime = System.DateTime.Now.Hour * 60 * 60;
				int endTime = startTime + 3 * 60 * 60;

				day = "TUESDAY";
				startTime = (9 + AM) * 60 * 60;
				endTime = startTime + 3 * 60 * 60;



				Debug.Log (day);
				Debug.Log (startTime + " (in seconds) -> in hours: " + startTime / 60 / 60 + ":00");
				Debug.Log (endTime + " (in seconds)");

				string requestString = "https://cobalt.qas.im/api/1.0/courses/filter?q=location:\"" + building + "\" AND day:\"" + day + "\" AND start:>=" + startTime + " AND end:<=" + endTime;
				Debug.Log (requestString);

				using (UnityWebRequest www = UnityWebRequest.Get (requestString))
				//using (UnityWebRequest www = UnityWebRequest.Get("https://cobalt.qas.im/api/1.0/courses/filter?q=location:\"BA\" AND day:\"FRIDAY\" AND start:<=54000 AND end:>=54000"))
 {				//using (UnityWebRequest www = UnityWebRequest.Get("https://cobalt.qas.im/api/1.0/courses/filter?q=day:\"THURSDAY\" AND start:>=" + startTime + " AND end:<=" + endTime)) 
					www.SetRequestHeader ("Authorization", "0jefdSlRNqys1AJNosfagSeOBIEnUVME");
					yield return www.SendWebRequest ();

					if (www.isNetworkError || www.isHttpError) {
						Debug.Log (www.error);
					} else {
						// Show results as text
						Debug.Log (www.downloadHandler.text);
						data = www.downloadHandler.text;
						// Or retrieve results as binary data
						byte[] results = www.downloadHandler.data;
					}
				}

				json = JSON.Parse (data);

				Debug.Log (json.Count);
				Debug.Log ("Available Classes:");
				courses.Clear ();
				for (int i = 0; i < json.Count; i++) {
                    CourseData curCourse = new CourseData(json[i][1], json[i][2]);
					Debug.Log (curCourse.code + ", " + curCourse.fullName);
					courses.Add (curCourse);
				}

				LoadCourses ();
		
			}
		}


	}

    void GetCourse2() {
		UnityWebRequest www = UnityWebRequest.Get ("https://cobalt.qas.im/api/1.0/buildings/080?key=0jefdSlRNqys1AJNosfagSeOBIEnUVME");
		Debug.Log(www.downloadHandler.text);
	}

	string GetApproximateLatLng() {
		string result = "?q=lat:<";
		result += (lat - uncertainty) + " AND lat:>";
		result += (lat + uncertainty);

		Debug.Log (result);
		return result;
	}

	IEnumerator GetCourse()
	{
		using (UnityWebRequest www = UnityWebRequest.Get("https://cobalt.qas.im/api/1.0/buildings/filter?q=lat:>43.659667948 AND lat:<43.659667950"))
		{
			www.SetRequestHeader ("Authorization", "0jefdSlRNqys1AJNosfagSeOBIEnUVME");
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error);
			}
			else
			{
				// Show results as text
				//Debug.Log(www.downloadHandler.text);
				data = www.downloadHandler.text;
				// Or retrieve results as binary data
				byte[] results = www.downloadHandler.data;
			}
		}

		//Debug.Log (data);


		JSONNode json = JSON.Parse (data);
		Debug.Log (json);
		string building = json [0] [1];
		string buildingLong = json [0] [2];
		Debug.Log (building);
        buildingText.text = "You are in: " + buildingLong;


	string day = System.DateTime.Now.DayOfWeek.ToString().ToUpper(); 
	int startTime = System.DateTime.Now.Hour * 60 * 60;
	int endTime = startTime + 3 * 60 * 60;

        //Debug.Log (day);
        //Debug.Log (time);

        
        int AM = 0;
        int PM = 12;

        day = "TUESDAY";
		startTime = (9 + AM) * 60 * 60;
        endTime = startTime + 3 * 60 * 60;

        

        Debug.Log(day);
        Debug.Log(startTime + " (in seconds) -> in hours: " + startTime / 60 / 60 + ":00");
        Debug.Log(endTime + " (in seconds)");

        string requestString = "https://cobalt.qas.im/api/1.0/courses/filter?q=location:\"" + building + "\" AND day:\"" + day + "\" AND start:>=" + startTime + " AND end:<=" + endTime;
        Debug.Log(requestString);

        using (UnityWebRequest www = UnityWebRequest.Get(requestString))
        //using (UnityWebRequest www = UnityWebRequest.Get("https://cobalt.qas.im/api/1.0/courses/filter?q=location:\"BA\" AND day:\"FRIDAY\" AND start:<=54000 AND end:>=54000"))
        //using (UnityWebRequest www = UnityWebRequest.Get("https://cobalt.qas.im/api/1.0/courses/filter?q=day:\"THURSDAY\" AND start:>=" + startTime + " AND end:<=" + endTime)) 

        {
            www.SetRequestHeader("Authorization", "0jefdSlRNqys1AJNosfagSeOBIEnUVME");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            } else {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
                data = www.downloadHandler.text;
                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }

		json = JSON.Parse (data);

		Debug.Log (json.Count);
		Debug.Log ("Available Classes:");
		courses.Clear ();
        for (int i = 0; i < json.Count; i++) {
            CourseData curCourse = new CourseData(json[i][1], json[i][2]);
            Debug.Log(curCourse.code + ", " + curCourse.fullName);
            courses.Add(curCourse);
        }

        LoadCourses();
    }

	IEnumerator GetLocation()
	{
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
			//yield break;

		// Start service before querying location
		Input.location.Start();

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			print("Timed out");
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			print("Unable to determine device location");
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
		}

		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();

		//StartCoroutine (GetCourse ());
	}
}
