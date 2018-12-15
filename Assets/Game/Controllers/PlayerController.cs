using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSanta.Models;
using UnityEngine.XR;

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

        internal PlayerController(Transform player)
        {
            playerModel = new PlayerModel(player);
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

            if(Input.GetKey(KeyCode.Tab))
            {
                tabDown = true;
            }

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                tabDown = false;
            }
        }

        Ray ray;
        internal void Grab()
        {
            if(mouseDown && !playerModel.HoldingItem)
            {
                ray = playerModel.camera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
                Debug.DrawRay(ray.origin, ray.direction * playerModel.grabDistance, Color.blue);

                RaycastHit hit;
                bool intersects = Physics.Raycast(ray, out hit, playerModel.grabDistance);

                Transform item = hit.transform;
                if(intersects)
                {
                    if(item.tag == "Item")
                    {
                        playerModel.sackController.AddToSack(item.gameObject);

                        Debug.Log("GRAB ITEM");

                        playerModel.item = item.gameObject;
                        playerModel.item.transform.parent = playerModel.rightHand;
                        playerModel.item.transform.position = playerModel.rightHand.Find("Item").position;
                        playerModel.item.GetComponent<Rigidbody>().useGravity = false;
                        playerModel.item.GetComponent<Rigidbody>().freezeRotation = true;
                        playerModel.item.GetComponent<Rigidbody>().detectCollisions = false;
                    }
                }
            }

            if(mouseDown && playerModel.HoldingItem)
            {
                playerModel.item.transform.position = playerModel.rightHand.Find("Item").position;
            }

            if(!mouseDown && playerModel.HoldingItem)
            {
                playerModel.item.GetComponent<Rigidbody>().useGravity = true;
                playerModel.item.GetComponent<Rigidbody>().freezeRotation = false;
                playerModel.item.GetComponent<Rigidbody>().detectCollisions = true;
                playerModel.item.transform.parent = GameObject.Find("House").transform;
                playerModel.item = null;
            }
        }

        internal void SackStatus()
        {
            if(tabDown)
            {
                playerModel.sackController.ShowSack();
            }
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
