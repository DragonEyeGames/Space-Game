using Godot;
using System;

public partial class BasicBullet : Bullet
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public async override void OnArea2DBodyEntered(Node2D body)
	{
		if(exploding==false) {
			GetNode<AudioStreamPlayer2D>("Explode").Play();
			exploding=true;
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
