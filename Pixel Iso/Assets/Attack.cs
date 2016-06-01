using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Attack : MonoBehaviour {


	public GameObject prefab;
	//the prefab for the hitbox that gets instantiated

	public float damage;
	//what will get multiplied by the charge to give the amount of actual damage dealt

	public float fullHitBonus;


	public GameObject target;
	//when the hitbox gets a collision, it sends the GO reference of what it hits to this

	private GameObject hitBox;
	//the GO reference for the hitbox once its instantiated, it destroys itself quickly

	public float charge;
	//a number from 0 to 1 which builds up in the chargeUp function

	private bool charging;
	//whether or not chargeUp runs every tick

	private bool waitingForNextSwing;
	//whether we're still in the window after the button is released when the next button down will unleash the attack

	public Slider slider;
	//the UI element which displays the current charge

	public Image image;

	Transform myTransform;
	//the transform of the player, used as reference for the position of the instantiated hitbox

	private float chargePauseTime;
	//the global time at which it stops waiting for the attack to be executed, and the charge is reset to 0

	private PlayerController playerController;

	public float chargeSpeed;
	//how quickly the charge bar fills up.  1 means 1 second.




	void Start () {
		myTransform = GetComponent<Transform> ();
		playerController = GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckifAttacking ();
		if (chargePauseTime <= Time.time && waitingForNextSwing == true) {
			waitingForNextSwing = false;
			charge = 0;
			slider.value = charge;
			image.color = Color.black;
		}

		if (charging == true){
			chargeUp();
		}

		//if (target != null) {
//			if (charge >= 1) {
//				target.GetComponent<health> ().deductHealth (charge * damage + fullHitBonus);
//				image.color = Color.black;
//			} else
//			target.GetComponent<health> ().deductHealth (charge * damage);
//			target = null;
//			charge = 0f;
//			slider.value = charge;
		//}
	}

	void CheckifAttacking()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			charging = true;
			chargePauseTime = Mathf.Infinity;
			playerController.speed = 2f;
			if (waitingForNextSwing == true) {
				AttackFunction ();
			}
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			playerController.speed = 6f;
			charging = false;
			waitingForNextSwing = true;
			chargePauseTime = Time.time + 1f;
		}



	}

	void AttackFunction()
	{
		
		//charge = 0;
		//slider.value = charge;

		if (playerController.H == 1 && playerController.V == 0) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (1, 0, 0), Quaternion.identity) as GameObject;
		} else if (playerController.H == -1 && playerController.V == 0) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (-1, 0, 0), Quaternion.identity) as GameObject;
		} else if (playerController.H == 0 && playerController.V == 1) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (0, 1, 0), Quaternion.identity) as GameObject;
		} else if (playerController.H == 0 && playerController.V == -1) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (0, -1, 0), Quaternion.identity) as GameObject;
		} else if (playerController.H == 1 && playerController.V == 1) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (.7f, .7f, 0), Quaternion.identity) as GameObject;
		} else if (playerController.H == 1 && playerController.V == -1) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (.7f, -.7f, 0), Quaternion.identity) as GameObject;
		} else if (playerController.H == -1 && playerController.V == -1) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (-.7f, -.7f, 0), Quaternion.identity) as GameObject;
		} else if (playerController.H == -1 && playerController.V == 1) {
			hitBox = Instantiate (prefab, myTransform.position + new Vector3 (-.7f, .7f, 0), Quaternion.identity) as GameObject;
		}


		hitBox.GetComponent<AttackColliderScript> ().creator = gameObject;
		if (charge >= 1) {
			hitBox.GetComponent<AttackColliderScript> ().damage = charge * damage + fullHitBonus;
			image.color = Color.black;
		} else
			hitBox.GetComponent<AttackColliderScript> ().damage = charge * damage;

		waitingForNextSwing = false;
		charge = 0;
		slider.value = charge;
		image.color = Color.black;
	}

	void chargeUp()
	{
		if (charge <= 1) {
			charge += chargeSpeed * Time.deltaTime;
			slider.value = charge;
		} else {image.color = Color.white;}

	}

//	public void noTarget()
//	{
//		waitingForNextSwing = false;
//		charge = 0;
//		slider.value = charge;
//		image.color = Color.black;
//	}
}

//you need a system where theres a trigger when no target is hit so it can cancel the charge