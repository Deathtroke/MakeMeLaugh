class_name Card
extends Resource


enum EffectType {Atk, Def, Util}
enum TargetType {Self, Single, AOE}

@export_group("Card Attributes")
@export var id: String
@export var effectType: EffectType
@export var targetType: TargetType

func is_single_target() -> bool:
	return targetType == TargetType.Single
