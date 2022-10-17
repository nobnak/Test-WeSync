using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using WeSyncSys;

public class TestProj
{
    public const float DELTA = 1e-3f;

    [OneTimeSetUp]
    public void Init() {
        SceneManager.LoadScene("Scenes/TestProj");
    }

    [UnityTest]
    public IEnumerator TestProjWithEnumeratorPasses() {

        yield return null;

        IWeSync currWe = null;
        foreach (var r in SceneManager.GetActiveScene().GetRootGameObjects()) {
            if (currWe == null) currWe = r.GetComponentInChildren<IWeSync>();
        }
        Assert.NotNull(currWe);

        var cmain = Camera.main;
        var proj = currWe.Proj;

        var pos_center = cmain.transform.position + 2f * cmain.nearClipPlane * cmain.transform.forward;
        var uv_center = proj.WorldPosToUV(pos_center);
        Assert.AreEqual(0.5f, uv_center.x, DELTA);
        Assert.AreEqual(0.5f, uv_center.y, DELTA);

        var aspect = cmain.aspect;
        var pos0 = pos_center - new Vector3(aspect * cmain.orthographicSize, cmain.orthographicSize);
        var uv0 = proj.WorldPosToUV(pos0);
        Assert.AreEqual(0f, uv0.x, DELTA);
        Assert.AreEqual(0f, uv0.y, DELTA);

        var pos1 = pos_center + new Vector3(aspect * cmain.orthographicSize, cmain.orthographicSize);
        var uv1 = proj.WorldPosToUV(pos1);
        Assert.AreEqual(1f, uv1.x, DELTA);
        Assert.AreEqual(1f, uv1.y, DELTA);
    }
}
