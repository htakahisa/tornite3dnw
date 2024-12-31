using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PracticeMode : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Practice();
        }

    }

    public void Practice() {
        SceneManager.LoadScene("Practice");
    }

    public void Dictionary() {
        SceneManager.LoadScene("Dictionary");
    }
}
