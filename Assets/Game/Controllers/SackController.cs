using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSanta.Models;

namespace ProjectSanta.Controllers
{
    internal class SackController
    {
        internal SackModel sackModel;

        internal SackController(Transform sack)
        {
            sackModel = new SackModel(sack);
        }

        internal void ShowSack(bool active)
        {
            SackOverflow();

            GameObject[] arr = sackModel.items.ToArray();
            for(int i = 0; i < arr.Length; ++i)
            {
                Debug.Log("Item " + (i + 1) + ": " + arr[i].name);
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

        internal void AddToSack(GameObject item)
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
                GameObject item = sackModel.items.Peek();

                sackModel.items.Dequeue();
            }
        }
    }
}