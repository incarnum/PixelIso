using UnityEngine;
using System.Collections;

public class GoblinSpawner : MonoBehaviour {
	private GameObject Goblin;
	private Transform myTransform;
	private float nextGoblinTime;
	private bool makingGoblin;
	public GameObject prefab;
	public float currentGoblinCount;
	public float idealGoblinCount;
	public float spawnDelay;
	// Use this for initialization
	void Start () {
		myTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentGoblinCount < idealGoblinCount && makingGoblin == false) {
			nextGoblinTime = Time.time + spawnDelay;
			makingGoblin = true;
		}

		if (nextGoblinTime < Time.time && makingGoblin == true) {
			SpawnGoblin ();
			currentGoblinCount += 1;
			makingGoblin = false;
		}
	}

	void SpawnGoblin() {
		Goblin = Instantiate (prefab, myTransform.position, Quaternion.identity) as GameObject;
		Goblin.GetComponent<Spawned> ().creator = gameObject;
	}
}
