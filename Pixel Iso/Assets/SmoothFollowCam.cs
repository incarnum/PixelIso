using UnityEngine;
using System.Collections;


//Desired display, Damp Time: and Target(Focus):
public class SmoothFollowCam : MonoBehaviour {

	//damper on camera can be removed, but I think it's dope.
	public float dampTime = 0.1f;
	//sets velocity
	private Vector3 velocity = Vector3.zero;
	//choose what to transform. In this case, target.
	public Transform Target;

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Target)
		{
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(Target.position);
			Vector3 delta = Target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

	}
}