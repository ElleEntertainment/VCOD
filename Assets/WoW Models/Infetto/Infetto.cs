using UnityEngine;
using System.Collections;
using UnityEditor;

public class Infetto : MonoBehaviour {

	int health;
	GameObject player;
	Animator anim;
	bool isInCombat = false;
	bool isAttacking = false;
	bool backHome = false;
	float attackTime;
	float attackAnimationDuration = 0.79F;
	float attackSpeed = 1.5F;
	bool attackRec = false;
	string damageRec;
	Vector3 initialPos;
	int ID = 0;
	// Use this for initialization
	void Start () {
		health = 150;
		player = GameObject.FindGameObjectsWithTag("Player")[0];
		anim = GetComponent<Animator>();
		initialPos = transform.position;
		damageRec = "";
		attackTime = attackAnimationDuration;
	}
	public void setId(int id){
		ID = id;
	}
	public int getId(){
		return ID;
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
			attackTime -= Time.deltaTime;
		}
        //------------------------------------------
        //Se il mob è in combat e la distanza è minore di 2 e non sta attaccando allora il mob comincia a attaccare
		if (isInCombat && distanceFromPlayer < 2 && !isAttacking) {
			isAttacking = true;
		}
		//-----------------------------------------------
		//questa non l'ho capita anche perchè non ho mai visto 2 booleani confrontati (sono 2 bool no?)
		//isAttacking è un booleano dell'infetto che viene messo a true se sta attaccando
		//anim.getBool o anim.setBool sono i metodi che prendono o settano un booleano all'animazione
		//in sostanza qui viene attivata o disattivata l'animazione in base al valore di isAttacking
		if (anim.GetBool ("attack") != isAttacking) {
			anim.SetBool ("attack", isAttacking);
			if(isAttacking){
				AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
				float duration = 0.79F;
				float curSpeed = anim.speed;
				anim.speed = duration / attackSpeed;
				AnimationInfo[] aniInfo = anim.GetCurrentAnimationClipState(0);
				AnimationClip clip = aniInfo[0].clip;
				
				Debug.Log ("La velocità dell'animazione è " + anim.speed);
			}
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
		//backHome è true quando l'infetto ha disingaggiato il player, questo controllo serve per far
		//tornare alla posizione iniziale l'infetto.
		if (backHome && distanceFromHome > 0) {
			transform.position = Vector3.MoveTowards(transform.position, initialPos, 0.05F);
			if(distanceFromHome <= 1){
				backHome = false;
				anim.SetBool("run", false);
			}
				
		}

        //se il bool è a true attacco il player
		if (isAttacking) {
			attackPlayer ();
		}
		else{
			anim.speed = 1;
		}


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
		attackTime -= Time.deltaTime;
		if (attackTime  < 0.0F) {
			int dam = Mathf.RoundToInt(Random.Range(5, 15));
			Debug.Log("Player Attacked da " + ID);
			player.SendMessage("applyDamage", dam);
			attackTime = attackSpeed;
		} 
	}

	void applyDamage(int damage){
		health -= damage;
		damageRec = "<b>" + damage + "</b>";
	}

	/* Va portato sulla classe textManager
	void OnGUI(){
		if (attackRec) {
				GUI.color = Color.white;
				GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 2, 50, 50), damageRec);
		}
	}
	*/
}
