using UnityEngine;
using System.Collections;
using System;

public class Controller_pg : MonoBehaviour
{

		//inizializzazione variabili
		public Text_Manager TM;
		float speed = 1;
		bool jump = false;
		bool falling = false;
		bool run = true;
		float YPos = 0;
		float jumpHeigth = 2;
		float attackTime = 1.0F;
		int health;
		int maxHealth;
		string damageRec = "";
		bool attackRec = false;
		public GUISkin customSkin;
		Infetto currentTarget = null;
		bool wasDead = false;
		bool isAttacking;
		Vector3 spawnPos;
		int level = 1;
		int exp;
		int expToNextLevel;
        long tempo_attacco;
        long tempo_ora_regen_health;
        long t;
		Vector3 mousePosition;
		public GameObject rightArm;
		public GameObject Contenitore;
        float camera_pos_x, camera_pos_y, camera_pos_z, camera_rot_x, camera_rot_y, camera_rot_z;
        GameObject camera;
        bool isTargetting;
        GameObject doppietta;
        float doppietta_pos_x, doppietta_pos_y, doppietta_pos_z, doppietta_rot_x, doppietta_rot_y, doppietta_rot_z;
		//--------------------------------


		// Use this for initialization
		void Start ()
		{
		level = 1;
		exp = 0;
		expToNextLevel = Mathf.RoundToInt(150 * 1.1F);
		jump = false;
		run = true;
		health = 250;
		maxHealth = health;
		spawnPos = transform.position;
		TM.SendMessage ("playerText", health + "-" + maxHealth + "-" + " " + "-" + level + "-" + exp + "-" + expToNextLevel);
		isAttacking = false;
        tempo_attacco = UnixTimeNow();
        tempo_ora_regen_health = UnixTimeNow();
        t = UnixTimeNow();
		mousePosition = Input.mousePosition;
        load("player");
        isTargetting = false;

        camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera_pos_x = camera.transform.position.x;
        camera_pos_y = camera.transform.position.y;
        camera_pos_z = camera.transform.position.z;
        camera_rot_x = camera.transform.rotation.x;
        camera_rot_y = camera.transform.rotation.y;
        camera_rot_z = camera.transform.rotation.z;

        doppietta = GameObject.FindGameObjectWithTag("Doppietta");
        doppietta_pos_x = doppietta.transform.position.x;
        doppietta_pos_y = doppietta.transform.position.y;
        doppietta_pos_z = doppietta.transform.position.z;
        doppietta_rot_x = doppietta.transform.rotation.x;
        doppietta_rot_y = doppietta.transform.rotation.y;
        doppietta_rot_z = doppietta.transform.rotation.z;
		}

