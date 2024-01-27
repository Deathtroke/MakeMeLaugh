using Godot;
using System;
using System.Threading.Tasks;

public partial class enemy : Area2D
{
    const int ARROW_OFFSET = 10;

    [Export] public EnemyStats stats
    {
        set
        {
            setStats(value);
        }
        get
        {
            return stats;
        }
    }
    
    private Sprite2D _sprite2D;
    private stats_ui _stats_ui;
    public Sprite2D _arrow;


    private EnemyAI enemy_ai;

    public Enemy_Action curren_action
    {
        set
        {
            curren_action = value;
        }
        get
        {
            return curren_action;
        }
    }

    public override void _Ready()
    {
        _stats_ui = GetNode<stats_ui>("StatsUI");
        _sprite2D = GetNode<Sprite2D>("Sprite2D");
        _arrow = GetNode<Sprite2D>("Arrow");
    }
    
    private void setStats(EnemyStats value)
    {
        stats = value.create_instance();
        // If _stats is not null, unsubscribe update_stats from the StatsChanged event
        if (stats != null)
        {
            stats.StatsChanged -= update_stats;
        }

        // Subscribe update_stats to the StatsChanged event
        stats.StatsChanged += update_stats;
        stats.StatsChanged += update_action;

        update_enemy();
    }

    void setup_ai()
    {
        if (enemy_ai != null)
        {
            enemy_ai.QueueFree();
        }

        EnemyAI ai = stats.ai.Instantiate() as EnemyAI;
        AddChild(ai);
        enemy_ai = ai;
        enemy_ai._enemy = this;
    }
    
    private void update_stats(object sender, EventArgs e)
    {
        _stats_ui.update_stats(stats);
    }
    private async void update_enemy()
    {
        if (!(stats is Stats)) return;
        if (!this.IsInsideTree())
        {
            await Task.Delay(200);
        }
        _sprite2D.Texture = stats.Art;
        _arrow.Position = Vector2.Right * (_sprite2D.GetRect().Size.X / 2 + ARROW_OFFSET);
        setup_ai();
        update_stats(null, null);
    }

    public void do_turn()
    {
        stats._block = 0;

        if (curren_action == null)
        {
            return;
        }
        
        curren_action.perform_action();
    }    
    private void take_damage(int amount)
    {
        if (stats.Health <= 0) return;
		
        stats.take_damage(amount);
        if (stats.Health <= 0) QueueFree();
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
