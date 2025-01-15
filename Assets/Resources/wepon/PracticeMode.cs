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
        //    Practice();
        }

    }

    public void Practice() {
        SceneManager.LoadScene("Practice");
    }

    public void Dictionary() {
        SceneManager.LoadScene("Dictionary");
    }

    public void Collecting()
    {
        SceneManager.LoadScene("Collecting");
    }

    public void Classic()
    {
        SceneManager.LoadScene("Classic");
    }

    public void Silver()
    {
        SceneManager.LoadScene("Silver");
    }

    public void Yor()
    {
        SceneManager.LoadScene("Yor");
    }

    public void Noel()
    {
        SceneManager.LoadScene("Noel");
    }

    public void Duelist()
    {
        SceneManager.LoadScene("Duelist");
    }
    public void Lounge()
    {
        SceneManager.LoadScene("lounge");
    }

}
