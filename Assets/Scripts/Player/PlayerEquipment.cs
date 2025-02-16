using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Example.Armament;

namespace Example.Player
{
    public class PlayerEquipment : MonoBehaviour
    {
        public AudioSource pickUpAudioSource;
        public Animator animator;
        
        private Weapon _weaponSlotOne;
        private Weapon _weaponSlotTwo;
        private uint _slotSelected = 0;
        
        void Start()
        {
            _weaponSlotOne = null;
            _weaponSlotTwo = null;
        }

        public void EquipWeapon(Weapon weapon)
        {
            pickUpAudioSource.Play();
            
            _weaponSlotOne = weapon;
            _slotSelected = 1;
            
            ActiveWeapon(weapon);

            PropagateAnimatorParameter(weapon);
        }

        private void ActiveWeapon(Weapon weapon)
        {
            Weapon[] weapons = GetComponentsInChildren<Weapon>(true);
            foreach (var w in weapons)
            {
                if (weapon.GetName() == w.GetName())
                {
                    w.gameObject.SetActive(true);
                    break;
                }
            }
        }

        public bool HasWeaponEquipment()
        {
            return _slotSelected > 0;
        }

        private void PropagateAnimatorParameter(Weapon weapon)
        {
            if (weapon.GetWeaponType() == WeaponType.RIFLE)
            {
                animator.SetBool("HasRifle", true);
            }
        }
    }
}