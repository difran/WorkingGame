using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MenuHouse
{

    public class InifiniteScrollList : MonoBehaviour
    {

        public List<RectTransform> inifiniteList;

        public RectTransform containerList;

        public RectTransform TopLocalTransformReference;
        public RectTransform DownLocalTransformReference;

        public ScrollRect scrollRect;

        public List<RectTransform> containersForOrderUp = new List<RectTransform>();
        public List<RectTransform> containersForOrderDown = new List<RectTransform>();

        PositionObjectController positionObjectController;


        // Use this for initialization
        void Start()
        {
            positionObjectController = this.gameObject.GetComponent<PositionObjectController>();
        }

        void OnEnable()
        {
            UpdateList();
            print("add listener");
            scrollRect.onValueChanged.AddListener(ListenerMethod);
        }

        void OnDisable()
        {
            print("remove listener");
            scrollRect.onValueChanged.RemoveListener(ListenerMethod);
        }


        bool moveUp;
        float previousValue;
        void ListenerMethod(Vector2 value)//check if the bar is moving to up or move to down
        {


           
            print("on disable if < 5");
            if (inifiniteList.Count < 5) { this.enabled = false; scrollRect.movementType = ScrollRect.MovementType.Elastic; }

            if (previousValue == value.y) { return; }
            bool moveup = false;

            if (previousValue > value.y)
            {
                moveup = true;
            }

            if (previousValue < value.y)
            {
                moveup = false; 
            }         

            previousValue = value.y;

            moveUp = moveup;

            DoSomething();

        }

        public void DoSomething()
        {

            int moreUps = 1;

            int moreDowns = 1;

            foreach (var item in inifiniteList)
            {

                if (item == null) { UpdateList(); return; }

                if (moveUp)
                {//checking if the object go across the top
                    if (item.position.y > TopLocalTransformReference.position.y)
                    {
                        containersForOrderUp.Add(item);
                        OrderInScene(item, true, moreUps);
                        moreUps++;

                    }
                }
                else
                { //checking if the object go across the botom
                    if (item.position.y < DownLocalTransformReference.position.y)
                    {
                        containersForOrderDown.Add(item);
                        OrderInScene(item, false, moreDowns);
                        moreDowns++;
                      
                    }
                }
            }




            OrderAll();

        }


        void OrderAll()
        {
            OrderInHierarchy();
            //order the lists in the Hierarchy

            UpdateList();

         
            
        }

        void UpdateList()
        {
            print("actualiza lista de scroll");
            inifiniteList.Clear();
            containersForOrderUp.Clear();
            containersForOrderDown.Clear();

            RectTransform[] list = containerList.GetComponentsInChildren<RectTransform>();



            foreach (var item in list)
            {
                if (item.name == "Container")
                    inifiniteList.Add(item);
            }

            print(inifiniteList.Count + "cantidad de elementos");

        }


        void OrderInHierarchy()
    {
            if (containersForOrderUp.Count > 0)
            {
                foreach (var item in containersForOrderUp)
                {
                    item.SetAsLastSibling();//order this in the last place on the hierarchi 
                }
            }

            if (containersForOrderDown.Count > 0)
            {
                foreach (var item in containersForOrderDown)
                {
                    item.SetAsFirstSibling();//order this in the first place on the hierarchi
                }
            }
    }

    void OrderInScene(RectTransform item, bool putDown, int index = 1)
    {
        if (putDown)
        {
            item.localPosition = inifiniteList[inifiniteList.Count - 1].localPosition;
            item.localPosition -= positionObjectController.heightContainer * index;
        }
        else
        {
            item.localPosition = inifiniteList[0].localPosition;
            item.localPosition += positionObjectController.heightContainer * index;
        }

    }
        
    }
}
