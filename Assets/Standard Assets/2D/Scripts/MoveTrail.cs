using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets._2D.Scripts
{
    public class MoveTrail : MonoBehaviour
    {
        public int moveSpeed = 200;     
        private void Update()
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            Destroy(this.gameObject, 1);
        }
    }
}
