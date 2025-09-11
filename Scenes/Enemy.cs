using Godot;
using System;

public partial class Enemy : Node2D
{
	[Export]
	public bool firingLazer=false;
	[Export]
	public float xMax=270;
	[Export]
	public bool leftRocket=false;
	[Export]
	public bool rightRocket=false;
	public float xMin=-1;
	public override void _Process(double delta) {
		//Position=new Vector2(Position.X, Position.Y+1);
		if(firingLazer==false){
			 xMin+=25;
			if(xMax<xMin) {
				xMax=xMin;
			}
		} else {
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
	
	public void UpdateLazer(){
		GetNode<Line2D>("Line2D").Visible=true;
			GetNode<Line2D>("LeftLazerEffect").Visible=true;
			GetNode<Line2D>("LeftLazerEffect2").Visible=true;
			var myLine2D = GetNode<Line2D>("Line2D");
			myLine2D.ClearPoints();
			myLine2D.AddPoint(new Vector2(-14, xMin));
			var xPos=xMin;
			while (xPos<xMax) {
				xPos+=GD.RandRange(5, 10);
				GD.Print(xPos);
				myLine2D.AddPoint(new Vector2(GD.RandRange(-12, -16), xPos));
			}
			myLine2D = GetNode<Line2D>("LeftLazerEffect");
			myLine2D.ClearPoints();
			myLine2D.AddPoint(new Vector2(-14, xMin));
			xPos=xMin;
			while (xPos<xMax) {
				xPos+=GD.RandRange(5, 10);
				GD.Print(xPos);
				myLine2D.AddPoint(new Vector2(GD.RandRange(-12, -16), xPos));
			}
			myLine2D = GetNode<Line2D>("LeftLazerEffect2");
			myLine2D.ClearPoints();
			myLine2D.AddPoint(new Vector2(-14, xMin));
			xPos=xMin;
			while (xPos<xMax) {
				xPos+=GD.RandRange(5, 10);
				GD.Print(xPos);
				myLine2D.AddPoint(new Vector2(GD.RandRange(-12, -16), xPos));
			}
	}
}
