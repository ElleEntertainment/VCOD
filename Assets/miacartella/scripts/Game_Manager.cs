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
        JSONObject data = DbManager.loadWorld();
        if (data.Count > 0)
        {
            nInfetti = data.Count;
            infetti = new Infetto[nInfetti];
            for (int i = 0; i < data.Count; i++)
            {
                float position_x = (float)System.Convert.ToDouble(data[i].GetField("position_x"));
                float position_y = (float)System.Convert.ToDouble(data[i].GetField("position_y"));
                float position_z = (float)System.Convert.ToDouble(data[i].GetField("position_z"));
                Vector3 v = new Vector3(position_x, position_y, position_z);
                float rotation_x = (float)System.Convert.ToDouble(data[i].GetField("rotation_x"));
                float rotation_y = (float)System.Convert.ToDouble(data[i].GetField("rotation_y"));
                float rotation_z = (float)System.Convert.ToDouble(data[i].GetField("rotation_z"));
                Quaternion q = new Quaternion(rotation_x, rotation_y, rotation_z, 0);
                Infetto inf = Instantiate(infetto, v, q) as Infetto;
                inf.setId(System.Convert.ToInt32(data[i].GetField("idSpawn")));
                inf.setLevel(player.getLevel());
                infetti[i] = inf;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*for (int i = 0; i < infetti.Length; i++)
        {
            if (infetti[i] != null)
            {
                if (infetti[i].isDead())
                {
                    infetti[i] = null;
                    player.SendMessage("setExp", 15);
                    player.SendMessage("updateExp");
                    break;
                }
            }
        }*/
	}
	//getter per array di infetti (servirà più avanti sicuramente)
	public Infetto[] getInfettiInMap(){
		return infetti;
	}
    public Infetto getInfettiMorti() {

        return infetto;
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
