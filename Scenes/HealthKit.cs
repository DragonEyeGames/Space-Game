using Godot;
using System;

public partial class HealthKit : CharacterBody2D
{
	private void OnArea2DBodyEntered(Node2D body)
	{
		if (body is Player)
		{
			var player = body as Player;
			player.Heal();
		}
	}
}
