using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraDrag : MonoBehaviour
{

	[HideInInspector]
	public Vector3 ResetCamera;
	[HideInInspector]
    public Vector3 Origin;
	[HideInInspector]
    public Vector3 Diference;
	[HideInInspector]
    public bool Drag = false;
	[HideInInspector]
	public float DefaultSize;
    
    void Awake()
    {
        ResetCamera = Camera.main.transform.position;
		DefaultSize = Camera.main.orthographicSize;
    }

    protected void OnEnable()
    {
        Camera.main.transform.position = ResetCamera;
    }

    public virtual void ItemDraggedAt(float xRatio, float yRatio) {}

	public void UpdateDrag()
	{
        if (Input.GetMouseButtonDown(0))
        {
            if (Drag == false && CanDrag())
            {
                Drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Drag = false;
        }
		if (Input.GetMouseButton(0))
        {
            Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
        }
	}

    bool CanDrag()
    {
        return !EventSystem.current.IsPointerOverGameObject();
    }
}