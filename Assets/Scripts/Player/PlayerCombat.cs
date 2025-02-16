using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Example.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        public PlayerEquipment equipment;
        public Animator animator;
        
        private bool _isAim;

        private void Update()
        {
            Aim();
            Attack();
        }

        private void Aim()
        {
            if (Input.GetMouseButton(1))
            {
                if (equipment.HasWeaponEquipment())
                {
                    _isAim = true;
                }
            }
            else
            {
                _isAim = false;
            }
            animator.SetBool("IsAim", _isAim);
        }

        private void Attack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_isAim && equipment.HasWeaponEquipment())
                {
                    Debug.Log("Shot!");
                }
            }
        }
    }
}