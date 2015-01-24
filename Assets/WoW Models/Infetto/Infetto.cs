﻿using UnityEngine;
using System.Collections;

public class Infetto : MonoBehaviour {

	int health;
	GameObject player;
	Animator anim;
	bool isInCombat = false;
	bool isAttacking = false;
	bool backHome = false;
	Vector3 initialPos;
	// Use this for initialization
	void Start () {
		health = 150;
		player = GameObject.FindGameObjectsWithTag("Player")[0];
		anim = GetComponent<Animator>();
		initialPos = transform.position;
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

		if (isInCombat) {
			posToFace = player.transform.position;
		} else {
			posToFace = initialPos;
		}
		facePosition (posToFace);

		if (isInCombat && distanceFromPlayer >= 2) {
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.05F);
			isAttacking = false;
		}
		if (anim.GetBool ("attack") != isAttacking)
						anim.SetBool ("attack", isAttacking);
		if (isInCombat && distanceFromPlayer < 2 && !isAttacking) {
			isAttacking = true;
		}
		if (isInCombat && distanceFromHome >= 25) {
			isInCombat = false;
			backHome = true;
			isAttacking = false;
		}
		if (backHome && distanceFromHome > 0) {
			transform.position = Vector3.MoveTowards(transform.position, initialPos, 0.05F);
			if(distanceFromHome <= 1){
				backHome = false;
				anim.SetBool("run", false);
			}
				
		}


	}

	void facePosition(Vector3 pos){
			if (isInCombat) {
			Vector3 target = pos - transform.position;
			float angle = Vector3.Angle(target, transform.forward);
			Vector3 targetDir = pos - transform.position;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 30, 1.0F);
			transform.rotation = Quaternion.LookRotation(newDir);
		}
	}

	void attackPlayer(){

	}
}