using Godot;
using System;

public partial class Boss : Enemy
{
	[Export] public CustomCamera camera;
	private bool collidingWithPlayer = false;
	private bool firingLazer = false;
	private bool leftRocket = false;
	private bool rightRocket = false;
	private float xMin = -1;
	[Export] public float xMax = 270;
	
	public async void Shake() {
		Vector2 origin = GlobalPosition;
		for (int i = 0; i < 120; i++) {
			GlobalPosition=new Vector2(origin.X+GD.RandRange(-2, 2), origin.Y+GD.RandRange(-2, 2));
			await ToSignal(GetTree().CreateTimer(0), SceneTreeTimer.SignalName.Timeout);
		}
	}
	
	public async void AI() {
		if(dead==false) {
			firingLazer=false;
			leftRocket=false;
			rightRocket=false;
			await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout); 
			int choice = GD.RandRange(1, 5);
			if(choice==1) {
				Shake();
				GetNode<AudioStreamPlayer2D>("Charge").Play();
				GetNode<GpuParticles2D>("GPUParticles2D").Emitting=true;
				RumbleController.Rumble(0.2f, 1.8f);
				await ToSignal(GetTree().CreateTimer(1.8f), SceneTreeTimer.SignalName.Timeout);
				GetNode<GpuParticles2D>("GPUParticles2D").Emitting=false;
				await ToSignal(GetTree().CreateTimer(1.2f), SceneTreeTimer.SignalName.Timeout);
				firingLazer=true;
				leftRocket=false;
				rightRocket=false;
				GameManager.camera.shaking=true;
				GetNode<AudioStreamPlayer2D>("Lazer").Play();
				RumbleController.Rumble(1.0f, 2.5f);
				await ToSignal(GetTree().CreateTimer(2.5f), SceneTreeTimer.SignalName.Timeout);
				GameManager.camera.shaking=false;
				AI();
			} else {
				firingLazer=false;
				leftRocket=true;
				rightRocket=true;
				RumbleController.Rumble(0.3f, 0.15f);
				await ToSignal(GetTree().CreateTimer(2f), SceneTreeTimer.SignalName.Timeout);
				AI();
			}
		}
	}
	public override void _Ready(){
		var controllers = Input.GetConnectedJoypads();
		
		GD.Print("Connected controllers:");
		foreach (int deviceId in controllers)
		{
			string name = Input.GetJoyName(deviceId);
			GD.Print($"Device ID: {deviceId}, Name: {name}");
		}
		Initialize(0, 350);
		AI();
	}
	public override void UpdateWeapons(){
		GetNode<Line2D>("Line2D").Visible=true;
			GetNode<Line2D>("LeftLazerEffect").Visible=true;
			GetNode<Line2D>("LeftLazerEffect2").Visible=true;
			var myLine2D = GetNode<Line2D>("Line2D");
			myLine2D.ClearPoints();
			myLine2D.AddPoint(new Vector2(-14, xMin));
			var xPos=xMin;
			while (xPos<xMax) {
				xPos+=GD.RandRange(15, 30);
				myLine2D.AddPoint(new Vector2(GD.RandRange(-12, -16), xPos));
			}
			myLine2D = GetNode<Line2D>("LeftLazerEffect");
			myLine2D.ClearPoints();
			myLine2D.AddPoint(new Vector2(-14, xMin));
			xPos=xMin;
			while (xPos<xMax) {
				xPos+=GD.RandRange(15, 30);
				myLine2D.AddPoint(new Vector2(GD.RandRange(-12, -16), xPos));
			}
			myLine2D = GetNode<Line2D>("LeftLazerEffect2");
			myLine2D.ClearPoints();
			myLine2D.AddPoint(new Vector2(-14, xMin));
			xPos=xMin;
			while (xPos<xMax) {
				xPos+=GD.RandRange(15, 30);
				myLine2D.AddPoint(new Vector2(GD.RandRange(-12, -16), xPos));
		}
	}
	
	public override void _Process(double delta) {
		var parent=GetParent() as PathFollow2D;
		parent.ProgressRatio+=10*(float)delta;
		if(parent.ProgressRatio>=99.9) {
			this.Reparent(this.GetParent().GetParent().GetParent());
		}
		//Position=new Vector2(Position.X, Position.Y+1);
		if(firingLazer==false){
			 xMin+=25;
			if(xMax<xMin) {
				xMax=xMin;
			}
		} else {
			if(collidingWithPlayer) {
				player.TakeDamage(4f);
				GD.Print("DAMAGE");
			}
			if(xMin!=-1) {
				xMin=-1;
				xMax=0;
			}
			if(xMax<400){
				xMax+=25;
			}
		}
		if(xMax>0) {
			UpdateWeapons();
		} else {
			GetNode<Line2D>("Line2D").Visible=false;
			GetNode<Line2D>("LeftLazerEffect").Visible=false;
			GetNode<Line2D>("LeftLazerEffect2").Visible=false;
		}
		if(leftRocket){
			leftRocket=false;
			Rocket rocket = (Rocket)GetNode<Rocket>("LeftRocket").Duplicate();
			AddChild(rocket);
			rocket.GlobalPosition = GetNode<Rocket>("LeftRocket").GlobalPosition;
			rocket.Visible=true;
			rocket.fired=true;
			rocket.Reparent( GetTree().Root);
			rocket.Fire();
		}
		if(rightRocket){
			rightRocket=false;
			Rocket rocket = (Rocket)GetNode<Rocket>("RightRocket").Duplicate();
			AddChild(rocket);
			rocket.GlobalPosition = GetNode<Rocket>("RightRocket").GlobalPosition;
			rocket.Visible=true;
			rocket.fired=true;
			rocket.Reparent( GetTree().Root);
			rocket.Fire();
		}
		if(player!=null && IsInstanceValid(player)) {
			float rotationSpeed = Mathf.DegToRad(3f);
			var storedRotation = Rotation;
			Rotation=Mathf.LerpAngle(Rotation,  (player.GlobalPosition - GlobalPosition).Angle(), rotationSpeed);
		}
	}
	
	public async override void TakeDamage(int damage) {
		health-=damage;
		if(health<=damage) {
			Die();
		}
		GetNode<AnimationPlayer>("AnimationPlayer").Play("flash");
	}
	
	public async override void Die() {
		if(dead==false) {
			GameManager.camera.shaking=false;
			GameManager.score+=50;
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
			GD.Print(GetParent().GetParent().GetParent());
			GetParent().GetParent().GetParent().QueueFree();
		}
	}
	
	public void OnLazerAreaEntered(Node2D body)
	{
		collidingWithPlayer=true;
		player=body.GetParent() as Player;
	}
	
	public void OnLazerAreaExited(Node2D body)
	{
		collidingWithPlayer=false;
	}
}
