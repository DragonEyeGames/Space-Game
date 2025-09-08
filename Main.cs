using Godot;
using System;

public partial class Main : Node2D
{
	private ColorRect shaderRect;
	
	public override void _Ready()
	{
		shaderRect = GetNode<ColorRect>("$ParallaxBackground/ParallaxLayer/ColorRect");
		shaderRect.Material.SetShaderParameter("randomTranslation", new Vector2(1, 1));
	}
}
