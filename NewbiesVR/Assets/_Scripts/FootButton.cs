using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootButton : MonoBehaviour
{
    Animator animator;
    // public Animation anima;
    // Outline outline;

    public List<GameObject> triggerList;
    public Animator Door;
    GameObject item;
    private void Start()
    {
        animator = GetComponent<Animator>();
        triggerList = new List<GameObject>();
        // outline = GetComponent<Outline>();
        // animation = 
    }
    void Update()
    {   
        //람다식 item을 다지워 => 아이템은 == null이야.
        triggerList.RemoveAll(item => item == null);
        if(triggerList.Count == 0)
        {

            animator.SetBool("FootButton",false);
            Door.SetBool("Door",false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        triggerList.Add(other.transform.gameObject);
        Debug.Log("FootButtonPressed");
        animator.SetBool("FootButton",true);
        Door.SetBool("Door",true);
        // outline.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        triggerList.Remove(other.transform.gameObject);
        Debug.Log("FootButtonRealised");

        // outline.enabled = false;
    }
}
