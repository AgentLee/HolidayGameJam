using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectSanta.Models;
using Application = app.Application;

namespace ProjectSanta.Controllers
{
    internal class SackController
    {
        internal SackModel sackModel;
        internal Text sackPointsText;
        internal Text emptySackText;

        internal int SackPoints
        {
            get { return sackModel.sackPoints; }
            set { sackModel.sackPoints = value; }
        }
        
        internal ItemController[] Sack
        {
            get { return sackModel.items.ToArray(); }
        }

        internal SackController(Transform sack)
        {
            sackModel = new SackModel(sack);
            emptySackText = GameObject.Find("EmptySack").GetComponent<Text>();
            emptySackText.enabled = false;

            GenerateSack();
        }

        internal void GenerateSack()
        {
            sackPointsText = sackModel.sack.Find("SackPoints").GetComponent<Text>();
            UpdateSackPoints();

            GameObject prefab = Resources.Load<GameObject>("Prefabs/SackItem");

            for(int i = 0; i < Global.SACK_SIZE; ++i)
            {
                GameObject go = GameObject.Instantiate<GameObject>(prefab);
                go.transform.parent = sackModel.itemHolder;
            }
        }

        internal void UpdateSackPoints()
        {
            sackPointsText.text = SackPoints.ToString();
        }

        internal void ShowSack(bool active, ListController listController)
        {
            SackOverflow();

            for(int i = 0; i < Global.SACK_SIZE; ++i)
            {
                if (i >= Sack.Length)
                {
                    sackModel.itemHolder.GetChild(i).GetComponent<Text>().enabled = false;
                }
                else
                {
                    PersonController person = listController.listModel.people[i];
                    int personType = (int)person.Type;
                    int itemType = (int)Sack[i].Type;

                    sackModel.itemHolder.GetChild(i).GetComponent<Text>().enabled = true;
                    sackModel.itemHolder.GetChild(i).GetComponent<Text>().text = Sack[i].Name;

                    Color color = Color.black;
                    switch (Sack[i].Type)
                    {
                        case Type.NEUTRAL:
                            color = Color.blue;
                            break;
                        case Type.NAUGHTY:
                            color = Color.red;
                            break;
                        case Type.NICE:
                            color = Color.green;
                            break;
                    }

                    if(itemType == personType || itemType == 0)
                    {
                        sackModel.itemHolder.GetChild(i).GetComponent<Text>().text += " - " + person.Name; 
                    }
                    else
                    {
                        sackModel.itemHolder.GetChild(i).GetComponent<Text>().text += " - " + person.Name; ;
                    }

                    sackModel.itemHolder.GetChild(i).GetComponent<Text>().color = color;
                }
            }

            sackModel.sack.gameObject.SetActive(active);
        }

        internal bool SackOverflow()
        {
            if(sackModel.items.Count + 1 > 5)
            {
                Debug.Log("Empty sack!");
                emptySackText.enabled = true;
                return true;
            }

            emptySackText.enabled = false;
            return false;
        }

        internal bool AddToSack(ItemController item, ListController listController)
        {
            if(!SackOverflow())
            {
                sackModel.items.Enqueue(item);

                int idx = Sack.Length - 1;
                int personIdx = idx;
                PersonController person = listController.listModel.people[idx];

                while(person.GiftAssigned)
                {
                    Debug.Log(person.Name + " already got a gift");

                    idx++;
                    person = listController.listModel.people[idx];
                }

                Debug.Log("Assigning " + person.Name + " to " + item.Name);

                int itemType = (int)item.Type;
                int personType = (int)person.Type;

                //Debug.Log("Person: " + listController.listModel.people[idx].Name + " " + personType + " " + itemType);

                if (Global.CompareTypes(listController.listModel.people[idx].Type, item.Type))
                {
                    SackPoints += item.PointValue;
                }
                else
                {
                    SackPoints -= item.PointValue;
                    item.PointValue = -item.PointValue;
                }

                if(item.itemModel.status != ItemStatus.DUMPED)
                {
                    listController.UpdateList(idx);
                }

                UpdateSackPoints();

                return true;
            }

            return false;
        }

        internal void EmptySack(PlayerController playerController)
        {
            while(sackModel.items.Count > 0)
            {
                ItemController item = sackModel.items.Peek();
                item.itemModel.transform.gameObject.SetActive(true);
                item.itemModel.status = ItemStatus.DUMPED;
                Debug.Log("Put " + item.Name + " on belt");

                // Change shader of item
                item.Color = Color.yellow;

                sackModel.items.Dequeue();
            }

            SackPoints = 0;
        }
    }
}