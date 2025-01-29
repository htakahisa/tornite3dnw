using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatEffect : MonoBehaviour {

    [SerializeField]
    private GameObject effect1;
    [SerializeField]
    private GameObject effect2;
    [SerializeField]
    private GameObject effect3;


    private Image image;

    public static CatEffect pfe;

    private bool IsStun = false;

    public void ApplyFlash(float duration) {
        StartCoroutine(FlashRoutine(duration));

    }

    private void Awake() {
        

        pfe = this;
    }

    private IEnumerator FlashRoutine(float duration) {

        int num = Random.Range(1, 5);

        GameObject effect = null;
        if (num == 1 || num == 2)
        {
            effect = effect1;
        }
        else if (num == 3)
        {
            effect = effect2;
        }
        else 
        {
            effect = effect3;
        }


        effect.SetActive(true); // �t���b�V���I�[�o�[���C��\��
        yield return new WaitForSeconds(duration); // �w�肳�ꂽ���ԑ҂�
        if (effect != null)
        {
            effect.SetActive(false); // �t���b�V���I�[�o�[���C���\��
        }
    }



}
