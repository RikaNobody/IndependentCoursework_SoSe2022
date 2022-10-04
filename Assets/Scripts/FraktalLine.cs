using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FraktalLine : FraktalGenerator
{
    LineRenderer _lineRenderer;

    public FractalHUDManager hudManager;

    [SerializeField]
    public Material[] possibleColors = new Material[5];


    [Range(0, 1)]
    public float _lerpAmount;

    public float _generateMultiplier;

    Vector3[] _lerpPosition;

    bool aWasPressed, sWasPressed;

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
        if (hudManager.shapeChanged)
        {
            DrawShapeNew();
            KochFraktalGenerator(_targetPosition, true, _generateMultiplier);
            _lerpPosition = new Vector3[_position.Length];
            _lineRenderer.positionCount = _position.Length;
            _lineRenderer.SetPositions(_position);
            _lerpAmount = 0;
            hudManager.shapeChanged = false;
        }
        if (hudManager.colorIsChanged)
        {
            GetNewColor(hudManager.choosenColor);
            hudManager.colorIsChanged = false;
        }

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
                KochFraktalGenerator(_targetPosition, true, _generateMultiplier);
                _lerpPosition = new Vector3[_position.Length];
                _lineRenderer.positionCount = _position.Length;
                _lineRenderer.SetPositions(_position);
                _lerpAmount = 0;
                aWasPressed = true;
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                KochFraktalGenerator(_targetPosition, false, _generateMultiplier);
                _lerpPosition = new Vector3[_position.Length];
                _lineRenderer.positionCount = _position.Length;
                _lineRenderer.SetPositions(_position);
                _lerpAmount = 0;
                sWasPressed = true;
            }

            if (aWasPressed && sWasPressed)
            {
                Destroy(hudManager.tutorial);
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

    public void GetNewColor(int color)
    {
        switch (color)
        {
            case 0:
                _lineRenderer.material = possibleColors[0];
                Debug.Log("White");
                break;
            case 1:
                _lineRenderer.material = possibleColors[1];
                Debug.Log("Red");
                break;
            case 2:
                _lineRenderer.material = possibleColors[2];
                Debug.Log("Blue");
                break;
            case 3:
                _lineRenderer.material = possibleColors[3];
                Debug.Log("Green");
                break;
            case 4:
                _lineRenderer.material = possibleColors[4];
                Debug.Log("Purple");
                break;
        }
    }
}
