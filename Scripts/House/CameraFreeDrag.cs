using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFreeDrag : CameraDrag
{
	public float LimitLeft;
	public float LimitRight;
	public float LimitUp;
	public float LimitDown;

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
		position.x = Mathf.Max(LimitLeft, position.x);
		position.x = Mathf.Min(LimitRight, position.x);
		position.y = Mathf.Min(LimitUp, position.y);
		position.y = Mathf.Max(LimitDown, position.y);
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
		if (yRatio <  margin)
        {
            pos.y -= movement * (1 - yRatio / margin);
        }
        else if (yRatio > 1 - margin)
        {
            pos.y += movement * (1 - (1-yRatio) / margin);
        }
        Camera.main.transform.position = LimitPosition(pos);
    }

	new void OnEnable()
    {
		base.OnEnable();
        Camera.main.orthographicSize = DefaultSize;
    }
}
