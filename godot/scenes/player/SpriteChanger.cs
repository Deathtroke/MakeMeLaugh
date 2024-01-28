using Godot;
using System;
using System.Threading.Tasks;

public partial class SpriteChanger : Sprite2D
{
	[Export] private Texture2D _idle, _attack, _dead;
	private void _ready()
	{
		Texture = _idle;
	}
	
	public async void show_attack()
	{
		if (Texture == _dead || Texture == _attack)  return;
		Texture = _attack;
		await Task.Delay(2000);
		Texture = _idle;
	}
	public async void show_dead()
	{
		if (Texture == _dead || Texture == _attack)  return;
		Texture = _dead;
		await Task.Delay(2000);
		Texture = _idle;
	}
}
