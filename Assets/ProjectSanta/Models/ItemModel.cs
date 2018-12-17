using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ProjectSanta.Controllers;

namespace ProjectSanta.Models
{
    internal enum ItemStatus
    {
        HELD = 1,
        IN_SACK = 2,
        GROUNDED = 3,
        IN_SLEIGH = 4
    }

    internal enum Type
    {
        NEUTRAL = 0,
        NAUGHTY = 1,
        NICE = 2
    }

    internal class ItemModel
    {
        internal Transform transform;
        internal Rigidbody rigidBody;
        internal Color highlightColor;
        internal Color color;

        internal string name;
        internal int pointValue;
        internal ItemStatus status;
        internal Type type;

        internal PersonController person;

        internal ItemModel(Transform transform)
        {
            this.transform = transform;
            rigidBody = transform.GetComponent<Rigidbody>();
            color = transform.GetComponent<Renderer>().material.color;
            status = ItemStatus.GROUNDED;

            ParseItem();
        }

        void ParseItem()
        {
            for(int i = 0; i < transform.name.Length; ++i)
            {
                if(transform.name[i] == '_')
                {
                    break;
                }

                name += transform.name[i];
            }

            if (transform.name.Contains("Naughty") || transform.name.Contains("naughty"))
            {
                type = Type.NAUGHTY;
                highlightColor = Color.red;
            }
            else if (transform.name.Contains("Nice") || transform.name.Contains("nice"))
            {
                type = Type.NICE;
                highlightColor = Color.green;
            }
            else
            {
                type = Type.NEUTRAL;
                highlightColor = Color.blue;
            }

            int count = 0;
            for(int i = 0; i < transform.name.Length; ++i)
            {
                if(transform.name[i] == '_')
                {
                    count++;
                    continue;
                }

                if(count == 2)
                {
                    pointValue = (int)Char.GetNumericValue(transform.name[i]);
                    break;
                }
            }
        }
    }
}