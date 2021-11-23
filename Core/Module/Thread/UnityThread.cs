/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-04                    *
* Nowtime:           18:20:59                      *
* Description:                                     *
* History:                                         *
***************************************************/
#define ENABLE_UPDATE_FUNCTION_CALLBACK
#define ENABLE_LATEUPDATE_FUNCTION_CALLBACK
#define ENABLE_FIXEDUPDATE_FUNCTION_CALLBACK

namespace Framework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;

    public class UnityThread : MonoBehaviour
    {
        //our (singleton) instance
        private static UnityThread instance = null;

        ////////////////////////////////////////////////UPDATE IMPL////////////////////////////////////////////////////////
        //Holds actions received from another Thread. Will be coped to actionCopiedQueueUpdateFunc then executed from there
        private static readonly List<Action> actionQueuesUpdateFunc = new List<Action>();

        //holds Actions copied from actionQueuesUpdateFunc to be executed
        private readonly List<Action> mAactionCopiedQueueUpdateFunc = new List<Action>();

        // Used to know if whe have new Action function to execute. This prevents the use of the lock keyword every frame
        private static volatile bool noActionQueueToExecuteUpdateFunc = true;


        ////////////////////////////////////////////////LATEUPDATE IMPL////////////////////////////////////////////////////////
        //Holds actions received from another Thread. Will be coped to actionCopiedQueueLateUpdateFunc then executed from there
        private static readonly List<Action> actionQueuesLateUpdateFunc = new List<Action>();

        //holds Actions copied from actionQueuesLateUpdateFunc to be executed
        private readonly List<Action> mActionCopiedQueueLateUpdateFunc = new List<Action>();

        // Used to know if whe have new Action function to execute. This prevents the use of the lock keyword every frame
        private static volatile bool noActionQueueToExecuteLateUpdateFunc = true;



        ////////////////////////////////////////////////FIXEDUPDATE IMPL////////////////////////////////////////////////////////
        //Holds actions received from another Thread. Will be coped to actionCopiedQueueFixedUpdateFunc then executed from there
        private static readonly List<Action> actionQueuesFixedUpdateFunc = new List<Action>();

        //holds Actions copied from actionQueuesFixedUpdateFunc to be executed
        private readonly List<Action> mActionCopiedQueueFixedUpdateFunc = new List<Action>();

        // Used to know if whe have new Action function to execute. This prevents the use of the lock keyword every frame
        private static volatile bool noActionQueueToExecuteFixedUpdateFunc = true;


        //Used to initialize UnityThread. Call once before any function here
        public static void InitUnityThread(bool visible = false)
        {
            if (instance != null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                // add an invisible game object to the scene
                GameObject obj = new GameObject("MainThreadExecuter");
                if (!visible)
                {
                    obj.hideFlags = HideFlags.HideAndDontSave;
                }

                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<UnityThread>();
            }
        }

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        //////////////////////////////////////////////COROUTINE IMPL//////////////////////////////////////////////////////
#if (ENABLE_UPDATE_FUNCTION_CALLBACK)
        public static void ExecuteCoroutine(IEnumerator action)
        {
            if (instance != null)
            {
                ExecuteInUpdate(() => instance.StartCoroutine(action));
            }
        }

        ////////////////////////////////////////////UPDATE IMPL////////////////////////////////////////////////////
        public static void ExecuteInUpdate(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            lock (actionQueuesUpdateFunc)
            {
                actionQueuesUpdateFunc.Add(action);
                noActionQueueToExecuteUpdateFunc = false;
            }
        }

        public void Update()
        {
            if (noActionQueueToExecuteUpdateFunc)
            {//如果当前为ture 则说明 有操作正在执行
                return;
            }

            //Clear the old actions from the actionCopiedQueueUpdateFunc queue
            mAactionCopiedQueueUpdateFunc.Clear();
            lock (actionQueuesUpdateFunc)
            {
                //Copy actionQueuesUpdateFunc to the actionCopiedQueueUpdateFunc variable
                mAactionCopiedQueueUpdateFunc.AddRange(actionQueuesUpdateFunc);
                //Now clear the actionQueuesUpdateFunc since we've done copying it
                actionQueuesUpdateFunc.Clear();
                noActionQueueToExecuteUpdateFunc = true;
            }

            // Loop and execute the functions from the actionCopiedQueueUpdateFunc
            for (int i = 0; i < mAactionCopiedQueueUpdateFunc.Count; i++)
            {
                mAactionCopiedQueueUpdateFunc[i].Invoke();
            }
        }
#endif

        ////////////////////////////////////////////LATEUPDATE IMPL////////////////////////////////////////////////////
#if (ENABLE_LATEUPDATE_FUNCTION_CALLBACK)
        public static void ExecuteInLateUpdate(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            lock (actionQueuesLateUpdateFunc)
            {
                actionQueuesLateUpdateFunc.Add(action);
                noActionQueueToExecuteLateUpdateFunc = false;
            }
        }


        public void LateUpdate()
        {
            if (noActionQueueToExecuteLateUpdateFunc)
            {
                return;
            }

            //Clear the old actions from the actionCopiedQueueLateUpdateFunc queue
            mActionCopiedQueueLateUpdateFunc.Clear();
            lock (actionQueuesLateUpdateFunc)
            {
                //Copy actionQueuesLateUpdateFunc to the actionCopiedQueueLateUpdateFunc variable
                mActionCopiedQueueLateUpdateFunc.AddRange(actionQueuesLateUpdateFunc);
                //Now clear the actionQueuesLateUpdateFunc since we've done copying it
                actionQueuesLateUpdateFunc.Clear();
                noActionQueueToExecuteLateUpdateFunc = true;
            }

            // Loop and execute the functions from the actionCopiedQueueLateUpdateFunc
            for (int i = 0; i < mActionCopiedQueueLateUpdateFunc.Count; i++)
            {
                mActionCopiedQueueLateUpdateFunc[i].Invoke();
            }
        }
#endif

        ////////////////////////////////////////////FIXEDUPDATE IMPL//////////////////////////////////////////////////
#if (ENABLE_FIXEDUPDATE_FUNCTION_CALLBACK)
        public static void ExecuteInFixedUpdate(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            lock (actionQueuesFixedUpdateFunc)
            {
                actionQueuesFixedUpdateFunc.Add(action);
                noActionQueueToExecuteFixedUpdateFunc = false;
            }
        }

        public void FixedUpdate()
        {
            if (noActionQueueToExecuteFixedUpdateFunc)
            {
                return;
            }

            //Clear the old actions from the actionCopiedQueueFixedUpdateFunc queue
            mActionCopiedQueueFixedUpdateFunc.Clear();
            lock (actionQueuesFixedUpdateFunc)
            {
                //Copy actionQueuesFixedUpdateFunc to the actionCopiedQueueFixedUpdateFunc variable
                mActionCopiedQueueFixedUpdateFunc.AddRange(actionQueuesFixedUpdateFunc);
                //Now clear the actionQueuesFixedUpdateFunc since we've done copying it
                actionQueuesFixedUpdateFunc.Clear();
                noActionQueueToExecuteFixedUpdateFunc = true;
            }

            // Loop and execute the functions from the actionCopiedQueueFixedUpdateFunc
            for (int i = 0; i < mActionCopiedQueueFixedUpdateFunc.Count; i++)
            {
                mActionCopiedQueueFixedUpdateFunc[i].Invoke();
            }
        }
#endif

        public static void Job(Action action)
        {
            Task.Factory.StartNew(() => action.Invoke());
        }

        public void OnDisable()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}