using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDrag : MonoBehaviour
{
    [SerializeField] private bool _isDragable;
    [SerializeField] private UnitParameters _unitParameter;
    [SerializeField] private float _offset;
    [SerializeField] private Unit _mergeTarget;
    [Space]
    [SerializeField] private DragState _dragState;
    [SerializeField] private UnitDragObject _dragObjectPrefab;
    [SerializeField] private UnitDragObject _dragObject;
    [SerializeField] private Transform _dragObjectTransform;
    [SerializeField] private Vector3 _mousePosition;

    private Button _attachedButton;

    public UnitDragObject DragObjectPrefab { get => _dragObjectPrefab; }
    public bool IsDragable { get => _isDragable; set => _isDragable = value; }
    public UnitParameters UnitParameter { get => _unitParameter; set => _unitParameter = value; }
    public Unit MergeTarget { get => _mergeTarget; set => _mergeTarget = value; }

    private enum DragState
    {
        NotDrag,
        Begin,
        Drag,
        End
    }

    private void Start()
    {
        _attachedButton = GetComponent<Button>();
        _dragObjectTransform = MainCanvas.Instance.transform;
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _offset;

        _mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnMouseDown()
    {
        if (_isDragable)
        {
            _dragState = DragState.Begin;
            CreateDragImage();
        }
    }

    private void OnMouseDrag()
    {
        if (_isDragable)
        {
            _dragState = DragState.Drag;

            if (_dragObject != null)
            {
                _dragObject.transform.position = new Vector3(_mousePosition.x, _mousePosition.y, transform.position.z);
            }
        }
    }

    private void OnMouseUp()
    {
        if (_isDragable)
        {
            _dragState = DragState.NotDrag;

            DestroyDragImage();

            if (_mergeTarget != null)
            {
                _mergeTarget.Merge(GetComponent<Unit>());
            }
        }
    }

    private void CreateDragImage()
    {
        _dragObject = Instantiate(_dragObjectPrefab, _mousePosition, Quaternion.identity, _dragObjectTransform);
        _dragObject.SetupObject(_unitParameter, GetComponent<Unit>());
    }

    private void DestroyDragImage()
    {
        Destroy(_dragObject.gameObject, 0.1f);
    }
}
