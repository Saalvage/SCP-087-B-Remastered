using System.Collections;
using UnityEngine;

public class MatchEvent : MonoBehaviour
{
	[Header("Wait times:")]
	public const float minWait = 300; // 300
	public const float maxWait = 900; // 900

	void Awake()
	{
		StartCoroutine(MatchBurnout());
	}

	IEnumerator MatchBurnout()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minWait, maxWait));
			Globals.isMatchOff = true;
			Globals.dFilter.blurAmount = 6f;
			Globals.matchSrc.PlayOneShot(Globals.fireOff);
			yield return new WaitForSeconds(3);

			Globals.matchSrc.PlayOneShot(Globals.fireOn);
			yield return new WaitForSeconds(0.75f);

			LightFlicker.Max = 2.0f;
			Globals.isMatchOff = false;
			yield return new WaitForSeconds(0.5f);

			LightFlicker.Max = 1.0f;
			Globals.dFilter.blurAmount = 0.5f;
		}
	}
}