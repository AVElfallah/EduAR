using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Meta.WitAi.TTS.Utilities;
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
    public GameObject counterWithImage;
    public Text botText;
    public string objectsName = "Apples";
    public UnityEvent nextEvent;
    /// Play and learn the bot. This is a coroutine. You can use it as a yield statement in a foreach statement.
    /// 
    /// 
    /// @return The coroutine that will start the game loop. This coroutine will return when the bot has finished playing and learn
    public IEnumerator PlayAndLearn()
    {
        counterWithImage.SetActive(true);
        var speakIt = $"There are {_objectCount} hidden {objectsName} in your place.\nFind them.";
        botText.text = speakIt;
        _speaker.Speak(speakIt);
        /// Yields a new wait time.
        while (_speaker.IsSpeaking || _speaker.IsLoading)
        {
            yield return new WaitForSeconds(0.1f);
        }
        _objectPrefab.transform.position = new Vector3(
            Random.Range(0, distance),
            Random.Range(0, distance),
            moveFromZ
        );
        _objectPrefab.GetComponent<OnTouchGameObject>().TouchAllowing();
        List<GameObject> gameObjects = new List<GameObject>();
        gameObjects.Add(_objectPrefab);
        /// Creates a new game object and adds it to the list of Game Objects.
        for (int i = 1; i < _objectCount; i++)
        {
            var pos = new Vector3(
                Random.Range(0, distance) / i,
                Random.Range(0, distance) / i,
                moveFromZ
            );
            var xprefab = Instantiate(_objectPrefab, pos, _objectPrefab.transform.localRotation);
            xprefab.GetComponent<OnTouchGameObject>().TouchAllowing();
            gameObjects.Add(xprefab);
        }
        /// Yields a wait time until the object count is equal to the number of objects in the counter.
        Text counter = counterWithImage.GetComponentInChildren<Text>();
        /// This method is called when the number of objects in
        ///  the game objects is equal to the number of objects in the game objects.
        while (true)
        {
            int destroyedCount = gameObjects.Where(x => x == null).Count();
            counter.text = destroyedCount.ToString();
            int countObjectes = int.Parse(counter.text);
            /// This method is called when the number of objects is equal to the number of objects in the object count.
            /// Yields a wait for the object to be loaded.
            if (countObjectes == _objectCount)
            {
                botText.text = "Very good.\n you have achieved a good progress";
                _speaker.Speak("Very good, you have achieved a good progress");
                /// Yields a new wait time.
                while (_speaker.IsSpeaking || _speaker.IsLoading)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(4);
        nextEvent.Invoke();
        counterWithImage.SetActive(false);
    }
}
