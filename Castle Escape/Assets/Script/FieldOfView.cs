using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;


    public List<Transform> visibleTargets = new List<Transform>();


    private void Start()
    {
        StartCoroutine(FindTarget());
    }

    IEnumerator FindTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            FindVisibleTargets();
        }
    }


    private void Update()
    {
        if(viewRadius ==0 && viewAngle == 0)
        {
            GameManager.instance.Fight = false;
        }
    }

    public  void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward,dirToTarget) < viewAngle / 2)
            {
                float disToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget,disToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    GameManager.instance.Fight = true;
                }
               
            }
            else
            {
                GameManager.instance.Fight = false;
            }

        }

    }

    public Vector3 DirFromAngle(float angleInDegrees,bool angleIsGlobal){
        if (!angleIsGlobal){
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

   
}
