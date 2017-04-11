using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Private variables
    private Camera _mainCamera;
    private Vector3 stageDimensions;
    private float targetHighY;
    private float targetLowY;
    private bool _mustReachHighY;
    private bool _mustReset;
    private Vector3 _initialPosition;
    private Vector3 _initialCamSize;

    [SerializeField]
    private float minimumBlocHeight = 4.0f;

    #endregion // Private variables

    void Start()
    {
        _mainCamera = Camera.main;
        _initialPosition = _mainCamera.transform.position;
        _initialCamSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    private void LateUpdate()
    {
        GameObject[] blockArray = GameObject.FindGameObjectsWithTag("Bloc");
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float _maxBlocY = 0.0f;
        foreach (var bloc in blockArray) {
            if (bloc.transform.position.y > _maxBlocY) {
                _maxBlocY = bloc.transform.position.y;
            }
        }

        if (_maxBlocY > minimumBlocHeight) {
            if (_maxBlocY >= stageDimensions.y - 1.0f) {
                _mainCamera.orthographicSize += 0.5f;
                targetHighY = _mainCamera.transform.position.y + 0.5f;
                _mainCamera.transform.position = new Vector3(_mainCamera.transform.position.x, targetHighY, _mainCamera.transform.position.z);
            }
            else if (_maxBlocY < stageDimensions.y - 2.0f) {
                _mainCamera.orthographicSize -= 0.5f;
                targetHighY = _mainCamera.transform.position.y - 0.5f;
                _mainCamera.transform.position = new Vector3(_mainCamera.transform.position.x, targetHighY, _mainCamera.transform.position.z);
            }
        }


        /*if (_mainCamera.transform.position.y < targetHighY) {
            _mustReachHighY = true;
        }
        else {
            _mustReachHighY = false;
        }
        */
        // For smooth
        //float lerpTargetY = Mathf.Lerp(_mainCamera.transform.position.y, targetHighY, 10.0f * Time.deltaTime);

        /*
        if (_mustReset) {
            _mainCamera.transform.position = _initialPosition;
            _mainCamera.orthographicSize = _initialCamSize.y;
        }
        

        if (_mustReachHighY) {
            _mainCamera.transform.position = new Vector3(_mainCamera.transform.position.x, targetHighY, _mainCamera.transform.position.z);
        }
        */
    }
}
