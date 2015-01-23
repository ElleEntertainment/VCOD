using UnityEngine;
using System.Collections;

public class Controller_pg : MonoBehaviour {

	float speed = 1;
	Animator anim;
	Rigidbody rig;
	bool jump = false;
	bool falling = false;
	float YPos = 0;
	float jumpHeigth = 2;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rig = GetComponent<Rigidbody> ();
		jump = false;
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

		//Usiamo i tasti per muoverci
		if (Input.GetKey (KeyCode.W)) {
			//anim.SetFloat("direction", move);
			anim.SetBool("walk", true);
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
		if (Input.GetKeyUp (KeyCode.W)) {
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

}
