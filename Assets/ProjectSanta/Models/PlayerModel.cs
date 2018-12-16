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
        internal Transform sack;

        internal Rigidbody rigidBody;

        internal float grabDistance;
        internal float highlightDistance;
        internal SackController sackController;
        internal ListController listController;

        internal ItemController item;
        internal bool HoldingItem { get { return item != null; } }
    

        internal PlayerModel(Transform player, Transform sack, Transform list)
        {
            transform = player;

            camera = player.Find("Main Camera");
            this.sack = player.Find("Sack");
            Debug.Log(sack.name);
            leftHand = camera.Find("Left Hand");
            rightHand = camera.Find("Right Hand");
            rigidBody = transform.GetComponent<Rigidbody>();
            //rigidBody.angularDrag = Mathf.Infinity;
            //rigidBody.drag = Mathf.Infinity;

            grabDistance = 2.5f;
            highlightDistance = 10f;

            sackController = new SackController(sack);
            listController = new ListController(list);

            item = null;
        }
    }
}

