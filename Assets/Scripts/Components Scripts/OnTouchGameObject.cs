using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnTouchGameObject : MonoBehaviour
{

    private bool isTouched = false;
    public bool isAllowToBeTouched => isTouched;
    public Text counter;
    // Start is called before the first frame update


    public void TouchAllowing()
    {
        Debug.Log(this.gameObject.name + " touch allowed");
        isTouched = true;
    }

    void Update()
    {
        if (isAllowToBeTouched)
        {

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Get the touch position
                Vector2 touchPosition = Input.GetTouch(0).position;

                Ray ray = Camera.main.ScreenPointToRay(touchPosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Touched " + hit.transform.gameObject.name);
                    
                    Debug.Log("Touched " + hit.transform.gameObject.name);
                    // Destroy the game object that was touched
                    Destroy(hit.transform.gameObject);
                }

            }

        }
    }

    private void OnDestroy()
    {
        int xi = int.Parse(counter.text);
        xi += 1;
        counter.text = xi.ToString();
    }
}
