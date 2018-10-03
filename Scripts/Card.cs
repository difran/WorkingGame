using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
	public string PetID
	{
		get
		{
			return petID;
		}
	}
	public Image AnimalImage
	{
		get
		{
			return transform.Find("Animal").GetComponent<Image>();
		}
	}
	public Color CardColor
	{
		get
		{
			return GetComponent<Image> ().color;
		}
	}
	public bool IsActive
	{
		get
		{
			return isActive;
		}
		set
		{
			isActive = value;
			UpdateStatus();
		}
	}
	[SerializeField]
	bool isActive = true;
	public Color ActiveColor;
	public Color DeactiveColor;
	public bool IsNew = false;
	[SerializeField]
	string petID;
	public string Name;
	[TextArea]
	public string Description;
	[TextArea]
	public string Places;
	CardsGrid cardsGrid;

	void Start()
	{
		cardsGrid = transform.parent.GetComponent<CardsGrid>();
		
	}

	void UpdateStatus()
	{
		Image characterImage = AnimalImage;
		if (characterImage.sprite != null)
		{
			characterImage.color = isActive ?  ActiveColor : DeactiveColor;
		}

		Transform newness = transform.Find("Newness");
		if (newness)
		{
			if (isActive)
			{
				newness.gameObject.SetActive(IsNew);
			}
			else
			{
				newness.gameObject.SetActive(false);
			}
		}
	}

	void OnValidate()
	{
		UpdateStatus();
	}

	public void Click()
	{
		cardsGrid.CardPopup.OnSelected (this);
	}
}
