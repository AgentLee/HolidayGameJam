using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSanta.Controllers
{
    internal class InitializeController
    {
        PlayerController playerController;
        SceneController sceneController;

        internal InitializeController(Transform player, Transform house, Transform list, Transform sack)
        {
            playerController = new PlayerController(player, sack, list);
            sceneController = new SceneController(house);

            References.initializeController = this;
            References.itemsOnBelt = new List<ItemController>();
        }

        public void Update()
        {
            playerController.Update();
            sceneController.Update();
        }
    }
}
