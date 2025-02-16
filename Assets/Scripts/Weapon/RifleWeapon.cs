using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Example.Armament
{
    public class RifleWeapon : Weapon
    {
        public uint bulletsPerMagazine = 30;
        public uint totalBullets = 125;
        
        private uint _bulletMagazine = 0;
        
        private void Start()
        {
            _bulletMagazine = bulletsPerMagazine;
            totalBullets -= bulletsPerMagazine;
        }

        public override string GetName()
        {
            return "Fusil M16";
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.RIFLE;
        }
    }
}