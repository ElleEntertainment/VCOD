using UnityEngine;
using System.Collections;

//Classe per gestire il gioco (creazione infetti e spawn vari)
//----------------------------------
public class Game_Manager : MonoBehaviour {

    public int num_infetti = 5;
	Infetto[] infetti = new Infetto[5];
	public  Infetto infetto;
	Vector3[] positions = new Vector3[5];
	Quaternion[] rotations = new Quaternion[5];
	public Controller_pg player;
    
	// Use this for initialization
	void Start () {
		//istanzio 5 infetti e assegno loro un id
		for (int j = 0; j < num_infetti; j++) { 
			positions[j] = new Vector3(Random.Range(750, 850), 0, Random.Range(750, 850));
		}
		for (int k = 0; k < num_infetti; k++) {
			rotations[k] = new Quaternion(0, Random.Range(-1, 1), 0, 0);
		}
		for (int i = 0; i < num_infetti; i++) {
			Infetto inf = Instantiate(infetto ,positions[i], rotations[i]) as Infetto;
			inf.setId(i);
			inf.setLevel(player.getLevel());
			infetti[i] = inf;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < infetti.Length; i++) {
			if(infetti[i] != null){
				if(infetti[i].isDead()){
					Destroy(infetti[i].gameObject);
					createNewInfetto(i);
				}
			}
		}
	}
	//getter per array di infetti (servirà più avanti sicuramente)
	public Infetto[] getInfettiInMap(){
		return infetti;
	}
	public void createNewInfetto(int id){
	Infetto inf = Instantiate(infetto ,new Vector3(Random.Range(750, 850), 0, Random.Range(750, 850)), new Quaternion(0, Random.Range(-1, 1), 0, 0)) as Infetto;
	inf.setId(id);
	inf.setLevel(player.getLevel());
	infetti[id] = inf;
	}
}
