using UnityEngine;
using System.Collections;

//classe per gestire i movimenti della telecamera
public class Move : MonoBehaviour {

	Vector3 initPos; 
	Transform player;
	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player").transform;
		initPos = transform.position - player.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Translate(Vector3.back / 30, Space.Self);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Translate(Vector3.forward / 30, Space.Self);
		}
	}
}
