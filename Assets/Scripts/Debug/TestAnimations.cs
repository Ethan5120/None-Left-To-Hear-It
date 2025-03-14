using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimations : MonoBehaviour
{
    Animator animator;
    [SerializeField] Animation walk;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(walk.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
