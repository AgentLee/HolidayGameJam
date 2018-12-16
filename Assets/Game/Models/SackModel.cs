using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSanta.Models
{
    internal class SackModel
    {
        internal Queue<GameObject> items;

        internal Transform sack;

        internal SackModel(Transform sack)
        {
            items = new Queue<GameObject>();

            this.sack = sack;
        }
    }
}