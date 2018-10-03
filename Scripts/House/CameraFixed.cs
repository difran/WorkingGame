using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixed : CameraDrag
{
    [Header("This is total map")]
    public Vector3 position;
	public float size;
	Camera cam;
	void Awake()
	{
		cam = Camera.main;
	}

	void OnEnable()
    {
        cam.orthographicSize = size;
		cam.transform.position = position;
    }
}
