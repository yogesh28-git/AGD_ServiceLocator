using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Utilities;

namespace ServiceLocator.Bloon
{
    public class BloonPool : GenericObjectPool<BloonController>
    {
        private BloonView bloonPrefab;
        private List<BloonScriptableObject> bloonScriptableObjects;

        public BloonPool(BloonView bloonPrefab, List<BloonScriptableObject> bloonScriptableObjects)
        {
            this.bloonPrefab = bloonPrefab;
            this.bloonScriptableObjects = bloonScriptableObjects;
        }

        public BloonController GetBloon(BloonType bloonType)
        {
            BloonController bloon = GetItem();
            BloonScriptableObject scriptableObjectToUse = bloonScriptableObjects.Find(so => so.Type == bloonType);
            bloon.Init(scriptableObjectToUse);
            return bloon;
        }

        protected override BloonController CreateItem() => new BloonController(bloonPrefab);
    }
}