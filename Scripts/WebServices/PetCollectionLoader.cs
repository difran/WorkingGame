using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace WebServices 
{
    /*
        Names and descriptions of Fluffy Flaffys characters can be found in https://docs.google.com/document/d/1KBzlOOsIuZidB4PgRcaisO6L1b0U-25bTD7s5q16ztc/
     */

    [RequireComponent(typeof(WebServiceUserData))]
    public class PetCollectionLoader : MonoBehaviour
    {
        // Turn on offlineTest to deactivate server connection and test the collection offline
        [SerializeField]
        bool offlineTest;
        [SerializeField]
        int daysOfNewness = 3;
        [SerializeField]
        CardsGrid cardsGrid;
        WebServiceUserData webServiceUserData;
        [SerializeField]
        GameObject cardGrid;

        public List<Card> cardsList = new List<Card>();

        void Start()
        {
            webServiceUserData = GetComponent<WebServiceUserData>();
            if (!(offlineTest && Application.isEditor))
            {
                cardsGrid.SetAllActive(false);
                InitConnection();
            }
        }

        public void InitConnection()
        {
            webServiceUserData.Init(OnCollectionRecieved);
        }

        void OnCollectionRecieved()
        {
            Dictionary<string, Card> cardsIndexed = cardsGrid.CardsIndexed;
            Dictionary<string, Breed> breedsIndexed = webServiceUserData.BreedsIndexed;
            foreach (KeyValuePair<string, Card> item in cardsIndexed)
            {
                Breed petServerData;
                if (breedsIndexed.TryGetValue(item.Key, out petServerData))
                {
                    Card card = item.Value;
                    card.Name = petServerData.breedName;
                    card.Description = petServerData.description.Replace("\\n", System.Environment.NewLine);
                    card.Places = petServerData.places.Replace("\\n", System.Environment.NewLine);
                    card.IsNew = IsNew(petServerData.capturedAt);
                    card.IsActive = petServerData.owned;
                   
                }
                else
                {
                    Debug.LogError("Fluffy Error: server didn't return any fucking infomation about "+item.Key+". Please add that record on database.");
                }
            }

            Analytics.instance.PetsCountShow();
            Analytics.instance.PetNew();

            EffectCards e = cardGrid.GetComponent<EffectCards>();
            e.OffEffectCards();
        }

        bool IsNew(DateTime capturedAt)
        {
            // Get a DateTime three days older from now
            DateTime threshold = DateTime.Now.Subtract( new TimeSpan(daysOfNewness, 0, 0, 0) );
            // Check if captured date is earlier than the date generated before
            return capturedAt > threshold;
        }

    }
}