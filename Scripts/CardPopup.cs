using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardPopup : MonoBehaviour
{
    [SerializeField]
    Image animal;
    [SerializeField]
    Transform bubblesColor;
    [SerializeField]
    Text animalName;
    [SerializeField]
    Text description;
    [SerializeField]
    Text place;
    [SerializeField]
    GameObject rays;
    [SerializeField]
    Image buttonClose;

    [SerializeField]
    AudioSource audioSurce;


    public void OnSelected(Card card)
	{

        this.gameObject.SetActive(true);
      
        animal.sprite = card.AnimalImage.sprite;
        animal.color = card.AnimalImage.color;

        animalName.text = card.Name;
		description.text = card.Description;
		place.text = card.Places;
        GetComponent<PopUp>().Activate();
        rays.SetActive(card.IsActive);

        if (card.IsActive)
        {
            audioSurce.Play();
            description.enabled = true;
            place.enabled = false;
        }
        else
        {
            description.enabled = false;
            place.enabled = true;
        }

        Analytics.instance.PetShow(card.Name, card.IsActive);

        // Set colors of bubbles
        Color color = card.CardColor;
        bubblesColor.GetComponent<Image>().color = color;
        buttonClose.color = color;
        foreach (RawImage image in bubblesColor.GetComponentsInChildren<RawImage>())
        {
            image.color = color;
        }
	}
}
