using Godot;
using System;

[GlobalClass]
public partial class Stats : Resource
{
    // Define the event based on the EventHandler delegate
    public event EventHandler StatsChanged;
    
    // Method to safely invoke the event
    protected virtual void OnStatsChanged()
    {
        // If there are any subscribers, invoke the event
        StatsChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public void Notify()
    {
        OnStatsChanged();
    }
    
    [Export] public int MaxHp = 1;
    [Export] public Texture Art;

    private int _health;
    private int _block;

    public int Health
    {
        get { return _health; }
        set { SetHealth(value); }
    }

    private void SetHealth(int value)
    {
        // Your logic for setting health
        _health = Mathf.Clamp(value, 0, MaxHp);
        OnStatsChanged();
    }
    
    public int Block
    {
        get { return _health; }
        set { SetBlock(value); }
    }

    private void SetBlock(int value)
    {
        // Your logic for setting health
        _block = Mathf.Clamp(value, 0, 999);
        OnStatsChanged();
    }
    
    private void take_damage(int amount)
    {
        if (amount <= 0) return;

        int initialDamage = amount;
        amount = Mathf.Clamp(_block - initialDamage, 0, amount);
        SetBlock(Mathf.Clamp(_block - initialDamage, 0, _block));
        SetHealth(_health - amount);
    }
    
    private void heal(int amount)
    {
        if (amount <= 0) return;
        SetHealth(_health + amount);
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
