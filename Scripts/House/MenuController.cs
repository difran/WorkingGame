using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace MenuHouse
{

    public class MenuController : MonoBehaviour
    {
        public RectTransform menu;
        public RectTransform posOpened;
        public RectTransform posClosed;
        public GameObject buttonOpen;

        public GameObject blocker;

        public void OpenMenu()
        {
            menu.parent.gameObject.SetActive(true);
            blocker.gameObject.SetActive(true);

            menu.DOLocalMove(posOpened.localPosition, 0.3f);
        }

        public void CloseMenu()
        {
            blocker.gameObject.SetActive(false);
            menu.DOLocalMove(posClosed.localPosition, 0.3f).OnComplete(TurnOffMenu);
        }

        void TurnOffMenu()
        {
            menu.parent.gameObject.SetActive(false);
            buttonOpen.SetActive(true);
        }

    }

}
