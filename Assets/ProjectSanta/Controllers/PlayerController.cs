﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectSanta.Models;
using UnityEngine.XR;
using ProjectSanta;

namespace ProjectSanta.Controllers
{
    internal class PlayerController
    {
        PlayerModel playerModel;

        Vector3 velocity = Vector3.zero;
        readonly float speed = 5f;
        readonly float lookSensitivity = 1.25f;

        Text scoreText;
        internal bool HoldingItem { get { return playerModel.holdingItem; } }

        internal Vector3 Position
        {
            get { return playerModel.transform.position; }
        }

        internal float HighlightDistance
        {
            get { return playerModel.highlightDistance; }
        }

        internal int Score
        {
            get { return playerModel.score; }
            set { playerModel.score = value; }
        }

        internal PlayerController(Transform player, Transform sack, Transform list)
        {
            playerModel = new PlayerModel(player, sack, list);
            scoreText = GameObject.Find("Score").GetComponent<Text>();

            References.playerController = this;
        }

        internal void StandardMove()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");


            Vector3 moveHorizontal = playerModel.camera.right * moveX;
            Vector3 moveVertical = playerModel.camera.forward * moveZ;

            velocity = (moveHorizontal + moveVertical).normalized * speed;

            if(moveX == 0&& moveZ == 0)
            {
                playerModel.rigidBody.velocity = Vector3.zero;
            }

            if(velocity != Vector3.zero)
            {
                playerModel.rigidBody.MovePosition(playerModel.rigidBody.position + velocity * Time.fixedDeltaTime);
            }

            // Jump?
        }

        Vector3 rotation = Vector3.zero;
        internal void StandardLook()
        {
            float rotY = Input.GetAxisRaw("Mouse X");
            Vector3 rot = new Vector3(0f, rotY, 0f) * lookSensitivity;
            rotation = rot;
            playerModel.rigidBody.MoveRotation(playerModel.rigidBody.rotation * Quaternion.Euler(rotation));

            float rotX = Input.GetAxisRaw("Mouse Y") * lookSensitivity;
            currRotX -= rotX;
            currRotX = Mathf.Clamp(currRotX, -60f, 60);
            playerModel.camera.localEulerAngles = new Vector3(currRotX, 0f, 0f);
        }
        float currRotX = 0, currRotY;

        bool mouseDown, tabDown, qDown, rDown;

        internal void CheckMouse()
        {
            if(Input.GetMouseButtonDown(0))
            {
                mouseDown = true;
            }

            if(Input.GetMouseButtonUp(0))
            {
                mouseDown = false;
            }

            if(Input.GetKeyDown(KeyCode.Tab))
            {
                tabDown = true;
            }

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                tabDown = false;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                qDown = true;
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                qDown = false;
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                rDown = true;
            }