		// Update is called once per frame
		void Update ()
		{
		if (currentTarget != null) {
			Vector3 targetDir = currentTarget.transform.position - Contenitore.transform.position;
			Vector3 newDir = Vector3.RotateTowards(Contenitore.transform.up, targetDir, 30, 1.0F);
			rightArm.transform.rotation = Quaternion.LookRotation(newDir);
		}

		//Sistema di Rotazione (non ce n'è bisogno per ora)
		if (Input.GetKey (KeyCode.A)) 
				transform.Rotate (Vector3.up, -1); //-1 sono i gradi di rotazione
		else
		if (Input.GetKey (KeyCode.D))
				transform.Rotate (Vector3.up, 1);
	
	
		// FINE SISTEMA DI ROTAZIONE


		//Usiamo i tasti per muoverci
        if (Input.GetKey(KeyCode.K)) //Tasto per debuggare
        {
            long tempo_now = UnixTimeNow();
            long t = tempo_now - tempo_attacco;
            Debug.Log("Differenza tempo = " + t);

            Debug.Log("Time.DeltaTime = " + Time.deltaTime);
            //Debug.Log("Rotazioneeeeeeeeeeeeeeee" + transform.rotation.y);
        }

        //tasto per spawnare npc
        if (Input.GetKey(KeyCode.U))
        {
            insertNpc();
        }
        if (Input.GetKey(KeyCode.Mouse1)) //dx mouse
        {
            //sposta telecamera sull'arma
            //TODO: eseguire questa funzione se il tasto è premuto, quando viene rilasciato isTargetting = false;
            if (!isTargetting)
            {
                camera.transform.position = new Vector3(doppietta_pos_x, doppietta_pos_y, doppietta_pos_z);
                camera.transform.rotation = new Quaternion(doppietta_rot_x, doppietta_rot_y, doppietta_rot_z, 0);
                isTargetting = true;
                Debug.Log("Camera spostata sull'arma");
            }
            else
            {
                camera.transform.position = new Vector3(camera_pos_x, camera_pos_y, camera_pos_z);
                camera.transform.rotation = new Quaternion(camera_rot_x, camera_rot_y, camera_rot_z, 0);
                isTargetting = false;
                Debug.Log("Camera spostata sul personaggio");
            }
        }

		//Fine sistema di movimento semplice

		if(Input.GetMouseButtonDown(0)){
			if(currentTarget!=null){
				currentTarget.stopParticle();
				currentTarget = null;
				TM.setTargetTrue(false);
			}
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			save ();
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			load ("player");
		}

		//Check morte del player
		if (health <= 0) {
				Debug.Log ("Player morto.");
				//Respawna il player
				transform.position = spawnPos;
				//Resetta la vita
				health = maxHealth;
				wasDead = true;
				TM.SendMessage ("playerText", health + "-" + maxHealth + "-" + " " + "-" + level + "-" + exp + "-" + expToNextLevel);
		}
		
	    if (currentTarget != null)
	    {
	            if (Vector3.Distance(transform.position, currentTarget.transform.position) <= 2)
	            {
	                isAttacking = true;
	                attackEnemy(currentTarget);
	            }
	        	else
	            	isAttacking = false;
	    }
	    else
	        isAttacking = false;

        if (currentTarget != null && !isAttacking)
        {
            TM.setTargetTrue(true);
            string targetInfo = currentTarget.getHealth() + "-" + currentTarget.getMaxHealth() + "-" + " " + "-" + currentTarget.getLevel() + "-";
            TM.SendMessage("targetText", targetInfo);
        }
        if (currentTarget != null)
        {
            if (currentTarget.isDead())
            {
                currentTarget = null;
                TM.setTargetTrue(false);
            }
        }

        if (currentTarget == null && !isAttacking) 
        {
            
            /*
             * ********************************************
             * Health regeneration System
             * ********************************************/
            /*tempo_ora_regen_health = UnixTimeNow(); //registro quando sono uscito fuori dal combat
            
            if ((tempo_ora_regen_health - t) >= 5)
            {
                int max_health_player = getMaxHealthPlayer();
                if (health < max_health_player)
                    health = health + (max_health_player / 100) * 3; //+3% ogni 5 secondi (in questo caso +7.5)
                if (health >= max_health_player)
                    health = max_health_player;
                TM.SendMessage("playerText", health + "-" + maxHealth + "-" + " " + "-" + level + "-" + exp + "-" + expToNextLevel);
                t = UnixTimeNow();
                //Debug.Log("Doing Health regeneration");
                /*if (health == max_health_player)
                    Debug.Log("Health regeneration completed");
            }*/
        }
        

		}

		void attackEnemy (Infetto target)
		{
			if (attackTime - Time.deltaTime <= 0) {
			int dam = UnityEngine.Random.Range (10, 25);
			target.SendMessage("applyDamage", dam);
			string targetInfo = currentTarget.getHealth()+"-"+currentTarget.getMaxHealth()+"-"+dam+"-"+currentTarget.getLevel()+"-";
			TM.SendMessage("targetText", targetInfo);
			attackTime = 1.5F;
			} else
				attackTime -= Time.deltaTime;
		}
	
