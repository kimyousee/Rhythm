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
    public Queue<GameObject> activeNotes; 
    public int score;
    public int bps;
    private int currentNote;
    private float idleCounter;
    private float delay;
    private string keyHeld;
    private GameController gameCtr;
    private SongObject songObj;

    // private int[] activeChildren ;
    void Awake(){
        key = "";
        timer = 0.0f;
        holdTimer = 0.0f;
        currentNote = 0;
        idleCounter = 0;
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
        delay = 1f/bps * 10f * 2f + 0.1f;
        activeNotes = new Queue<GameObject>();
        // activeChildren = new int[notes.Count];
    }
    
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime; //Increment time
        // Debug.Log(noteType[currentNote]);
        // Debug.Log(currentNote);
        // When a key is pushed, destroy the target. Calculate score
        if (currentNote != -1 && currentNote < noteType.Count){
            if (noteType[currentNote] == "push1"){
                if (!string.IsNullOrEmpty(key) && key.Length != 0) {
                    score = gameCtr.calculateScore(notes[currentNote],timer,key);
                    // reset
                    timer = 0;
                    key = "";
                    popActiveChild();
                    // Debug.Log("Set push button to not active");
                }
            }
            else if (noteType[currentNote] == "hold1"){
                if (!string.IsNullOrEmpty(key) && (key.Length != 0 || keyHeld.Length != 0)) { //if they are pushing/holding a key
                    if (Input.GetButton(key)){ //count how long they hold key
                        keyHeld = key;
                        holdTimer += Time.deltaTime;
                    }

                }
                else if (string.IsNullOrEmpty(key) && holdTimer == 0) {// when they don't press anything, time 
                    idleCounter += Time.deltaTime;
                    if (idleCounter >= delay) {
                        popActiveChild();
                    }
                }

                if (key.Length != 0 && keyHeld.Length != 0 && Input.GetButtonUp(keyHeld) && holdTimer != 0 && currentNote >= 0){ //held key is released
                    // Debug.Log("HERE_HOLD");
                    // if (!(this.transform.GetChild(currentNote).gameObject.active)){ // if note is not active
                    //     holdTimer = 0;
                    // }
                    gameCtr.calculateScore(notes[currentNote],timer,key,"",holdTimer);
                    timer = 0;
                    holdTimer = 0; 
                    key = "";
                    keyHeld = "";
                    // Debug.Log("In hold note exit");
                    popActiveChild();

                }
                // Debug.Log("Holding for: " + holdTimer);
                // Debug.Log("idleCounter: " + idleCounter + " Versus delay: " + delay);

            }
            //make push2 case
        }

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

    public void popActiveChild(){
        if ( currentNote != -1 && activeNotes.Peek().active){
            activeNotes.Peek().SetActive(false); ////
            activeNotes.Dequeue(); // pop first note on queue
            if (currentNote != notes.Count-1){
                currentNote++; // move to next note, except when currentNote is the last note
            } else currentNote = -1;
        }
    }
}
