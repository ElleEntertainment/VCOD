using UnityEngine;
using System.Collections;

//Classe per gestire il gioco (creazione infetti e spawn vari)
//----------------------------------
public class Game_Manager : MonoBehaviour {

	Infetto[] infetti;
	public  Infetto infetto;
	public Player player;
    int nInfetti = 0;
    
	// Use this for initialization
	void Start () {
        DbManager.setInstance();
        string[] data = DbManager.loadWorld();
        if (data.Length > 0)
        {
            nInfetti = data.Length;
            infetti = new Infetto[nInfetti];
            char[] carattereDivisore = { '|' };
            string[] values;
            for (int i = 0; i < data.Length; i++)
            {
                values = data[i].Split(carattereDivisore);
                float position_x = (float)System.Convert.ToDouble(values[1]); //Metto System. davanti altrimenti mi da errori
                float position_y = (float)System.Convert.ToDouble(values[2]);
                float position_z = (float)System.Convert.ToDouble(values[3]);
                Vector3 v = new Vector3(position_x, position_y, position_z);
                float rotation_x = (float)System.Convert.ToDouble(values[4]);
                float rotation_y = (float)System.Convert.ToDouble(values[5]);
                float rotation_z = (float)System.Convert.ToDouble(values[6]);
                Debug.Log("x: " + position_x + " | y: " + position_x + " | z: " + position_z + " | ox: " + rotation_x + " | oy: " + rotation_y + " | oz: " + rotation_z);
                Quaternion q = new Quaternion(rotation_x, rotation_y, rotation_z, 0);
                Infetto inf = Instantiate(infetto, v, q) as Infetto;
                inf.setId(System.Convert.ToInt32(values[0]));
                inf.setLevel(player.getLevel());
                infetti[i] = inf;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		/*for (int i = 0; i < nInfetti; i++) {
			//check death
            if(infetti[i].isDead()){
				Destroy(infetti[i].gameObject);
			}
            //check collision
            
		}*/
	}
	//getter per array di infetti (servirà più avanti sicuramente)
	public Infetto[] getInfettiInMap(){
		return infetti;
	}
    public Player getPgInMap()
    {
        return player;
    }
    //ToDO: instantiate di un infetto con range variabile tra punto di spawn e qualche unità
	/*public void createNewInfetto(int id){
	Infetto inf = Instantiate(infetto ,new Vector3(Random.Range(750, 850), 0, Random.Range(750, 850)), new Quaternion(0, Random.Range(-1, 1), 0, 0)) as Infetto;
	inf.setId(id);
	inf.setLevel(player.getLevel());
	infetti[id] = inf;
	}*/

    
}
