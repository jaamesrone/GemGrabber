using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Observer
{
    private bool _isTurboOn;
    private Vector3 _initialPos;
    private float _shakeMagnitude = 0.1f;
    private BikeController _bikeController;

    private void OnEnable()
    {
        _initialPos = gameObject.transform.localPosition;
    }

    private void Update()
    {
        if (_isTurboOn)
        {
            gameObject.transform.localPosition = _initialPos + (Random.insideUnitSphere * _shakeMagnitude);
        }
        else
        {
            gameObject.transform.localPosition = _initialPos;
        }
    }

    public override void Notify(Subject subject)
    {
        //throw new System.NotImplementedException();
        if (!_bikeController)
        {
            _bikeController = subject.GetComponent<BikeController>();
        }
        if (_bikeController)
        {
            _isTurboOn = _bikeController.isTurboOn;
        }
    }
}
