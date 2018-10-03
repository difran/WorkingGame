using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemSupport : MonoBehaviour
{
    Rigidbody2D myRigibody;
    ItemDrag itemDrag;
    List<Collider2D> collidedObjects = new List<Collider2D>();
    bool inTheAir = false;
    bool fitsInArea = false;

    void Start ()
    {
        Init();
    }

    public void Init()
    {
        itemDrag = gameObject.GetComponentInParent<ItemDrag>();
        myRigibody = GetComponentInParent<Rigidbody2D>();
    }

    public void OnItemDropped()
    {
        inTheAir = true;
        foreach (Collider2D collider in collidedObjects)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("StandingArea"))
            {
                Standing(collider.transform.parent);
                break;
            }

            if (collider.gameObject.layer == LayerMask.NameToLayer("InteractionArea"))
            {
                collider.gameObject.GetComponent<InteractiveObject>().ActiveInteraction(itemDrag.gameObject);
                break;
            }
        }
        if (inTheAir)
        {
            myRigibody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void Standing(Transform newParent)
    {
      //  print("PASA STANDING");
        inTheAir = false;
        myRigibody.bodyType = RigidbodyType2D.Kinematic;
        myRigibody.velocity = Vector2.zero;
        transform.parent.transform.SetParent(newParent);
        itemDrag.SetDepthPosition();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (!collidedObjects.Contains(collider))
        {
            collidedObjects.Add(collider);
        }

        
        if (collider.gameObject.layer == LayerMask.NameToLayer("StandingArea") && inTheAir)
        {
           Standing(collider.transform.parent);
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("CheckSizeArea"))
        {
            fitsInArea = CheckIfFits(collider.gameObject);
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("StandingAreaBySize") && fitsInArea)
        {
            Standing(collider.transform.parent);
        }
        
    }

    bool CheckIfFits(GameObject go)
    {
        SpriteRenderer a = go.GetComponent<SpriteRenderer>();
        Vector3 scale = go.transform.localScale;

        float areaX = a.sprite.bounds.size.x * scale.x;
        float areaY = a.sprite.bounds.size.y * scale.y;

        SpriteRenderer b = itemDrag.gameObject.GetComponent<SpriteRenderer>();
        Vector3 scaleB = itemDrag.gameObject.transform.localScale;

        float itemX = b.sprite.bounds.size.x * scaleB.x;
        float itemY = b.sprite.bounds.size.y * scaleB.y;

        return itemX < areaX && itemY < areaY;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (collidedObjects.Contains(col))
        {
            collidedObjects.Remove(col);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        OnTriggerEnter2D(col);
    }
}
