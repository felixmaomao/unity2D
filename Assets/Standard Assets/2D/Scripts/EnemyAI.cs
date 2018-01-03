using UnityEngine;
using Pathfinding;
using System.Collections;

namespace Assets.Standard_Assets._2D.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Seeker))]
    public class EnemyAI : MonoBehaviour
    {
        public Transform target;
        public float updateRate = 2f;

        private Seeker seeker;
        private Rigidbody2D rb;

        public Path path;

        public float speed = 300f;
        public ForceMode2D fMode;

        [HideInInspector]
        public bool pathEnded = false;
        //ai向下一个点移动的最大距离
        public float nextWayPointDistance = 3;
        //我们这在向这儿移动的点
        private int currentWayPoint = 0;

        private void Start()
        {
            seeker = GetComponent<Seeker>();
            rb = GetComponent<Rigidbody2D>();
            if (target == null)
            {
                Debug.LogError("No Player Found?Panic!");
                return;
            }
            // 执行路径寻找方法，并将结果返回给onPathComplete 方法
            seeker.StartPath(transform.position,target.position,OnPathComplete);
            StartCoroutine(UpdatePath());
        }

        IEnumerator UpdatePath()
        {
            if (target==null)
            {
                //todo insert a player search here;
                //return false;
            }
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            yield return new WaitForSeconds(1f / updateRate);
            StartCoroutine(UpdatePath());
        }

        public void OnPathComplete(Path p)
        {
            Debug.Log("we got a path ,did it have an error?"+p.error);
            if (!p.error)
            {
                path = p;
                currentWayPoint = 0;
            }
        }

        private void FixedUpdate()
        {
            if (target == null)
            {
                //todo insert a player search here;
                return;
            }
            if (path==null)
            {
                return;
            }
            if (currentWayPoint>=path.vectorPath.Count)
            {
                if (pathEnded)
                {
                    return;
                }
                Debug.Log("end of path reached");
                pathEnded = true;
                return;
            }
            pathEnded = false;

            //direction to the next waypoint
            Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
            dir *= speed * Time.deltaTime;

            //move the ai
            rb.AddForce(dir, fMode);

            float dist = Vector3.Distance(transform.position,path.vectorPath[currentWayPoint]);
            if (dist<nextWayPointDistance)
            {
                currentWayPoint++;
                return;
            }
        
        }
    }
}
