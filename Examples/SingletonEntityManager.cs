using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityUtilities
{
    // Drop an instance of this in the scene.
    // The instance can accessed via SingletonEntityManager.Instance.
    // E.g.: SingletonEntityManager.Instance.AddEntity(newEntity);

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
