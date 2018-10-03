using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserName : MonoBehaviour
{
    [SerializeField]
    Text userNameText;
    [SerializeField]
    ExternalInterface externalInterface;

    void Start()
    {
        Debug.Log("name:  "+externalInterface.Username);
        userNameText.text = externalInterface.Username;
    }

    public void PutName(string name)
    {
        userNameText.text = name;
    }
}
