using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FraktalGenerator : MonoBehaviour
{
    protected enum _initiator
    {
        Triangle,
        Square,
        Pentagon,
        Hexagon,
        Heptagon,
        Octagon
    };

    protected enum _axis
    {
        XAxis,
        YAxis,
        ZAxis
    };

    public struct LineSegment
    {
        public Vector3 StartPosition { get; set; }
        public Vector3 EndPosition { get; set; }
        public Vector3 Direction { get; set; }
        public float Length { get; set; }
    }

    [SerializeField]
    protected _axis axis = new _axis();

    [SerializeField]
    protected _initiator initiator = new _initiator();

    [SerializeField]
    protected AnimationCurve _generator;
    protected Keyframe[] _keyframes;

    protected int _generationCount;

    protected int _initiatorPointAmount;

    private Vector3[] _initiatorPoint;
    private Vector3 _rotateVector;
    private Vector3 _rotatateAxis;
    [SerializeField]
    protected float _initiatorSize;

    private float _initaialRotation;

    protected Vector3[] _position;
    protected Vector3[] _targetPosition;
    private List<LineSegment> _lineSegment;


    private void Awake()
    {
        GetInitiatorPoints();

        _position = new Vector3[_initiatorPointAmount + 1];
        _targetPosition = new Vector3[_initiatorPointAmount + 1];
        _lineSegment = new List<LineSegment>();
        _keyframes = _generator.keys;

        _rotateVector = Quaternion.AngleAxis(_initaialRotation, _rotatateAxis) * _rotateVector;

        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            _position[i] = _rotateVector * _initiatorSize;
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotatateAxis) * _rotateVector;
        }

        _position[_initiatorPointAmount] = _position[0];
        _targetPosition = _position;
    }

    protected void KochFraktalGenerator(Vector3[] positions, bool outwards, float generatorMultiplier)
    {
        _lineSegment.Clear();
        for (int i = 0; i < positions.Length; i++)
        {
            LineSegment line = new LineSegment();
            line.StartPosition = positions[i];
            if (i == positions.Length - 1)
            {
                line.EndPosition = positions[0];
            }
            else
            {
                line.EndPosition = positions[i + 1];
            }
            line.Direction = (line.EndPosition - line.StartPosition).normalized;
            line.Length = Vector3.Distance(line.EndPosition, line.StartPosition);
            _lineSegment.Add(line);
        }
        List<Vector3> newPosition = new List<Vector3>();
        List<Vector3> targetPosition = new List<Vector3>();

        for (int i = 0; i < _lineSegment.Count; i++)
        {
            newPosition.Add(_lineSegment[i].StartPosition);
            targetPosition.Add(_lineSegment[i].EndPosition);

            for (int j = 0; i < _keyframes.Length - 1; j++)
            {
                float moveAmount = _lineSegment[i].Length * _keyframes[j].time;
                float hightAmount = (_lineSegment[i].Length * _keyframes[j].value) * generatorMultiplier;
                Vector3 movePosition = _lineSegment[i].StartPosition + (_lineSegment[i].Direction * moveAmount);
                Vector3 direction;
                if (outwards)
                {
                    direction = Quaternion.AngleAxis(-90, _rotatateAxis) * _lineSegment[i].Direction;
                }
                else
                {
                    direction = Quaternion.AngleAxis(90, _rotatateAxis) * _lineSegment[i].Direction;
                }
                newPosition.Add(movePosition);
                targetPosition.Add(movePosition + (direction * hightAmount));
            }
        }
        newPosition.Add(_lineSegment[0].StartPosition);
        targetPosition.Add(_lineSegment[0].StartPosition);
        _position = new Vector3[newPosition.Count];
        _targetPosition = new Vector3[targetPosition.Count];
        _position = newPosition.ToArray();
        _targetPosition = targetPosition.ToArray();

        _generationCount++;
    }

    private void OnDrawGizmos()
    {
        GetInitiatorPoints();
        _initiatorPoint = new Vector3[_initiatorPointAmount];
        _rotateVector = Quaternion.AngleAxis(_initaialRotation, _rotatateAxis) * _rotateVector;

        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            _initiatorPoint[i] = _rotateVector * _initiatorSize;
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotatateAxis) * _rotateVector;
        }

        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            Gizmos.color = Color.white;
            Matrix4x4 rotateMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.matrix = rotateMatrix;

            if (i < _initiatorPointAmount - 1)
            {
                Gizmos.DrawLine(_initiatorPoint[i], _initiatorPoint[i + 1]);
            }
            else
            {
                Gizmos.DrawLine(_initiatorPoint[i], _initiatorPoint[0]);
            }
        }
    }
    private void GetInitiatorPoints()
    {
        switch (initiator)
        {
            case _initiator.Triangle:
                _initiatorPointAmount = 3;
                _initaialRotation = 0;
                break;
            case _initiator.Square:
                _initiatorPointAmount = 4;
                _initaialRotation = (360 / _initiatorPointAmount) / 2;
                break;
            case _initiator.Pentagon:
                _initiatorPointAmount = 5;
                _initaialRotation = (360 / _initiatorPointAmount) / 2;
                break;
            case _initiator.Hexagon:
                _initiatorPointAmount = 6;
                _initaialRotation = (360 / _initiatorPointAmount) / 2;
                break;
            case _initiator.Heptagon:
                _initiatorPointAmount = 7;
                _initaialRotation = (360 / _initiatorPointAmount) / 2;
                break;
            case _initiator.Octagon:
                _initiatorPointAmount = 8;
                _initaialRotation = (360 / _initiatorPointAmount) / 2;
                break;
            default:
                _initiatorPointAmount = 3;
                _initaialRotation = (360 / _initiatorPointAmount) / 2;
                break;
        }

        switch (axis)
        {
            case _axis.XAxis:
                _rotateVector = new Vector3(1, 0, 0);
                _rotatateAxis = new Vector3(0, 0, 1);
                break;

            case _axis.YAxis:
                _rotateVector = new Vector3(0, 1, 0);
                _rotatateAxis = new Vector3(1, 0, 0);
                break;
            case _axis.ZAxis:
                _rotateVector = new Vector3(0, 0, 1);
                _rotatateAxis = new Vector3(0, 1, 0);
                break;
            default:
                _rotateVector = new Vector3(0, 1, 0);
                _rotatateAxis = new Vector3(1, 0, 0);
                break;
        }
    }
}
