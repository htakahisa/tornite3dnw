using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Judge : MonoBehaviour {


    Text text;
    static string judgement;

    // Start is called before the first frame update
    void Start() {

        text = gameObject.GetComponent<Text>();

        StartCoroutine(Texture());
    }

    // Update is called once per frame
    void Update() {

    }

    public void Upset() {


        judgement = "Upset";

    }

    public void Assassin() {

        judgement = "Assassin";
    }

    public void Reckless() {

        judgement = "Reckless";

    }

    public void Olive() {

        judgement = "Olive";

    }


    IEnumerator Texture() {
        text.text = judgement;
        yield return new WaitForSeconds(3); // 2•b‘Ò‚Â
        text.text = "";
    }
}
