using UnityEngine;
using System.Collections;
//using UnityEditor;

public class Infetto : MonoBehaviour {

	int health;
	int maxHealth;
	GameObject player;
	Animator anim;
	bool isInCombat;
	bool isAttacking;
	bool backHome;
	bool dead;
	float attackTime;
	float attackAnimationDuration;
	float attackSpeed;
	bool attackRec;
	string damageRec;
	Vector3 initialPos;
	GameObject sphere;
	public Text_Manager TM;
	int level;
	int ID = 0;
	// Use this for initialization
	void Start () {
		attackAnimationDuration = 0.79F;
		attackSpeed = 1.5F;
		maxHealth = 150;
		health = maxHealth;
		player = GameObject.FindGameObjectsWithTag("Player")[0];
		anim = GetComponent<Animator>();
		initialPos = transform.position;
		damageRec = "";
		attackTime = attackAnimationDuration;
		isInCombat = false;
		isAttacking = false;
		backHome = false;
		dead = false;
		attackRec = false;
		sphere = transform.GetChild (2).gameObject;
		Component[] comp = sphere.GetComponents <Renderer>();
		foreach (Renderer r in comp){
			r.enabled = false;
			Debug.Log(r.name + " disabilitato");
		}
		sphere.GetComponent<Light> ().enabled = false;

        
	}
	public void setId(int id){
		ID = id;
	}
	public void setLevel(int lev){
		level = lev;
	}
	public int getLevel(){
		return level;
	}
	public int getId(){
		return ID;
	}
	public int getHealth(){
		return health;
	}
	public int getMaxHealth(){
		return maxHealth;
	}
	public void startParticle(){
		sphere.renderer.enabled = true;
		Component[] comp = sphere.GetComponents <Renderer>();
		foreach (Renderer r in comp){
			r.enabled = true;
			Debug.Log(r.name + " abilitato");
		}
		sphere.GetComponent<Light> ().enabled = true;
	}
	public void stopParticle(){
		sphere.renderer.enabled = false;
		Component[] comp = sphere.GetComponents <Renderer>();
		foreach (Renderer r in comp){
			r.enabled = false;
			Debug.Log(r.name + " disabilitato");
		}
		sphere.GetComponent<Light> ().enabled = false;
	}
	// Update is called once per frame
	void Update () {
        
		if (!dead) {
			Vector3 posToFace;

			float distanceFromPlayer = Vector3.Distance (transform.position, player.transform.position);
			float distanceFromHome = Vector3.Distance (transform.position, initialPos);
			if (distanceFromPlayer <= 8 && distanceFromPlayer >= 2 && !isInCombat && !backHome) {
					isInCombat = true;
					anim.SetBool ("run", true);


			}
			//-------setta il target del player



			//------ posiziona il mob verso la direzione del player
			if (isInCombat) {
					posToFace = player.transform.position;
			} else {
					posToFace = initialPos;
			}
			facePosition (posToFace);
			//-------------------------------------------------------

			// se il mob è in combat e la distanza è maggiore o uguale a 2 si muove verso il player e non attacca
			if (isInCombat && distanceFromPlayer >= 2) {
					transform.position = Vector3.MoveTowards (transform.position, player.transform.position, 0.05F);
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
					if (isAttacking) {
							AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo (0);
							float duration = 0.79F;
							float curSpeed = anim.speed;
							anim.speed = duration / attackSpeed;
							AnimationInfo[] aniInfo = anim.GetCurrentAnimationClipState (0);
							AnimationClip clip = aniInfo [0].clip;
	
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
					transform.position = Vector3.MoveTowards (transform.position, initialPos, 0.05F);
					if (distanceFromHome <= 1) {
							backHome = false;
							anim.SetBool ("run", false);
							health = maxHealth;
					}
	
			}

			//se il bool è a true attacco il player
			if (isAttacking) {
					attackPlayer ();
			} else {
					anim.speed = 1;
			}
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
	public bool isDead(){
		return dead;
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
		if (health <= 0) {
			dead = true;
			player.SendMessage("setExp", level * 15);
		}
		damageRec = "<b>" + damage + "</b>";
        Debug.Log("Danno player to infetto = " + damage);
	}

	//collisione del proiettile con l'infetto
	void OnCollisionEnter (Collision collision)
	{

		Collider ammo = collision.collider;
		Debug.Log (ammo.gameObject.tag);
		if (ammo.gameObject.tag == "Proiettile")
				applyDamage (10);
	}
}
