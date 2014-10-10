using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
Stores all the notes for each song
*/
public class SongNotes : MonoBehaviour {
    public string song;
    private SongObject songObCtr;
    // Use this for initialization
    void Start () {
        GameObject songCtrObj = GameObject.Find("SongObject");
        songObCtr = songCtrObj.GetComponent<SongObject>();
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    // list of notes for each song... **have to change depending on difficulty**
    public List<Note> songNotes(string song){
        List<Note> Notes = new List<Note>(); // [time, note]
        if (song == "") { //make this a switch statement later
            Notes.Add(new Note(1.50f,"push1",new Vector3(20,20,0),1));
            Notes.Add(new Note(3.00f,"hold1",new Vector3(10,10,0),5,2.00f));
        }
        return Notes;
    }


}
