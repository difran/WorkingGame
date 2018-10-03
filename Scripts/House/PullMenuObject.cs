using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

namespace MenuHouse
{
 
    public class PullMenuObject : MonoBehaviour
    {

        Vector3 startPosition;
        Vector3 originalScale;
        float scaleRatio = 1.3f;

        public GameObject currentItem;
        RectTransform currentRectTransform;

        float mousePositionX;

        public RectTransform outMenuTag;
        public RectTransform outPassRightMenuTag;
        public RectTransform outPassLeftMenuTag;
        public ScrollRect scrollRect;
        public RectTransform list;

        public GameObject currentFlannyForSpawn;
        public Transform currentParentToSpawn;

        bool desencastrado = false;

        InputOnUi InputUi;
        PositionObjectController positionObjectController;

      //  PushMenuObject pushObject;



        void Start()
        {
          //  pushObject = this.gameObject.GetComponent<PushMenuObject>();
            InputUi = this.gameObject.GetComponent<InputOnUi>();
            positionObjectController = this.gameObject.GetComponent<PositionObjectController>();
            this.enabled = false;
        }


        public void OnTouchBegin(GameObject go, bool desencastradoGo = false)
        {
            currentItem = go;

            currentRectTransform = go.GetComponent<RectTransform>();
            originalScale = currentRectTransform.localScale;
            currentRectTransform.transform.DOScale(originalScale * scaleRatio, 0.1f);
           
            this.enabled = true;
            desencastrado = desencastradoGo;


            positionObjectController.UpdateList();
            positionObjectController.enabled = true;
        }

        public bool OutCurrentObject
        {
            set
            {
                desencastrado = value;
            }
        }

        public void OnDrag()
        {

                if (Input.mousePosition.x < outMenuTag.position.x)
                {

                 
                currentItem.transform.position = Input.mousePosition;

                if (!desencastrado)
                {

                   currentItem.transform.SetParent(gameObject.transform);
                    scrollRect.enabled = false;
                    desencastrado = true;
                    print("desencastre");
                   
                }


            }
                else
                {

                    if (desencastrado)
                    {
                    currentItem.transform.position = Input.mousePosition;
                    //print("saco la bola");
                 
                   // DestroyAndOrder();
                    }

            }

          

        }


        public void EndDrag()
        {

            if (desencastrado)
            {
                if (Input.mousePosition.x < outMenuTag.position.x)//si suelta el flanny antes del menu, en el juego
                {
                    print("sale");
                    currentRectTransform.transform.DOScale(originalScale, 0.1f);
                    this.enabled = false;

                      if(!positionObjectController.EmpityList) scrollRect.enabled = true;
                  //  scrollRect.enabled = true;

                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    GameObject newFlanny = GameObject.Instantiate(currentFlannyForSpawn, new Vector3(pos.x, pos.y, 5), currentFlannyForSpawn.transform.rotation, currentParentToSpawn);

                    FlannyObjectMenu flanny = currentItem.GetComponent<FlannyObjectMenu>();

                    SpriteRenderer currentFlannyImage = newFlanny.GetComponent<SpriteRenderer>();
                    FlannyInGameInfo infoFlanny = newFlanny.GetComponent<FlannyInGameInfo>();

                    infoFlanny.flanny = flanny.flanny;
                    infoFlanny.color = flanny.color;

                    currentFlannyImage.sprite = flanny.myFlanny.sprite;

                    GameObject.Destroy(currentItem);
                    currentItem = null;

                 

                   

                }
                else//si suelta el flanny sobre el menu
                {

                    bool especificPosition = false;
                    Vector2 value = new Vector2(0, 0);

                   if (Input.mousePosition.x > outPassRightMenuTag.position.x)//si se paso del menu, le resto la posicion pasada para que funcione como si se soltara dentro del menu
                    {
                        especificPosition = true;
                        value = new Vector2(40, 0);
                    }

                    if (Input.mousePosition.x < outPassLeftMenuTag.position.x)//si se paso del menu, le resto la posicion pasada para que funcione como si se soltara dentro del menu
                    {
                        especificPosition = true;
                      //  print(Input.mousePosition.x + " mouse" + outPassRightMenuTag.position.x + "pass menu");
                        value = new Vector2(-40, 0);
                    }

                    GameObject go = InputUi.GetSomeObjectUi("Container", especificPosition, value.x);
                    if(go != null) FitBubble(go);
                    else//no toco un container, entonces veo si toco el fondo
                    {
                        go = InputUi.GetSomeObjectUi("backgroundUp", especificPosition, value.x); //chequeo si toco la parte superior del background
                        if (go != null)
                        {
                            FitBubbleOnBackground(MoveToBackground.Up);
                        }
                        else//chequeo si toco la parte inferior del background
                        {
                            go = InputUi.GetSomeObjectUi("backgroundDown", especificPosition, value.x);
                            if (go != null)
                            {
                                FitBubbleOnBackground(MoveToBackground.Down);
                            }
                        }



                    }
                    // resizedListMenu();
                    this.enabled = false;
                }
              

            }
            else
            {
                this.enabled = false;
                scrollRect.enabled = true;
                currentRectTransform.transform.DOScale(originalScale, 0.1f);
            }

         

        }

        /*
        bool llamarunavez = false;
        void DestroyAndOrder()
        {
            if (llamarunavez) return;
            GameObject go = InputUi.GetSomeObjectUi("Container");

            positionObjectController.DestroyAndOrder(go);
            llamarunavez = true;
        }
        */

        //ordena la burbuja cuando soltas el flanny sobre el fondo (sirve para cuando no hay burbujas en el menu)
        void FitBubbleOnBackground(MoveToBackground where)
        {
            GameObject newContainer;

            if (positionObjectController.EmpityList)
            {
                print("esta vacio es aca");

                newContainer =  positionObjectController.OrderObjectOnBackgraundMiddle();

                currentItem.transform.SetParent(newContainer.transform);
                currentItem.transform.position = newContainer.transform.position;
                currentRectTransform.transform.DOScale(originalScale, 0.1f);
                return;
            }

            
            if (where == MoveToBackground.Up)
            {
               
                FitBubble(positionObjectController.FirstObjectFromTheList);
                
            }
            if (where == MoveToBackground.Down)
            {
                FitBubble(positionObjectController.LastObjectFromTheList);
                
            }
        }

        //ordena la burbuja cuando soltas el flanny sobre otra burbuja
        void FitBubble(GameObject clickedContainer)
        {
   
            //esto me devuelve el container bien ubicado de donde voy a poner la burbuja
        GameObject newContainer = positionObjectController.orderObject(clickedContainer);

           //current item is the bubble object
            currentItem.transform.SetParent(newContainer.transform);
            currentItem.transform.position = newContainer.transform.position;
            currentRectTransform.transform.DOScale(originalScale, 0.1f);
        }

        void Update()
        {
            OnDrag();
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                EndDrag();
            }
        }
    }
    
}
