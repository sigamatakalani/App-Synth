using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public float currCountdownValue;
    string sceneNameVar;
    bool on = false;

    public void openNewScene(string sceneName) 
    {
        sceneNameVar = sceneName;
        on = true;
        StartCoroutine(StartCountdown());
        Debug.Log("enter: " + currCountdownValue);
    }

    public void stopOpenScene()
    {
        on = false;
        currCountdownValue = 0f;
        Debug.Log("exit: " + currCountdownValue);
    }

    void Update()
    {
        if (currCountdownValue == 0 && sceneNameVar != null && on) {
            SceneManager.LoadScene(sceneNameVar);
        }
        
    }

    public IEnumerator StartCountdown(float countdownValue = 4)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
    }
}
