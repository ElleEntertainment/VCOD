using UnityEngine;
using System.Collections;

public class Infetto : MonoBehaviour {

	int health;
	GameObject player;
	Animator anim;
	bool isInCombat = false;
	bool isAttacking = false;
	bool backHome = false;
	float attackTime = 1.5F;
	bool attackRec = false;
	string damageRec;
	Vector3 initialPos;
	// Use this for initialization
	void Start () {
		health = 150;
		player = GameObject.FindGameObjectsWithTag("Player")[0];
		anim = GetComponent<Animator>();
		initialPos = transform.position;
		damageRec = "";
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 posToFace;

		float distanceFromPlayer = Vector3.Distance (transform.position, player.transform.position);
		float distanceFromHome = Vector3.Distance (transform.position, initialPos);
		if (distanceFromPlayer <= 8 && distanceFromPlayer >= 2 && !isInCombat && !backHome) {
			isInCombat = true;
			anim.SetBool("run", true);
		}


        //------ posiziona il mob verso la direzione del player
		if (isInCombat) 
        {
			posToFace = player.transform.position;
		} 
        else 
        {
			posToFace = initialPos;
		}
		facePosition (posToFace);
        //-------------------------------------------------------

        // se il mob è in combat e la distanza è maggiore o uguale a 2 si muove verso il player e non attacca
		if (isInCombat && distanceFromPlayer >= 2) {
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.05F);
			isAttacking = false;
		}
        //-----------------------------------------------
        //questa non l'ho capita anche perchè non ho mai visto 2 booleani confrontati (sono 2 bool no?)
		if (anim.GetBool ("attack") != isAttacking)
						anim.SetBool ("attack", isAttacking);
        //------------------------------------------
        //Se il mob è in combat e la distanza è minore di 2 e non sta attaccando allora il mob comincia a attaccare
		if (isInCombat && distanceFromPlayer < 2 && !isAttacking) {
			isAttacking = true;
		}
        //------------
        //Se è in combat ma la distanza dal punto di spawn è maggiore o uguale a 25 torna al punto di spawn e resetto l'aggro
		if (isInCombat && distanceFromHome >= 25) {
			isInCombat = false;
			backHome = true;
			isAttacking = false;
		}
        //-----------------------------
        //questo confronto non l'ho capito
		if (backHome && distanceFromHome > 0) {
			transform.position = Vector3.MoveTowards(transform.position, initialPos, 0.05F);
			if(distanceFromHome <= 1){
				backHome = false;
				anim.SetBool("run", false);
			}
				
		}

        //se il bool è a true attacco il player
		if(isAttacking)
			attackPlayer();


	}

	void facePosition(Vector3 pos){
			if (isInCombat || backHome) {
			Vector3 target = pos - transform.position;
			float angle = Vector3.Angle(target, transform.forward);
			Vector3 targetDir = pos - transform.position;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 30, 1.0F);
			transform.rotation = Quaternion.LookRotation(newDir);
		}
	}

	void attackPlayer(){
		if (attackTime - Time.deltaTime <= 0) {
			Random.seed = (int)Time.time;
			int dam = Mathf.RoundToInt(Random.Range(5, 15));
			player.SendMessage("applyDamage", dam);
			attackTime = 1.5F;
			Debug.Log("Player Attacked");

		} else
			attackTime -= Time.deltaTime;
	}

	void applyDamage(int damage){
		health -= damage;
		damageRec = "<b>" + damage + "</b>";
	}

	void OnGUI(){
		if (attackRec) {
				GUI.color = Color.white;
				GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2, 50, 50), damageRec);
		}
	}
}
