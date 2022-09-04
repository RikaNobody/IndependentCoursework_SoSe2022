using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FraktalLine : FraktalGenerator
{
    LineRenderer _lineRenderer;
    public FractalHUDManager hudManager;


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
        SelectShape();

        if (hudManager.startButtonPressed)
        {
            //Debug.Log("INITIATOR: " + initiator);

            _lerpAmount = hudManager.GetComponent<FractalHUDManager>().lerpAmountValue;
            _generateMultiplier = hudManager.GetComponent<FractalHUDManager>()._multiplierValue;
            _useBezierCurves = hudManager.GetComponent<FractalHUDManager>().useBezier;
            _bezierVertexCount = hudManager.GetComponent<FractalHUDManager>().bezierVertexCount;

            if (_generationCount != 0)
            {
                for (int i = 0; i < _position.Length; i++)
                {
                    _lerpPosition[i] = Vector3.Lerp(_position[i], _targetPosition[i], _lerpAmount);

                }

                if (_useBezierCurves)
                {
                    _bezierPosition = BezierCurve(_lerpPosition, _bezierVertexCount);
                    _lineRenderer.positionCount = _bezierPosition.Length;
                    _lineRenderer.SetPositions(_bezierPosition);
                }
                else
                {
                    _lineRenderer.positionCount = _lerpPosition.Length;
                    _lineRenderer.SetPositions(_lerpPosition);
                }


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

    void SelectShape()
    {
        Debug.Log("SELECT SHAPE");
        int dropDownValue = hudManager.GetComponent<FractalHUDManager>().chooseShapeDropdown.value;
        switch (dropDownValue)
        {
            case 0:
                initiator = _initiator.Triangle;
                Debug.Log("CASE 0");
                break;
            case 1:
                initiator = _initiator.Square;
                Debug.Log("CASE 1");
                break;
            case 2:
                initiator = _initiator.Pentagon;
                Debug.Log("CASE 2");
                break;
            case 3:
                initiator = _initiator.Hexagon;
                Debug.Log("CASE 3");
                break;
            case 4:
                initiator = _initiator.Heptagon;
                Debug.Log("CASE 4");
                break;
            case 5:
                initiator = _initiator.Octagon;
                Debug.Log("CASE 5");
                break;
            default:
                initiator = _initiator.Triangle;
                break;

        }
    }
}
