using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private PlayerMovement player;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 fixedOffset = offset + new Vector3(0, player.Scale.x *10, -player.Scale.x * 10);
        transform.position = Vector3.Lerp(transform.position, target.position + fixedOffset, Time.deltaTime);
	}
}
