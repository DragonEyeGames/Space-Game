using Godot;
using System;
using System.Collections.Generic;

public partial class name : Control
{
	List<string> letters = new List<string>{"A", "B", "C", "D", "E", "F" ,"G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W" ,"X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", " "};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void upPressed() {
		int index = letters.IndexOf(GetNode<RichTextLabel>("Letter").Text);
		if(index+2<=letters.Count){
			GetNode<RichTextLabel>("Letter").Text=letters[index+1];
		} else {
			GetNode<RichTextLabel>("Letter").Text=letters[0];
		}
	}
	
	public void downPressed() {
		int index = letters.IndexOf(GetNode<RichTextLabel>("Letter").Text);
		if(index-1>=0){
			GetNode<RichTextLabel>("Letter").Text=letters[index-1];
		} else {
			GetNode<RichTextLabel>("Letter").Text=letters[letters.Count-1];
		}
	}
}
