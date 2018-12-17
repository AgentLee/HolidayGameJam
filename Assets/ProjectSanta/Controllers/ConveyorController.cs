using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSanta.Controllers
{
    internal class ConveyorController : MonoBehaviour
    {
        internal List<ItemController> itemsToRemove;

        public float speed = 0.015f;
        // Start is called before the first frame update
        void Start()
        {
            itemsToRemove = new List<ItemController>();
        }

        // Update is called once per frame
        void Update()
        {
            for(int i = 0; i < References.itemsOnBelt.Count; ++i)
            {
                ItemController item = References.itemsOnBelt[i];

                if(item.IsOnConveyor())
                {
                    item.Position += transform.right * speed;
                }
                else if(item.isWrapped)
                {
                    itemsToRemove.Add(item);
                }
            }

            foreach(ItemController item in itemsToRemove)
            {
                References.itemsOnBelt.Remove(item);

                StartCoroutine(Destroy(item));
            }

            itemsToRemove.Clear();
        }

        IEnumerator Destroy(ItemController item)
        {
            References.playerController.Score += item.PointValue;

            yield return new WaitForSeconds(3f);

            item.Destroy();
        }
    }
}
