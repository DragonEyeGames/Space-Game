using Godot;
using System;

public partial class Enemy : Node2D
{
	[Export]
	public bool firing=false;
	[Export]
	public float xMax=270;
	public override void _Process(double delta) {
		//Position=new Vector2(Position.X, Position.Y+1);
		if(firing==false){
			 if(xMax>0){
				xMax-=10;
			}
			if(xMax<0){
				xMax=0;
			}
		} else {
			if(xMax<200){
				xMax+=10;
			}
		}
		if(xMax>0) {
			UpdateLazer();
		} else {
			GetNode<Line2D>("Line2D").Visible=false;
			GetNode<Line2D>("LeftLazerEffect").Visible=false;
			GetNode<Line2D>("LeftLazerEffect2").Visible=false;
		}
	}
	
	public void UpdateLazer(){
		GetNode<Line2D>("Line2D").Visible=true;
			GetNode<Line2D>("LeftLazerEffect").Visible=true;
			GetNode<Line2D>("LeftLazerEffect2").Visible=true;
			var myLine2D = GetNode<Line2D>("Line2D");
			myLine2D.ClearPoints();
			myLine2D.AddPoint(new Vector2(-14, -1));
			var xPos=-1;
			while (xPos<xMax) {
				xPos+=GD.RandRange(5, 10);
				GD.Print(xPos);
				myLine2D.AddPoint(new Vector2(GD.RandRange(-12, -16), xPos));
			}
			myLine2D = GetNode<Line2D>("LeftLazerEffect");
			myLine2D.ClearPoints();
			myLine2D.AddPoint(new Vector2(-14, -1));
			xPos=-1;
			while (xPos<xMax) {
				xPos+=GD.RandRange(5, 10);
				GD.Print(xPos);
				myLine2D.AddPoint(new Vector2(GD.RandRange(-12, -16), xPos));
			}
			myLine2D = GetNode<Line2D>("LeftLazerEffect2");
			myLine2D.ClearPoints();
			myLine2D.AddPoint(new Vector2(-14, -1));
			xPos=-1;
			while (xPos<xMax) {
				xPos+=GD.RandRange(5, 10);
				GD.Print(xPos);
				myLine2D.AddPoint(new Vector2(GD.RandRange(-12, -16), xPos));
			}
	}
}
