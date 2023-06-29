using NUnit.Framework;
using UnityEngine;
using ServiceLocator.Wave;
using ServiceLocator.Bloon;
using System.Collections.Generic;
using ServiceLocator.Main;

[TestFixture]
public class WaveServiceTests
{
    private WaveService waveService;

    [SetUp]
    public void Setup()
    {
        // Create a mock WaveScriptableObject for testing
        WaveScriptableObject waveScriptableObject = ScriptableObject.CreateInstance<WaveScriptableObject>();
        waveScriptableObject.BloonTypeDataMap = ScriptableObject.CreateInstance<BloonTypeDataMap>();
        waveScriptableObject.BloonTypeDataMap.BloonPrefab = new GameObject().AddComponent<BloonView>();
        waveScriptableObject.BloonTypeDataMap.BloonScriptableObjects = new List<BloonScriptableObject>();

        // Create a mock Transform for the bloon container
        Transform bloonContainer = new GameObject().transform;

        // Create a new instance of WaveService for testing
        waveService = new WaveService(waveScriptableObject, bloonContainer);

        WaveData waveData;
        waveData.WaveID = waveService.CurrentWaveId + 1;
        waveData.ListOfBloons = new List<BloonType>();

        List<WaveData> waveDatas = new List<WaveData>();
        waveDatas.Add(waveData);
        waveService.WaveDatas = waveDatas;
    }

    [Test]
    public void StarNextWave_IncrementsCurrentWaveId()
    {
        // Arrange
        int initialWaveId = waveService.CurrentWaveId;

        // Act
        waveService.StarNextWave();

        // Assert
        Assert.AreEqual(initialWaveId + 1, waveService.CurrentWaveId);
    }

    [Test]
    public void GetSoundServiceIntance_WithSingletonDependency_Fails()
    {
        Assert.IsNotNull(GameService.Instance);
    }
}
