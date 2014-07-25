using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    public float timer;     // used to time when they hit target (for points)
    public float holdTimer; // used to get points for holding
    public string key;      // check if and what key was pressed
    public Note note;       // get info of actual note to hit
    public string noteType; // to know if target has to be pushed or held

    private GameController gameCtr;
    //private SongObject songOpt;

    void Awake(){
        GameObject gameCtrObj = GameObject.Find ("GameController");
        //GameObject songOptObj = GameObject.Find ("SongObject");
        gameCtr = gameCtrObj.GetComponent<GameController>();
        //songOpt = songOptObj.GetComponent<SongObject>();
        timer = 0.0f;
        holdTimer = 0.0f;
    }

    // Use this for initialization
    protected virtual void Start () {

    }
    
    // Update is called once per frame
    protected virtual void Update () {
        timer += Time.deltaTime; //Increment time

        // When a key is pushed, destroy the target. Calculate score
        if (noteType == "push1"){
            if ("" != key) {
                gameCtr.calculateScore(note,timer,key);
                timer = 0;
                gameObject.SetActive(false);
            }
        }
        if (noteType == "hold1"){
            if ("" != key) {
                if (Input.GetButton(key)){ //count how long they hold key
                    holdTimer += Time.deltaTime;
                }
                if (Input.GetButtonUp(key)){ //held key is released
                    gameCtr.calculateScore(note,timer,key,"",holdTimer);
                    timer = 0;
                    holdTimer = 0;
                    gameObject.SetActive(false);
                }
            }

        }
        //make push2 case

        /*
        // Detect when circle is solid, then fade out
        Color c = renderer.material.color;
        //Debug.Log (c.a);
        if (c.a >= 0.99 && timer > 0.01) { //if circle is solid
            StartCoroutine (Fade ("out"));
        }
        // Detect when circle is completely gone
        if (c.a <= 0.01 && timer > 0.01) {
            timer = 0;
            // move target to next spot (?)

        }*/
    }
}
