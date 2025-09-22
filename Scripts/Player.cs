using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public int speed {get; set;} = 200;
	private bool canShoot = true;
	private float health = 20;
	private bool canMove=true;
	private bool invincible=false;
	private bool dead = false;
	
	Texture2D SDam = ResourceLoader.Load<Texture2D>("res://Assets/Foozle_2DS0011_Void_MainShip/Main Ship/Main Ship - Bases/PNGs/Main Ship - Base - Slight damage.png");
	Texture2D Dam = ResourceLoader.Load<Texture2D>("res://Assets/Foozle_2DS0011_Void_MainShip/Main Ship/Main Ship - Bases/PNGs/Main Ship - Base - Damaged.png");
	Texture2D VDam = ResourceLoader.Load<Texture2D>("res://Assets/Foozle_2DS0011_Void_MainShip/Main Ship/Main Ship - Bases/PNGs/Main Ship - Base - Very damaged.png");
	Texture2D NoDam = ResourceLoader.Load<Texture2D>("res://Assets/Foozle_2DS0011_Void_MainShip/Main Ship/Main Ship - Bases/PNGs/Main Ship - Base - Full health.png");
	
	public void GetInput()
	{
		Rotation+=(Input.GetActionStrength("Look_Right") - Input.GetActionStrength("Look_Left"))*.1f;
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
	public void ChangeSprite()
	{
			if(health<=8){
				GetNode<Sprite2D>("MainShip").Texture= VDam;
			}
			else if(health <= 12){
				GetNode<Sprite2D>("MainShip").Texture = Dam;
			}
			else if(health <= 16){
				GetNode<Sprite2D>("MainShip").Texture = SDam;
			}
			else if(health > 16){
				GetNode<Sprite2D>("MainShip").Texture = NoDam;
			}
			
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
			if(health<=0) {
				Die();
			}
			ChangeSprite();

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
	
	public void Heal()
	{
		health += 10;
		if(health > 20)
		{
			health = 20;
			ChangeSprite();
		}
	}
	
}
