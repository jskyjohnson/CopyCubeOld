using UnityEngine;
using System.Collections;

public class PylonGun : MonoBehaviour {
	public Transform target;
	
	void Update () 
	{
		Vector3 relativePos = (target.position + new Vector3(0f, 0f, 0)) - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		
		Quaternion current = transform.localRotation;
		
		transform.localRotation = Quaternion.Slerp(current, rotation, 1f);
		transform.Translate(0, 0, Time.deltaTime);
	}
}