using UnityEngine;
using System.Collections;

// Stores some variables and keeps track of score
public class GameController : MonoBehaviour {
    //private MainMenu chosen;
    public string solButton; //solution key/correct key to press for most points
    public string difficulty; //from MainMenu object
    public string song; // from SongSelect object
    public int maxNotes;
    public int bps;

    private int score;
    private MainMenu chosen;
    void Awake(){
        bps = 20; //temp!
        maxNotes = bps / 18;
        DontDestroyOnLoad (transform.gameObject); // Saves variables from other loads (i.e. difficulty)
    }

    // Use this for initialization
    void Start () {
        score = 0;
    }

    public int calculateScore(Note note, float time, string keyPushed, string keyPushed2="", float holdTimer=0f){
        int button  = noteToVal(keyPushed);
        int button2 = noteToVal(keyPushed2);
        
        float timeDiff = Mathf.Abs(note.time - time);
        if (timeDiff > 0.5) return score;
        
        // needed to know how far the button they pushed is from the actual button
        int noteDiff  = Mathf.Abs(button  - note.button);
        int noteDiff2 = Mathf.Abs(button2 - note.button2);

        // score for hold notes
        if (holdTimer != 0f){
            float holdDiff = Mathf.Abs(holdTimer - note.hold);
            // double hold notes
            if ( note.button2 != 0 && keyPushed2 != ""){
                if ( ((note.button  == button && note.button2 == button2) ||
                     ( note.button2 == button && note.button  == button2) ) ) {
                    score += (int)(500+holdTimer*100-timeDiff*500-holdDiff*100);
                    return score;
                }
                // for a double note, 1 button is right
                if ( (note.button  == button && note.button2 != button2) ||
                      note.button2 == button && note.button  != button2){
                    score += (int)(300+holdTimer*50-noteDiff*20-noteDiff2*20-timeDiff*200-holdDiff*50);
                    return score;
                }
                // none of the pushed buttons are right
                score += (int)(120+holdTimer*10-noteDiff*5-noteDiff2*5-timeDiff*10-holdDiff*10);
                return score;
            }
            // single hold note
            if (note.button2 == 0){
                if (note.button == button){
                    score += (int)(100+holdTimer*20-timeDiff*100-holdDiff*20);
                    return score;
                }
                else {
                    score += (int)(30+holdTimer*5-noteDiff*2-timeDiff*10-holdDiff*5);
                    return score;
                }
            }
        }
        else { // no hold
            // double note score
            if ( note.button2 != 0 && button2 != 0){
                if ( ((note.button  == button && note.button2 == button2) ||
                     ( note.button2 == button && note.button  == button2) ) ) {
                    score += (int)(500-timeDiff*500);
                    return score;
                }
                // for a double note, 1 button is right
                if ( (note.button  == button && note.button2 != button2) ||
                      note.button2 == button && note.button  != button2){
                    score += (int)(300-noteDiff*20-noteDiff2*20-timeDiff*200);
                    return score;
                }
                // none of the pushed buttons are right
                score += (int)(120-noteDiff*5-noteDiff2*5-timeDiff*10);
                return score;
            }
            // single note
            if (note.button2 == 0){
                if (note.button == button){
                    score += (int)(100-timeDiff*100);
                    return score;
                }
                else {
                    score += (int)(30-noteDiff*2-timeDiff*10);
                    return score;
                }
            }
        }
        return score;
    }

    int noteToVal(string note){
        switch (difficulty) {
            case "free":
                switch (note) {
                    case "Inp1a": return 1;
                    case "Inp1b": return 1;
                    case "Inp2a": return 2;
                    case "Inp2b": return 2;
                    case "Inp3a": return 3;
                    case "Inp3b": return 3;
                    case "Inp4a": return 4;
                    case "Inp4b": return 4;
                    case "Inp5a": return 5;
                    case "Inp5b": return 5;
                    case "Inp6a": return 6;
                    case "Inp6b": return 6;
                    case "Inp7a": return 7;
                    case "Inp7b": return 7;
                    case "Inp8a": return 8;
                    case "Inp8b": return 8;
                    case "Inp9a": return 9;
                    case "Inp9b": return 9;
                    case "Inp10a": return 10;
                    case "Inp10b": return 10;
                    default: return 0;
                }
            case "hard":
                switch (note) {
                    case "Inp1a": return 1;
                    case "Inp1b": return 1;
                    case "Inp2a": return 1;
                    case "Inp2b": return 1;
                    case "Inp3a": return 1;
                    case "Inp3b": return 1;
                    case "Inp4a": return 2;
                    case "Inp4b": return 2;
                    case "Inp5a": return 2;
                    case "Inp5b": return 2;
                    case "Inp6a": return 2;
                    case "Inp6b": return 2;
                    case "Inp7a": return 3;
                    case "Inp7b": return 3;
                    case "Inp8a": return 3;
                    case "Inp8b": return 3;
                    case "Inp9a": return 3;
                    case "Inp9b": return 3;
                    case "Inp10a": return 4;
                    case "Inp10b": return 4;
                    default: return 0;
                }
            case "medium":
                switch (note) {
                    case "Inp1a": return 1;
                    case "Inp2a": return 1;
                    case "Inp3a": return 1;
                    case "Inp4a": return 1;
                    case "Inp5a": return 1;
                    case "Inp6a": return 1;
                    case "Inp7a": return 1;
                    case "Inp8a": return 1;
                    case "Inp9a": return 1;
                    case "Inp10a": return 1;
                    case "Inp1b": return 10;
                    case "Inp2b": return 10;
                    case "Inp3b": return 10;
                    case "Inp4b": return 10;
                    case "Inp5b": return 10;
                    case "Inp6b": return 10;
                    case "Inp7b": return 10;
                    case "Inp8b": return 10;
                    case "Inp9b": return 10;
                    case "Inp10b": return 10;
                    default: return 0;
                }
            case "easy":
                return 1;
            default: return 1;
        }
    }
}
