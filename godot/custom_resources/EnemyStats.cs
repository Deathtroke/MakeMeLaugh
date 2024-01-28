using Godot;
using System;

[GlobalClass]
public partial class EnemyStats : Stats
{
    [Export] public PackedScene ai;
    
    public EnemyStats create_instance()
    {
        var instance = new EnemyStats();
        instance.MaxHp = MaxHp;
        instance.Art = Art;
        instance.Health = MaxHp;
        instance.Block = 0;
        instance.ai = ai;
        return instance;
    }
}
