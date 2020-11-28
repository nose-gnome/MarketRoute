using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public NavMeshAgent Object;
    public LineRenderer lineRenderer;

    public Transform tes;
    public Transform yes;

    private Camera main;
    private NavMeshPath path;

    public bool place = true;

    public Transform ailse;
    public Transform secondAilse;

    void Start()
    {
        path = new NavMeshPath();
        main = Camera.main;
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.CompareTag("Aisle") && place)
                {
                    ailse = hit.collider.gameObject.transform;
                    place = false;
                }
                else if (hit.collider.CompareTag("Aisle") && !place)
                {
                    secondAilse = hit.collider.gameObject.transform;
                    place = true;
                }
            }
        }

        if (ailse != null && secondAilse != null)
        {
            NavMesh.SamplePosition(ailse.position, out NavMeshHit hit1, 200, NavMesh.AllAreas);
            NavMesh.SamplePosition(secondAilse.position, out NavMeshHit hit2, 200, NavMesh.AllAreas);

            NavMesh.CalculatePath(hit1.position, hit2.position, NavMesh.AllAreas, path);
        }

        ShowPath();
    }

    void ShowPath()
    {
        lineRenderer.positionCount = path.corners.Length;

        for (int i = 0; i < path.corners.Length; i++)
        {
            lineRenderer.SetPosition(i, path.corners[i]);
        }
    }
}
