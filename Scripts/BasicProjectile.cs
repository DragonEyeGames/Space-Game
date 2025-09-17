using Godot;
using System;

public partial class BasicProjectile : Area2D
{
	private bool exploding=false;
	
	public override void _PhysicsProcess(double delta)
	{
		if(exploding==false) {
			Position = new Vector2(Position.X, Position.Y - 6);
		}
	}
	
	private async void OnArea2DBodyEntered(Node2D body)
	{
		if(exploding==false) {
			GetNode<AudioStreamPlayer2D>("Explode").Play();
			exploding=true;
			GD.Print("Hit");
			Node2D newEnemy=body.GetParent() as Node2D;
			if (newEnemy is Enemy enemy)
			{
				enemy.TakeDamage(5);
			}
			RumbleController.Rumble(0.5f, 0.1f);
			GetNode<AnimatedSprite2D>("Explosion").Play("explode");
			GetNode<AnimatedSprite2D>("Explosion").Visible=true;
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Visible=false;
			await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
			QueueFree();
		}
	}
}
