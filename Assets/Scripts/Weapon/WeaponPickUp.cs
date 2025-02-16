using System;
using System.Collections;
using System.Collections.Generic;
using Example.Player;
using TMPro;
using UnityEngine;

namespace Example.Armament
{
    public class WeaponPickUp : MonoBehaviour
    {
        public TextMeshProUGUI pickUpWeaponText;
        public Weapon weapon;
        
        private bool _isPlayerInRange;
        private GameObject _player;
        
        // Start is called before the first frame update
        void Start()
        {
            _isPlayerInRange = false;
            pickUpWeaponText.text = "Presiona F para recoger " + weapon.GetName();
            SetPickUpTextActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (_isPlayerInRange && Input.GetKeyDown(KeyCode.F) && _player != null)
            {
                PickUp();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = true;
                SetPickUpTextActive(true);

                _player = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInRange = false;
                SetPickUpTextActive(false);

                _player = null;
            }
        }

        private void PickUp()
        {
            PlayerEquipment equipment = _player.GetComponent<PlayerEquipment>();
            if (equipment != null)
            {
                equipment.EquipWeapon(weapon);
                
                Destroy(gameObject);
                SetPickUpTextActive(false);
            }
        }

        private void SetPickUpTextActive(bool status)
        {
            if (pickUpWeaponText != null)
            {
                pickUpWeaponText.gameObject.SetActive(status);
            }
        }
    }
}

