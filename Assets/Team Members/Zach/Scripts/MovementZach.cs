using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Luke;

namespace ZachFrench
{
    public class MovementZach : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;
        public List<Waypoint> wayPoints;
        public int wayPointNumber;
        
        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent.SetDestination(wayPoints[0].transform.position);
            print(wayPoints.Count);
        }

        // Update is called once per frame
        void Update()
        {
            NPCMovement();
        }

        public void NPCMovement()
        {
            foreach (Waypoint wayPoint in wayPoints)
            {
                print(wayPoint);
               if (navMeshAgent.remainingDistance < 1f)
               {
                   navMeshAgent.SetDestination(wayPoint.transform.position);
               }
            }

        }
    }
}
