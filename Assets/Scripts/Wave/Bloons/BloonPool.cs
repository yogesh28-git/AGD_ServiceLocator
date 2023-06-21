using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Utilities;

namespace ServiceLocator.Bloon
{
    public class BloonPool : GenericObjectPool<BloonController>
    {
        private BloonView bloonPrefab;
        private List<BloonConfiguration> bloonConfigurations;

        public BloonPool(BloonView bloonPrefab, List<BloonConfiguration> bloonConfigurations)
        {
            this.bloonPrefab = bloonPrefab;
            this.bloonConfigurations = bloonConfigurations;
        }

        public BloonController GetBloon(BloonType bloonType)
        {
            BloonController bloon = GetItem();
            BloonConfiguration configToUse = bloonConfigurations.Find(config => config.BloonType == bloonType);
            bloon.Init(configToUse.BloonScriptableObject);
            return bloon;
        }

        protected override BloonController CreateItem() => new BloonController(bloonPrefab);
    }
}