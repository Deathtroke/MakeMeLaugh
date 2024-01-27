using Godot;
using System;

public partial class player : Node2D
{

	private Sprite2D _sprite2D;
	private stats_ui stats_ui;
	
	
	public override void _Ready()
	{
		stats_ui = GetNode<stats_ui>("StatsUI");
	}
	
	private CharacterStats _stats;
	[Export] public CharacterStats Stats
	{
		get { return _stats; }
		set { setStats(value); }
	}

	private void setStats(CharacterStats value)
	{
		_stats = value.create_instance();
		
			// If _stats is not null, unsubscribe update_stats from the StatsChanged event
		if (_stats != null)
		{
			_stats.StatsChanged -= update_stats;
		}

		_stats = value.create_instance();

		// Subscribe update_stats to the StatsChanged event
		_stats.StatsChanged += update_stats;

		update_player();
	}

	private async void update_player()
	{
		if (!(_stats is CharacterStats)) return;
		if (!this.IsInsideTree())
		{
			await ToSignal(this, "ready");
		}
		_sprite2D.Texture = _stats.Art;
		update_stats(null, null);
	}
	
	private void update_stats(object sender, EventArgs e)
	{
		stats_ui.update_stats(_stats);
	}
	
	private void take_damage(int amount)
	{
		if (_stats.Health <= 0) return;
		
		_stats.take_damage(amount);
		
		
		if (_stats.Health <= 0) QueueFree();
	}
}
