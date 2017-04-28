using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	TargetJoint2D tj;
	GameObject player;

	// Use this for initialization
	void Start () 
	{
		tj = GetComponent<TargetJoint2D> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		tj.target = (Vector2)player.transform.position;
	}
}
