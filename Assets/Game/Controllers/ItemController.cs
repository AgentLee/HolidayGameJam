using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSanta.Models;

namespace ProjectSanta.Controllers
{
    internal class ItemController
    {
        internal ItemModel itemModel;

        internal ItemController(Transform transform)
        {
            itemModel = new ItemModel(transform);
        }

        internal void PickedUp()
        {

        }

        internal void Dropped()
        {

        }
    }
}