using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSanta.Controllers;

namespace ProjectSanta.Views
{
    internal class InitializeView : MonoBehaviour
    {
        public Transform player;
        public Transform house;
        public Transform sack, list;

        InitializeController initializeController;

        // Start is called before the first frame update
        void Start()
        {
            initializeController = new InitializeController(player, house, list, sack);

            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            initializeController.Update();
        }
    }
}
