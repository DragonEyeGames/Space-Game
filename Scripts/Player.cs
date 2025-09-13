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
				SpawnBullet();
				canShoot = false;
				await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
				canShoot = true;
			}
		}
	}

		

	public async void TakeDamage(float damage) {
		if(invincible==false) {
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
			canMove=false;
			dead=true;
			GetNode<AnimatedSprite2D>("Exhuast").Visible=false;
			GetNode<Sprite2D>("Engine").Visible=false;
			GetNode<Sprite2D>("MainShip").Visible=false;
			GetNode<AnimatedSprite2D>("Kaboom").Play("boom");
			await ToSignal(GetTree().CreateTimer(2.5f), SceneTreeTimer.SignalName.Timeout);
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
