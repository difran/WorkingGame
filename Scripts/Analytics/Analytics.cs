using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class Analytics : MonoBehaviour {

    public static Analytics instance;

    [SerializeField]
    GameObject cardGrid;

    string set = "01";
    string page = "01";

    int CountPetHave()
    {
        int countPets = 0;
        Card[] cards = cardGrid.transform.GetComponentsInChildren<Card>();
        foreach (Card card in cards)
        {
            if (card.IsActive)
                countPets++;
        }
        return countPets;
    }

    List<string> NewPets()
    {
        List<string> newPets = new List<string>();
        Card[] cards = cardGrid.transform.GetComponentsInChildren<Card>();
        foreach (Card card in cards)
        {
            if (card.IsNew)
                newPets.Add(card.Name);
        }
        return newPets;
    }

    // Use this for initialization
    void Awake ()
    {
        instance = this;
    }


    public void PetsCountShow()
    {
        int petsCount = CountPetHave();
        GameAnalytics.NewDesignEvent("colection:set_" + set + ":page_" + page + ":show_list:" + petsCount.ToString());
    }

    public void PetShow(string namePet, bool havePet)
    {
        if (havePet)
        {
            GameAnalytics.NewDesignEvent("colection:set_" + set + ":page_" + page + ":select:" + namePet + "_Has_true");
          //  print("colection:set_" + set + ":page_" + page + ":select:" + namePet + "_Has_true");
        }
        else
        {
            GameAnalytics.NewDesignEvent("colection:set_" + set + ":page_" + page + ":select:" + namePet + "_Has_false");
          //  print("colection:set_" + set + ":page_" + page + ":select:" + namePet + "_Has_false");
        }
    }


    public void PetNew()
    {
        List<string> news = NewPets();
        foreach (var name in news)
        {
            GameAnalytics.NewDesignEvent("colection:set_" + set + ":page_" + page + ":show_new:" + name);
        }
    }

    public void ShowInfo()
    {
        GameAnalytics.NewDesignEvent("colection:show_info");
    }

    public void ShowError()
    {
        GameAnalytics.NewDesignEvent("colection:show_error");
    }
 

}
