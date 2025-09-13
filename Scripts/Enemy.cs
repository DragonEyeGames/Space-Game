using Godot;
using System;

public partial class Enemy : Node2D
{
	public Player player;
	private float speed;
	public float health;
	public bool dead;
	
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
			GetNode<AudioStreamPlayer2D>("Explode").Reparent( GetTree().Root);
			dead=true;
			GetNode<Sprite2D>("Cover").Visible=false;
			GetNode<AnimatedSprite2D>("Kaboom").Play("boom");
			Vector2 globalPos = GetNode<AnimatedSprite2D>("Kaboom").GlobalPosition;
			AnimatedSprite2D kaboom = GetNode<AnimatedSprite2D>("Kaboom");
			kaboom.Reparent( GetTree().Root);
			kaboom.GlobalPosition=globalPos;
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
