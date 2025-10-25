extends Node

@export var attack_cooldown := 0.5
@export var attack_damage := 20
@onready var anim := $"../AnimatedSprite2D"
@onready var area := $"../AttackArea"

var can_attack := true

func _process(_delta):
    if Input.is_action_just_pressed("attack") and can_attack:
        attack()

func attack():
    can_attack = false
    anim.play("attack")
    for body in area.get_overlapping_bodies():
        if body.has_method("take_damage"):
            body.take_damage(attack_damage)
    await get_tree().create_timer(attack_cooldown).timeout
    can_attack = true
