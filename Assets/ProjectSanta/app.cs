using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    public class Application : MonoBehaviour
    {
        public static Application Instance { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        //public void StartCoroutine(IEnumerator coroutine)
        //{
        //    StartCoroutine(coroutine);
        //}
    }
}
