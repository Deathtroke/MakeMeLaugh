using Godot;
using System;
using System.Threading.Tasks;

public partial class enemy : Area2D
{
	const int ARROW_OFFSET = 10;
	
	private Sprite2D _sprite2D;
	private stats_ui _stats_ui;
	public Sprite2D _arrow;
	private EnemyAI enemy_ai;

	public Enemy_Action curren_action;
	[Export] public SpriteChanger Art;
	private Vector2 _defaultPosition, _hoverPosition;
	private bool hovering = false;
	
	public override void _Ready()
	{
		_stats_ui = GetNode<stats_ui>("StatsUI");
		_sprite2D = GetNode<Sprite2D>("Sprite2D");
		_arrow = GetNode<Sprite2D>("Arrow");
		t = DateTime.Now;
		
		_defaultPosition = Position;
		_hoverPosition = Position + Vector2.Up * 30 + Vector2.Right * -5;
	}

	private DateTime t;

	public override void _Process(double delta)
	{
		if (DateTime.Now >= t)
		{
			t = DateTime.Now.Add(TimeSpan.FromSeconds(0.5));
			update_enemy();
		}
	}
	
	private EnemyStats _stats;
	[Export] public EnemyStats Stats
	{
		get { return _stats; }
		set { setStats(value); }
	}

	private void setStats(EnemyStats value)
	{
		_stats = value.create_instance();

		// If _stats is not null, unsubscribe update_stats from the StatsChanged event
		if (_stats != null)
		{
			_stats.StatsChanged -= update_stats;
		}

		// Subscribe update_stats to the StatsChanged event
		_stats.StatsChanged += update_stats;

		update_enemy();
	}

	void setup_ai()
	{
		if (enemy_ai != null)
		{
			enemy_ai.QueueFree();
		}

		EnemyAI ai = _stats.ai.Instantiate() as EnemyAI;
		AddChild(ai);
		enemy_ai = ai;
		enemy_ai.set_enemy(this);
	}
	
	private void update_stats(object sender, EventArgs e)
	{
		_stats_ui.update_stats(_stats);
		update_action();
	}
	private async void update_enemy()
	{
		if (!(_stats is Stats)) return;
		if (!this.IsInsideTree())
		{
			await Task.Delay(200);
		}
		
		//_sprite2D.Texture = _stats.Art;
		_arrow.Position = Vector2.Right * (_sprite2D.GetRect().Size.X / 2 + ARROW_OFFSET);
		setup_ai();
		update_stats(null, null);
	}

	public void do_turn()
	{
		GD.Print("start e turn do");

		Art.show_attack();
		//_stats._block = 0;

		GD.Print("has action: " + (curren_action != null));
		
		GD.Print("do: " + curren_action.type);
		
		if (curren_action == null)
		{
			return;
		}
		
		curren_action.perform_action();
	}    
	
	public void take_damage(int amount)
	{
		Art.show_dead();
		if (_stats.Health <= 0) return;
		
		_stats.take_damage(amount);

		if (_stats.Health <= 0)
		{
			QueueFree();
			
		}
	}

	public void update_action()
	{
		if (enemy_ai == null)
		{
			return;
		}

		if (curren_action == null)
		{
			curren_action = enemy_ai.get_action();
			GD.Print(curren_action);
			return;
		}
	}
	public void hover()
	{
		GD.Print("I start hovering!");
		if (!hovering)
		{
			Position = _hoverPosition;
			hovering = true;
		}
	}

	public void hoverEnd()
	{
		
		Position = _defaultPosition;
		hovering = false;
	}
}
