using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTowers
{
    public class ConstructHandler : MonoBehaviour
    {
        #region Private variables
        private BlockManager blockManager;
        private HUDController hudController;
        private MunitionRepository munitionRepository;
        private InputManager _inputManager;
        private List<GameObject> _blocList; // List of bloc "materials" we'll work with

        // Current bloc attributes
        private GameObject _instantiatedBloc; // Keep reference of the current bloc
        private Rigidbody2D _blocRigidbody; // Rigidbody of the current bloc
        private BoxCollider2D _blocCollider; // Collider of the current bloc

        private bool _isNewBlocNeeded; // false by default
        private bool _isResetPoint; // false by default

        private Vector3 _newStartingPosition; // Used to store the new instantiate pos (Y will reset it to _startingPoint)
        private Quaternion _newStartingRotation; // Used to store the new instantiate rot (Y will reset it to _startingPoint)
        private GameObject _playerBlocContainer; // GameObject container to store player's instantiated blocs

        private bool _isDPHorizontalUsed; // To know if the horizontal DPad has been used
        private bool _isDPVerticalUsed; // To know if the vertical DPad has been used
        private float _minMoveWait = 0.08f; // Minimum time (in sec) before we can move more
        private float _lastVMoveTime = 0.0f;
        private float _lastHMoveTime = 0.0f;

        private Color _spriteInitialColor; // Store the initial sprite color to apply opacity for the one in use

        // Box Limits
        private Transform _topLimit;
        private Transform _rightLimit;
        private Transform _leftLimit;
        private Transform _bottomLimit;

        private float _minDeltaXY = 0.1f;
        private int _currentBlocLevel;

        [SerializeField]
        [Tooltip("Starting point to construct")]
        private Transform _startingPoint;

        [SerializeField]
        [Tooltip("Array of different bloc materials")]
        private GameObject[] _myBlocs;
        #endregion // Private variables

        #region Public variables
        [Tooltip("Player's ID (1 for player 1)")]
        public int _playerID;
        // TODO REFACTOR PLEASE CALL FLORIAN
        public limitZoneResize _myLimitZone;
        #endregion // Public variables

        private void Awake()
        {
            _inputManager = InputManager.getInstance();

            // Debug reminders if we forget to setup things correctly
            if (_myBlocs == null || _myBlocs.Length <= 0) {
                Debug.LogWarning("The material bloc list is empty, please put at least one GameObject");
            }

            if (_playerID != 1 && _playerID != 2) {
                Debug.LogWarning("You forgot to assign player ID to 1 or 2");
            }

            // If we forgot to assign the starting, try to find it or display a warning
            if (_startingPoint == null) {
                if (_playerID == 1)
                    _startingPoint = GameObject.Find("Starting Point P1").transform;
                if (_playerID == 2)
                    _startingPoint = GameObject.Find("Starting Point P2").transform;
                if (_startingPoint == null)
                    Debug.LogWarning("You forgot to assign or create the starting point");
            }

            // Find the children limits and initialize Vector3 variables
            foreach (Transform child in this.gameObject.transform) {
                switch (child.tag) {
                    case "TopLimit":
                        _topLimit = child.transform;
                        break;
                    case "RightLimit":
                        _rightLimit = child.transform;
                        break;
                    case "LeftLimit":
                        _leftLimit = child.transform;
                        break;
                    case "BottomLimit":
                        _bottomLimit = child.transform;
                        break;
                    default:
                        break;
                }
            }
        }

        internal void changeSelectedBlock()
        {
            Destroy(_instantiatedBloc);
            _isNewBlocNeeded = true;
        }

        private void Start()
        {
            blockManager = LevelManager.getInstance().getBlockManager();
            munitionRepository = LevelManager.getInstance().getMunitionRepository();
            hudController = LevelManager.getInstance().getHUDController();
            _blocList = new List<GameObject>();

            // Create player new bloc container
            _playerBlocContainer = new GameObject("Player bloc container");

            foreach (GameObject materials in _myBlocs) {
                _blocList.Add(materials);
            }

            // Setup the 4 limits
            GridManagement.AlignTransform(_topLimit);
            _topLimit.position = GridManagement.NearestGridPos(_topLimit.position);

            GridManagement.AlignTransform(_bottomLimit);
            _bottomLimit.position = GridManagement.NearestGridPos(_bottomLimit.position);

            GridManagement.AlignTransform(_rightLimit);
            _rightLimit.position = GridManagement.NearestGridPos(_rightLimit.position);

            GridManagement.AlignTransform(_leftLimit);
            _leftLimit.position = GridManagement.NearestGridPos(_leftLimit.position);

            setNewBloc();
        }

        private void Update()
        {
            #region Controls
            // Handle the Left Joystick Horizontal movement as a button to move smoothly (timer) in the grid
            float moveTime = Time.time - _lastHMoveTime;
            if (_inputManager.GetAxis(SpaceTower.Inputs.LHorizontalPlayer, _playerID) != 0 && moveTime > _minMoveWait) {
                if (_inputManager.GetAxis(SpaceTower.Inputs.LHorizontalPlayer, _playerID) == +1) {
                    if (!IsInTheGrid(new Vector3(1, 0, 0)))
                        return;
                    _instantiatedBloc.transform.position += new Vector3(1, 0, 0);
                    GridManagement.AlignTransform(_instantiatedBloc.transform);
                }
                else if (_inputManager.GetAxis(SpaceTower.Inputs.LHorizontalPlayer, _playerID) == -1) {
                    if (!IsInTheGrid(new Vector3(-1, 0, 0)))
                        return;
                    _instantiatedBloc.transform.position += new Vector3(-1, 0, 0);
                    GridManagement.AlignTransform(_instantiatedBloc.transform);
                }
                _lastHMoveTime = Time.time;
            }

            // Handle the Left Joystick Vertical movement as a button to move smoothly (timer) in the grid
            moveTime = Time.time - _lastVMoveTime;
            if (_inputManager.GetAxis(SpaceTower.Inputs.LVerticalPlayer, _playerID) != 0 && moveTime > _minMoveWait) {
                if (_inputManager.GetAxis(SpaceTower.Inputs.LVerticalPlayer, _playerID) == +1) {
                    if (!IsInTheGrid(new Vector3(0, 1, 0)))
                        return;
                    _instantiatedBloc.transform.position += new Vector3(0, 1, 0);
                    GridManagement.AlignTransform(_instantiatedBloc.transform);
                }
                else if (_inputManager.GetAxis(SpaceTower.Inputs.LVerticalPlayer, _playerID) == -1) {
                    if (!IsInTheGrid(new Vector3(0, -1, 0)))
                        return;
                    _instantiatedBloc.transform.position += new Vector3(0, -1, 0);
                    GridManagement.AlignTransform(_instantiatedBloc.transform);
                }
                _lastVMoveTime = Time.time;
            }

            // RB button pressed, we rotate the current bloc by 90 degrees on its right
            if (_inputManager.GetButtonDown(SpaceTower.Inputs.RBumperPlayer, _playerID)) {
                _instantiatedBloc.transform.localRotation *= Quaternion.Euler(0, 0, -90);
            }

            // RB button pressed, we rotate the current bloc by 90 degree on its left
            if (_inputManager.GetButtonDown(SpaceTower.Inputs.LBumperPlayer, _playerID)) {
                _instantiatedBloc.transform.localRotation *= Quaternion.Euler(0, 0, 90);
            }

            // Y button pressed, reset all the bloc and start from the initial start position
            if (_inputManager.GetButtonDown(SpaceTower.Inputs.ButtonYPlayer, _playerID)) {
                foreach (Transform childBloc in _playerBlocContainer.transform) {
                    Destroy(childBloc.gameObject);
                    _isResetPoint = true;
                }
                _myLimitZone.setStopScaling(false);
                _isNewBlocNeeded = true;
            }

            // D'Pad Horizontal gestion as button
            if (_inputManager.GetAxis(SpaceTower.Inputs.DPHorizontalPlayer, _playerID) != 0) {
                if (_isDPHorizontalUsed == false) {
                    if (_inputManager.GetAxis(SpaceTower.Inputs.DPHorizontalPlayer, _playerID) == +1) {
                        if (!IsInTheGrid(new Vector3(1, 0, 0)))
                            return;
                        _instantiatedBloc.transform.position += new Vector3(1, 0, 0);
                        GridManagement.AlignTransform(_instantiatedBloc.transform);
                    }
                    else if (_inputManager.GetAxis(SpaceTower.Inputs.DPHorizontalPlayer, _playerID) == -1) {
                        if (!IsInTheGrid(new Vector3(-1, 0, 0)))
                            return;
                        _instantiatedBloc.transform.position += new Vector3(-1, 0, 0);
                        GridManagement.AlignTransform(_instantiatedBloc.transform);
                    }
                    _isDPHorizontalUsed = true;
                }
            }

            if (_inputManager.GetAxis(SpaceTower.Inputs.DPHorizontalPlayer, _playerID) == 0) {
                _isDPHorizontalUsed = false;
            }

            // D'Pad Vertical gestion as button
            if (_inputManager.GetAxis(SpaceTower.Inputs.DPVerticalPlayer, _playerID) != 0) {
                if (_isDPVerticalUsed == false) {
                    if (_inputManager.GetAxis(SpaceTower.Inputs.DPVerticalPlayer, _playerID) == +1) {
                        if (!IsInTheGrid(new Vector3(0, 1, 0)))
                            return;
                        _instantiatedBloc.transform.position += new Vector3(0, 1, 0);
                        GridManagement.AlignTransform(_instantiatedBloc.transform);
                    }
                    else if (_inputManager.GetAxis(SpaceTower.Inputs.DPVerticalPlayer, _playerID) == -1) {
                        if (!IsInTheGrid(new Vector3(0, -1, 0)))
                            return;
                        _instantiatedBloc.transform.position += new Vector3(0, -1, 0);
                        GridManagement.AlignTransform(_instantiatedBloc.transform);
                    }
                    _isDPVerticalUsed = true;
                }
            }

            if (_inputManager.GetAxis(SpaceTower.Inputs.DPVerticalPlayer, _playerID) == 0) {
                _isDPVerticalUsed = false;
            }

            // Button A pressed, we activate current bloc rigidbody and collider
            // And we instantiate a new bloc
            if (_inputManager.GetButtonDown(SpaceTower.Inputs.ButtonAPlayer, _playerID)) {
                BlocBehavior bloc = _instantiatedBloc.GetComponent<BlocBehavior>();
                MunitionDAO munitionInfo = munitionRepository.getInfo(_playerID, bloc.level);
                if (munitionInfo.numberOfBlocksLeft > 0) {
                    munitionRepository.removeBlock(_playerID, bloc.level, 1);
                    hudController.updateMunitions();
                    // We take the bloc script and return true if it collide with the floor or an other bloc so that we don't instantiate it
                    if (bloc.getIsCollideBloc())
                        return;

                    // The bloc doesn't collide with floor or other bloc, make it have real world attributes and set back its initial colors
                    _blocRigidbody.isKinematic = false;
                    _blocCollider.isTrigger = false;
                    bloc.isLaid = true;
                    _isNewBlocNeeded = true;
                    _instantiatedBloc.GetComponent<SpriteRenderer>().color = _spriteInitialColor;
                }
                else {
                    Debug.Log("No more munitions");
                }
            }

            #endregion // Controls

            if (_isNewBlocNeeded) {
                setNewBloc();
            }
        }

        #region Public methods
        public void moveBackTopLimit()
        {
            var tempTop = _topLimit.position - new Vector3(0, 1, 0);

            foreach (Transform childBloc in _playerBlocContainer.transform) {
                var deltaX = Mathf.Abs(childBloc.transform.position.y - tempTop.y);
                if (childBloc.GetComponent<BlocBehavior>().isLaid && deltaX <= _minDeltaXY) {
                    _myLimitZone.setStopScaling(true);
                }
            }

            _topLimit.position -= new Vector3(0, 1, 0);

            if (!IsInTheGrid(new Vector3(0, 0, 0))) {
                _instantiatedBloc.transform.position += new Vector3(0, -1, 0);
                GridManagement.AlignTransform(_instantiatedBloc.transform);
            }
        }

        public void moveBackRightLimit()
        {
            var tempRight = _rightLimit.position - new Vector3(1, 0, 0);

            foreach (Transform childBloc in _playerBlocContainer.transform) {
                var deltaX = Mathf.Abs(childBloc.transform.position.x - tempRight.x);
                if (childBloc.GetComponent<BlocBehavior>().isLaid && deltaX <= _minDeltaXY) {
                    _myLimitZone.setStopScaling(true);
                }
            }

            _rightLimit.position -= new Vector3(1, 0, 0);

            if (!IsInTheGrid(new Vector3(0, 0, 0))) {
                _instantiatedBloc.transform.position -= new Vector3(1, 0, 0);
                GridManagement.AlignTransform(_instantiatedBloc.transform);
            }
        }

        public void moveBackLeftLimit()
        {
            var tempLim = _leftLimit.position + new Vector3(1, 0, 0);

            foreach (Transform childBloc in _playerBlocContainer.transform) {
                var deltaX = Mathf.Abs(childBloc.transform.position.x - tempLim.x);
                if (childBloc.GetComponent<BlocBehavior>().isLaid && deltaX <= _minDeltaXY) {
                    _myLimitZone.setStopScaling(true);
                    return;
                }
            }

            _leftLimit.position += new Vector3(1, 0, 0);

            if (!IsInTheGrid(new Vector3(0, 0, 0))) {
                _instantiatedBloc.transform.position += new Vector3(1, 0, 0);
                GridManagement.AlignTransform(_instantiatedBloc.transform);
            }
        }
        #endregion // public methods

        #region Private methods
        // Used to instantiate a new bloc
        private void setNewBloc()
        {
            // If we need a new start position or it's the first bloc initialization
            if (_isResetPoint || _instantiatedBloc == null) {
                _newStartingPosition = _startingPoint.position;
                _isResetPoint = false;
            }
            else {
                _newStartingPosition = _instantiatedBloc.transform.position;
                _newStartingRotation = _instantiatedBloc.transform.rotation;
            }

            // Instantiate bloc, disable rigidbody, collider is trigger, set player ID, change opacity and store initial color
            int selectedBlockLevel = LevelManager.getInstance().getSelectedBlockLevel(_playerID);
            int blockToSelectIndex = selectedBlockLevel - 1;
            _instantiatedBloc = Instantiate(_blocList[blockToSelectIndex], _newStartingPosition, _newStartingRotation, _playerBlocContainer.transform);
            _blocRigidbody = _instantiatedBloc.GetComponent<Rigidbody2D>();
            _blocCollider = _instantiatedBloc.GetComponent<BoxCollider2D>();
            _instantiatedBloc.GetComponent<BlocBehavior>().PlayerID = _playerID;
            _currentBlocLevel = _instantiatedBloc.GetComponent<BlocBehavior>().level;

            if (_currentBlocLevel == 2 || _currentBlocLevel == 4) {
                _minDeltaXY = 0.8f;
            }
            else {
                _minDeltaXY = 0.1f;
            }

            _spriteInitialColor = _instantiatedBloc.GetComponent<SpriteRenderer>().color;
            _instantiatedBloc.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .7f);

            _blocRigidbody.isKinematic = true;
            _blocCollider.isTrigger = true;
            _isNewBlocNeeded = false;
        }

        // To know if we are in the limits
        private bool IsInTheGrid(Vector3 PosToCheck)
        {
            // TODO HANDLE NULL EXCEPTION
            Vector3 actualBlocPos = _instantiatedBloc.transform.position;
            actualBlocPos += PosToCheck;

            // Top Limit
            if (actualBlocPos.y >= _topLimit.position.y)
                return false;

            // Bottom Limit
            if (actualBlocPos.y <= _bottomLimit.position.y)
                return false;

            // Right Limit
            if (actualBlocPos.x >= _rightLimit.position.x)
                return false;

            // Left Limit
            if (actualBlocPos.x <= _leftLimit.position.x)
                return false;

            return true;
        }
        #endregion // Private methods
    }
}
