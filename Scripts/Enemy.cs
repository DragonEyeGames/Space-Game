using Godot;
using System;

public partial class Enemy : Node2D
{
	public Player player;
	[Export]
	private float speed;
	[Export]
	public float health;
	public bool dead;
	public bool canFire=true;
	
	public void Initialize(int Speed, float Health) {
		speed=Speed;
		health=Health;
	}
	
	public override async void _Process(double delta) {
		if(dead==false) {
			if(GetNode<RayCast2D>("RayCast2D").IsColliding() && canFire) {
				Fire();
				canFire=false;
				await ToSignal(GetTree().CreateTimer(1.4f), SceneTreeTimer.SignalName.Timeout);
				canFire=true;
			}
			Position=new Vector2(Position.X, Position.Y+speed);
		}
	}
	
	public async virtual void Die() {
		if(dead==false) {
			RumbleController.Rumble(1.0f, 0.2f);
			GetNode<AudioStreamPlayer2D>("Explode").Play();
			GetNode<AudioStreamPlayer2D>("Explode").Reparent( GetTree().Root);
			dead=true;
			GetNode<Sprite2D>("Cover").Visible=false;
			if(GD.RandRange(1, 2)==1) {
				GetNode<AnimatedSprite2D>("Kaboom").Play("explode");
			} else {
				GetNode<AnimatedSprite2D>("Kaboom").Play("explode-x");
			}
			Vector2 globalPos = GetNode<AnimatedSprite2D>("Kaboom").GlobalPosition;
			AnimatedSprite2D kaboom = GetNode<AnimatedSprite2D>("Kaboom");
			kaboom.Reparent( GetTree().Root);
			kaboom.GlobalPosition=globalPos;
			QueueFree();
		}
	}
	
	public virtual async void TakeDamage(int damage) {
		health-=damage;
		if(health<=0) {
			Die();
		}
		GetNode<AnimationPlayer>("AnimationPlayer").Play("flash");
	}
	
	public virtual void Fire() {
		Rocket rocket = (Rocket)GetNode<Rocket>("LeftRocket").Duplicate();
		AddChild(rocket);
		rocket.GlobalPosition = GetNode<Rocket>("LeftRocket").GlobalPosition;
		rocket.Visible=true;
		rocket.fired=true;
		rocket.Reparent( GetTree().Root);
		rocket.Fire();
		Rocket rocket2 = (Rocket)GetNode<Rocket>("RightRocket").Duplicate();
		AddChild(rocket2);
		rocket2.GlobalPosition = GetNode<Rocket>("RightRocket").GlobalPosition;
		rocket2.Visible=true;
		rocket2.fired=true;
		rocket2.Reparent( GetTree().Root);
		rocket2.Fire();
	}
	
	public virtual void WeaponsHandling() {
		
	}
}
