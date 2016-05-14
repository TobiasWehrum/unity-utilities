using System.Collections.Generic;
using UnityEngine;

namespace UnityUtilities.Examples
{
    /* If the following component is added on a game object in the scene, it could be accessed from anywhere
     * via SingletonEntityManager.Instance, e.g.: SingletonEntityManager.Instance.AddEntity(newEntity);
     * 
     * This is available even before its SingletonEntityManager.Awake() is called.
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
