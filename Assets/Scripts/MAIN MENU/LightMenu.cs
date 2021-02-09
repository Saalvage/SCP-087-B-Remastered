using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMenu : MonoBehaviour
{
	public Light Light;
	private Queue<float> Queue = new Queue<float>();
	private const int Smoothing = 20;
	public static float Max = 1.1f;
	private const float Min = 0.75f;
	private float LastSum;

	void Start()
	{
		while (Queue.Count < Smoothing)
		{
			Queue.Enqueue(0f);
		}
	}

	void Update()
	{
		LastSum -= Queue.Dequeue();

		if (MainMenuGlobals.isMatchOff)
		{
			Queue.Enqueue(0f);
		}
		else
		{
			float newVal = Min + Random.value * (Max - Min);
			Queue.Enqueue(newVal);
			LastSum += newVal;
		}

		Light.intensity = LastSum / Smoothing;
	}

}
