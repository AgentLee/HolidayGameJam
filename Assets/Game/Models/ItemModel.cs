using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSanta.Models
{
    internal enum ItemStatus
    {
        HELD = 1,
        IN_SACK = 2,
        GROUNDED = 3,
        IN_SLEIGH = 4
    }

    internal class ItemModel
    {
        internal Transform transform;
        internal Rigidbody rigidBody;

        internal ItemStatus status;

        internal ItemModel(Transform transform)
        {
            this.transform = transform;
            rigidBody = transform.GetComponent<Rigidbody>();

            status = ItemStatus.GROUNDED;
        }
    }
}