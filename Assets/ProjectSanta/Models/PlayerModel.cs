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
        internal Animator animator;

        internal float grabDistance;
        internal float highlightDistance;
        internal float droppingDistance;
        internal SackController sackController;
        internal ListController listController;

        internal ItemController item;

        internal int score;

        //internal bool HoldingItem { get { return HoldingItem; } set { HoldingItem = value; } }
        //internal bool StoringItem { get { return StoringItem; } set { StoringItem = value; } }
        internal bool holdingItem;
        internal bool storingItem;

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
            animator = rightHand.Find("Mitten").GetComponent<Animator>();
            grabDistance = 2f;
            highlightDistance = 10f;
            droppingDistance = 8f;

            sackController = new SackController(sack);
            listController = new ListController(list);

            item = null;
            score = 0;

            holdingItem = false;
            storingItem = false;
        }
    }
}

