using Godot;
using System;

public partial class Main : Node2D
{
	
	public override void _Ready()
	{
		ColorRect shaderRect = GetNode<ColorRect>("ParallaxBackground/ParallaxLayer/ColorRect");
		(shaderRect.Material as ShaderMaterial).SetShaderParameter("randomTranslation", new Vector2((float)GD.RandRange(0.0, 10.0), (float)GD.RandRange(0.0, 10.0)));
		ColorRect shaderRectTwo = GetNode<ColorRect>("ParallaxBackground/ParallaxLayer2/ColorRect");
		(shaderRectTwo.Material as ShaderMaterial).SetShaderParameter("randomTranslation", new Vector2((float)GD.RandRange(0.0, -10.0), (float)GD.RandRange(0.0, -10.0)));
	}
	
	public override void _Process(double delta)
	{
		
	}
}
