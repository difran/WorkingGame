using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableObject : MonoBehaviour
{
	[SerializeField]
	GameObject open;
	[SerializeField]
	GameObject close;
	[SerializeField]
	GameObject content;

	void Start()
	{
		OnClose();
	}

	public void OnOpen()
	{
		close.SetActive(false);
		open.SetActive(true);
		content.SetActive(true);
	}

	public void OnClose()
	{
		close.SetActive(true);
		open.SetActive(false);
		content.SetActive(false);
	}
}
