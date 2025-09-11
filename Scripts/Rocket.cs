using Godot;
using System;

public partial class Rocket : CharacterBody2D
{
	public bool fired=false;
	public override void _Process(double delta) {
		MoveAndSlide();
		GetNode<GpuParticles2D>("Rocket/Flame").Emitting = fired;
	}
	
	public void Fire() {
		Velocity=new Vector2(0, 200);
	}
	
	private async void OnArea2DBodyEntered(Node2D body)
	{
		if(fired) {
			GD.Print("Entered");
			Velocity=new Vector2(0, 0);
			GetNode<AnimatedSprite2D>("Kaboom").Play("default");
			GetNode<AnimatedSprite2D>("Kaboom").Visible=true;
			GetNode<AnimatedSprite2D>("Rocket").Visible=false;
			await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
			QueueFree();
		}
	}
}
