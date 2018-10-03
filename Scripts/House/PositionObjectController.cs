using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MenuHouse
{

    public class PositionObjectController : MonoBehaviour
    {
        public List<RectTransform> containers;

        public GameObject centerContainer;

        public GameObject prefabContainer;

        public VerticalLayoutGroup centerGrid;

        public List<RectTransform> containersUp = new List<RectTransform>();
        public List<RectTransform> containersDown = new List<RectTransform>();

        public Vector3 heightContainer = new Vector3(0, 133.1f, 0);

        public RectTransform centerPositionEmpity;

       

        public bool EmpityList
        {
            get
            {
                UpdateList();
                if (containers.Count == 0) return true;
                else return false;
            }
        }

        public GameObject FirstObjectFromTheList
        {
            get
            {
               return containers[0].gameObject;
            }
        }

        public GameObject LastObjectFromTheList
        {
            get
            {
                return containers[containers.Count -1].gameObject;
            }
        }

        // Use this for initialization
        void Start()
        {
            this.enabled = false;
        }

        void Update()
        {
           // print("mirando si hay un padre vacio");

            if(containers.Count > 0)
            foreach (var item in containers)
            {
                    if (item.childCount == 0) { DestroyAndOrder(item.gameObject); }
            }
        }

        public void UpdateList()
        {
            print("UPDATE LIST");

            containers.Clear();
            containersUp.Clear();
            containersDown.Clear();

            RectTransform[] rawList = centerContainer.GetComponentsInChildren<RectTransform>();

            foreach (var item in rawList)
            {
                if (item.name == "Container")
                    containers.Add(item);
            }

            print("CARGO LAS LISTAS");
        
        }

        public void DestroyAndOrder(GameObject ClickedContainer)
        {
           // centerGrid.enabled = false;

            bool saveInListDown = false;
         
          //  UpdateList();

            foreach (var item in containers)
            {
                //se fija que container tocaste
                //a partir del que tocaste lo empieza a guardar en la lista de abajo
                if (item.transform.GetInstanceID() == ClickedContainer.transform.GetInstanceID())
                {
                    print("si es este");
                    saveInListDown = true;
                    //item.gameObject.SetActive(false);

                }

                
                if (saveInListDown)
                {
                    containersDown.Add(item);
                }
                else
                {
                    containersUp.Add(item);
                }
                
            }

            //pull
          

            
/*
            Vector3 middleHeighContainer = heightContainer / 2;
             MoveList(containersUp, Move.Down, middleHeighContainer); 
             MoveList(containersDown, Move.Up, middleHeighContainer);
  */             

            MoveList(containersDown, Move.Up, heightContainer);

            this.enabled = false;
       
            Destroy(ClickedContainer);
            
        }


        /// <summary>
        /// this make the animation`s menu objects and return the position where should be the new flanny
        /// </summary>
        /// <param name="ClickedContainer"></param>
        /// <returns></returns>
        public GameObject orderObject(GameObject ClickedContainer)
        {
            UpdateList();

            print(ClickedContainer.transform.position);
            print(Input.mousePosition);

            bool currentContainerSaveInListDown = false;

            //se fija si tocaste en la mirad superior o inferior del container para luego guardarlo en la lista que corresponda
            //this calculate where will save the clicked container
            
            if (ClickedContainer.transform.position.y > Input.mousePosition.y) print("Touch Down");
            if (ClickedContainer.transform.position.y < Input.mousePosition.y) { print("Touch Up"); currentContainerSaveInListDown = true; }
            

           // centerGrid.enabled = false;

            bool saveInListDown = false;
            bool thisIsTheClickedItem = false;


            int i = 0;

            foreach (var item in containers)
            {
                
                //se fija que container tocaste
                //a partir del que tocaste lo empieza a guardar en la lista de abajo
                if (item.transform.GetInstanceID() == ClickedContainer.transform.GetInstanceID())
                {
                    print("si es este");
                    saveInListDown = true;
                    thisIsTheClickedItem = true;
                }


                if (thisIsTheClickedItem)
                {
                    
                    //old system
                    if (i == 0 || i == containers.Count)
                    {
                        if(i == 0) containersUp.Add(item);
                        if (i == containers.Count) containersUp.Add(item);
                    }

                    if(currentContainerSaveInListDown) containersDown.Add(item);
                    else containersUp.Add(item);

                    

                    thisIsTheClickedItem = false;

                   


                }
                else
                {
                    if (saveInListDown)
                    {
                        containersDown.Add(item);
                    }
                    else
                    {
                        containersUp.Add(item);
                    }
                }

                i++;
            }
            /*
            Vector3 middleHeighContainer = heightContainer / 2;//genero unidad de medida de medio objeto

            MoveList(containersUp, Move.Up, middleHeighContainer);//todos los de la lista de arriba los muevo para arriba

            MoveList(containersDown, Move.Down, middleHeighContainer);//todos los de la lista de abajo los muevo para abajo

            
            Vector3 clickedContainer = ClickedContainer.transform.localPosition;
            Vector3 posNewContainerLessMiddle = clickedContainer - middleHeighContainer;
            Vector3 posNewContainerAddMiddle = clickedContainer + middleHeighContainer;
            */

            
            Vector3 clickedContainer = ClickedContainer.transform.localPosition;
            Vector3 posNewContainerLessMiddle = clickedContainer - heightContainer;
            Vector3 posNewContainerSame = clickedContainer;
            Vector3 posNewContainerAddMiddle = clickedContainer + heightContainer;//old system is middlecontainer

            MoveList(containersDown, Move.Down, heightContainer);//todos los de la lista de abajo los muevo para abajo

            GameObject container = GameObject.Instantiate(prefabContainer, centerContainer.transform);

            if (currentContainerSaveInListDown)
            {
                container.GetComponent<RectTransform>().localPosition = posNewContainerSame;//old system was posnewContainerAddMiddle
            }
            else
            {
                container.GetComponent<RectTransform>().localPosition = posNewContainerLessMiddle;
            }

            container.name = "Container";

            foreach (var item in containersDown)
            {
                RectTransform rectT = item.GetComponent<RectTransform>();
                rectT.SetAsLastSibling();
            }
            return container;

        }


        void MoveList(List<RectTransform> list, Move direction, Vector3 howAnyMove)
        {
            foreach (var item in list)
            {
                RectTransform rectT = item.GetComponent<RectTransform>();
                if (direction == Move.Up)
                {
                     rectT.DOLocalMove(rectT.localPosition + howAnyMove, 0.5f);
                }
                if (direction == Move.Down)
                {
                    rectT.DOLocalMove(rectT.localPosition - howAnyMove, 0.5f);
                }
            }
        }

        public GameObject OrderObjectOnBackgraundMiddle()
        {
            UpdateList();
            print("llega a midle");
            GameObject container = GameObject.Instantiate(prefabContainer, centerGrid.transform);

            container.GetComponent<RectTransform>().position = centerPositionEmpity.position;
            container.name = "Container";
          
            return container;
        }






    }


    enum Move
    {
        Up,
        Down,
    }

    enum MoveToBackground
    {
        Up,
        Down,
        Middle,
    }
}
