using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSanta.Controllers
{
    internal class PersonController
    {
        internal PersonModel personModel;

        internal string Name
        {
            get { return personModel.name; }
            set { personModel.name = value; }
        }

        internal Global.PersonType Type
        {
            get { return personModel.type; }
            set { personModel.type = value; }
        }

        internal bool GiftAssigned
        {
            get { return personModel.giftAssigned; }
            set { personModel.giftAssigned = true; }
        }

        internal PersonController(string name, Global.PersonType personType)
        {
            personModel = new PersonModel(name, personType);
        }

        internal PersonController()
        {
            personModel = new PersonModel();
        }
    }
}