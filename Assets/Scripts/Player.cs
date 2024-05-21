using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public event EventHandler OnPickedSomething;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter _selectedCounter;
    }
    
    [SerializeField] float _moveSpeed = 8f;
    [SerializeField] GameInput _gameInput;
    [SerializeField] LayerMask _countersLayerMask;
    [SerializeField] Transform _counterTopPoint;

    bool _isWalking;
    Vector3 _lastInteractDir;
    BaseCounter _selectedCounter;
    KitchenObject _kitchenObject;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance!");
        }
        Instance = this;
    }
    void Start()
    {
        _gameInput.OnInteractAction += GameInput_OnInteractAction;
        _gameInput.OnInteractAlternateAction += GameInputOnOnInteractAlternateAction;
    }
    
    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    
    void GameInputOnOnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        
        if (_selectedCounter != null)
        {
            _selectedCounter.InteractAlternate(this);
        }
    }
    void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        
        if (_selectedCounter != null)
        {
            _selectedCounter.Interact(this);
        }
    }
    void HandleInteractions()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            _lastInteractDir = moveDir;
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractDir, out RaycastHit raycastHit, interactDistance, _countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // Has ClearCounter
                if (baseCounter != _selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
                else
                {
                    //SetSelectedCounter(null);
                }
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
        //Debug.Log(_selectedCounter);
    }
    void HandleMovement()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        
        // This is for smoother movement
        //Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        
        //This is for snappier movement
        Vector3 moveDir = new Vector3(Mathf.Round(inputVector.x), 0f, Mathf.Round(inputVector.y)).normalized;

        float moveDistance = _moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight,  playerRadius, moveDir, moveDistance);
                       

        if (!canMove)
        {
            // Cannot move towards this dir
            
            // Attempt only x movement

            Vector3 moveDirX = new Vector3(Mathf.Round(moveDir.x), 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && 
                      !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight,  playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Can move only on the x
                moveDir = moveDirX;
            }
            else
            {
                // Cannot move only on the x
                
                // Attempt only z movement
                Vector3 moveDirZ = new Vector3(0, 0, Mathf.Round(moveDir.z)).normalized;
                canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && 
                          !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight,  playerRadius, moveDirZ, moveDistance);
                
                if (canMove)
                {
                    // can move only on z
                    moveDir = moveDirZ;
                }
                else
                {
                    // cannot move in any dir
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * _moveSpeed * Time.deltaTime;
        }

        _isWalking = moveDir != Vector3.zero;
        float rotSpeed = 15f;
        transform.forward = Vector3.Slerp(transform.forward,moveDir, Time.deltaTime * rotSpeed);
    }
    public bool IsWalking()
    {
        return _isWalking;
    }
    void SetSelectedCounter(BaseCounter selectedCounter)
    {
        _selectedCounter = selectedCounter;
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            _selectedCounter = _selectedCounter,
        });
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return _counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;

        if (_kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }
    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
