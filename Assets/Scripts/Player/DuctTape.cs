using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuctTape : MonoBehaviour
{
    [SerializeField] Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.unscaledDeltaTime;
        anim.Update(deltaTime);
    }
}
