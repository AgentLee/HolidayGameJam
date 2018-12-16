using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSanta.Models;
using UnityEngine.XR;
using ProjectSanta;

namespace ProjectSanta.Controllers
{
    internal class PlayerController
    {
        PlayerModel playerModel;

        float minVert = -45.0f;
        float maxVert = 45.0f;
        float sensHorizontal = 10.0f;
        float sensVertical = 10.0f;

        float rotX = 0;

        Vector3 velocity = Vector3.zero;
        float speed = 5f;
        float lookSensitivity = 3f;

        internal Vector3 Position
        {
            get
            {
                return playerModel.transform.position;
            }
        }

        internal float HighlightDistance
        {
            get
            {
                return playerModel.highlightDistance;
            }
        }

        internal PlayerController(Transform player, Transform sack, Transform list)
        {
            playerModel = new PlayerModel(player, sack, list);

            References.playerController = this;
        }

        internal void StandardMove()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");

            Vector3 moveHorizontal = playerModel.camera.right * moveX;
            Vector3 moveVertical = playerModel.camera.forward * moveZ;

            velocity = (moveHorizontal + moveVertical).normalized * speed;

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

            float rotX = Input.GetAxisRaw("Mouse Y");
            Vector3 camRot = new Vector3(rotX, 0f, 0f) * lookSensitivity;
            playerModel.camera.Rotate(-camRot);

        }
        Vector3 currRotX, currRotY;

        bool mouseDown;
        bool tabDown;
        bool fDown;
        bool qDown;

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

            if(Input.GetKeyDown(KeyCode.F))
            {
                fDown = true;
            }

            if (Input.GetKeyUp(KeyCode.F))
            {
                fDown = false;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                qDown = true;
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                qDown = false;
            }
        }

        bool storeItem;
        Ray ray, rayL, rayR;
        internal void Grab()
        {
            playerModel.animator.SetBool("mouseDown", mouseDown);
            if(mouseDown)
            {
                playerModel.animator.Play("Close");
            }

            if (mouseDown && !playerModel.HoldingItem)
            {
                ray = playerModel.camera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
                rayL = playerModel.camera.GetComponent<Camera>().ViewportPointToRay(new Vector3(.75f, 0.5f, 0.0f));
                rayR = playerModel.camera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.25f, 0.5f, 0.0f));
                Debug.DrawRay(ray.origin, ray.direction * playerModel.grabDistance, Color.blue);
                Debug.DrawRay(rayL.origin, rayL.direction * playerModel.grabDistance, Color.green);
                Debug.DrawRay(rayR.origin, rayR.direction * playerModel.grabDistance, Color.red);

                RaycastHit hit;
                bool intersects = Physics.Raycast(ray, out hit, playerModel.grabDistance);

                if (intersects)
                {
                    Transform item = hit.transform;
                    if (item.tag == "Item")
                    {
                        ItemController ic = References.sceneController.Find(item.name);

                        if(ic != null)
                        {

                            Debug.Log("GRAB ITEM");

                            playerModel.item = ic;
                            playerModel.item.itemModel.transform.parent = playerModel.rightHand;
                            playerModel.item.itemModel.transform.position = playerModel.rightHand.Find("Item").position;

                            ic.PickedUp();
                        }
                    }
                }
            }

            if (mouseDown && playerModel.HoldingItem)
            {
                playerModel.item.itemModel.transform.position = playerModel.rightHand.Find("Item").position;

                if(qDown)
                {
                    playerModel.item.itemModel.transform.parent = playerModel.sack;
                    //Debug.Log("PARENT: " + playerModel.item.transform.parent.name);
                    storeItem = true;
                    playerModel.item.itemModel.transform.gameObject.SetActive(false);
                }
            }

            if (!mouseDown && playerModel.HoldingItem)
            {
                playerModel.item.Dropped();

                if(storeItem)
                {
                    // logic is kinda funky here...
                    playerModel.sackController.AddToSack(playerModel.item);
                    storeItem = false;
                }
                else
                {
                    playerModel.item.itemModel.transform.parent = GameObject.Find("House").transform;
                }

                playerModel.item = null;
            }
        }

        internal void SackStatus()
        {
            playerModel.sackController.ShowSack(tabDown);
        }

        internal void ListStatus()
        {
            playerModel.listController.ShowList(fDown);
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
            SackStatus();
            ListStatus();
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
