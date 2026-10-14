using SFML.Graphics;
using SFML.System;
using SFML.Window;

/// <summary>
/// Represents a fish in the aquarium game, with attributes for size, hunger, happiness, and movement.
/// </summary>

public class Fish : Component
{
    private static readonly Random random = new();

    // Attributes 
    public float Size { get; private set; } = 1f;
    public float Hunger { get; private set; } = 0f;
    public float Happiness { get; private set; } = 100f;

    // Tuning 
    public const float MaxSize = 3f;
    private const float HungerPerSecond = 2f;
    private const float HungryThreshold = 60f;
    private const float HappinessLossPerSecond = 3f;
    private const float HappinessGainPerSecond = 1f;
    private const float BaseSpeed = 60f;

    // Movement 
    private Vector2f targetPosition;
    private bool isFacingRight = true;
    private float restTimeLeft;

    // Placeholder for the sprite
    private readonly CircleShape bodyShape = new(10f);
    private readonly ConvexShape tailShape = new(3);

    // Aquarium (all the fish live here, so system.cs only needs one call)
    private const int StartingFishCount = 5;
    private static readonly List<Fish> allFish = new();
    private static readonly Clock frameClock = new();
    private static bool isAquariumCreated = false;

    public Fish(Vector2f startPosition) : base(startPosition)
    {
        targetPosition = startPosition;
        allFish.Add(this);

        bodyShape.Origin = new Vector2f(10f, 10f);
        bodyShape.FillColor = new Color(255, 140, 0);
        tailShape.SetPoint(0, new Vector2f(0f, 0f));
        tailShape.SetPoint(1, new Vector2f(-14f, -9f));
        tailShape.SetPoint(2, new Vector2f(-14f, 9f));
        tailShape.FillColor = new Color(255, 100, 0);
    }

    // Call this once per frame from system.cs (between Clear and Display)
    public static void UpdateAndDrawAll(RenderWindow window)
    {
        if (!isAquariumCreated)
        {
            CreateAquarium(window);
        }

        float deltaTime = frameClock.Restart().AsSeconds();
        FloatRect tankBounds = new FloatRect(
            new Vector2f(0f, 0f),
            new Vector2f(window.Size.X, window.Size.Y)
        );

        // Updates and draws every Component (fish now, other things later)
        UpdateAndDrawAllComponents(deltaTime, tankBounds, window);
    }

    // Spawns the fish and sets up the F key (feed all)
    private static void CreateAquarium(RenderWindow window)
    {
        for (int i = 0; i < StartingFishCount; i++)
        {
            float x = random.Next(50, (int)window.Size.X - 50);
            float y = random.Next(50, (int)window.Size.Y - 50);
            new Fish(new Vector2f(x, y));
        }

        window.KeyPressed += (sender, e) =>
        {
            if (e.Code == Keyboard.Key.F)
            {
                foreach (Fish fish in allFish)
                {
                    fish.Feed();
                }
            }
        };

        frameClock.Restart();
        isAquariumCreated = true;
    }

    // Update 
    public override void Update(float deltaTime, FloatRect tankBounds)
    {
        if (!IsAlive) return;

        UpdateNeeds(deltaTime);
        UpdateMovement(deltaTime, tankBounds);
    }

    private void UpdateNeeds(float deltaTime)
    {
        Hunger = Math.Min(100f, Hunger + HungerPerSecond * deltaTime);

        if (Hunger >= HungryThreshold)
            Happiness -= HappinessLossPerSecond * deltaTime;
        else
            Happiness += HappinessGainPerSecond * deltaTime;

        Happiness = Math.Clamp(Happiness, 0f, 100f);

        if (Hunger >= 100f) IsAlive = false; // starved
    }

    private void UpdateMovement(float deltaTime, FloatRect tankBounds)
    {
        if (restTimeLeft > 0f)
        {
            restTimeLeft -= deltaTime;
            return;
        }

        Vector2f toTarget = targetPosition - Position;
        float distance = MathF.Sqrt(toTarget.X * toTarget.X + toTarget.Y * toTarget.Y);

        if (distance < 4f)
        {
            PickNewTarget(tankBounds);
            restTimeLeft = (float)(random.NextDouble() * 1.5);
            return;
        }

        // Unhappy fish swim slower
        float speed = BaseSpeed * (0.5f + Happiness / 200f);
        Vector2f direction = toTarget / distance;
        Position += direction * speed * deltaTime;

        if (MathF.Abs(direction.X) > 0.05f)
            isFacingRight = direction.X > 0f;
    }
    private void PickNewTarget(FloatRect tankBounds)
    {
        const float margin = 30f;
        float x = tankBounds.Position.X + margin + (float)random.NextDouble() * (tankBounds.Size.X - margin * 2);
        float y = tankBounds.Position.Y + margin + (float)random.NextDouble() * (tankBounds.Size.Y - margin * 2);
        targetPosition = new Vector2f(x, y);
    }

    // Actions 
    public void Feed(float amount = 40f)
    {
        if (!IsAlive) return;
        Hunger = Math.Max(0f, Hunger - amount);
        Size = Math.Min(MaxSize, Size + 0.1f);
        Happiness = Math.Min(100f, Happiness + 5f);
    }

    public int GetSellPrice() => (int)(10 * Size);

    // Drawing
    public override void Draw(RenderWindow window)
    {
        if (!IsAlive) return;

        float direction = isFacingRight ? 1f : -1f;

        bodyShape.Position = Position;
        bodyShape.Scale = new Vector2f(1.6f * Size * direction, Size);

        tailShape.Position = Position + new Vector2f(-direction * 14f * Size, 0f);
        tailShape.Scale = new Vector2f(Size * direction, Size);

        window.Draw(tailShape);
        window.Draw(bodyShape);
    }
}