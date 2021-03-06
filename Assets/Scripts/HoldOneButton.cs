﻿using UnityEngine;
using System.Collections;

public class HoldOneButton : MonoBehaviour {
    public string colour;
    public Note note;
    public Target target;
    
    // Use this for initialization
    void Start () {
        GameObject targCtrObj = GameObject.Find("Target");
        target = targCtrObj.GetComponent<Target>();
        this.transform.parent = target.transform;
        // target.noteType.Add("hold1");
        target.note = note;
    }
    
    // Update is called once per frame
    void Update () {
        string key = keyHeld();
        target.key = key;
    }

    string keyHeld() {
        if (Input.GetButton("Inp1a")) return "Inp1a";
        if (Input.GetButton("Inp2a")) return "Inp2a";
        if (Input.GetButton("Inp3a")) return "Inp3a";
        if (Input.GetButton("Inp4a")) return "Inp4a";
        if (Input.GetButton("Inp5a")) return "Inp5a";
        if (Input.GetButton("Inp6a")) return "Inp6a";
        if (Input.GetButton("Inp7a")) return "Inp7a";
        if (Input.GetButton("Inp8a")) return "Inp8a";
        if (Input.GetButton("Inp9a")) return "Inp9a";
        if (Input.GetButton("Inp10a")) return "Inp10a";
        if (Input.GetButton("Inp1b")) return "Inp1b";
        if (Input.GetButton("Inp2b")) return "Inp2b";
        if (Input.GetButton("Inp3b")) return "Inp3b";
        if (Input.GetButton("Inp4b")) return "Inp4b";
        if (Input.GetButton("Inp5b")) return "Inp5b";
        if (Input.GetButton("Inp6b")) return "Inp6b";
        if (Input.GetButton("Inp7b")) return "Inp7b";
        if (Input.GetButton("Inp8b")) return "Inp8b";
        if (Input.GetButton("Inp9b")) return "Inp9b";
        if (Input.GetButton("Inp10b")) return "Inp10b";
        return "";
    }
}
