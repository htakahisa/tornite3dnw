using UnityEngine;

public class Billboard : MonoBehaviour {
    void Update() {
        // �J�����̕������擾
        Vector3 cameraDirection = Camera.main.transform.position - transform.position;

        // �I�u�W�F�N�g�̌������J�����̕����ɐݒ�
        transform.rotation = Quaternion.LookRotation(cameraDirection);
    }
}
