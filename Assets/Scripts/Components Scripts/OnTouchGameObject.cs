using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnTouchGameObject : MonoBehaviour
{
    private bool isTouched = false;
    public bool isAllowToBeTouched => isTouched;
    public AudioClip onClickAudio;
    public AudioSource audioSource;
    public Text counter;
    // Start is called before the first frame update


    void Start()
    {

    }
    public void TouchAllowing()
    {
        Debug.Log(this.gameObject.name+" touch allowed");
        isTouched = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log("mouse touched");
        if (isAllowToBeTouched)
        {
            audioSource.PlayOneShot(onClickAudio);
            new WaitForSeconds(2f);
            Debug.Log("is game touched");
            Destroy(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        int xi = int.Parse(counter.text);
        xi += 1;
        counter.text = xi.ToString();
    }
}
