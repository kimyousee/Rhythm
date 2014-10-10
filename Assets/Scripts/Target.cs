using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Target : MonoBehaviour {

    public float timer;     // used to time when they hit target (for points)
    public float holdTimer; // used to get points for holding
    public List<string> key;      // check if and what key was pressed
    public Note note;       // get info of actual note to hit
    public List<Note> notes;
    public List<string> noteType; // to know if target has to be pushed or held
    public int score;
    private int currentNote;
    private GameController gameCtr;
    private SongObject songObj;

    void Awake(){
        
        //GameObject songOptObj = GameObject.Find ("SongObject");
        
        //songOpt = songOptObj.GetComponent<SongObject>();
        timer = 0.0f;
        holdTimer = 0.0f;
        currentNote = 0;
    }

    // Use this for initialization
    protected virtual void Start () {
        GameObject gameCtrObj = GameObject.Find ("GameController");
        gameCtr = gameCtrObj.GetComponent<GameController>();
        GameObject songCtrObj = GameObject.Find ("SongObject");
        songObj = songCtrObj.GetComponent<SongObject>();
        // note = songCtrObj.GetComponent<Note>();
        notes = songObj.notes;
    }
    
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime; //Increment time
        // Debug.Log(currentNote);
        // find active children indexs
        int[] activeChildren = new int[notes.Count];
        for(int i = currentNote; i<this.transform.GetChildCount() && i<notes.Count;i++){
            if (activeChildren[i] == 1 && !this.transform.GetChild(i).gameObject.active){
                activeChildren[i] = 0;
            }
            else if (this.transform.GetChild(i).gameObject.active){
                activeChildren[i] = 1;
                if (i>0 && activeChildren[i-1] == 0) currentNote = i;
            }
        }

        // When a key is pushed, destroy the target. Calculate score
        Debug.Log(key[currentNote]);
        if (noteType[currentNote] == "push1"){
            if ("" != key[currentNote]) {
                score=gameCtr.calculateScore(notes[currentNote],timer,key[currentNote]);
                timer = 0;

                this.transform.GetChild(currentNote).gameObject.SetActive(false); ////
            }
        }
        if (noteType[currentNote] == "hold1"){
            if ("" != key[currentNote]) { //if they are pushing/hosding a key
                if (Input.GetButton(key[currentNote])){ //count how long they hold key
                    holdTimer += Time.deltaTime;
                }
                if (Input.GetButtonUp(key[currentNote])){ //held key is released
                    gameCtr.calculateScore(notes[currentNote],timer,key[currentNote],"",holdTimer);
                    timer = 0;
                    holdTimer = 0;
                    //gameObject.SetActive(false);
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
