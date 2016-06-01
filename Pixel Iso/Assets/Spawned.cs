using UnityEngine;
using System.Collections;

public class Spawned : MonoBehaviour {
	public GameObject creator;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy(){
		creator.GetComponent<GoblinSpawner> ().currentGoblinCount -= 1;
	}
}
