using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AnimationRotationRays : MonoBehaviour {

    public Vector3 velocityRotation;
	// Update is called once per frame
	void Update ()
    {
        this.transform.Rotate(velocityRotation * Time.deltaTime);	
	}
}
