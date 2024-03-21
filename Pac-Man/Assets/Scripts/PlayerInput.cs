using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

namespace PacMan
{
    public class PlayerInput : MonoBehaviour
    {
        public float Horizontal => enabled ? Input.GetAxis("Horizontal") : 0f;
        public float Vertical => enabled ? Input.GetAxis("Vertical") : 0f;
    }
}
