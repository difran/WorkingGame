using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopUp : MonoBehaviour
{
    [SerializeField]
    GameObject background;
    [SerializeField]
    bool clickMeansClose = true;
    Fader backgroundFader;
    Vector3 originScale;
    RectTransform rt;
    bool hasAnimationInProgress = false;

    [SerializeField]
    AudioSource audioSurce;

    void Init ()
	{
        rt = this.GetComponent<RectTransform>();
        originScale = rt.localScale;
        backgroundFader = background.GetComponent<Fader>();
    }

	public void Activate ()
	{
      
        hasAnimationInProgress = true;
        if (backgroundFader == null) Init();
        backgroundFader.FadeIn();
        rt.localScale = new Vector3(0, 0, 0);
        this.gameObject.SetActive(true);
        audioSurce.Play();
        rt.DOScale(originScale,0.3f).SetEase(Ease.OutBack).OnComplete(ActivationFinished);
    }

	public void Deactivate ()
	{
        audioSurce.Play();
        hasAnimationInProgress = true;
        backgroundFader.FadeOut();
        rt.DOScale(new Vector3(0,0,0), 0.3f).SetEase(Ease.InBack).OnComplete(Off); 
    }

    void ActivationFinished ()
    {
        hasAnimationInProgress = false;
    }

    void Off()
    {
        this.gameObject.SetActive(false);
        rt.localScale = originScale;
        hasAnimationInProgress = false;
    }

    void Update() {
        if (clickMeansClose && Input.GetMouseButtonDown(0) && !hasAnimationInProgress)
        {
            Deactivate();
        }
    }
}
