using Godot;
using System;

public partial class HealthKit : CharacterBody2D
{
	
	private void OnArea2DBodyEntered(Node2D body)
	{
		GD.Print(body);
		if (body.GetParent() is Player) 
		{
			Player player = body.GetParent() as Player;
			player.Heal();
		}
	}
}
