using Godot;
using System;

public partial class Enemy : Node2D
{
	private Player player;
	private float speed;
	private float health;
	private bool dead;
	
	public void Initialize(int Speed, float Health) {
		speed=Speed;
		health=Health;
	}
	
	public override void _Process(double delta) {
		if(dead==false) {
			Position=new Vector2(Position.X, Position.Y+speed);
			WeaponsHandling();
		}
	}
	
	public async void Die() {
		if(dead==false) {
			RumbleController.Rumble(1.0f, 0.2f);
			GetNode<AudioStreamPlayer2D>("Explode").Play();
			GetNode<CollisionPolygon2D>("Hitbox/CollisionPolygon2D").Disabled=true;
			dead=true;
			GetNode<Sprite2D>("Cover").Visible=false;
			GetNode<AnimatedSprite2D>("Kaboom").Play("boom");
			await ToSignal(GetTree().CreateTimer(2.5f), SceneTreeTimer.SignalName.Timeout);
			QueueFree();
		}
	}
	
	public async void TakeDamage(int damage) {
		health-=damage;
		if(health<=damage) {
			Die();
		}
		GetNode<AnimationPlayer>("AnimationPlayer").Play("flash");
	}
	
	public virtual void WeaponsHandling() {
		
	}
}