            if (Input.GetKeyUp(KeyCode.R))
            {
                rDown = false;
            }
        }

        Ray ray, rayL, rayR;
        internal void Grab()
        {
            playerModel.animator.SetBool("mouseDown", mouseDown);
            if(mouseDown)
            {
                playerModel.animator.Play("Close");

                // If player isn't holding anything, raycast to pick up nearest object.
                if(!playerModel.holdingItem)
                {
                    //ray = playerModel.camera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
                    //rayL = playerModel.camera.GetComponent<Camera>().ViewportPointToRay(new Vector3(.75f, 0.5f, 0.0f));
                    //rayR = playerModel.camera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.25f, 0.5f, 0.0f));
                    //Debug.DrawRay(ray.origin, ray.direction * playerModel.grabDistance, Color.blue);
                    //Debug.DrawRay(rayL.origin, rayL.direction * playerModel.grabDistance, Color.green);
                    //Debug.DrawRay(rayR.origin, rayR.direction * playerModel.grabDistance, Color.red);

                    //RaycastHit hit;
                    //bool intersects = Physics.Raycast(ray, out hit, playerModel.grabDistance);

                    //if (intersects)
                    //{
                    //    Transform item = hit.transform;
                    //    if (item.tag == "Item")
                    //    {
                    //        ItemController ic = References.sceneController.Find(item.name);

                    //        if (ic != null)
                    //        {
                    //            Debug.Log("GRAB ITEM");

                    //            playerModel.item = ic;
                    //            playerModel.item.itemModel.transform.parent = playerModel.rightHand;
                    //            playerModel.item.itemModel.transform.position = playerModel.rightHand.Find("Item").position;
                    //            playerModel.holdingItem = true;

                    //            ic.PickedUp();
                    //        }
                    //    }
                    //}

                    Debug.Log("NOT HOLDING");
                    ItemController closest = null;
                    float minDist = Mathf.Infinity;
                    foreach (ItemController item in References.sceneController.items)
                    {
                        Vector3 itemPos = item.Position;
                        float dist = Vector3.Distance(itemPos, Position);

                        if (dist < minDist && dist <= playerModel.grabDistance && item.itemModel.status == ItemStatus.GROUNDED || item.itemModel.status == ItemStatus.DUMPED)
                        {
                            minDist = dist;
                            closest = item;
                        }
                    }

                    if (closest != null)
                    {
                        playerModel.item = closest;
                        playerModel.item.itemModel.transform.parent = playerModel.rightHand;
                        playerModel.item.itemModel.transform.position = playerModel.rightHand.Find("Item").position;
                        playerModel.holdingItem = true;
                        if(playerModel.item.itemModel.status != ItemStatus.DUMPED)
                            playerModel.item.itemModel.status = ItemStatus.HELD;
                        playerModel.item.PickedUp();
                    }
                }
                // If player is already holding something, make sure object moves with the player.
                // If the player presses Q, the item gets prepped to be stored in the sack
                else
                {
                    Debug.Log("HOLDING");
                    playerModel.item.itemModel.transform.position = playerModel.rightHand.Find("Item").position;

                    if(qDown)
                    {
                        Debug.Log("STORING");
                        playerModel.item.itemModel.transform.parent = playerModel.sack;
                        playerModel.storingItem = true;
                    }
                }
            }

            // Store the object in the sack once the player lets go of Q.
            if(!qDown && playerModel.storingItem && playerModel.item != null)
            {
                if(playerModel.item.itemModel.status == ItemStatus.HELD)
                {
                    Debug.Log("STORED");
                    playerModel.item.itemModel.status = ItemStatus.IN_SACK;
                    playerModel.item.Dropped(false);
                    playerModel.sackController.AddToSack(playerModel.item, playerModel.listController);
                    playerModel.storingItem = false;
                    playerModel.holdingItem = false;
                }
            }

            // Player let go of left mouse, if they're holding something, let it go and reparent to the house.
            if (!mouseDown)
            {
                if(playerModel.holdingItem)
                {
                    Debug.Log("DROPPING ITEM");
                    playerModel.item.itemModel.status = ItemStatus.GROUNDED;
                    playerModel.item.Dropped(true);
                    playerModel.item.itemModel.transform.parent = GameObject.Find("House").transform;
                    if(playerModel.item.IsOnConveyor())
                    {
                        Debug.Log("CONVEYOR");
                    }
                    playerModel.holdingItem = false;
                }

                playerModel.item = null;
            }
        }

        internal void EmptySack()
        {
            float dist = Vector3.Distance(Position, GameObject.FindGameObjectWithTag("ConveyorBelt").transform.position);
            if(rDown && dist <= playerModel.droppingDistance)
            {
                playerModel.sackController.EmptySack(this);
            }
        }

        internal void UpdateScore()
        {
            scoreText.text = Score.ToString();
        }

        internal void SackStatus()
        {
            playerModel.sackController.ShowSack(tabDown, playerModel.listController);
            playerModel.listController.ShowList(tabDown);
        }

        internal void StayGrounded()
        {
            RaycastHit hit;
            if (Physics.Raycast(playerModel.transform.position, -Vector3.up, out hit))
            {
                float distToGround = hit.distance;

                Vector3 pos = playerModel.transform.position;
                pos.y = distToGround - playerModel.transform.GetComponent<Collider>().bounds.extents.y;

                playerModel.transform.position = pos;
            }
        }

        private void StandardLoop()
        {
            CheckMouse();
            StandardMove();
            StandardLook();
            //StayGrounded();
            Grab();
            EmptySack();
            SackStatus();
            UpdateScore();
        }

        private void VRLoop()
        {

        }
       
        public void Update()
        {
            if(XRDevice.isPresent)
            {
                VRLoop();
            }
            else
            {
                StandardLoop();
            }
        }
    }
}
