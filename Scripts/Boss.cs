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
	public void UpdateLazer(){
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
		parent.ProgressRatio+=.1f;
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
			if(xMax<200){
				xMax+=25;
			}
		}
		if(xMax>0) {
			UpdateLazer();
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
			if(player.Position.X<Position.X) {
			Position = new Vector2(Position.X-.5f, Position.Y);
		}
		else if(player.Position.X>Position.X) {
			Position = new Vector2(Position.X+.5f, Position.Y);
		}
		if(player.Position.Y<Position.Y) {
			Position = new Vector2(Position.X, Position.Y-.5f);
		}
		else if(player.Position.Y<Position.Y) {
			Position = new Vector2(Position.X, Position.Y+.5f);
		}
		}
	}
	
	public async void TakeDamage(int damage) {
		health-=damage;
		if(health<=damage) {
			Die();
		}
		GetNode<AnimationPlayer>("AnimationPlayer").Play("flash");
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
