using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float hideDistance = 11f;

    // Update is called once per frame
    void Update()
    {
        var direction = target.position - transform.position;

        if (direction.magnitude < hideDistance)
        {
            SetChildActive(false);
        }
        else
        {
            SetChildActive(true);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

    private void SetChildActive(bool value)
    {
        transform.GetChild(0).gameObject.SetActive(value);
    }
}
