using System;
using Infrastructure.Services;
using Services.Input;
using UnityEngine;

namespace Logic.Input
{
    public class SwipeDetector : MonoBehaviour
    {
        [SerializeField] private float _minDistanceForSwipe = 20f;
        [SerializeField] private bool _detectSwipeOnlyAfterRelease = false;

        private Vector2 _fingerDownPosition;
        private Vector2 _fingerUpPosition;

        private IInputService _inputService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            foreach (Touch touch in UnityEngine.Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _fingerUpPosition = touch.position;
                    _fingerDownPosition = touch.position;
                }

                if (!_detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
                {
                    _fingerDownPosition = touch.position;
                    DetectSwipe();
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    _fingerDownPosition = touch.position;
                    DetectSwipe();
                }
            }
        }

        private void DetectSwipe()
        {
            if (SwipeDistanceCheckMet())
            {
                if (IsVerticalSwipe())
                {
                    var direction = _fingerDownPosition.y - _fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                    SendSwipe(direction);
                }
                else
                {
                    var direction = _fingerDownPosition.x - _fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                    SendSwipe(direction);
                }
                _fingerUpPosition = _fingerDownPosition;
            }
            else
            {
                _inputService.InvokeOnTap();
            }
        }

        private bool IsVerticalSwipe()
        {
            return VerticalMovementDistance() > HorizontalMovementDistance();
        }

        private bool SwipeDistanceCheckMet()
        {
            return VerticalMovementDistance() > _minDistanceForSwipe || HorizontalMovementDistance() > _minDistanceForSwipe;
        }

        private float VerticalMovementDistance()
        {
            return Mathf.Abs(_fingerDownPosition.y - _fingerUpPosition.y);
        }

        private float HorizontalMovementDistance()
        {
            return Mathf.Abs(_fingerDownPosition.x - _fingerUpPosition.x);
        }

        private void SendSwipe(SwipeDirection direction)
        {
            SwipeData swipeData = new SwipeData()
            {
                Direction = direction,
                StartPosition = _fingerDownPosition,
                EndPosition = _fingerUpPosition
            };
        
            _inputService.InvokeOnSwipe(swipeData);
        }
    }

    public struct SwipeData
    {
        public Vector2 StartPosition;
        public Vector2 EndPosition;
        public SwipeDirection Direction;
    }

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}