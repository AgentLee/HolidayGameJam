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

        internal void UpdateList(int idx)
        {
            listModel.people[idx].GiftAssigned = true;
            Color c = listModel.nameHolder.GetChild(idx).Find("Name").GetComponent<Text>().color;
            c.a = 0.25f;
            listModel.nameHolder.GetChild(idx).Find("Name").GetComponent<Text>().color = c;
        }

        internal void GenerateList()
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Person");

            List<int> indices = new List<int>();
            for(int i = 0; i < Global.LIST_SIZE; ++i)
            {
                int randomNum = Random.Range(1, 1000);
                int _idx = randomNum % listModel.Names.Length;
                int _status = randomNum % 2;

                int n = 4;
                while(indices.Contains(_idx))
                {
                    randomNum = (int)Mathf.Floor(Random.Range(1, Mathf.Pow(10, n)));
                    n++;

                    _idx = randomNum % listModel.Names.Length;
                    _status = randomNum % 2;
                }

                PersonController person = new PersonController();

                GameObject go = GameObject.Instantiate(prefab);
                go.transform.parent = listModel.nameHolder;
                go.transform.Find("Name").GetComponent<Text>().text = listModel.Names[_idx];
                person.Name = listModel.Names[_idx];

                if (_status == 1)
                {
                    go.transform.Find("Status").GetComponent<Text>().text = "Naughty";
                    go.transform.Find("Status").GetComponent<Text>().color = Color.red;
                    person.Type = Global.PersonType.NAUGHTY;
                }
                else
                {
                    go.transform.Find("Status").GetComponent<Text>().text = "Nice";
                    go.transform.Find("Status").GetComponent<Text>().color = Color.green;
                    person.Type = Global.PersonType.NICE;
                }

                listModel.people.Add(person);
            }
        }
    }
}