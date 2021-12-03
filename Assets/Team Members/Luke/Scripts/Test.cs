using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Animator ani;
    
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetBool("isWaiting", true);
        ani.SetBool("isObserving", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
