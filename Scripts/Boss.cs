using Godot;
using System;

public partial class Boss : Node2D
{
	[Export]
	public bool firingLazer=false;
	[Export]
	public float xMax=200;
	[Export]
	public bool leftRocket=false;
	[Export]
	public bool rightRocket=false;
	private bool collidingWithPlayer=false;
	public float xMin=-1;
	public Player player;
	[Export]
	public CustomCamera camera;
	private int health=100;
	private bool dead = false;
	
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
				GetNode<GpuParticles2D>("GPUParticles2D").Emitting=true;
				await ToSignal(GetTree().CreateTimer(1.5f), SceneTreeTimer.SignalName.Timeout);
				GetNode<GpuParticles2D>("GPUParticles2D").Emitting=false;
				await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
				firingLazer=true;
				leftRocket=false;
				rightRocket=false;
				camera.shaking=true;
				await ToSignal(GetTree().CreateTimer(4f), SceneTreeTimer.SignalName.Timeout);
				camera.shaking=false;
				AI();
			} else {
				firingLazer=false;
				leftRocket=true;
				rightRocket=true;
				await ToSignal(GetTree().CreateTimer(2f), SceneTreeTimer.SignalName.Timeout);
				AI();
			}
		}
	}
	public override void _Ready(){
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
		//Position=new Vector2(Position.X, Position.Y+1);
		if(firingLazer==false){
			 xMin+=25;
			if(xMax<xMin) {
				xMax=xMin;
			}
		} else {
			if(collidingWithPlayer) {
				player.TakeDamage(2f);
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
			rocket.Fire();
		}
		if(rightRocket){
			rightRocket=false;
			Rocket rocket = (Rocket)GetNode<Rocket>("RightRocket").Duplicate();
			AddChild(rocket);
			rocket.GlobalPosition = GetNode<Rocket>("RightRocket").GlobalPosition;
			rocket.Visible=true;
			rocket.fired=true;
			rocket.Fire();
		}
	}
	
	public async void Die() {
		if(dead==false) {
			dead=true;
			GetNode<Sprite2D>("Cover").Visible=false;
			GetNode<Sprite2D>("Engine").Visible=false;
			GetNode<CharacterBody2D>("LeftRocket").Visible=false;
			GetNode<CharacterBody2D>("RightRocket").Visible=false;
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
