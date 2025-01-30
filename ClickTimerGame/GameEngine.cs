using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;

public class GameEngine
{
    private Canvas gameCanvas;
    private Random random;
    private List<GameObject> objects;
    private DispatcherTimer spawnTimer;
    private DispatcherTimer gameTimer;
    private double spawnInterval = 1.0;

    public int Score { get; private set; } = 0;
    public int MissedCount { get; private set; } = 0;
    public int MaxMisses { get; private set; } = 3;

    public static GameEngine Instance { get; private set; }

    public bool GameOverFlag { get; set; } = false;  // Flag pro kontrolu, zda byla hra již ukončena

    public event Action<int> ScoreUpdated;
    public event Action GameOver;

    public GameEngine(Canvas canvas)
    {
        if (canvas == null)
        {
            throw new ArgumentNullException(nameof(canvas), "Hra se nezdařila načíst.");
        }

        gameCanvas = canvas;
        random = new Random();
        objects = new List<GameObject>();

        Instance = this; // Nastavíme statickou instanci hry pro přístup k metodám z jiných tříd

        spawnTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(spawnInterval)
        };
        spawnTimer.Tick += SpawnObject;
        spawnTimer.Start();

        gameTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(50)
        };
        gameTimer.Tick += UpdateObjects;
        gameTimer.Start();
    }

    private void SpawnObject(object sender, EventArgs e)
    {
        try
        {
            double size = random.Next(35, 80); // Zvýšena minimální velikost na 30
            double x = random.Next(0, (int)Math.Max(0, gameCanvas.ActualWidth - size));
            double y = random.Next(0, (int)Math.Max(0, gameCanvas.ActualHeight - size));
            double lifetime = Math.Max(0.5, random.NextDouble() * 2);

            GameObject gameObject;

            // 20% šance, že bude objekt negativní
            bool isNegative = random.Next(0, 10) > 7;

            if (isNegative)
            {
                gameObject = new NegativeGameObject(x, y, size, lifetime);
            }
            else
            {
                gameObject = new GameObject(x, y, size, lifetime);
            }

            gameObject.Shape.MouseLeftButtonDown += (s, args) =>
            {
                try
                {
                    gameObject.OnClick();
                    objects.Remove(gameObject);
                    gameCanvas.Children.Remove(gameObject.Shape);

                    // Pokud je objekt negativní, odečteme body
                    if (gameObject is NegativeGameObject)
                    {
                        Score--; // Odečteme bod za negativní objekt
                    }
                    else
                    {
                        Score++; // Zvyšujeme skóre, pokud není objekt negativní
                    }

                    // Upozorníme na změnu skóre
                    ScoreUpdated?.Invoke(Score);

                    // Zrychlení spawnu při vyšším skóre
                    if (Score % 10 == 0 && spawnInterval > 0.3)
                    {
                        spawnInterval -= 0.1;
                        spawnTimer.Interval = TimeSpan.FromSeconds(spawnInterval);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error handling click: {ex.Message}");
                }
            };

            objects.Add(gameObject);
            gameCanvas.Children.Add(gameObject.Shape);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error spawning object: {ex.Message}");
        }
    }

    private void UpdateObjects(object sender, EventArgs e)
    {
        try
        {
            var expiredObjects = new List<GameObject>();

            foreach (var obj in objects)
            {
                // Přidání pohybu
                obj.Move(random.Next(-5, 6), random.Next(-5, 6));

                if (!obj.IsClicked)
                {
                    obj.Lifetime -= 0.05;
                    if (obj.Lifetime <= 0 && !(obj is NegativeGameObject)) // Negativní objekty nemohou být missed
                    {
                        expiredObjects.Add(obj);
                    }
                }
            }

            foreach (var expired in expiredObjects)
            {
                objects.Remove(expired);
                gameCanvas.Children.Remove(expired.Shape);
                if (!(expired is NegativeGameObject)) // Negativní objekty nezvyšují missed
                {
                    MissedCount++;
                }
            }

            if (MissedCount >= MaxMisses)
            {
                EndGame("Konec hry! Příliš mnoho missed objektů.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating objects: {ex.Message}");
        }
    }

    // Nová metoda pro ukončení hry
    public void EndGame(string message)
    {
        GameOver?.Invoke();
        spawnTimer.Stop();
        gameTimer.Stop();
    }
}
