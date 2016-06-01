using UnityEngine;
using System.Collections;

public class enemyAttack : MonoBehaviour {
	public float nextAttackTime;
	public float swingTime;
	public GameObject prefab;
	private Transform myTransform;
	public GameObject target;
	public float damage;
	private Animator anim;
	private GameObject enemy;
	private Rigidbody2D rb;
	public float speed;
	public float aggroRange;
	public float deAggroRange;
	private bool chasing;
	private float facing;
	private GameObject hitBox;
	private bool inCombat;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		myTransform = GetComponent<Transform> ();
		nextAttackTime = Mathf.Infinity;
		swingTime = Mathf.Infinity;
		enemy = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector3.Distance (transform.position, enemy.GetComponent<Transform> ().position) <= aggroRange) {
			chasing = true;
			EnterCombat ();
		}
		if (Vector3.Distance (transform.position, enemy.GetComponent<Transform> ().position) <= 1f) {
			chasing = false;
		}
		if (Vector3.Distance (transform.position, enemy.GetComponent<Transform> ().position) >= deAggroRange) {
			chasing = false;
			ExitCombat ();
		}

			
		if (chasing == true) {
			Chase ();
		}

		if (nextAttackTime <= Time.time) {
			swingTime = Time.time + .5f;
			anim.SetTrigger("Attack");
			nextAttackTime = Mathf.Infinity;
		}
		if (swingTime <= Time.time) {
			Swing ();
		}
	
		if (target != null) {
			target.GetComponent<health> ().deductHealth (damage);
			target = null;
		}
	}

	void EnterCombat(){
		if (inCombat == false) {
			nextAttackTime = Time.time + 1.5f;
			anim.SetTrigger ("Charging");
			inCombat = true;
		}
	}

	void ExitCombat(){
		if (inCombat == true) {
			nextAttackTime = Mathf.Infinity;
			swingTime = Mathf.Infinity;
			inCombat = false;
		}
	}

	void Chase(){
		var heading = enemy.GetComponent<Transform> ().position - myTransform.position;
		var distance = heading.magnitude;
		var direction = heading / distance;

		rb.MovePosition(myTransform.position + direction * speed * Time.deltaTime);
		//^whether this creature moves to follow the enemy

		if (direction.x < .3 && direction.x > -.3 && direction.y > .3) {
			anim.SetFloat ("FacingX", 0);
			anim.SetFloat("FacingY", 1);
			facing = 1;
		} else if (direction.x > .3 && direction.y > .3) {
			anim.SetFloat ("FacingX", 1);
			anim.SetFloat("FacingY", 1);
			facing = 2;
		} else if (direction.x > .3 && direction.y < .3  && direction.y > -.3) {
			anim.SetFloat ("FacingX", 1);
			anim.SetFloat("FacingY", 0);
			facing = 3;
		} else if (direction.x > .3 && direction.y < -.3) {
			anim.SetFloat ("FacingX", 1);
			anim.SetFloat("FacingY", -1);
			facing = 4;
		} else if (direction.x < .3 && direction.x > -.3 && direction.y < -.3) {
			anim.SetFloat ("FacingX", 0);
			anim.SetFloat("FacingY", -1);
			facing = 5;
		} else if (direction.x < -.3 && direction.y < -.3) {
			anim.SetFloat ("FacingX", -1);
			anim.SetFloat("FacingY", -1);
			facing = 6;
		} else if (direction.x < -.3 && direction.y < .3 && direction.y > -.3) {
			anim.SetFloat ("FacingX", -1);
			anim.SetFloat("FacingY", 0);
			facing = 7;
		} else if (direction.x < -.3 && direction.y > .3) {
			anim.SetFloat ("FacingX", -1);
			anim.SetFloat("FacingY", 1);
			facing = 8;
		}
	}

	void Swing(){
		if (facing == 1 || facing == 0) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (0, 1, 0), Quaternion.identity) as GameObject;
		} else if (facing == 2) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (.7f, .7f, 0), Quaternion.identity) as GameObject;
		} else if (facing == 3) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (1, 0, 0), Quaternion.identity) as GameObject;
		} else if (facing == 4) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (.7f, -.7f, 0), Quaternion.identity) as GameObject;
		} else if (facing == 5) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (0, -1, 0), Quaternion.identity) as GameObject;
		} else if (facing == 6) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (-.7f, -.7f, 0), Quaternion.identity) as GameObject;
		} else if (facing == 7) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (-1, 0, 0), Quaternion.identity) as GameObject;
		} else if (facing == 8) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (-.7f, .7f, 0), Quaternion.identity) as GameObject;
		}

		hitBox.GetComponent<AttackColliderScriptGoblin> ().creator = gameObject;

		nextAttackTime = Time.time + 1.5f;
		anim.SetTrigger("Charging");
		swingTime = Mathf.Infinity;
	}



	//public void noTarget() {
	//}
}
