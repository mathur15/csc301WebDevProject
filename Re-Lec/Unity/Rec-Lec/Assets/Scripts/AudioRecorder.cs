using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class AudioRecorder : MonoBehaviour {

	AudioClip AC;
	int num = 0;

	float time;
	bool timing;
	bool recording;

	//public Button recordButton;

	public int actualSample;

    public string currentSoundPath = string.Empty;

	// Use this for initialization
	void Start () {
		Debug.Log (ApplicationModel.day + " " + ApplicationModel.month);
		Debug.Log (ApplicationModel.course);
	}

	public void Record() {
		if (!recording) {
			recording = !recording;
			//recordButton.GetComponentInChildren<Text> ().text = "Stop Recording";
			Debug.Log ("starting");
			AC = Microphone.Start (null, false, 60, 44100);
			StartCoroutine (Timer ());

		} else {
			recording = !recording;
			//recordButton.GetComponentInChildren<Text> ().text = "Record";
			timing = false;
			Debug.Log (time);

			actualSample = Mathf.RoundToInt (time * 44100);
			Debug.Log (actualSample);

			Microphone.End (null);
			Debug.Log ("saving");

			System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo (Application.dataPath + "/Resources/Test_Output/Audio");
			foreach (System.IO.FileInfo fi in dir.GetFiles()) {
				if (fi.Extension.Equals (".wav")) {
					num++;
				}
			}
            currentSoundPath = "Resources/Test_Output/Audio/test_" + num;

            SavWav.Save ("Resources/Test_Output/Audio/test_" + num, AC, actualSample);


		}
	}


	// Update is called once per frame
	void Update () {

//		if (count == 0) {
//			if (Input.GetKeyDown (KeyCode.Space)) {
//				Debug.Log ("starting");
//				AC = Microphone.Start (null, false, 60, 44100);
//				StartCoroutine (Timer ());
//				count++;
//			}
//		} else if (count == 1) {
//			if (Input.GetKeyDown (KeyCode.Space)) {
//				count++;
//
//				//stopping
//				timing = false;
//				Debug.Log (time);
//
//				actualSample = Mathf.RoundToInt (time * 44100);
//				Debug.Log (actualSample);
//
//				Microphone.End (null);
//				Debug.Log ("saving");
//
//				System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo (Application.dataPath + "/Resources/Test_Output/Audio");
//				foreach (System.IO.FileInfo fi in dir.GetFiles()) {
//					if (fi.Extension.Equals (".wav")) {
//						num++;
//					}
//				}
//				SavWav.Save ("Resources/Test_Output/Audio/test_" + num, AC, actualSample);
//			}
//		}
	}

	IEnumerator Timer () {
		timing = true;
		while (timing) {
			time += Time.deltaTime;
			yield return new WaitForFixedUpdate ();
		}
	}

	public static class SavWav {

		static int actualSample;
		const int HEADER_SIZE = 44;

		public static bool Save(string filename, AudioClip clip, int actual) {

			actualSample = actual;

			if (!filename.ToLower().EndsWith(".wav")) {
				filename += ".wav";
			}

			var filepath = Path.Combine(Application.dataPath, filename);

			Debug.Log(filepath);

			// Make sure directory exists if user is saving to sub dir.
			Directory.CreateDirectory(Path.GetDirectoryName(filepath));

			using (var fileStream = CreateEmpty(filepath)) {

				ConvertAndWrite(fileStream, clip);

				WriteHeader(fileStream, clip);
			}

			return true; // TODO: return false if there's a failure saving the file
		}

		public static AudioClip TrimSilence(AudioClip clip, float min) {
			var samples = new float[actualSample];

			clip.GetData(samples, 0);

			return TrimSilence(new List<float>(samples), min, clip.channels, clip.frequency);
		}

		public static AudioClip TrimSilence(List<float> samples, float min, int channels, int hz) {
			return TrimSilence(samples, min, channels, hz, false, false);
		}

		public static AudioClip TrimSilence(List<float> samples, float min, int channels, int hz, bool _3D, bool stream) {
			int i;

			for (i=0; i<samples.Count; i++) {
				if (Mathf.Abs(samples[i]) > min) {
					break;
				}
			}

			samples.RemoveRange(0, i);

			for (i=samples.Count - 1; i>0; i--) {
				if (Mathf.Abs(samples[i]) > min) {
					break;
				}
			}

			samples.RemoveRange(i, samples.Count - i);

			var clip = AudioClip.Create("TempClip", samples.Count, channels, hz, _3D, stream);

			clip.SetData(samples.ToArray(), 0);

			return clip;
		}

		static FileStream CreateEmpty(string filepath) {
			var fileStream = new FileStream(filepath, FileMode.Create);
			byte emptyByte = new byte();

			for(int i = 0; i < HEADER_SIZE; i++) //preparing the header
			{
				fileStream.WriteByte(emptyByte);
			}

			return fileStream;
		}

		static void ConvertAndWrite(FileStream fileStream, AudioClip clip) {

			var samples = new float[actualSample];

			clip.GetData(samples, 0);

			Debug.Log (samples.Length);

			Int16[] intData = new Int16[samples.Length];
			//converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]

			Byte[] bytesData = new Byte[samples.Length * 2];
			//bytesData array is twice the size of
			//dataSource array because a float converted in Int16 is 2 bytes.

			int rescaleFactor = 32767; //to convert float to Int16

			for (int i = 0; i<samples.Length; i++) {
				intData[i] = (short) (samples[i] * rescaleFactor);
				Byte[] byteArr = new Byte[2];
				byteArr = BitConverter.GetBytes(intData[i]);
				byteArr.CopyTo(bytesData, i * 2);
			}

			fileStream.Write(bytesData, 0, bytesData.Length);
		}

		static void WriteHeader(FileStream fileStream, AudioClip clip) {

			var hz = clip.frequency;
			var channels = clip.channels;
			var samples = actualSample;

			fileStream.Seek(0, SeekOrigin.Begin);

			Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
			fileStream.Write(riff, 0, 4);

			Byte[] chunkSize = BitConverter.GetBytes(fileStream.Length - 8);
			fileStream.Write(chunkSize, 0, 4);

			Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
			fileStream.Write(wave, 0, 4);

			Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
			fileStream.Write(fmt, 0, 4);

			Byte[] subChunk1 = BitConverter.GetBytes(16);
			fileStream.Write(subChunk1, 0, 4);

			UInt16 two = 2;
			UInt16 one = 1;

			Byte[] audioFormat = BitConverter.GetBytes(one);
			fileStream.Write(audioFormat, 0, 2);

			Byte[] numChannels = BitConverter.GetBytes(channels);
			fileStream.Write(numChannels, 0, 2);

			Byte[] sampleRate = BitConverter.GetBytes(hz);
			fileStream.Write(sampleRate, 0, 4);

			Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2
			fileStream.Write(byteRate, 0, 4);

			UInt16 blockAlign = (ushort) (channels * 2);
			fileStream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

			UInt16 bps = 16;
			Byte[] bitsPerSample = BitConverter.GetBytes(bps);
			fileStream.Write(bitsPerSample, 0, 2);

			Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
			fileStream.Write(datastring, 0, 4);

			Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
			fileStream.Write(subChunk2, 0, 4);

			//		fileStream.Close();
		}
	}
}
