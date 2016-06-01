using UnityEngine;
using System.Collections;

public class AttackColliderScriptGoblin : MonoBehaviour {
	public float damage;
	public GameObject creator;
	// Use this for initialization
	void Start ()
	{
		//creator = GameObject.Find ("Goblin");
		Destroy(gameObject, .03f);
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject!= creator && other.gameObject.tag == "Player")
		{
			creator.GetComponent<enemyAttack>().target = other.gameObject;

			//creator.GetComponent<Attack>().attackOnUpdate = true;
		}
	}

	//void OnDestroy(){
	//	creator.GetComponent<enemyAttack> ().noTarget ();
	//}


}
