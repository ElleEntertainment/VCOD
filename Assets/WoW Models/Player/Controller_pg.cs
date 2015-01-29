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
        long tempo_attacco;
        long tempo_ora_regen_health;
		//--------------------------------


		// Use this for initialization
		void Start ()
		{
		anim = GetComponent<Animator> ();
		rig = GetComponent<Rigidbody> ();
		jump = false;
		run = true;
		health = 250;
		maxHealth = health;
		spawnPos = transform.position;
		TM.SendMessage ("playerText", health + "-" + maxHealth + "-");
		isAttacking = false;
        tempo_attacco = UnixTimeNow();
        tempo_ora_regen_health = UnixTimeNow();
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
        if (Input.GetKey(KeyCode.K))
        {
            long tempo_now = UnixTimeNow();
            long t = tempo_now - tempo_attacco;
            Debug.Log("Differenza tempo = " + t);
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
				TM.SendMessage ("playerText", health + "-" + maxHealth + "-");
		}

        if (Input.GetKey(KeyCode.R))
        {
            if (currentTarget != null)
            {
                long tempo_ora = UnixTimeNow(); 
                long diff_tempo = tempo_ora - tempo_attacco;
                if (diff_tempo >= 2) //se sono passati 2 secondi...
                {
                    if (Vector3.Distance(transform.position, currentTarget.transform.position) <= 2)
                    {
                        isAttacking = true;
                        attackEnemy(currentTarget);
                        tempo_attacco = UnixTimeNow();
                    }
                }
                else
                    isAttacking = false;
            }
            else
                isAttacking = false;
        }

        if (currentTarget != null && !isAttacking)
        {
            TM.setTargetTrue(true);
            string targetInfo = currentTarget.getHealth() + "-" + currentTarget.getMaxHealth() + "-";
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
            if ((tempo_attacco - tempo_ora_regen_health) >= 5 || (tempo_attacco - tempo_ora_regen_health) == 0)
            {
                int max_health_player = getMaxHealthPlayer();
                if (health < max_health_player)
                    health = health + (max_health_player / 100) * 3; //+3% ogni 5 secondi
                if (health > max_health_player)
                    health = max_health_player;
                TM.SendMessage("playerText", health + "-" + maxHealth + "-0");
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
			string targetInfo = currentTarget.getHealth()+"-"+currentTarget.getMaxHealth()+"-"+dam;
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
				TM.SendMessage ("playerText", health + "-" + maxHealth + "-" + damage);
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

		void setTarget (Infetto inf)
		{
				if (inf != currentTarget)
						currentTarget = inf;
				Debug.Log ("L'id del target è " + inf.getId ());
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
