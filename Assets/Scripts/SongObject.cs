using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Manages the song's targets
public class SongObject : MonoBehaviour {
    private GameController gameCtr; //used to get song,diff,bps
    private PushOneButton pushOne;
    private HoldOneButton holdOne;
    private PushTwoButtons pushTwo;
    private string difficulty;
    private string song;
    private int bps;         //needed to know how fast to fade in/out
    public List<Note> notes; //list to keep all notes for the song

    //called before start function
    void Awake(){
        // get GameObjects with components we need
        GameObject gameCtrObj = GameObject.Find("GameController");
        // GameObject pushOneObj = GameObject.Find("PushOneButton");
        // GameObject holdOneObj = GameObject.Find("HoldOneButton");
        // GameObject pushTwoObj = GameObject.Find("PushTwoButtons");

        gameCtr = gameCtrObj.GetComponent<GameController>();
        //targetCtr = targetCtrObj.GetComponent<Target>();

        // pushOne = pushOneObj.GetComponent<PushOneButton>();
        // holdOne = holdOneObj.GetComponent<HoldOneButton>();
        // pushTwo = pushTwoObj.GetComponent<PushTwoButtons>();

        difficulty = gameCtr.difficulty;
        song = gameCtr.song;
        bps = gameCtr.bps;
    }

    void Start(){
        //switch (song){
        //case "<songName>": 
        notes = songNotes(""); //replace "" with song later
        //play audio
        StartCoroutine(spawnNotes(notes));
    }
    
    // Update is called once per frame
    void Update() {
        //while audio is playing
        //spawn notes
        // StartCoroutine(spawnNotes(notes));
    }

    // list of notes for each song... **have to change depending on difficulty**
    List<Note> songNotes(string song){
        List<Note> Notes = new List<Note>(); // [time, note]
        if (song == "") {
            Notes.Add(new Note(1.50f,"push1",new Vector3(20,20,0),8));
            Notes.Add(new Note(3.00f,"push1",new Vector3(10,10,0),5));
        }

        return Notes;
    }

    void setUpNote(Note note){
        GameObject obj = NotesPool.current.getPooledObject(note.noteType);
        if (obj == null) return;

        obj.transform.position = note.position;
        obj.transform.rotation = transform.rotation;
        float[] col = noteColour(note.button);
        obj.renderer.material.color = new Color(col[0],col[1],col[2]);

        obj.SetActive(true);
        StartCoroutine(Fade("in",obj));

    }

    IEnumerator spawnNotes(List<Note> notes) {
        // while there are notes
        for (int noteNum = 0; noteNum < notes.Count ; noteNum += 1) {
            switch (notes[noteNum].noteType){
                case "push1":
                    setUpNote(notes[noteNum]);
                    break;
                case "hold1":
                    setUpNote(notes[noteNum]);
                    break;
                case "push2":
                    setUpNote(notes[noteNum]);
                    break;
                default:
                    break;
            }
            // spawn next note at the time when fade starts
            if (noteNum +1 < notes.Count)
                yield return new WaitForSeconds(notes[noteNum+1].time
                                                -notes[noteNum].time
                                                -(1f/bps)*10 );
            else
                yield return new WaitForSeconds(notes[noteNum].time
                                                +(1f/bps)*10);
        }
    }

    IEnumerator Fade(string into, GameObject obj) {
        float rate = 1f/bps;
        if (into == "in") {
            for (float f = 0f; f <= 1.05f; f += 0.100f) {
                Color c = obj.renderer.material.color;
                c.a = f;
                obj.renderer.material.color = c;
                yield return new WaitForSeconds (rate);
                // Debug.Log(obj.renderer.material.color.a);
                if (obj.renderer.material.color.a >= 0.99) { //if circle is solid
                    //StartCoroutine (Fade ("out",obj));
                    into = "out";
                    break;
                }
            }
        }
        if (into == "out") {
            for (float f = 1f; f >= 0.05f; f -= 0.100f) {
                Color c = obj.renderer.material.color;
                c.a = f;
                obj.renderer.material.color = c;
                yield return new WaitForSeconds (rate);
            }
        }
    }

