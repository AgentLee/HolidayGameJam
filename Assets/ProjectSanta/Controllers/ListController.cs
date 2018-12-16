using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectSanta.Models;

namespace ProjectSanta.Controllers
{
    internal class ListController
    {
        internal ListModel listModel;

        internal ListController(Transform list)
        {
            listModel = new ListModel(list);

            GenerateList();
        }

        internal void ShowList(bool active)
        {
            listModel.list.gameObject.SetActive(active);
        }

        internal void GenerateList()
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Person");
            for(int i = 0; i < Global.LIST_SIZE; ++i)
            {
                int idx = (int)Mathf.Floor(Random.Range(0, listModel.Names.Length - 1));
                int status = (int)Mathf.Floor(Random.Range(1, 2));

                GameObject go = GameObject.Instantiate(prefab);
                go.transform.parent = listModel.nameHolder;
                go.transform.Find("Name").GetComponent<Text>().text = listModel.Names[idx];
                go.transform.Find("Status").GetComponent<Text>().text = (status == 1) ? "Naughty" : "Nice";
            }
        }
    }
}