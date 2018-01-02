using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

namespace Assets.Standard_Assets._2D.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Seeker))]
    public class EnemyAI : MonoBehaviour
    {
        public Transform target;
        public float updateRate = 2f;

        private Seeker seeker;
       
    }
}
