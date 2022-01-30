using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirroredShape : MonoBehaviour
{
    public LitShape targetShape;
    public bool Spinning;
    public float SpinRate = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Spinning)
        {
            float step = SpinRate * Time.deltaTime;
            transform.Rotate(step, 0, 0);
        }
    }
}
