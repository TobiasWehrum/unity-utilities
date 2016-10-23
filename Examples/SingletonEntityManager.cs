using System.Collections.Generic;
using UnityEngine;

namespace UnityUtilities.Examples
{
    /* If the following component is added on a game object in the scene, it could be accessed from anywhere
     * via SingletonEntityManager.Instance, e.g.: SingletonEntityManager.Instance.AddEntity(newEntity);
     * 
     * This is available even before SingletonEntityManager.Awake() is called.
     * 
     * If you want to use OnDestroy(), you have to override it like shown in the example below. All other MonoBehaviour
     * callbacks can be used as usual.
    */

    public class SingletonEntityManager : SingletonMonoBehaviour<SingletonEntityManager>
    {
        List<GameObject> entities;

        public IEnumerable<GameObject> Entities
        {
            get { return entities; }
        }

        void Awake()
        {
            entities = new List<GameObject>();
        }

        // If you want to use OnDestroy(), you have to override it like this
        protected override void OnDestroy()
        {
            base.OnDestroy();
            Debug.Log("Destroyed");
        }

        public void AddEntity(GameObject entity)
        {
            entities.Add(entity);
        }

        public void RemoveEntity(GameObject entity)
        {
            entities.Remove(entity);
        }
    }
}
