using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoomDrag : CameraDrag
{

	public float limitLeft;
	public float limitRight;
    public Transform cameraPosition;
    public float size;

    void Awake()
    {
        ResetCamera = cameraPosition.position;
        DefaultSize = Camera.main.orthographicSize;
    }

    void LateUpdate()
    {
        UpdateDrag();
        if (Drag == true)
        {
			Vector3 newPosition = Origin - Diference;
            Camera.main.transform.position = LimitPosition(newPosition);
        }
    }

    Vector3 LimitPosition(Vector3 position)
	{
		position.y = ResetCamera.y;
        position.x = Mathf.Max(limitLeft, position.x);
        position.x = Mathf.Min(limitRight, position.x);
		return position;
	}

    public override void ItemDraggedAt(float xRatio, float yRatio)
    {
        Vector3 pos = Camera.main.transform.position;
        float margin = .1f;
        float movement = .1f;
        if (xRatio <  margin)
        {
			float depth = 
            pos.x -= movement * (1 - xRatio / margin);
        }
        else if (xRatio > 1 - margin)
        {
            pos.x += movement * (1 - (1-xRatio) / margin);
        }
        Camera.main.transform.position = LimitPosition(pos);
    }

    new void OnEnable()
    {
       // base.OnEnable();
        Camera.main.transform.position = cameraPosition.position;
        Camera.main.orthographicSize = size;
    }


    
}
