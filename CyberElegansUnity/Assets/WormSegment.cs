using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSegment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var eulerAngles = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0.0f);
    }
}
