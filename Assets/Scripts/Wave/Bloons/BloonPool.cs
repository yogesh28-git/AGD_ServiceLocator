using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Utilities;

/*  This part of the code demonstrates the Object Pooling design pattern.
 *  If you're interested in learning about Object Pooling, you can find
 *  a dedicated course on Outscal's website.
 *  Link: https://outscal.com/courses
 * */

namespace ServiceLocator.Wave.Bloon
{
    public class BloonPool : GenericObjectPool<BloonController>
    {
        private BloonView bloonPrefab;
        private List<BloonScriptableObject> bloonScriptableObjects;
        private Transform bloonContainer;

        public BloonPool(BloonView bloonPrefab, List<BloonScriptableObject> bloonScriptableObjects, Transform bloonContainer)
        {
            this.bloonPrefab = bloonPrefab;
            this.bloonScriptableObjects = bloonScriptableObjects;
            this.bloonContainer = bloonContainer;
        }

        public BloonController GetBloon(BloonType bloonType)
        {
            BloonController bloon = GetItem();
            BloonScriptableObject scriptableObjectToUse = bloonScriptableObjects.Find(so => so.Type == bloonType);
            bloon.Init(scriptableObjectToUse);
            return bloon;
        }

        protected override BloonController CreateItem() => new BloonController(bloonPrefab, bloonContainer);
    }
}