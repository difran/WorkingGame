using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ItemDrag : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f,1f)]
    float moveSmooth = 0.2f;
    [Space(15)]
    public GameObject itemSuportObject;
    ItemSupport itemSuport;
    [Space(15)]
    Vector3 originalScale;
    [SerializeField]
    float DragScaleRatio = 1.1f;
    Quaternion originalRotation;
    Transform originalParent;
    CameraDrag[] camDrags;
    CameraDrag activeCamDrag;
    [HideInInspector]
    public UnityEvent OnInitDrag;
    Collider2D myCollider;

    public GameObject bubble;
    public GameObject bubbleParticle;
    Transform parentOnSelected;

    public List<ItemDrag> myCloth = new List<ItemDrag>();

    public void HeIsWearingClothes(ItemDrag wichone)
    {
        print("me pongo ropa");
        myCloth.Add(wichone);
    }

    public void RemoveClothes(ItemDrag wichone)
    {
        //myCloth.BinarySearch(wichone);
        myCloth.Remove(wichone);
        //hacer el remove para todas las prendas
    }


    void Start()
    {
        itemSuport = itemSuportObject.GetComponent<ItemSupport>();
        originalScale = gameObject.transform.localScale;
        originalRotation = transform.rotation;

        // originalParent = transform.parent;
        originalParent = GameObject.Find("House Container").transform;

        camDrags = Camera.main.GetComponents<CameraDrag>();
        myCollider = GetComponent<Collider2D>();
        parentOnSelected = GameObject.Find("House Container/Container Selected Object").transform;
    }

    void Reset()
    {
        gameObject.transform.localScale = originalScale;
        transform.rotation = originalRotation;
        OnInitDrag.Invoke();
        OnInitDrag.RemoveAllListeners();
    }


    void OnMouseDown()
    {
        if (bubble != null)
        {
            Destroy(bubble);
            Instantiate(bubbleParticle, this.transform.position, this.transform.rotation);
        }


        print("on mouse down");
        print(this.gameObject.name + " touch this ");

        // por ahora la ropa no va
        if (myCloth.Count > 0)
        {
            ItemDrag SomeCloth = GetWearing();

            if (SomeCloth != null)
            {
                print("encontro la prenda");
                SomeCloth.OnMouseDown();//aca se resetea la prenda
                RemoveClothes(SomeCloth);
                return;
            }

            //habria que apagar el mouse drag de momento
        }
        
        Reset();
        //este metodo lo hizo milton, es para cubrir objetos, despues lo vuelvo a hacer bien
       // itemSuport.Standing(originalParent);
        gameObject.transform.DOScale(originalScale * DragScaleRatio, 0.1f);

        OnSelectedObject();

        foreach (var camDrag in camDrags)
        {
            if (camDrag.isActiveAndEnabled)
            {
                activeCamDrag = camDrag; break;
            }
        }
    }


    ItemDrag GetWearing()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(worldPoint, Vector2.zero);

        Debug.Log( "get wearing");

        foreach (var item in hits)
        {
            foreach (var cloth in myCloth)
            {
                if (item.collider.gameObject.GetInstanceID() == cloth.gameObject.GetInstanceID())
                {
                    Debug.Log("comparando " + item.collider.gameObject.name + " " + cloth.gameObject.name);
                    return cloth;
                }
            }

            Debug.Log(item.collider.name + "raycast");
        }
        return null;
    }

    void OnMouseUp()
    {
     

        print(this.gameObject.name + " mouse up this ");

        OnSelectedObject(true);
        itemSuport.OnItemDropped();
        gameObject.transform.DOScale(originalScale, 0.1f);
    }
 
    void OnMouseDrag()
    {
        transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), moveSmooth);
        Vector3 mousePos = Input.mousePosition;
        float x = mousePos.x / Screen.width;
        float y = mousePos.y / Screen.height;
        activeCamDrag.ItemDraggedAt(x, y);

    }

    public void SetDepthPosition()
    {
        Vector3 minBound = myCollider.bounds.min;
        var position = transform.localPosition;
        position.z = (minBound.y + 100) / 20;
        transform.localPosition = position;
    }

    public void WearCloth()
    {
        itemSuportObject.SetActive(false);
        Rigidbody2D rigi = gameObject.GetComponent<Rigidbody2D>();
        Destroy(rigi);
    }

    //reset cloth
    public void TakeOffCloth()
    {
        this.gameObject.AddComponent<Rigidbody2D>();
        Rigidbody2D rigi = gameObject.GetComponent<Rigidbody2D>();
        rigi.freezeRotation = true;

        ItemSupport itemSupport = itemSuportObject.GetComponent<ItemSupport>();
        itemSupport.Init();
        itemSuportObject.SetActive(true);

    }

    public void ChangeParent(Transform currentParent)
    {
        gameObject.transform.SetParent(currentParent);
        originalParent = currentParent;
    }

    public void OnSelectedObject(bool back = false)
    {
        if (back)
        {
            gameObject.transform.SetParent(originalParent);
            return;
        }
        gameObject.transform.SetParent(parentOnSelected);
    }
}