using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public void FadeIn()
    {
        this.gameObject.SetActive(true);
        Image alpha = gameObject.transform.GetComponent<Image>();
        alpha.DOFade(0.5f, 0.5f);
    }

    public void FadeOut()
    {
        Image alpha = gameObject.transform.GetComponent<Image>();
        alpha.DOFade(0f, 0.5f).OnComplete(DesactiveGameObject);
    }

    void DesactiveGameObject()
    {
        this.gameObject.SetActive(false);
    }
}
