using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

    public static TargetManager tm = null;
    private float Interval = 0;
    private float FlashIntervalCount = 0;
    private float FlashInterval = 0;

    public List<GameObject> flash;

    void Start() {
       tm = this;
       FlashInterval = Random.Range(5f, 10f);
    }

    private void Update() {
        Interval += Time.deltaTime;
        FlashIntervalCount += Time.deltaTime;
        if (Interval >= 1.5f) {
            ActivateRandomChildObject();
        }
        if (FlashIntervalCount >= FlashInterval) {
           // ActivateRandomFlash();
        }
    }
    public void ActivateRandomFlash() {
        FlashIntervalCount = 0;
        FlashInterval = Random.Range(5f, 10f);


        // �����_����1�̎q�I�u�W�F�N�g��I��
        int randomIndex = Random.Range(0, flash.Count);
        GameObject selectedFlash = flash[randomIndex];
        // �v���n�u�̏����ʒu�Ɖ�]���w��
        Vector3 initialPosition = selectedFlash.transform.position;
        Quaternion initialRotation = selectedFlash.transform.rotation;

        Instantiate(selectedFlash, initialPosition, initialRotation);

    }


    public void ActivateRandomChildObject() {
        Interval = 0;
        // �e�I�u�W�F�N�g�̑S�Ă̎q�I�u�W�F�N�g���擾
        List<GameObject> children = GetAllChildObjects(gameObject);

        if (children.Count == 0) return;

        // �����_����1�̎q�I�u�W�F�N�g��I��
        int randomIndex = Random.Range(0, children.Count);
        GameObject selectedChild = children[randomIndex];

        // ���ׂĂ̎q�I�u�W�F�N�g�̃A�N�e�B�u��Ԃ�ݒ�
        foreach (GameObject child in children) {
            child.SetActive(child == selectedChild);
        }
       
    }
 

    List<GameObject> GetAllChildObjects(GameObject parent) {
        List<GameObject> childObjects = new List<GameObject>();
        foreach (Transform child in parent.transform) {
            childObjects.Add(child.gameObject);
            // �ċA�I�ɂ���Ɏq�I�u�W�F�N�g���擾����
            childObjects.AddRange(GetAllChildObjects(child.gameObject));
        }
        return childObjects;
    }
}
