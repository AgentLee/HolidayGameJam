using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSanta.Models;

namespace ProjectSanta.Controllers
{
    internal class ListController
    {
        internal ListModel listModel;

        internal ListController(Transform list)
        {
            listModel = new ListModel(list);
        }

        internal void ShowList(bool active)
        {
            listModel.list.gameObject.SetActive(active);
        }
    }
}