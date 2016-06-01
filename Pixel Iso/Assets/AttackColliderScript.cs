using UnityEngine;
using System.Collections;

public class AttackColliderScript : MonoBehaviour {
	public float damage;
	public GameObject creator;

	// Use this for initialization
	void Start ()
	{
		Destroy(gameObject, .3f);
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject!= creator && other.gameObject.tag == "Goblin")
		{
			//creator.GetComponent<Attack>().target = other.gameObject;

			//creator.GetComponent<Attack>().attackOnUpdate = true;

			other.GetComponent<health> ().deductHealth (damage);
			other.GetComponent<health> ().lastHitBy = creator;
			Destroy (gameObject);
		}
	}
}
