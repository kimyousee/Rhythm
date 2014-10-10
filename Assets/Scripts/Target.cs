using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
This class manages all notes currently active in the game
*/
public class Target : MonoBehaviour {

    public float timer;     // used to time when they hit target (for points)
    public float holdTimer; // used to get points for holding
    public string key = ""; // check if and what key was pressed
    public Note note;       // get info of actual note to hit
    public List<Note> notes;
    public List<string> noteType; // to know if target has to be pushed or held
    public int score;
    public int bps;
    private int currentNote;
    private float idleCounter;
    private GameController gameCtr;
    private SongObject songObj;

    void Awake(){
        key = "";
        timer = 0.0f;
        holdTimer = 0.0f;
        currentNote = 0;
        idleCounter = 1f/bps;
    }

    // Use this for initialization
    protected virtual void Start () {
        GameObject gameCtrObj = GameObject.Find ("GameController");
        gameCtr = gameCtrObj.GetComponent<GameController>();
        GameObject songCtrObj = GameObject.Find ("SongObject");
        songObj = songCtrObj.GetComponent<SongObject>();
        // note = songCtrObj.GetComponent<Note>();
        notes = songObj.notes;
        bps = gameCtr.bps;
    }
    
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime; //Increment time
        // find active children indeces
        int[] activeChildren = new int[notes.Count];
        for(int i = currentNote; i<this.transform.GetChildCount() && i<notes.Count;i++){
            // if it's set active in the array but it isn't active anymore, deactivate it
            if (activeChildren[i] == 1 && !this.transform.GetChild(i).gameObject.active){
                activeChildren[i] = 0;
            } // if it's still active
            else if (this.transform.GetChild(i).gameObject.active){
                activeChildren[i] = 1;
                if (i>0 && activeChildren[i-1] == 0) currentNote = i;
                if (i==0) currentNote = i;
                Debug.Log("HERE");
                Debug.Log(currentNote);
            }
        }
        Debug.Log(noteType[currentNote]);
        Debug.Log(currentNote);
        // When a key is pushed, destroy the target. Calculate score
        if (noteType[currentNote] == "push1"){
            if (!string.IsNullOrEmpty(key) && key.Length != 0) {
                score = gameCtr.calculateScore(notes[currentNote],timer,key);
                // reset
                timer = 0;
                key = "";
                //set child to false
                this.transform.GetChild(currentNote).gameObject.SetActive(false); ////
            }
        }
        if (noteType[currentNote] == "hold1"){
            holdTimer = 0;
            if (!string.IsNullOrEmpty(key) && key.Length != 0) { //if they are pushing/hosding a key
                if (Input.GetButton(key)){ //count how long they hold key
                    holdTimer += Time.deltaTime;
                }
                if (Input.GetButtonUp(key) ){ //held key is released
                    if (!(this.transform.GetChild(currentNote).gameObject.active)){
                        holdTimer = 0;
                    }
                    gameCtr.calculateScore(notes[currentNote],timer,key,"",holdTimer);
                    timer = 0;
                    holdTimer = 0; 
                    key = "";
                    this.transform.GetChild(currentNote).gameObject.SetActive(false);

                }
            }
            else if (string.IsNullOrEmpty(key) && holdTimer == 0) {// when they don't press anything, time 
                idleCounter -= Time.deltaTime;
                if (idleCounter <= 0) {
                    this.transform.GetChild(currentNote).gameObject.SetActive(false);
                }
            }
            Debug.Log(idleCounter);

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
