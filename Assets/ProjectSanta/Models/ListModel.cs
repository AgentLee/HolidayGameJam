using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSanta.Controllers;

namespace ProjectSanta.Models
{
    internal class ListModel
    {
        internal string[] Names = new string[]
        {
            "Olivia", "Emma", "Ava", "Charlotte", "Mia", "Sophia", "Isabella",
            "Harper", "Amelia", "Evelyn", "Noah", "Liam", "Benjamin", "Oliver",
            "William", "James", "Elijah", "Lucas", "Mason", "Michael"
        };

        internal Transform list, nameHolder;

        internal List<PersonController> people;

        internal ListModel(Transform list)
        {
            this.list = list;
            nameHolder = list.Find("NameHolder");
            people = new List<PersonController>();
        }
    }
}