using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFlanysMenu : MonoBehaviour {

    public GameObject flanysMenu;
    public List<FlannyObjectMenu> flanysInMenu;
    public List<FlannyInGameInfo> flanysCards;

    public bool debugMode;

    // Use this for initialization
    void Awake () {

        if (CardsInfoGetter.flannyInMenu == null) { Debug.Log("NO ESTA USANDO LOS FLANYS DE LA GRILLA"); return; }

        flanysCards = new List<FlannyInGameInfo>();
        flanysCards.AddRange(CardsInfoGetter.flannyInMenu);

        LoadAllFlanysInMenu();

        SetAllFlanysInMenu();
    }

    void LoadAllFlanysInMenu()
    {
        RectTransform[] rawList = flanysMenu.GetComponentsInChildren<RectTransform>();

        foreach (var item in rawList)
        {
            if (item.name == "Flanny")
            {
                FlannyObjectMenu f = item.GetComponent<FlannyObjectMenu>();
                flanysInMenu.Add(f);
            }

        }
    }

    void SetAllFlanysInMenu()
    {
        GameObject go = new GameObject();

        for (int i = 0; i < flanysInMenu.Count; i++)
        {
            if (i < flanysCards.Count)
            {
                flanysInMenu[i].flanny = flanysCards[i].flanny;
                flanysInMenu[i].color = flanysCards[i].color;
            }
            else
            {
                flanysInMenu[i].gameObject.SetActive(false);
                flanysInMenu[i].gameObject.transform.parent.SetParent(go.transform);
            }
        }

        Destroy(go);
    }
	
}