		void applyDamage (int damage)
		{
				health -= damage;
				if (health <= 0) {
						health = 0;
				}
				damageRec = "<b>" + damage + "</b>";
				attackRec = true;
				TM.SendMessage ("playerText", health + "-" + maxHealth + "-" + damage + "-" + level + "-" + exp + "-" + expToNextLevel);
				Debug.Log ("Damage Received " + damage);
		}

		public int getHealthPlayer ()
		{
			return health;
		}
        public int getMaxHealthPlayer()
        {
            return maxHealth;
        }
		public int getLevel(){
			return level;
		}
		
		void setTarget (Infetto inf)
		{		
			if (currentTarget != null)
					currentTarget.stopParticle ();
			if (inf != currentTarget)
					currentTarget = inf;
			Debug.Log ("L'id del target è " + inf.getId ());
			currentTarget.startParticle ();
		}
		void setExp(int experience){
			exp = exp + experience;
			if (exp >= expToNextLevel) {
				level++;
				exp = 0;
				expToNextLevel  = Mathf.RoundToInt((level * 150) * 1.1F);
			}
			string targetInfo = " - - - -" + experience;
			TM.SendMessage("targetText", targetInfo);
			TM.SendMessage ("playerText", health + "-" + maxHealth + "-" + "" + "-" + level + "-" + exp + "-" + expToNextLevel);
		}

		public Infetto getTarget()
		{
			if (currentTarget != null)
						return currentTarget;
				else {
						Debug.Log ("Nessun target selezionato");
						return null;
				}
		}

        void insertNpc() {
            float pos_x = transform.position.x;
            float pos_y = transform.position.y;
            float pos_z = transform.position.z;
            float ori_x = transform.rotation.x;
            float ori_y = transform.rotation.y;
            float ori_z = transform.rotation.z;
            DbManager.setInstance();
            string npc = "INSERT INTO nemici_info(position_x, position_y, position_z, orientation_x, orientation_y, orientation_z) VALUES(" + pos_x + ", " + pos_y + ", " + pos_z + ", " + ori_x + ", " + ori_y + ", " + ori_z + ");";
            Debug.Log(npc);
            DbManager.executeQuery(npc);
            Debug.Log("npc aggiunto nel db");
        }
		void save()
        {
		    DbManager.setInstance ();
            string myData = "INSERT OR REPLACE INTO player VALUES('player', " + level + ", " + exp + ", " + health + ", " + maxHealth + ", " + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ", " + transform.rotation.x + ", " + transform.rotation.y + ", " + transform.rotation.z + ");";
		    Debug.Log (myData);
		    DbManager.executeQuery(myData);
		}
        public void load(string s)
        {
            DbManager.setInstance();
            string myData = DbManager.loadPlayer(s);
            Debug.Log(myData);
            if (!myData.Equals(""))
            {
                char[] del = { '|' };
                string[] values = myData.Split(del);
                level = Convert.ToInt32(values[0]);
                exp = Convert.ToInt32(values[1]);
                health = Convert.ToInt32(values[2]);
                maxHealth = Convert.ToInt32(values[3]);
                spawnPos.x = (float)Convert.ToDouble(values[4]);
                spawnPos.y = (float)Convert.ToDouble(values[5]);
                spawnPos.z = (float)Convert.ToDouble(values[6]);
                transform.position = spawnPos;
                transform.Rotate((float)Convert.ToDouble(values[7]), (float)Convert.ToDouble(values[8]), (float)Convert.ToDouble(values[9]));
                expToNextLevel = Mathf.RoundToInt((level * 150) * 1.1F);
                TM.SendMessage("playerText", health + "-" + maxHealth + "-" + "" + "-" + level + "-" + exp + "-" + expToNextLevel);
            }
        }
        public long UnixTimeNow()
        {
            var timeSpan = (System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        public Controller_pg getPlayer()
        {
            return this;
        }
}
