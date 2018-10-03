using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CardsInfoGetter : MonoBehaviour {

    public List<Card> cardList;

    public static List<FlannyInGameInfo> flannyInMenu;


    public void GetFlanys()
    {
        flannyInMenu = new List<FlannyInGameInfo>();

        foreach (var item in cardList)
        {
            if (item.IsActive)
            {
                print(item.PetID);

                FlannyInGameInfo f = item.gameObject.GetComponent<FlannyInGameInfo>();
                flannyInMenu.Add(f);
            }
               
        }
        
        SceneManager.LoadScene("House", LoadSceneMode.Single);
    }
}
