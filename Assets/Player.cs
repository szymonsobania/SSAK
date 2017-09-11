using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        transform.GetComponentInParent<PlayerMovement>().Collision();
        float speed = other.gameObject.GetComponentInParent<Ring>().rotationSpeed;
        GetComponentInParent<PlayerMovement>().RotationSpeed =
            other.gameObject.GetComponentInParent<Ring>().rotationSpeed;
    }
}
