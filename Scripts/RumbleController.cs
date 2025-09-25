using Godot;
using System;

public partial class RumbleController: Node
{
	public static float vibrationTimeLeft = 0f;
	public static float currentPower = 0f;
	public static int deviceId = 0; // adjust to your controller id

	public static void Rumble(float power, float duration)
	{
		if (power > currentPower)
			currentPower = power;

		if (duration > vibrationTimeLeft)
			vibrationTimeLeft = duration;

		Input.StartJoyVibration(0, currentPower, currentPower, vibrationTimeLeft);
		Input.StartJoyVibration(1, currentPower, currentPower, vibrationTimeLeft);
	}
}
