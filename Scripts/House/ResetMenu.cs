using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MenuHouse
{

    public class ResetMenu : MonoBehaviour
    {

        PositionObjectController p;

        public List<Vector3> containersInitPos;
        Vector3 gridInitPosition;

        void Awake()
        {
            p = this.GetComponent<PositionObjectController>();
            
        }

        void Start()
        {

            SaveInitialPositions();
        }

        void GiveShapeAtMenu()
        {

            print("acomoda el menu");

            RectTransform r = p.centerGrid.GetComponent<RectTransform>();
            float ySizeGrid = p.containers.Count * 133;
            ySizeGrid += 133;
            r.sizeDelta = new Vector2(r.sizeDelta.x, ySizeGrid);

        }

        //solo ejecutar give shape cuando abris el menu 
        void OnEnable()
        {
            print("on enable");
        }

        //solo ejecutar give shape cuando abris el menu 
        void OnDisable()
        {


            SetInitialPositionsAtAllContainers(); // pone todas las burbujas arriba como al principio
            SetInitialPositionAtFather(); //pone la contenedor arriba coo al principio
          //  GiveShapeAtMenu(); //escala el tamaño del container a la canitdad de burbujas que tiene
           

            print("on disable");
        }
        
        void SaveInitialPositions()
        {
            RectTransform[] rawList = p.centerContainer.GetComponentsInChildren<RectTransform>();

            foreach (var item in rawList)
            {
                if (item.name == "Container")
                    containersInitPos.Add(item.localPosition);
            }

            gridInitPosition = p.centerGrid.transform.localPosition;

            print("SAVE INITIAL POSITION");
        }
        
        void SetInitialPositionsAtAllContainers()
        {

            for (int i = 0; i < p.containers.Count; i++)
            {
                p.containers[i].localPosition = containersInitPos[i];
            }

            print("center grid ubicacion inicial");
        }


        void SetInitialPositionAtFather()
        {
            p.centerGrid.transform.localPosition = gridInitPosition;
        }
    }
}


