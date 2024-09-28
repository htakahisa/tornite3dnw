using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    private Camera cam;
    // Start is called before the first frame update
    void Start() {
        cam = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            cam.fieldOfView -= 30;
        }
        if (Input.GetMouseButtonUp(1)) {
            cam.fieldOfView += 30;
        }

    }

    protected bool WallCheck(Vector3 targetPosition, Vector3 desiredPosition, LayerMask wallLayers, out Vector3 wallHitPosition) {
        if (Physics.Raycast(targetPosition, desiredPosition - targetPosition, out RaycastHit wallHit, Vector3.Distance(targetPosition, desiredPosition), wallLayers, QueryTriggerInteraction.Ignore)) {
            wallHitPosition = wallHit.point;
            return true;
        } else {
            wallHitPosition = desiredPosition;
            return false;
        }
    }

}
