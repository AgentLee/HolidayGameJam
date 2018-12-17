using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSanta.Controllers
{
    public class WrappingController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            other.transform.GetComponent<Renderer>().material = Resources.Load<Material>("Gift_Wrap");
            
            foreach(ItemController item in References.itemsOnBelt)
            {
                if(item.itemModel.transform == other.transform)
                {
                    item.isWrapped = true;
                    break;
                }
            }
        }
    }
}
