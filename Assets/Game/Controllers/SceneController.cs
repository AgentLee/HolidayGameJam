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

            // Not the best way to do it
            //items = new List<ItemController>();

            //GameObject[] _items = GameObject.FindGameObjectsWithTag("Item");

            //foreach(GameObject item in _items)
            //{
            //    ItemController ic = new ItemController(item.transform);
            //}
        }
    }
}