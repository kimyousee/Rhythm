using UnityEngine;
using System.Collections;

public class PushTwoButtons : MonoBehaviour {
    public string colour;
    public Note note;
    public Target target;

    // Use this for initialization
    void Start () {
        GameObject targCtrObj = GameObject.Find("Target");
        target = targCtrObj.GetComponent<Target>();
        this.transform.parent = target.transform;
        target.noteType.Add("push2");
    }
    
    // Update is called once per frame
    void Update () {
        string key = keysPushed();
        target.key.Add(key);
    }
    //... make function
    string keysPushed(){
        return "";
    }
}
