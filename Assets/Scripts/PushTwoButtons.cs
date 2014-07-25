using UnityEngine;
using System.Collections;

public class PushTwoButtons : Target {

    // Use this for initialization
    protected override void Start () {
        base.noteType = "hold2";
        base.Start();
    }
    
    // Update is called once per frame
    protected override void Update () {
        string key = keysPushed();
        base.key = key;
        base.Update();
    }
    //... make function
    string keysPushed(){
        return "";
    }
}
