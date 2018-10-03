using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MenuHouse
{

        public class PushMenuObject : MonoBehaviour {

        public GameObject Menu;
        public GameObject MenuButton;

        public Transform posEnterToMenu;

        public GameObject prefabContainerOnUI;
        public Transform ContainersParent;

        public GameObject prefabFlannyOnUI;
        public Transform currentParentToSpawn;

        PullMenuObject pullObject;

        public GameObject currentSelect;
        public GameObject selectContainer;

        void Start()
        {
            pullObject = this.gameObject.GetComponent<PullMenuObject>();
            selectContainer = GameObject.Find("House Container/Container Selected Object");
        }

        public void Init()
        {
            this.enabled = true;
        }
        /*
        public void TurnOffMenu()
        {
            this.enabled = false;
            Menu.SetActive(false);
            MenuButton.SetActive(true);
        }
        */

        void Update()
        {

            if (selectContainer.transform.childCount > 0)
            {
                currentSelect = selectContainer.transform.GetChild(0).gameObject;
            }

            if (currentSelect == null || currentSelect.layer != 10) return;

            if (currentSelect.transform.position.x > posEnterToMenu.transform.position.x)
            {
                AddFlannyToMenuZone();
                GameObject.Destroy(currentSelect);
            }

        }


        void AddFlannyToMenuZone()
        {
            Vector3 pos = Input.mousePosition;
            GameObject newFlanny = GameObject.Instantiate(prefabFlannyOnUI, new Vector3(pos.x, pos.y, 5), prefabFlannyOnUI.transform.rotation, currentParentToSpawn);
            newFlanny.name = "Flanny";

            FlannyObjectMenu flannyMenu = newFlanny.GetComponent<FlannyObjectMenu>();
            FlannyInGameInfo flannyInfoScene = currentSelect.GetComponent<FlannyInGameInfo>();
            flannyMenu.SetFlannyUI(flannyInfoScene.flanny, flannyInfoScene.color);

            pullObject.OnTouchBegin(newFlanny,true);

            pullObject.scrollRect.enabled = true;

        }

        public void AddContainerToMenuZone(Vector3 posMenu)
        {
           
            GameObject container = GameObject.Instantiate(prefabContainerOnUI, posMenu, prefabContainerOnUI.transform.rotation, ContainersParent);
            container.name = "Container";
            //pullObject.OnTouchBegin(container, true);

        }


    }

}
