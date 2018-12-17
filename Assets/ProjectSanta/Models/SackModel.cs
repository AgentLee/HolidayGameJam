using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSanta.Controllers;

namespace ProjectSanta.Models
{
    internal class SackModel
    {
        internal Queue<ItemController> items;

        internal Transform sack, itemHolder;
        internal int sackPoints;

        internal SackModel(Transform sack)
        {
            items = new Queue<ItemController>();

            this.sack = sack;
            itemHolder = sack.Find("ItemHolder");

            sackPoints = 0;
        }
    }
}