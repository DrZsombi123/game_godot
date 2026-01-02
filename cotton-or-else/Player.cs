using Godot;


public partial class Player : CharacterBody2D
{
	[Export]private float MoveSpeed = 100f;
	[Export] public int MaxHP = 3;

	public int CurrentHP { get; private set; }

	private AnimatedSprite2D animatedsprite;

	public bool HasKey = false;

	public override void _Ready()
	{
		animatedsprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		 CurrentHP = MaxHP;
	}

	private void HandleAnimation(Vector2 direction){

		if(direction == Vector2.Zero)
		{
			animatedsprite.Stop();
			return;
		}

		string anim = "";

		if(direction.X !=0){
			anim=direction.X>0 ? "walkright" : "walkleft";
		}
		else if(direction.Y != 0)
		{
			anim=direction.Y>0? "walkdown" : "walkup";
		}

		if(animatedsprite.Animation != anim){
			animatedsprite.Play(anim);
		}
	}

	private void GetInput()
	{
		Vector2 inputDirection = Vector2.Zero;
		//jobbra
		if (Input.IsActionPressed("jobbra"))
		{
			inputDirection.X += 1;
		}
		//balra
		if (Input.IsActionPressed("balra"))
		{
			inputDirection.X -= 1;
		}
	   
		//fel
		if (Input.IsActionPressed("elore"))
		{
			inputDirection.Y -= 1;
		}
	   

		//le
		if (Input.IsActionPressed("hatra"))
		{
			inputDirection.Y += 1;
		}

		inputDirection = inputDirection.Normalized();
		
		Velocity = inputDirection * MoveSpeed;

		HandleAnimation(inputDirection);
	}


	

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
	}

	 public void TakeDamage(int amount)
	{
		CurrentHP -= amount;
		CurrentHP = Mathf.Max(CurrentHP, 0);

		GD.Print("Player HP:", CurrentHP);

		if (CurrentHP <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		GD.Print("Player died");
		
	}
}
