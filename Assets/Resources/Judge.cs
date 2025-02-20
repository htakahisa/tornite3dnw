using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Judge : MonoBehaviour {


    Text text;
    static string judgement;

    public static Judge judge;

    private void Awake()
    {
        judge = this;
    }


    // Start is called before the first frame update
    void Start() {

        text = gameObject.GetComponent<Text>();

        
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
    public void Win()
    {

        judgement = "Win";

    }

    public void Lose()
    {

        judgement = "Lose";

    }

    public IEnumerator TextChange() {
        text.text = judgement;
        yield return new WaitForSeconds(3); // 2•b‘Ò‚Â
        text.text = "";
    }
}
