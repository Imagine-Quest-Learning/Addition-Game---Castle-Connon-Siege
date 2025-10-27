using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using System.Collections;

public class FadeControllerTests
{
    GameObject canvasGO;
    Image img;
    FadeController fader;

    [SetUp]
    public void SetUp()
    {
        canvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasRenderer));
        var imageGO = new GameObject("FadeImage", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        imageGO.transform.SetParent(canvasGO.transform);
        img = imageGO.GetComponent<Image>();
        img.color = new Color(0, 0, 0, 1f);

        fader = canvasGO.AddComponent<FadeController>();
        fader.fadeImage = img;
        fader.fadeDuration = 0.05f;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(canvasGO);
    }

    [UnityTest]
    public IEnumerator FadeOut_Reaches_Transparent()
    {
        yield return fader.FadeOut();
        Assert.LessOrEqual(img.color.a, 0.001f);
    }

    [UnityTest]
    public IEnumerator FadeIn_Reaches_Opaque()
    {
        img.color = new Color(0, 0, 0, 0f);
        yield return fader.FadeIn();
        Assert.GreaterOrEqual(img.color.a, 0.999f);
    }
}
