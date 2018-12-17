using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSanta.Controllers;
using ProjectSanta.Views;

namespace ProjectSanta
{
    internal static class References
    {
        internal static InitializeController initializeController;
        internal static InitializeView initializeView;
        internal static PlayerController playerController;
        internal static SceneController sceneController;
        internal static List<ItemController> itemsOnBelt;
    }
}