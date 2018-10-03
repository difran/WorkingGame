using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGrid : MonoBehaviour
{
	[System.NonSerialized]
	public Dictionary<string, Card> CardsIndexed = new Dictionary<string, Card>();
	public CardPopup CardPopup;

	void Awake ()
	{
		Card[] cards = transform.GetComponentsInChildren<Card>();
		foreach (Card card in cards)
		{
			CardsIndexed.Add(card.PetID, card);
		}
    }

	public void SetAllActive (bool status)
	{
		foreach(KeyValuePair<string, Card> entry in CardsIndexed)
		{
			entry.Value.IsActive = status;
        }
	}


}
