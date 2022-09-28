using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
   [Header("Limits")]
   [SerializeField] private float verticalAngel;
   private Camera _mainCamera;

   private Vector3 _direction;
   private Vector3 _tempDirection;
   private Quaternion _rotation;

   private float _currentVerticalAngle;
   private bool _canMove;
   private Quaternion _defaultRotation;

   private void Start()
   {
      Cursor.lockState = CursorLockMode.Locked;
      _mainCamera = Camera.main;
      _defaultRotation = _mainCamera.transform.localRotation;
      _direction = transform.forward;
   }

   private void OnEnable() => PlayerController.DontMoving += PlayerDontMoving;

   private void OnDisable() => PlayerController.DontMoving -= PlayerDontMoving;


   private void Update()
   {
      if (_canMove == false)
      {
         ResetPosition();
         return;
      }
      if(Cursor.lockState != CursorLockMode.Locked)
            return;
      
      float x = Input.GetAxis("Mouse X");
      float y = Input.GetAxis("Mouse Y");
      
      _tempDirection=  Quaternion.Euler(0, x, 0) * _direction;
      if (_tempDirection.x < 0.5f && _tempDirection.x > -0.5f)
         _direction = _tempDirection;
      
      _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle + y, -verticalAngel, verticalAngel);
      
      _rotation = Quaternion.LookRotation(_direction) * Quaternion.Euler(_currentVerticalAngle,0,0);

      _mainCamera.transform.rotation = _rotation;
   }

   private void ResetPosition()
   {
      _direction = transform.forward;
      _mainCamera.transform.localRotation = Quaternion.Lerp(_mainCamera.transform.localRotation, _defaultRotation,5.5f);
      _currentVerticalAngle = 0f;
      
   }  

   private bool PlayerDontMoving(Vector3 vector3)
   {
      if (vector3 == Vector3.zero)
      {
         _canMove = true;
         return true;
      }
      _canMove = false;
      return false;

   } 
   
}
