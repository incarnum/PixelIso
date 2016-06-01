using UnityEngine;
using System.Collections;

public class health : MonoBehaviour {

	public float currentHealth;
	public float maxHealth;
	public float regenPause;
	public float regenRate;
	private float nextRegenTime;
	public GameObject healthbar;
	public GameObject lastHitBy;
	public float XP;


	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		healthbar.transform.localScale = new Vector2( currentHealth / 75f, healthbar.transform.localScale.y);
		//healthbar.transform.position = new Vector2 (healthbar.transform.position.x - currentHealth / 150f, healthbar.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (nextRegenTime <= Time.time && currentHealth < maxHealth) {
			Regen ();
		}
			
	}

	public void deductHealth(float dmg)
	{
		currentHealth -= dmg;
		healthbar.transform.localScale = new Vector2( currentHealth / 75f, healthbar.transform.localScale.y);
		nextRegenTime = Time.time + regenPause;
		if (currentHealth <= 0f) {
			if (lastHitBy != null)
			lastHitBy.GetComponent<CombatLevel>().GainXP(XP);
			Destroy (gameObject);
		}
	}
	void Regen(){
		currentHealth += regenRate * Time.deltaTime;
		if (currentHealth > maxHealth)
			currentHealth = maxHealth;
		healthbar.transform.localScale = new Vector2( currentHealth / 75f, healthbar.transform.localScale.y);
		//healthbar.transform.position = new Vector2 (healthbar.transform.position.x - currentHealth / 150f, healthbar.transform.position.y);
	}
}
