using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Speech : Node2D
{
	private RichTextLabel _text;
	[Export] public Godot.Collections.Array<String> _text_array;
	// Called when the node enters the scene tree for the first time.
	
	private void _ready()
	{
		
		this.Visible = false;
		_text = GetTree().GetFirstNodeInGroup("joketext") as RichTextLabel;
	}
	public async void display()
	{
		if (Visible) return;
		Random rnd = new Random();
		_text.Text = _text_array[rnd.Next(0, _text_array.Count)];
		this.Visible = true;
		await Task.Delay(5000);
		this.Visible = false;
	}
}
