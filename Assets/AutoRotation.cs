using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour {
    [SerializeField]
    private Vector3 rotation;
    [SerializeField]
    private float speed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(rotation * speed * Time.deltaTime);
	}
}
