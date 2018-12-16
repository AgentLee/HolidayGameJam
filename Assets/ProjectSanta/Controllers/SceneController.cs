using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSanta.Controllers
{
    internal class SceneController
    {
        Transform house;
        internal List<ItemController> items;

        internal SceneController(Transform house)
        {
            this.house = house;

            items = new List<ItemController>();
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
            {
                ItemController ic = new ItemController(item.transform);
                items.Add(ic);
                //Debug.Log(ic.Name + " " + ic.Type + " " + ic.PointValue + " " + ic.Position);
            }

            References.sceneController = this;
        }

        internal void HighlightItems()
        {
            Vector3 playerPos = References.playerController.Position;

            foreach(ItemController item in items)
            {
                float dist = Vector3.Distance(playerPos, item.Position);
                if(dist <= References.playerController.HighlightDistance)
                {
                    item.Highlight();
                }
                else
                {
                    item.ResetColor();
                }
            }
        }

        internal ItemController Find(string name)
        {
            foreach(ItemController ic in items)
            {
                if(ic.FullName == name)
                {
                    return ic;
                }
            }

            return null;
        }

        public void Update()
        {
            HighlightItems();
        }
    }
}