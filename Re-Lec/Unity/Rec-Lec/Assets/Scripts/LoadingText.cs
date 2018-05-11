using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour {

    public bool startPlaying = true;
    [HideInInspector]public bool playing = true;

    private Text text;
    public string mainText = "Launching Camera";
    public string dot = ".";
    public int numDots = 4;
    private int curDot = 0;

    public float dotDelay = 0.2f;

    void Start() {

        text = GetComponent<Text>();

        playing = startPlaying;
        StartCoroutine(Animate());
    }

    public void SetStaticText(string staticText) {
        playing = false;
        mainText = staticText;
    }

    public void SetLoadingText(string loadingText) {
        playing = true;
        mainText = loadingText;
    }

    IEnumerator Animate() {
        while (true) {
            text.text = mainText;
            if (playing) {
                for (int i = 1; i <= curDot; i++) {
                    text.text += dot;
                }
                curDot = (curDot + 1) % numDots;
            }
            yield return new WaitForSeconds(dotDelay);
        }
    }
}
