using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Example.Armament
{
    public abstract class Weapon : MonoBehaviour
    {
        public abstract string GetName();
        public abstract WeaponType GetWeaponType();
    }

    public enum WeaponType
    {
        RIFLE = 0,
        PISTOL = 1,
        SUBMACHINE_GUN = 2,
        SNIPER = 3
    }
}
