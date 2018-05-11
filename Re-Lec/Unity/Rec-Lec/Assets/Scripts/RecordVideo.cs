using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RecordVideo : MonoBehaviour {

    public LoadingText loadingText;
    public GameObject progressSlider;

    private string courseCode;
    private string lectureNumber;
    private string username;

    private CameraCapture camCap;
    private Upload upload;

    public void StartRecording() { camCap.captureVideo(); }

    public void TakePicture() { camCap.takePhoto(); }

    public void PickPhoto() { camCap.pickPhoto(); }

    public void PickVideo() { camCap.pickVideo(); }

    void Start() {
        camCap = FindObjectOfType<CameraCapture>();
        upload = FindObjectOfType<Upload>();

        if (camCap == null) {
            camCap = GameObject.FindObjectOfType<CameraCapture>();
        }

        this.camCap.CaptureVideoCompleted += onCaptureVideoCompleted;
        this.camCap.TakePhotoCompleted += onTakePhotoCompleted;
        this.camCap.PickCompleted += onChooseFile;
        this.camCap.Failed += onCancelled;

        courseCode = ApplicationModel.courseCode;
        lectureNumber = ApplicationModel.lectureNumber;
        username = ApplicationModel.username;

        Debug.Log("CourseCode: " + courseCode);
        Debug.Log("CourseFullName: " + lectureNumber);
        Debug.Log("Username: " + username);

        loadingText.SetLoadingText("Launching Camera");
        StartRecording();
    }

    void onCaptureVideoCompleted(string fpath) {
        // Do file upload here
        loadingText.SetStaticText("Recorded video!\nStarting upload...");
        progressSlider.SetActive(true);

        upload.UploadFile(courseCode, "1", username, fpath, upload.url); 
    }

    void onTakePhotoCompleted(string fpath) {
        loadingText.SetStaticText("Took Photo");
    }

    void onChooseFile(string fpath) {
        loadingText.SetStaticText("Chose File");
    }

    void onCancelled(string errorInfo) {
        loadingText.SetStaticText("Operation Cancelled");
        SceneManager.LoadScene("UI");
    }
}
