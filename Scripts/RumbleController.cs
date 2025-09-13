using Godot;
using System;

public static class RumbleController
{
	public static float vibrationTimeLeft = 0f;
	public static float currentPower = 0f;
	public static int deviceId = 1; // adjust to your controller id

	public static void Rumble(float power, float duration)
	{
		if (power > currentPower)
			currentPower = power;

		if (duration > vibrationTimeLeft)
			vibrationTimeLeft = duration;

		Input.StartJoyVibration(deviceId, currentPower, currentPower, vibrationTimeLeft);
	}
}
