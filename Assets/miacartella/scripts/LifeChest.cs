using UnityEngine;
using System.Collections;

public class LifeChest : MonoBehaviour {
    int chestHit = 0;
    GameObject p;

	// Use this for initialization
	void Start () {
        p = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (chestHit == 3)
        {
            chestHit = 0;
            p.SendMessage("updateHealth", 10);
            Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        Collider oggetto = collision.collider;
        if (oggetto.gameObject.tag == "Proiettile")
            chestHit++;
    }
}
