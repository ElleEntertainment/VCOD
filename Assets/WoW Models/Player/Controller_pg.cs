using UnityEngine;
using System.Collections;

public class Controller_pg : MonoBehaviour {

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
	string damageRec = "";
	bool attackRec = false;
	public GUISkin customSkin;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rig = GetComponent<Rigidbody> ();
		jump = false;
		run = true;
		health = 250;
	}
	
	// Update is called once per frame
	void Update () {
		float move = Input.GetAxis ("Horizontal");
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		//Sistema di Rotazione (non ce n'è bisogno per ora)
		if (Input.GetKey (KeyCode.LeftArrow)) 
			    transform.Rotate (Vector3.up, -1); //-1 sono i gradi di rotazione
		else
			if (Input.GetKey (KeyCode.RightArrow))
					transform.Rotate (Vector3.up, 1);
			
		// FINE SISTEMA DI ROTAZIONE
		if (Input.GetKeyDown (KeyCode.C)) {
			run = !run;
			anim.SetBool("canRun", run);
		}

		//Usiamo i tasti per muoverci
		if (Input.GetKey (KeyCode.W)) {
			//anim.SetFloat("direction", move);
			if(run){
				anim.SetBool("run", true);
				anim.SetBool("walk", false);
				transform.Translate (Vector3.left * speed / 15, Space.Self);
			}
			else{
				anim.SetBool("run", false);
				anim.SetBool("walk", true);
				transform.Translate (Vector3.left * speed / 30, Space.Self);
			}
			transform.Translate (Vector3.left * speed / 30, Space.Self); // diviso 30 per ridurre la velocità
		}
		if (Input.GetKey (KeyCode.S))
			transform.Translate (Vector3.right * speed / 30, Space.Self);
		if (Input.GetKey (KeyCode.A))
			transform.Translate (Vector3.back * speed / 30, Space.Self);
		if (Input.GetKey (KeyCode.D))
			transform.Translate (Vector3.forward * speed / 30, Space.Self);
		//Fine sistema di movimento semplice
		//Ci sono gli if senza gli else per dare la possibilità di muoversi in più direzioni in una volta
		if (Input.GetKeyUp (KeyCode.W)) { //quando si rilascia il tasto W il personaggio non deve più correre
			if(run)
				anim.SetBool("run", false);
			else
				anim.SetBool("walk", false);
		}
	



		}
	void FixedUpdate() {
		if (!jump && !falling) {
			if(Input.GetKeyDown(KeyCode.Space)){
				jump = true;
				YPos = transform.position.y;
			}
		}
		do_jump();
	}
	void do_jump(){
		if (jump && !falling) {
			if(YPos + jumpHeigth >= transform.position.y){
				Debug.Log("Jumping");
				rig.AddForce(Vector3.up * 200);
				falling = true;
			}
		}
	}
	void OnCollisionEnter(Collision collision) {
		if (falling) {
			Debug.Log("Hit the ground");
			falling = false;
			jump = false;
		}
	}

	void attackEnemy(){
		if (attackTime - Time.deltaTime <= 0) {
			Random.seed = (int)Time.time;
			Random.Range(5, 15);
			int dam = (int)Random.value;
			//target.SendMessage("applyDamage", dam);
			
		} else
			attackTime -= Time.deltaTime;
	}
	
	void applyDamage(int damage){
		health -= damage;
		damageRec = "<b>" + damage + "</b>";
		attackRec = true;
		Debug.Log ("Damage Received " + damage);
	}
	
	void OnGUI(){
		GUI.skin = customSkin;
		GUI.color = Color.black;
		GUI.Label(new Rect (0, 0, 50, 50), "" + health + "");
		if (attackRec) {
				GUI.color = Color.red;
				GUI.Label (new Rect (Screen.width / 2 - 20, Screen.height / 2, 50, 50), damageRec);
		}
	}

}
