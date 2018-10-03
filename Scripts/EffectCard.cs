using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EffectCard : MonoBehaviour
{
    float timeActive = 0.2f;

    public void ActiveEffect()
    {

       this.gameObject.SetActive(false);
     
       Invoke("Desactive", timeActive);
    }

    void Desactive()
    {
        this.gameObject.SetActive(true);
    }

}
