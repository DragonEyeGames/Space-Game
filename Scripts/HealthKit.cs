using Godot;
using System;

public partial class HealthKit : CharacterBody2D
{
	private async void OnArea2DBodyEntered(Node2D body)
	{
		GD.Print(body);
		if (body.GetParent() is Player) 
		{
			Player player = body.GetParent() as Player;
			player.Heal();
			GetNode<AnimationPlayer>("AnimationPlayer").Play("Shrink");
			await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
			QueueFree();
		}
	}
}
