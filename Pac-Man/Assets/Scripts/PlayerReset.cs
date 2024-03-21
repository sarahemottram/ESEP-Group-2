using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace PacMan
{
    public class PlayerReset : MonoBehaviour
    {
        public void SetStartLocation(Vector3 startLocation) => this.startLocation = startLocation;
        private Vector3 startLocation;
        public void ResetPosition()
        {
            if (TryGetComponent<CharacterController>(out var cc))
            {
                cc.enabled = false;
                StartCoroutine(EnableCharacterController(cc));
            }
            gameObject.transform.position = startLocation;
            gameObject.transform.GetChild(0).rotation = Quaternion.identity;
        }

        private IEnumerator EnableCharacterController(CharacterController cc)
        {
            yield return null;
            yield return null;
            cc.enabled = true;
        }
    }
}
