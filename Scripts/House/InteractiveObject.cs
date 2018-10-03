using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    public GameObject posFinal;
    [SerializeField]
    bool oneAtATime = false;
    public UnityEvent OnInit;
    public UnityEvent OnFinish;
    public UnityEvent OnReset;
    GameObject item;
    int cantItems = 0;



    void Start()
    {
        OnReset.AddListener(OnResetActions);
    }

    public void ActiveInteraction(GameObject objectTrigger)
    {
        if (oneAtATime == true && cantItems > 0) return;
        item = objectTrigger;
        OnInit.Invoke();
    }

    public void InitBed()
    {
        print("Init");

        if (item.tag == "HouseCharacter")
        {
            ItemDrag itemDrag = item.GetComponent<ItemDrag>();
            itemDrag.OnInitDrag.AddListener(OnReset.Invoke);
            item.transform.DOMove(posFinal.transform.position, 0.5f);
            item.transform.DORotate(new Vector3(0, 0, 50),0.5f).OnComplete(OnFinish.Invoke);
            cantItems++;
        }
        if (item.tag == "HouseGlassesItem")
        {
            //this.gameObject.SetActive(false);


            ItemDrag itemDrag = item.GetComponent<ItemDrag>();
            itemDrag.WearCloth();

            itemDrag.OnInitDrag.AddListener(OnReset.Invoke);

            item.transform.DOMove(posFinal.transform.position, 0.5f);
            item.transform.DOPunchScale(new Vector2(1, 1), 0.5f);
            itemDrag.ChangeParent(gameObject.transform.parent);

            ItemDrag parent = item.transform.parent.GetComponent<ItemDrag>();

            parent.HeIsWearingClothes(itemDrag);
            // item.transform.DORotate(new Vector3(0, 0, 50), 0.5f).OnComplete(OnFinish.Invoke);
            cantItems++;
        }

    }

    void OnResetActions()
    {
        print("on reset action");

        if (item.tag == "HouseGlassesItem")
        {
            ItemDrag itemDrag = item.GetComponent<ItemDrag>();
            itemDrag.TakeOffCloth();
        }
            cantItems--;
    }
}
