using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField] Vector3 _rotateSpeed;

    void Update()
    {
        transform.Rotate(_rotateSpeed);
    }


}
