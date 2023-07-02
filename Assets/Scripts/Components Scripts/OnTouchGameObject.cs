using UnityEngine;
using UnityEngine.UI;
public class OnTouchGameObject : MonoBehaviour
{
    private bool isTouched = false;
    public bool isAllowToBeTouched => isTouched;
    public Text counter;
    /// Allow touch events to be sent to the game object. This is useful for checking if you are touching a GameObject
    public void TouchAllowing()
    {
        Debug.Log(this.gameObject.name + " touch allowed");
        isTouched = true;
    }
    /// Checks if the game object has touched and if so removes it from the game object list. This is called every frame
    void Update()
    {
        /// This method will be called when the user clicks on the screen.
        if (isAllowToBeTouched)
        {
            /// This method will be called when the user presses a touch.
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Get the touch position
                Vector2 touchPosition = Input.GetTouch(0).position;
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                RaycastHit hit;
                /// This method will be called when a Physics Raycast is called.
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Touched " + hit.transform.gameObject.name);

                    // Destroy the game object that was touched
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
