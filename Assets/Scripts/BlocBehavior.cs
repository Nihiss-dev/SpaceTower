using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTowers
{
    public class BlocBehavior : MonoBehaviour
    {
        public bool isLaid = false;
        public int level;

        #region Private variables
        private bool _isCollideBloc; // False by default
        private int _playerID;
        #endregion // private variables

        #region Private methods
        private void OnTriggerStay2D(Collider2D coll)
        {
           if (coll.gameObject.tag == "Bloc" || coll.gameObject.tag == "Floor")
                _isCollideBloc = true;
        }

        private void OnTriggerExit2D(Collider2D coll)
        {
            if (coll.gameObject.tag == "Bloc" || coll.gameObject.tag == "Floor")
                _isCollideBloc = false;
        }
        #endregion // Private methods

        #region Public methods
        public bool getIsCollideBloc()
        {
            return _isCollideBloc;
        }

        public int PlayerID
        {
            get { return _playerID; }
            set { _playerID = value; }
        }
        #endregion // Public methods
    }
}
