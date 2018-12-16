using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSanta.Models;

namespace ProjectSanta.Controllers
{
    internal class ItemController
    {
        internal ItemModel itemModel;

        internal string Name
        {
            get
            {
                return itemModel.name;
            }
        }

        internal string FullName
        {
            get
            {
                return itemModel.transform.name;
            }
        }

        internal Type Type
        {
            get
            {
                return itemModel.type;
            }
        }

        internal int PointValue
        {
            get
            {
                return itemModel.pointValue;
            }
        }

        internal Vector3 Position
        {
            get
            {
                return itemModel.transform.position;
            }
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

        internal void Dropped()
        {
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
    }
}