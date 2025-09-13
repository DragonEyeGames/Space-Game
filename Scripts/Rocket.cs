using Godot;
using System;

public partial class Rocket : CharacterBody2D
{
	private int damage=4;
	public bool fired=false;
	public override void _Process(double delta) {
		MoveAndSlide();
		GetNode<GpuParticles2D>("Rocket/Flame").Emitting = fired;
	}
	
	public void Fire() {
		Velocity=new Vector2(0, 200);
		GetNode<AudioStreamPlayer2D>("Launch").Play();
	}
	
	private async void OnArea2DBodyEntered(Node2D body)
	{
		if(fired) {
			Node2D newPlayer=body.GetParent() as Node2D;
			if (newPlayer is Player player)
			{
			player.TakeDamage(damage);
			}
			Velocity=new Vector2(0, 0);
			GetNode<AnimatedSprite2D>("Kaboom").Play("default");
			GetNode<AnimatedSprite2D>("Kaboom").Visible=true;
			GetNode<AnimatedSprite2D>("Rocket").Visible=false;
			GetNode<AudioStreamPlayer2D>("Collide").Play();
			await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
			QueueFree();
		}
	}
}
