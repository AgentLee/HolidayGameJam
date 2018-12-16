using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSanta.Controllers
{
    internal class SceneController
    {
        Transform house;
        //internal List<ItemController> items;
        internal List<Transform> items;

        internal SceneController(Transform house)
        {
            this.house = house;

            // Not the best way to do it
            //items = new List<ItemController>();

            //GameObject[] _items = GameObject.FindGameObjectsWithTag("Item");

            items = new List<Transform>();
            foreach(GameObject go in GameObject.FindGameObjectsWithTag("Item"))
            {
                items.Add(go.transform);
            }

            //foreach(GameObject item in _items)
            //{
            //    ItemController ic = new ItemController(item.transform);
            //}

            References.sceneController = this;
        }

        internal void HighlightItems()
        {
            Vector3 playerPos = References.playerController.Position;

            foreach(Transform item in items)
            {
                float dist = Vector3.Distance(playerPos, item.position);
                if(dist <= References.playerController.HighlightDistance)
                {
                    item.GetComponent<Renderer>().material.color = Color.red;
                }
                else
                {
                    item.GetComponent<Renderer>().material.color = Color.green;
                }
            }
        }

        public void Update()
        {
            HighlightItems();
        }
    }
}