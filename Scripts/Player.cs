using Godot;
using System;
//handles movement, mostly godot docs
public partial class Player : CharacterBody2D
{
	public int speed {get; set;} = 100;
	private bool canShoot = true;
	private float health = 10;
	private bool canMove=true;
	private bool invincible=false;
	private bool dead = false;
	
	
	
	public void GetInput()
	{
		Vector2 inputDirection = Input.GetVector("Left", "Right", "Forward", "Back");
		Velocity = inputDirection * speed; 
	}
		/*if(Velocity.X<0){
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
	*/
		private PackedScene _bullet = GD.Load<PackedScene>("res://Scenes/basic_projectile.tscn");
		
	public void SpawnBullet()
		{
			RumbleController.Rumble(0.1f, 0.1f);
			Node projectileHolder = GetNode<Node>("../ProjectileHolder");
			var spawnedBullet = _bullet.Instantiate();
			AddChild(spawnedBullet);
			spawnedBullet.Reparent(projectileHolder);
		}
		
	public async override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("Shoot"))
		{
			if(canShoot == true)
			{
				GetNode<AudioStreamPlayer2D>("Launch").Play();
				SpawnBullet();
				canShoot = false;
				await ToSignal(GetTree().CreateTimer(0.15f), SceneTreeTimer.SignalName.Timeout);
				canShoot = true;
			}
		}
	}

		

	public async void TakeDamage(float damage) {
		if(invincible==false) {
			RumbleController.Rumble(0.6f, 0.15f);
			health-=damage;
			invincible=true;
			if(health<=damage) {
				Die();
			}
			GetNode<AnimationPlayer>("AnimationPlayer").Play("flash");
			await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
			invincible=false; 
		}
	}
	
	public async void Die() {
		if(dead==false) {
			RumbleController.Rumble(1.0f, 0.4f);
			GetNode<AudioStreamPlayer2D>("Explode").Play();
			canMove=false;
			dead=true;
			GetNode<AnimatedSprite2D>("Exhuast").Visible=false;
			GetNode<Sprite2D>("Engine").Visible=false;
			GetNode<Sprite2D>("MainShip").Visible=false;
			GetNode<AnimatedSprite2D>("Kaboom").Play("boom");
			Vector2 globalPos = GetNode<AnimatedSprite2D>("Kaboom").GlobalPosition;
			AnimatedSprite2D kaboom = GetNode<AnimatedSprite2D>("Kaboom");
			kaboom.Reparent( GetTree().Root);
			kaboom.GlobalPosition=globalPos;
			QueueFree();
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if(canMove) {
			GetInput();
			MoveAndSlide();
			if(invincible){
				GlobalPosition=new Vector2(GlobalPosition.X+GD.RandRange(-2, 2), GlobalPosition.Y+GD.RandRange(-2, 2));
			}
		}
	}
}
