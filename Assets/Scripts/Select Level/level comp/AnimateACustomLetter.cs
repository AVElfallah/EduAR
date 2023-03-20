using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AnimateACustomLetter : MonoBehaviour
{
  //  public GameObject[] gameObjects;
    public AnimationClip animationClip;

    public RuntimeAnimatorController animationController;
    private GameObject[] gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        //set the size of the array to the number of children
        gameObjects=new GameObject[transform.childCount];
        for (int i = 0; i <transform.childCount; i+=1)
        {
            //get childe by index and store it in a variable
            Transform childTransform = transform.GetChild(i);
            
           //get animator component from child and store it in a variable
            Animator animator = childTransform.gameObject.GetComponent<Animator>();
            //set the animation controller to the animator
            animator.runtimeAnimatorController = animationController;
            //disable the animator to stop the animation and play it manually
            animator.enabled = false;
            // store the gameobject in an array
            gameObjects[i]=childTransform.gameObject;
        }
    }
    public void animateALetter(string letter){
        //loop through the array of gameobjects
        Debug.Assert(gameObjects.Length > 0, "gameObjects.Length > 0");
        
        for (int i = 0; i < gameObjects.Length; i+=1)
        {
            //get the animator component from the gameobject
            Animator animator = gameObjects[i].GetComponent<Animator>();
            //get the text component from the gameobject
            string name = gameObjects[i].name;
            //check if the text of the text component is equal to the letter
            if (name.ToLower() == letter.ToLower())
            {
                
                //enable the animator
                animator.enabled = true;
                //play the animation
                animator.Play(animationClip.name);
                //wait for the animation to finish
                StartCoroutine(waitForAnimation(animator));
                //disable the animator
                animator.enabled = false;
                break;
            }
        }
        
    }

IEnumerator waitForAnimation(Animator objectAnimator)
    {
        yield return new WaitForSeconds(objectAnimator.GetCurrentAnimatorStateInfo(0).length);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
