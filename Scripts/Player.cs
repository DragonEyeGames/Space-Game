using Godot;
using System;
//handles movement, mostly godot docs
public partial class Player : CharacterBody2D
{
	public int speed {get; set;} = 100;
	
	
	public void GetInput()
	{
		Vector2 inputDirection = Input.GetVector("Left", "Right", "Forward", "Back");
		Velocity = inputDirection * speed; 
		if(Velocity.X<0){
			if(Mathf.Abs(Velocity.Y)>0) {
				Rotation=-.1f;
			}
			else{
				Rotation=-.2f;
			}
		} else if (Velocity.X>0) {
			if(Mathf.Abs(Velocity.Y)>0) {
				Rotation=.1f;
			}
			else{
				Rotation=.2f;
			}
		}
		else{
			Rotation=0;
		}
	}		
		private PackedScene _bullet = GD.Load<PackedScene>("res://basic_projectile.tscn");
		
	public void SpawnBullet()
		{
			Node projectileHolder = 
			var spawnedBullet = _bullet.Instantiate<BasicProjectile>();
			AddChild(spawnedBullet);
			spawnedBullet.Reparent()
		}
		
	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("Shoot"))
		{
			SpawnBullet();
		}
	}
	
	
	
	
	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
	}
}