    // Array[3] to make RGB colour
    float[] noteColour(int button) {
        //float[] colour = new float[3];
        int val = noteNumToVal(button);
        switch (val){
            case 1: return new float[] {30f/255f,89f/255f,1f};
            case 2: return new float[] {30f/255f,1f,218f/255f};
            case 3: return new float[] {3f/255f,246f/255f,84f/255f};
            case 4: return new float[] {79f/255f,1f,34f/255f};
            case 5: return new float[] {182f/255f,1f,34f/255f};
            case 6: return new float[] {1f,226f/255f,34f/255f};
            case 7: return new float[] {247f/255f,173f/255f,0f};
            case 8: return new float[] {247f/255f,132f/255f,0f};
            case 9: return new float[] {247f/255f,82f/255f,0f};
            case 10:return new float[] {1f,23f/255f,23f/255f};
            default: return new float[] {}; break;
        }
    }
    // Used to determine colour of note
    int noteNumToVal(int note, bool hand=false){
        switch (difficulty) {
            case "free":
                switch (note) {
                    case 1: return 1;
                    case 2: return 2;
                    case 3: return 3;
                    case 4: return 4;
                    case 5: return 5;
                    case 6: return 6;
                    case 7: return 7;
                    case 8: return 8;
                    case 9: return 9;
                    case 10: return 10;
                    default: return 0;
                }
            case "hard":
                switch (note) {
                    case 1: return 1;
                    case 2: return 1;
                    case 3: return 1;
                    case 4: return 2;
                    case 5: return 2;
                    case 6: return 2;
                    case 7: return 3;
                    case 8: return 3;
                    case 9: return 3;
                    case 10: return 4;
                    default: return 0;
                }
            case "medium":
                switch (hand) {
                    case true: return 1;
                    case false: return 10;
                    default: return 0;
                }
            case "easy":
                return 1;
            default: return 1;
        }
    }
}

public class Note : IEquatable<Note>{ //make list of notes
    public float time;     // time when note appears (from start of scene)
    public string noteType;// what kind of note it is i.e. "hold1", "push1", etc
    public int button;     // 1-10; in the order: (RH)q2w3e4r5tv or p0o9i8u7yn(LH)
    public int button2=0;
    public float hold=0;   //starts at 0 and goes up to this number
    public Vector3 position; //add position
    public bool hand=false; // by default, the hand you hit the with doesn't matter
                            // if it does matter, fallse=right,true=left
    //colour based on button hit

    ///public T Item { get; set; }
    //1 button pressed
    public Note(float time, string noteType, Vector3 position, int button){ 
        this.time     = time;       
        this.noteType = noteType;   
        this.position = position;
        this.button   = button;
    }
    //1 button hold note
    public Note(float time, string noteType, Vector3 position, int button, float hold){
        this.time     = time;
        this.noteType = noteType;
        this.position = position;
        this.button   = button;
        this.hold     = hold;
    }
    //2 buttons pressed
    public Note(float time, string noteType, Vector3 position, int button, int button2){ 
        this.time     = time;
        this.noteType = noteType;
        this.position = position;
        this.button   = button;
        this.button2  = button2;
    }
    //2 buttons held down
    public Note(float time, string noteType, Vector3 position, int button, int button2, float hold){
        this.time     = time;
        this.noteType = noteType;
        this.position = position;
        this.button   = button;
        this.button2  = button2;
        this.hold     = hold;
    }

    public bool Equals(Note other) {
        //if (other == null) 
        return false;

        /*if (this.time == other.time) {
            return true;
        } else
            return false;*/
    }
    public override bool Equals(object obj){
        if (obj == null)
            return false;
        Note noteObj = obj as Note;
        if (noteObj == null)
            return false;
        else
            return Equals (noteObj);
    }
}
