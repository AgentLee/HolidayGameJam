using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSanta.Controllers
{
    internal class InitializeController
    {
        PlayerController playerController;
        SceneController sceneController;

        internal InitializeController(Transform player, Transform house)
        {
            playerController = new PlayerController(player);
            sceneController = new SceneController(house);
        }

        public void Update()
        {
            playerController.Update();
        }
    }
}
