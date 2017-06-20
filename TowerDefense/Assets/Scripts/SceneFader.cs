using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
	public Image img;
	public AnimationCurve curve;

	//Réutiliser le scene manager à l'avenir.
	//Tester le nom de la scène avant de switch, si c'est la même retry tout simple du level sans reload de scene sinon scene manager.

	void Start()
	{
		StartCoroutine(FadeIn());
	}

	public void FadeTo()
	{
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeIn()
	{
		float t = 1f;

		while (t > 0f)
		{
			t -= Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return null;
		}
		GameManager.instance.Launch();
	}

	IEnumerator FadeOut()
	{
		float t = 0f;

		while (t< 1f)
		{
			t += Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return null;
		}
		GameManager.instance.ResetGame();
		StartCoroutine(FadeIn());
	}
}