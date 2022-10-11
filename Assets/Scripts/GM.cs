using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public List<GameObject> resources = new List<GameObject>();

    public GameObject FindClosestResource(Vector3 sourcePos)
    {
        GameObject closestResource = null;
        float closest = 9999;

        // check distance between each node
        foreach (GameObject resourceNode in resources)
        {
            float distance = Vector3.Distance(sourcePos, resourceNode.transform.position);

            // new closest found
            if (distance < closest)
            {
                closest = distance;
                closestResource = resourceNode;
            }
        }

        return closestResource;
    }
}
