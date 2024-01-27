using Godot;
using System;

public partial class stats_ui : HBoxContainer
{
    private HBoxContainer block;
    private HBoxContainer health;
    private Label block_label;
    private Label health_label;
    
    public override void _Ready()
    {
        block = GetNode<HBoxContainer>("Block");
        health = GetNode<HBoxContainer>("Health");
        block_label = block.GetNode<Label>("BlockLabel");
        health_label = health.GetNode<Label>("HealthLabel");
    }

   public void update_stats(Stats stats)
    {
        block_label.Text = stats.Block.ToString();
        health_label.Text = stats.Health.ToString();
        
        block.Visible = stats.Block > 0;
        health.Visible = stats.Health > 0;
    }
}
