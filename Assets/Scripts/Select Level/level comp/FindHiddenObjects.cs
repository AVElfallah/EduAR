using Meta.WitAi.TTS.Utilities;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FindHiddenObjects : MonoBehaviour
{
    /// Creates a speaker that is used to play the sound. 
    public TTSSpeaker _speaker;

    /// Called when the prefab is created. This is where you can set properties

    public GameObject _objectPrefab;
    public int _objectCount = 0;
    public float moveFromZ = 2f;
    public float distance = 4.5f;
    public Text counter;
    public Text botText;
    public string objectsName = "Apples";
    public UnityEvent nextEvent;
    // Start is called before the first frame update


    /// Starts the Learn function. This is called by Unity's OnStart and OnStop functions

    void Start()
    {
       
        Coroutine x = StartCoroutine(PlayAndLearn());
    }

 


  

    /// Play and learn the game. This is the main loop of the game. It will loop until the user presses enter

    IEnumerator PlayAndLearn()
    {
        var speakIt = $"There are {_objectCount} hidden {objectsName} in your place.\nFind them.";
        botText.text = speakIt;
        _speaker.Speak(speakIt);
       
        _objectPrefab.transform.position = new Vector3(Random.Range(0, distance), Random.Range(0, distance), moveFromZ) ;
        _objectPrefab.GetComponent<OnTouchGameObject>().TouchAllowing();
   

        /// Creates a random object prefab and then calls Instantiate onTouchGameObject. touchAllowing on the object prefab.

        for (int i = 0; i < _objectCount; i++)
        {
            var pos = new Vector3(Random.Range(0, distance)/i, Random.Range(0, distance)/i, moveFromZ);
            var xprefab = Instantiate(_objectPrefab, pos,_objectPrefab.transform.localRotation);
           xprefab.GetComponent<OnTouchGameObject>().TouchAllowing();
       
        }
      _objectPrefab.SetActive(false);



        /// Yields a wait time until the object count is equal to the number of objects in the counter.

        while (true)
        {
            int countObjectes = int.Parse(counter.text);

            /// This method is called when the number of objects is equal to the number of objects in the object count.

            if (countObjectes == _objectCount)
            {
                botText.text="Very good.\n you have achieved a good progress";
                _speaker.Speak("Very good, you have achieved a good progress");
                
                break;
            }
            
            yield return new WaitForSeconds(.4f);
        }
        
        yield return new WaitForSeconds(4);
        nextEvent.Invoke();
        this.gameObject.SetActive(false);
      
    }
    
    // Update is called once per frame

    /// Updates the state of the object. This is called every frame to ensure that the object is up to date

    void Update()
    {

    }
}
