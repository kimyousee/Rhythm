using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GUIStyle title, play, exit, subtitle, easy, med, hard, free;
	//public string difficulty;
	private GameController gameCtr;
	static bool playClick = false;

	void Start(){
		// Get access to parts of GameController class
		GameObject gameCtrObject = GameObject.Find("GameController");
		if (gameCtrObject != null) {
			gameCtr = gameCtrObject.GetComponent<GameController>();
		}
	}
	void OnGUI () {
		int width = Screen.width / 3 + 75;

		//play.onHover.textColor = Color.green;
		//exit.onHover.textColor = Color.red;

		// Title: Rhythm
		GUI.Label (new Rect (Screen.width/2-75, Screen.height/4-75, 100, 90), "Rhythm", title);
		// Play Button
		if(playClick == false && GUI.Button(new Rect(width,Screen.height/3+50,300,100), "Play %", play)) {
			playClick = true;
		}
		// Exit Button
		if(playClick == false && GUI.Button(new Rect(width,Screen.height/2+50,300,100), "leave ;-;", exit)) {
			Application.Quit();
		}

		if (playClick == true){
			// Select difficulty
			GUI.Label (new Rect(width-50,Screen.height/3,400,100), "Difficulty :", subtitle);

			if (GUI.Button(new Rect(width+50,Screen.height/2,150,50), "easy :3", easy)){
				gameCtr.difficulty = "easy";
				Application.LoadLevel (1);
			}
			if (GUI.Button(new Rect(width+50,Screen.height/2+60,150,50), "medium ~", med)){
				gameCtr.difficulty = "medium";
			    Application.LoadLevel (1);
			}
			if (GUI.Button(new Rect(width+25,Screen.height/2+110,200,50), "hard > <", hard)){
				gameCtr.difficulty = "hard";
			    Application.LoadLevel (1);
			}
			if (GUI.Button(new Rect(width+25,Screen.height/2+165,200,50), "free ^ ^", free)){
				gameCtr.difficulty = "free";
			    Application.LoadLevel (1);
			}
		}
	}

}
