using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSanta.Controllers;

namespace ProjectSanta.Models
{
    internal class PlayerModel
    {
        internal Transform transform;
        internal Transform camera;
        internal Transform leftHand, rightHand;

        internal Rigidbody rigidBody;

        internal float grabDistance;
        internal SackController sackController;

        internal GameObject item;
        internal bool HoldingItem { get { return item != null; } }
    

        internal PlayerModel(Transform player)
        {
            transform = player;

            camera = player.Find("Main Camera");
            leftHand = camera.Find("Left Hand");
            rightHand = camera.Find("Right Hand");

            rigidBody = transform.GetComponent<Rigidbody>();
            //rigidBody.angularDrag = Mathf.Infinity;
            //rigidBody.drag = Mathf.Infinity;

            grabDistance = 2.5f;

            sackController = new SackController();

            item = null;
        }
    }
}

