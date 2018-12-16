using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectSanta.Models;

namespace ProjectSanta.Controllers
{
    internal class SackController
    {
        internal SackModel sackModel;

        internal SackController(Transform sack)
        {
            sackModel = new SackModel(sack);

            GenerateSack();
        }

        internal void GenerateSack()
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/SackItem");

            for(int i = 0; i < Global.SACK_SIZE; ++i)
            {
                GameObject go = GameObject.Instantiate<GameObject>(prefab);
                go.transform.parent = sackModel.itemHolder;
            }
        }

        internal void ShowSack(bool active)
        {
            SackOverflow();

            ItemController[] arr = sackModel.items.ToArray();
            for(int i = 0; i < Global.SACK_SIZE; ++i)
            {
                if (i >= arr.Length)
                {
                    sackModel.itemHolder.GetChild(i).GetComponent<Text>().enabled = false;
                }
                else
                {
                    sackModel.itemHolder.GetChild(i).GetComponent<Text>().enabled = true;
                    sackModel.itemHolder.GetChild(i).GetComponent<Text>().text = arr[i].Name;
                }
            }

            sackModel.sack.gameObject.SetActive(active);
        }

        internal bool SackOverflow()
        {
            if(sackModel.items.Count > 5)
            {
                Debug.Log("Empty sack!");
                return true;
            }

            return false;
        }

        internal void AddToSack(ItemController item)
        {
            if(!SackOverflow())
            {
                //item.SetActive(false);
                sackModel.items.Enqueue(item);
            }
        }

        internal void EmptySack()
        {
            while(sackModel.items.Count > 0)
            {
                ItemController item = sackModel.items.Peek();

                sackModel.items.Dequeue();
            }
        }
    }
}