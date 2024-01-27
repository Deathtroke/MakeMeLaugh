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
    
    
    public override void _Ready()
    {
        _stats_ui = GetNode<stats_ui>("StatsUI");
        _sprite2D = GetNode<Sprite2D>("Sprite2D");
        _arrow = GetNode<Sprite2D>("Arrow");
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
        enemy_ai._enemy = this;
    }
    
    private void update_stats(object sender, EventArgs e)
    {
        _stats_ui.update_stats(_stats);
    }
    private async void update_enemy()
    {
        if (!(_stats is Stats)) return;
        if (!this.IsInsideTree())
        {
            await Task.Delay(200);
        }
        _sprite2D.Texture = _stats.Art;
        _arrow.Position = Vector2.Right * (_sprite2D.GetRect().Size.X / 2 + ARROW_OFFSET);
        setup_ai();
        update_stats(null, null);
    }

    public void do_turn()
    {
        _stats._block = 0;

        if (curren_action == null)
        {
            return;
        }
        
        curren_action.perform_action();
    }    
    
    private void take_damage(int amount)
    {
        if (_stats.Health <= 0) return;
		
        _stats.take_damage(amount);
        if (_stats.Health <= 0) QueueFree();
    }

    public void update_action(object sender, EventArgs e)
    {
        if (enemy_ai == null)
        {
            return;
        }

        if (curren_action == null)
        {
            curren_action = enemy_ai.get_action();
            return;
        }
    }
}
