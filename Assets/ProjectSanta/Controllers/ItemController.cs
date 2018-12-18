using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSanta.Models;

namespace ProjectSanta.Controllers
{
    internal class ItemController
    {
        internal ItemModel itemModel;

        internal PersonController PersonAssigned
        {
            get { return itemModel.person; }
            set { itemModel.person = value; }
        }

        internal bool isWrapped;

        internal string Name
        {
            get { return itemModel.name; }
        }

        internal Color Color
        {
            get { return itemModel.color; }
            set { itemModel.transform.GetComponent<Renderer>().material.color = value; }
        }

        internal string FullName
        {
            get { return itemModel.transform.name; }
        }

        internal Type Type
        {
            get { return itemModel.type; }
        }

        internal int PointValue
        {
            get { return itemModel.pointValue; }
            set { itemModel.pointValue = value; }
        }

        internal Vector3 Position
        {
            get { return itemModel.transform.position; }
            set { itemModel.transform.position = value; }
        }

        internal ItemController(Transform transform)
        {
            itemModel = new ItemModel(transform);
        }

        internal void PickedUp()
        {
            itemModel.rigidBody.useGravity = false;
            itemModel.rigidBody.freezeRotation = true;
            itemModel.rigidBody.detectCollisions = false;
        }

        internal void Dropped(bool show)
        {
            itemModel.transform.gameObject.SetActive(show);
            itemModel.rigidBody.isKinematic = false;
            itemModel.rigidBody.useGravity = true;
            itemModel.rigidBody.freezeRotation = false;
            itemModel.rigidBody.detectCollisions = true;
        }

        internal void Highlight()
        {
            itemModel.transform.GetComponent<Renderer>().material.color = itemModel.highlightColor;
        }

        internal void ResetColor()
        {
            itemModel.transform.GetComponent<Renderer>().material.color = itemModel.color;
        }

        internal bool IsOnConveyor()
        {
            RaycastHit hit;
            if (Physics.Raycast(Position, Vector3.down, out hit))
            {
                if(hit.transform.tag == "ConveyorBelt")
                {
                    ConveyorController conveyor = hit.transform.GetComponent<ConveyorController>();
                    if(!References.itemsOnBelt.Contains(this))
                    {
                        Debug.Log("Adding to conveyor");
                        References.itemsOnBelt.Add(this);
                        itemModel.transform.parent = conveyor.transform.parent;
                    }

                    return true;
                }
            }

            return false;
        }

        internal void Destroy()
        {
            itemModel.transform.gameObject.SetActive(false);
            //References.sceneController.items.Remove(this);
            //GameObject.Destroy(itemModel.transform.gameObject);
        }
    }
}