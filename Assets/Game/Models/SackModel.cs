using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSanta.Models
{
    internal class SackModel
    {
        internal Queue<GameObject> items;

        internal SackModel()
        {
            items = new Queue<GameObject>();
        }
    }
}