using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatLevel : MonoBehaviour {
	private float experience;
	private float level;
	public Text textbox;
	// Use this for initialization
	void Start () {
		level = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GainXP(float xp){
		experience += xp;
		if (experience >= 100 * level) {
			experience -= 100 * level;
			level += 1;
			GetComponent<Attack> ().damage += 2;
			GetComponent<Attack> ().chargeSpeed += .2f;
			GetComponent<health> ().maxHealth += 10 * level;
		}
		textbox.text = "Level: " + level.ToString() + " | Experience: " + experience.ToString();
	}
}
