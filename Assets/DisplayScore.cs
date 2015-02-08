using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayScore : MonoBehaviour {
    Text scoreDisplayed;
    private int score = 0;
    private GameController gameCtr;

    void Awake() {
        GameObject gameCtrObj = GameObject.Find("GameController");
        gameCtr = gameCtrObj.GetComponent<GameController>();
        score = gameCtr.score;
    }
    // Use this for initialization
    void Start () {
        scoreDisplayed = gameObject.GetComponent<Text>();
        scoreDisplayed.text = "Score: " + score;
    }
    
    // Update is called once per frame
    void Update () {
        scoreDisplayed.text = "Score: " + score;
        score = gameCtr.score;
    }
}
