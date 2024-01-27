using Godot;
using System;

[GlobalClass]
public partial class Stats : Resource
{
    // Define the event based on the EventHandler delegate
    public event EventHandler StatsChanged;
    
    // Method to safely invoke the event
    public void On_stats_changed()
    {
        // If there are any subscribers, invoke the event
        StatsChanged?.Invoke(this, EventArgs.Empty);
    }
    
    private void notify()
    {
        On_stats_changed();
    }
    
    [Export] public int MaxHp = 1;
    [Export] public Texture Art;

    private int _health;
    private int _block;

    public int Health
    {
        get { return _health; }
        set { setHealth(value); }
    }

    private void setHealth(int value)
    {
        // Your logic for setting health
        _health = Mathf.Clamp(value, 0, MaxHp);
        On_stats_changed();
    }
    
    public int Block
    {
        get { return _health; }
        set { setBlock(value); }
    }

    private void setBlock(int value)
    {
        // Your logic for setting health
        _block = Mathf.Clamp(value, 0, 999);
        On_stats_changed();
    }
    
    private void take_damage(int amount)
    {
        if (amount <= 0) return;

        int initialDamage = amount;
        amount = Mathf.Clamp(_block - initialDamage, 0, amount);
        setBlock(Mathf.Clamp(_block - initialDamage, 0, _block));
        setHealth(_health - amount);
    }
    
    private void heal(int amount)
    {
        if (amount <= 0) return;
        setHealth(_health + amount);
    }

    private Resource create_instance()
    {
        var instance = new Stats();
        instance.MaxHp = MaxHp;
        instance.Art = Art;
        instance.Health = MaxHp;
        instance.Block = 0;
        return instance;
    }
}
