using UnityEngine;
using System.Collections;

public class PushOneButton : Target {
    public string colour;
    public Note note;

    void Awake(){
        base.noteType = "push1";
        base.note = note;
    }
    // Note sure if this is needed; check later.
    //protected override void Start () {
        //give it a colour
        //base.Start ();
    //}
    
    // Update is called once per frame
    protected override void Update () {
        string key = keyPushed();
        base.key = key;
        base.Update();
    }

    // Check which key is pushed
    public string keyPushed(){
        if (Input.GetButtonDown("Inp1a")) return "Inp1a";
        if (Input.GetButtonDown("Inp2a")) return "Inp2a";
        if (Input.GetButtonDown("Inp3a")) return "Inp3a";
        if (Input.GetButtonDown("Inp4a")) return "Inp4a";
        if (Input.GetButtonDown("Inp5a")) return "Inp5a";
        if (Input.GetButtonDown("Inp6a")) return "Inp6a";
        if (Input.GetButtonDown("Inp7a")) return "Inp7a";
        if (Input.GetButtonDown("Inp8a")) return "Inp8a";
        if (Input.GetButtonDown("Inp9a")) return "Inp9a";
        if (Input.GetButtonDown("Inp10a")) return "Inp10a";
        if (Input.GetButtonDown("Inp1b")) return "Inp1b";
        if (Input.GetButtonDown("Inp2b")) return "Inp2b";
        if (Input.GetButtonDown("Inp3b")) return "Inp3b";
        if (Input.GetButtonDown("Inp4b")) return "Inp4b";
        if (Input.GetButtonDown("Inp5b")) return "Inp5b";
        if (Input.GetButtonDown("Inp6b")) return "Inp6b";
        if (Input.GetButtonDown("Inp7b")) return "Inp7b";
        if (Input.GetButtonDown("Inp8b")) return "Inp8b";
        if (Input.GetButtonDown("Inp9b")) return "Inp9b";
        if (Input.GetButtonDown("Inp10b")) return "Inp10b";
        return "";
    }
}
