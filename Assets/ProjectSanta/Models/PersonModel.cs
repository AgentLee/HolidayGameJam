using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSanta
{
    internal class PersonModel
    {
        internal string name;
        internal Global.PersonType type;
        internal bool giftAssigned;

        internal PersonModel(string name, Global.PersonType personType)
        {
            this.name = name;
            type = personType;
            giftAssigned = false;
        }

        internal PersonModel()
        {

        }
    }
}