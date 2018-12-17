using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectSanta.Models;

namespace ProjectSanta
{
    internal static class Global
    {
        internal static int SACK_SIZE = 5;
        internal static int LIST_SIZE = 10;

        internal enum PersonType
        {
            NAUGHTY = 1,
            NICE = 2
        }

        internal static bool CompareTypes(PersonType personType, Type itemType)
        {
            return (int)personType == (int)itemType;
        }
    }
}