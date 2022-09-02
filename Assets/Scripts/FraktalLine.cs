using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FraktalLine : FraktalGenerator
{
    LineRenderer _lineRenderer;

    [Range(0, 1)]
    public float _lerpAmount;

    public float _generateMultiplier;
    Vector3[] _lerpPosition;
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.positionCount = _position.Length;
        _lineRenderer.SetPositions(_position);
    }


    void Update()
    {
        if (_generationCount != 0)
        {
            for (int i = 0; i < _position.Length; i++)
            {
                _lerpPosition[i] = Vector3.Lerp(_position[i], _targetPosition[i], -_lerpAmount);
            }

            _lineRenderer.SetPositions(_lerpPosition);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log(" - A - GEDR�CK DU EUMEL");
            KochFraktalGenerator(_targetPosition, true, _generateMultiplier);
            _lerpPosition = new Vector3[_position.Length];
            _lineRenderer.positionCount = _position.Length;
            _lineRenderer.SetPositions(_position);
            _lerpAmount = 0;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log(" - S - GEDR�CK DU EUMEL");
            KochFraktalGenerator(_targetPosition, false, _generateMultiplier);
            _lerpPosition = new Vector3[_position.Length];
            _lineRenderer.positionCount = _position.Length;
            _lineRenderer.SetPositions(_position);
            _lerpAmount = 0;
        }
    }
}
