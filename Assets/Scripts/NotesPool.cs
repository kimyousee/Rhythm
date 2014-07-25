using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotesPool : MonoBehaviour {

    public static NotesPool current;
    public GameObject pushOneNote;
    public GameObject holdOneNote;
    public GameObject pushTwoNote;
    public int pooledAmount = 10;
    public bool willGrow = true;

    List<GameObject> pushOneNotes;
    List<GameObject> holdOneNotes;
    List<GameObject> pushTwoNotes;

    void Awake(){
        current = this; //pointer to this whole object
    }

    //make the pool of notes
    void Start () {
        pushOneNotes = new List<GameObject>();
        holdOneNotes = new List<GameObject>();
        pushTwoNotes = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++){
            GameObject pushOneObj = (GameObject)Instantiate(pushOneNote);
            GameObject holdOneObj = (GameObject)Instantiate(holdOneNote);
            GameObject pushTwoObj = (GameObject)Instantiate(pushTwoNote);
            pushOneObj.SetActive(false);
            holdOneObj.SetActive(false);
            pushTwoObj.SetActive(false);
            pushOneNotes.Add(pushOneObj);
            holdOneNotes.Add(holdOneObj);
            pushTwoNotes.Add(pushTwoObj);
        }
    }

    public GameObject getPooledObject(string noteType){
        switch (noteType){
            case "push1":
                for (int i = 0; i < pushOneNotes.Count; i++){
                    if (pushOneNotes[i].activeInHierarchy){ //if the note we need is in the pool
                        return pushOneNotes[i];
                    }
                }
                break;
            case "hold1":
                for (int i = 0; i < holdOneNotes.Count; i++){
                    if (holdOneNotes[i].activeInHierarchy){ //if the note we need is in the pool
                        return holdOneNotes[i];
                    }
                }
                break;
            case "push2":
                for (int i = 0; i < pushTwoNotes.Count; i++){
                    if (pushTwoNotes[i].activeInHierarchy){ //if the note we need is in the pool
                        return pushTwoNotes[i];
                    }
                }
                break;
        }
        //out of objects and we're allowed to make more
        if (willGrow){
            switch (noteType){
                case "push1":
                    GameObject newPushOneNote = (GameObject)Instantiate(pushOneNote);
                    pushOneNotes.Add(newPushOneNote);
                    return newPushOneNote;
                case "hold1":
                    GameObject newHoldOneNote = (GameObject)Instantiate(holdOneNote);
                    holdOneNotes.Add(newHoldOneNote);
                    return newHoldOneNote;
                case "push2":
                    GameObject newPushTwoNote = (GameObject)Instantiate(pushTwoNote);
                    pushTwoNotes.Add(newPushTwoNote);
                    return newPushTwoNote;
            }

        }
        //out of objects and can't make any more
        return null;
    }
    

}
