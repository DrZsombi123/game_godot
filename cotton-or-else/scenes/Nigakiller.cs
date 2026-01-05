using Godot;
using System;

public partial class Nigakiller : CharacterBody2D
{
    [Export] public float Speed = 80f;
    [Export] public float StopDistance = 18f;
    [Export] public float AttackDistance = 48f;
    [Export] public float TimeBeforeAttack = 10f;
    [Export] public float AttackCooldown = 1.2f;
    [Export] public float AttackDuration = 0.4f;
    [Export] public int Damage = 1;

    private bool _damageDealtThisAttack = false;
    private CharacterBody2D _player;
    private AnimatedSprite2D _sprite;

    private float _timeSinceCotton = 0f;
    private float _attackCooldownTimer = 0f;
    private float _attackTimer = 0f;

    private bool _isAttacking = false;
    private Vector2 _lastDirection = Vector2.Down;

    public override void _Ready()
    {
        _player = GetNode<CharacterBody2D>("../Player");
    _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

    GameEvents.Instance.Connect(
        GameEvents.SignalName.CottonPicked,
        new Callable(this, nameof(OnCottonPicked))
    );

    }

    public override void _PhysicsProcess(double delta)
    {
        float d = (float)delta;

    // Count ONLY when not attacking
    if (!_isAttacking)
        _timeSinceCotton += d;

    // Unlock punishment AFTER full wait
    if (_timeSinceCotton >= TimeBeforeAttack)
        _canPunish = true;

    _attackCooldownTimer -= d;

    if (_isAttacking)
    {
        HandleAttack(d);
        return;
    }

    Vector2 toPlayer = _player.GlobalPosition - GlobalPosition;
    float distance = toPlayer.Length();

    // Never attack unless explicitly allowed
    if (_canPunish && CanAttack(distance))
    {
        StartAttack(toPlayer);
        return;
    }

    if (distance <= StopDistance)
    {
        Velocity = Vector2.Zero;
        PlayIdleAnimation();
        return;
    }

    FollowPlayer(toPlayer);
    }
    private bool _canPunish = false;
    private bool CanAttack(float distance)
    {
        return
        _canPunish &&
        distance <= AttackDistance &&
        _attackCooldownTimer <= 0f;
    }

    private void StartAttack(Vector2 direction)
    {
        _isAttacking = true;
        _attackTimer = AttackDuration;
        _attackCooldownTimer = AttackCooldown;
        _damageDealtThisAttack = false;


        Velocity = Vector2.Zero;

        UpdateDirection(direction);
        PlayAttackAnimation();
    }

    private void HandleAttack(float delta)
    {
        if (!_damageDealtThisAttack && _attackTimer <= AttackDuration * 0.5f)
    {
        DealDamageIfInRange();
        _damageDealtThisAttack = true;
    }

    _attackTimer -= delta;

    if (_attackTimer <= 0f)
    {
        _isAttacking = false;
    }
    }

    private void DealDamageIfInRange()
{
    float distance = GlobalPosition.DistanceTo(_player.GlobalPosition);

    if (distance <= AttackDistance && _player is Player player)
    {
        player.TakeDamage(Damage);
    }
}

    private void FollowPlayer(Vector2 direction)
    {
        direction = direction.Normalized();
        Velocity = direction * Speed;
        MoveAndSlide();

        UpdateDirection(direction);
        PlayMoveAnimation();
    }

    private void UpdateDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.X) > Mathf.Abs(direction.Y))
            _lastDirection = direction.X > 0 ? Vector2.Right : Vector2.Left;
        else
            _lastDirection = direction.Y > 0 ? Vector2.Down : Vector2.Up;
    }

    private void PlayMoveAnimation()
    {
        if (_isAttacking)
            return;

        if (_lastDirection == Vector2.Up)
            _sprite.Play("walkup");
        else if (_lastDirection == Vector2.Down)
            _sprite.Play("walkdown");
        else if (_lastDirection == Vector2.Left)
            _sprite.Play("walkleft");
        else if (_lastDirection == Vector2.Right)
            _sprite.Play("walkright");
    }

    private void PlayIdleAnimation()
    {
        if (_isAttacking)
            return;

        if (_lastDirection == Vector2.Up)
            _sprite.Play("idleup");
        else if (_lastDirection == Vector2.Down)
            _sprite.Play("idledown");
        else if (_lastDirection == Vector2.Left)
            _sprite.Play("idleleft");
        else if (_lastDirection == Vector2.Right)
            _sprite.Play("idleright");
    }

    private void PlayAttackAnimation()
    {
        if (_lastDirection == Vector2.Up)
            _sprite.Play("attackup");
        else if (_lastDirection == Vector2.Down)
            _sprite.Play("attackdown");
        else if (_lastDirection == Vector2.Left)
            _sprite.Play("attackleft");
        else if (_lastDirection == Vector2.Right)
            _sprite.Play("attackright");
    }

    private void OnCottonPicked()
    {
         _timeSinceCotton = 0f;
    _canPunish = false;

    _attackCooldownTimer = AttackCooldown;
    _attackTimer = 0f;
    _isAttacking = false;
    }
}
