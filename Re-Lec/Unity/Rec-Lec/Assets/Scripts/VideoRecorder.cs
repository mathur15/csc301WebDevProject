using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using NatCorderU.Core;
using UnityEngine.UI;
using System;

public class VideoRecorder : MonoBehaviour/*, IAudioSource*/ {

    public Text debugText;

    private WebCamTexture webcamTexture;

    private int screenWidth = Screen.width;
    private int screenHeight = Screen.height;

    private RawImage img;
    private Material mat;
    //private IAudioSource iAudioSource;
    private AudioSource audioSource;

    // Audio
    public int channelCount { get { return (int)AudioSettings.speakerMode; } }
    public int sampleRate { get { return AudioSettings.outputSampleRate; } }
    public bool paused, muted;
    private double timestamp, lastTime = -1; // Used to support pausing and resuming

    void Start () {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (var i = 0; i < devices.Length; i++) {
            Debug.Log(devices[i].name);
        }
        if (devices.Length > 0) {
            webcamTexture = new WebCamTexture(devices[0].name, screenWidth, screenHeight);

            img = GetComponent<RawImage>();
            img.texture = webcamTexture;
            mat = img.material;

            //iAudioSource = this;
            audioSource = GetComponent<AudioSource>();


            webcamTexture.Play();
        }
    }
	
	void Update () {
        /*
        if (NatCorder.IsRecording && webcamTexture.didUpdateThisFrame) {
            // Acquire an encoder frame
            var frame = NatCorder.AcquireFrame();
            // Blit with the preview material
            Graphics.Blit(webcamTexture, frame);
            // Commit the encoder frame for encoding
            NatCorder.CommitFrame(frame);
        }*/
    }

    public void StartRecording() {
        audioSource.clip = Microphone.Start(null, true, 1, 44100);
        audioSource.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSource.Play();
        //NatCorder.StartRecording(new Configuration(screenWidth, screenHeight), OnDoneRecording, iAudioSource);
        debugText.text = "Started recording";
    }

    public void StopRecording() {
        Microphone.End(null);
        //NatCorder.StopRecording();
        debugText.text = "Stopped recording";
    }

    void OnDoneRecording(string path) {
        debugText.text = "Saved recording to: " + path;
        Debug.Log("Saved recording to: " + path);

        #if UNITY_IOS || UNITY_ANDROID
        // Playback the video
        Handheld.PlayFullScreenMovie(path);
        #endif
    }
    /*
    void OnAudioFilterRead(float[] data, int channels) {
        if (!NatCorder.IsRecording) return;
        // Calculate time
        if (!paused) timestamp += lastTime > 0 ? AudioSettings.dspTime - lastTime : 0f;
        lastTime = AudioSettings.dspTime;
        // Send to NatCorder for encoding
        NatCorder.CommitSamples(data, (long)(timestamp * 1e+9f));
        if (muted) Array.Clear(data, 0, data.Length);
    }
    
    void IDisposable.Dispose() {
        NatCorderAudio.Destroy(this);
    }*/
}
