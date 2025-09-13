using Godot;
using System;

public partial class BasicProjectile : Area2D
{
	private bool exploding=false;
	
	public override void _PhysicsProcess(double delta)
	{
		if(exploding==false) {
			Position = new Vector2(Position.X, Position.Y - 2);
		}
	}
	
	private async void OnArea2DBodyEntered(Node2D body)
	{
		if(exploding==false) {
			exploding=true;
			GD.Print("Hit");
			Node2D newEnemy=body.GetParent() as Node2D;
			if (newEnemy is Boss enemy)
			{
			enemy.TakeDamage(5);
			}
			GetNode<AnimatedSprite2D>("Explosion").Play("explode");
			GetNode<AnimatedSprite2D>("Explosion").Visible=true;
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Visible=false;
			await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
			QueueFree();
		}
	}
}
