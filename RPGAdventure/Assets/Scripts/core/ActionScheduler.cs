using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction previousAction;
        public void startAction(IAction action) {
            if (previousAction == action) return;
            if (previousAction != null)
            {
                Debug.Log("cancelling "+previousAction);
                previousAction.cancel();
            }
            previousAction = action;
        }
        public void cancelAction()
        {
            startAction(null);
        }
    }
}