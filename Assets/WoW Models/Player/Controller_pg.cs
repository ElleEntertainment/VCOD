using UnityEngine;
using System.Collections;

public class Controller_pg : MonoBehaviour
{

		//inizializzazione variabili
		public Text_Manager TM;
		float speed = 1;
		Animator anim;
		Rigidbody rig;
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
		//--------------------------------


		// Use this for initialization
		void Start ()
		{
		level = 1;
		exp = 0;
		expToNextLevel = 150;
		anim = GetComponent<Animator> ();
		rig = GetComponent<Rigidbody> ();
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
		}
	
		// Update is called once per frame
		void Update ()
		{
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo (0);
		//Sistema di Rotazione (non ce n'è bisogno per ora)
		if (Input.GetKey (KeyCode.A)) 
				transform.Rotate (Vector3.up, -1); //-1 sono i gradi di rotazione
		else
		if (Input.GetKey (KeyCode.D))
				transform.Rotate (Vector3.up, 1);
	
		// FINE SISTEMA DI ROTAZIONE
		if (Input.GetKeyDown (KeyCode.C)) {
				run = !run;
				anim.SetBool ("canRun", run);
		}

		//Usiamo i tasti per muoverci
        if (Input.GetKey(KeyCode.K)) //Tasto per debuggare
        {
            long tempo_now = UnixTimeNow();
            long t = tempo_now - tempo_attacco;
            Debug.Log("Differenza tempo = " + t);

            Debug.Log("Time.DeltaTime = " + Time.deltaTime);
        }
		if (Input.GetKey (KeyCode.W)) {
				//anim.SetFloat("direction", move);
				if (run) {
						anim.SetBool ("run", true);
						anim.SetBool ("walk", false);
						transform.Translate (Vector3.left * speed / 15, Space.Self);
				} else {
						anim.SetBool ("run", false);
						anim.SetBool ("walk", true);
						transform.Translate (Vector3.left * speed / 30, Space.Self);
				}
				transform.Translate (Vector3.left * speed / 30, Space.Self); // diviso 30 per ridurre la velocità
		}
		if (Input.GetKey (KeyCode.S))
				transform.Translate (Vector3.right * speed / 30, Space.Self);
		/*if (Input.GetKey (KeyCode.A))
				transform.Translate (Vector3.back * speed / 30, Space.Self);
		if (Input.GetKey (KeyCode.D))
				transform.Translate (Vector3.forward * speed / 30, Space.Self);*/
		//Fine sistema di movimento semplice
		//Ci sono gli if senza gli else per dare la possibilità di muoversi in più direzioni in una volta
		if (Input.GetKeyUp (KeyCode.W)) { //quando si rilascia il tasto W il personaggio non deve più correre
				if (run)
						anim.SetBool ("run", false);
				else
						anim.SetBool ("walk", false);
		}
		if(Input.GetMouseButtonDown(0)){
			currentTarget.stopParticle();
			currentTarget = null;
			TM.setTargetTrue(false);
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

        if (currentTarget == null && !isAttacking) //health regen (ci penso io, devo capire come calcolare il tempo nel gioco)
        {
            
            tempo_ora_regen_health = UnixTimeNow(); //registro quando sono uscito fuori dal combat
            
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
                    Debug.Log("Health regeneration completed");*/
            }
        }
        

		}

		void FixedUpdate ()
		{
				if (!jump && !falling) {
						if (Input.GetKeyDown (KeyCode.Space)) {
								jump = true;
								YPos = transform.position.y;
						}
				}
				do_jump ();
		}

		void do_jump ()
		{
				if (jump && !falling) {
						if (YPos + jumpHeigth >= transform.position.y) {
								Debug.Log ("Jumping");
								rig.AddForce (Vector3.up * 200);
								falling = true;
								anim.SetBool("jump", true);
						}
				}
		}

		void OnCollisionEnter (Collision collision)
		{
				if (falling) {
						Debug.Log ("Hit the ground");
						falling = false;
						jump = false;
						anim.SetBool("jump", false);
				}
		}

		void attackEnemy (Infetto target)
		{
			if (attackTime - Time.deltaTime <= 0) {
			int dam =Random.Range (10, 25);
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

        public long UnixTimeNow()
        {
            var timeSpan = (System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }
}
