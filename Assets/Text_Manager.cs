using UnityEngine;
using System.Collections;


//Tutti gli aggiornamenti sui testi del game passeranno di qua
//---------------------------
public class Text_Manager : MonoBehaviour {

	public GUISkin customSkin;
	string playerCurHealth;
	string playerToTHealth;
	string targetCurHealth;
	string targetTotHealth;
	string targetDamage;
	string playerDamage;

	float disappearTime;
	// Use this for initialization
	void Start () {

		disappearTime = 0.7F;
	}
	
	// Update is called once per frame
	void Update () {
		//fa scomparire il testo del danno dopo un po' di tempo
		disappearTime -= Time.deltaTime;
		if (disappearTime <= 0) {
			playerDamage = "";
		}
	}

	void OnGUI(){
		GUI.skin = customSkin;
		GUI.color = Color.black;
		GUI.Box (new Rect (0, 0, 200, 25), playerCurHealth + "/" + playerToTHealth);
		GUI.color = Color.red;
		GUI.Label (new Rect (Screen.width / 2 - 20, Screen.height / 2 - 20, 40, 40), playerDamage);

	}
	//funzione che chiama il player quando deve aggiornare i suoi dati
	void playerText(string value) {
		char[] del = {'-'};
		string[] values = value.Split(del);
		playerCurHealth = values [0];
		playerToTHealth = values [1];
		playerDamage = values [2];
		disappearTime = 0.7F;
	}
}
