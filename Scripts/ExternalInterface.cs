using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class ExternalInterface : MonoBehaviour
{
	[DllImport("__Internal")]
    private static extern void SendClose();
	[DllImport("__Internal")]
    private static extern string GetUsername();
	[DllImport("__Internal")]
    private static extern string GetToken();
	[DllImport("__Internal")]
    private static extern bool GetIsDevlop();
	string username = "gaturro";
	string token;
	bool isDevlop = true;
	public string Username
	{
		get
		{
			return username;
		}
	}
	public string Token
	{
		get
		{
			return token;
		}
	}
	public bool IsDevlop
	{
		get
		{
			return isDevlop;
		}
	}

	void Awake()
	{
		if (!Application.isEditor)
		{
			username = GetUsername();
			token = GetToken();
			isDevlop = GetIsDevlop();

			Debug.Log("# Bubble Flanys started");
			Debug.Log("- username: "+username);
			Debug.Log("- token: "+token);
			Debug.Log("- isDevlop: "+isDevlop);
		}
	}

	public void SendCloseToMMO()
	{
		if (!Application.isEditor)
		{
			SendClose();
		}
		else
		{
			Debug.LogWarning("Note: when builded sends close message to parent interface");
		}
	}
}