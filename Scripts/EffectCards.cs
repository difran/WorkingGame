using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCards : MonoBehaviour {

    [SerializeField]
    GameObject[] cards;

    [SerializeField]
    GameObject bloquer;

    [SerializeField]
    float velocityEffect;

    int nowCard = 0;
    float time;
    bool init;
    int iterations;
    int maximunIterations = 3;


    void Start()
    {
        Invoke("InitEffect", 1);
    }

    void InitEffect()
    {
        init = true;
    }

    public void OffEffectCards()
    {
        this.enabled = false;
        bloquer.SetActive(false);

        foreach (var item in cards)
        {
            item.gameObject.SetActive(true);
        }
    }
  
	// Update is called once per frame
	void Update () {

        if (!init) return;

        time += Time.deltaTime;
        if (time > velocityEffect)
        {
            if (nowCard >= cards.Length)
            {
                nowCard = 0;
                if (iterations >= maximunIterations)
                {
                    OffEffectCards();
                    return;
                }

                 iterations++;
               
            }


            EffectCard e = cards[nowCard].GetComponent<EffectCard>();
            e.ActiveEffect();

            time = 0;

            nowCard++;

        }

    }

}
