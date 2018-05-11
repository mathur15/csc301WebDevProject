using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetClass : MonoBehaviour {

	public CourseFinder cf;
    public CourseData course;

	// Use this for initialization
	void Start () {
		cf = GameObject.Find ("Cobalt").GetComponent<CourseFinder> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Execute() {
        ApplicationModel.courseCode = course.code;
        SceneManager.LoadScene("camera");
		ApplicationModel.day = System.DateTime.Now.Date.Day.ToString();
		ApplicationModel.month = System.DateTime.Now.Date.Month.ToString();
		ApplicationModel.course = GetComponentInChildren<Text> ().text;
	}
}
