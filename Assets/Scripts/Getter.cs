using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Getter
    {
        public static Rigidbody2D GetRigibody2D(GameObject gameObject)
        {
            return gameObject.GetComponent<Rigidbody2D>();
        }
    }
}
