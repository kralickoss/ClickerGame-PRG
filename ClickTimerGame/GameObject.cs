using System.Windows.Controls;
using System.Windows.Media.Imaging;

public class GameObject
{
    public Image Shape { get; private set; }
    public double X { get; private set; }
    public double Y { get; private set; }
    public double Size { get; private set; }
    public bool IsClicked { get; private set; } = false;
    public double Lifetime { get; set; }

    public GameObject(double x, double y, double size, double lifetime = 2.0)
    {
        X = x;
        Y = y;
        Size = size;
        Lifetime = lifetime;

        Shape = new Image
        {
            Width = size,
            Height = size,
            Source = new BitmapImage(new Uri("pack://application:,,,/Images/fufar.png"))
        };

        Canvas.SetLeft(Shape, X);
        Canvas.SetTop(Shape, Y);
    }

    // Tato metoda je označena jako virtual, aby mohla být přepsána ve třídě NegativeGameObject
    public virtual void OnClick()
    {
        IsClicked = true;
    }

    public void Move(double deltaX, double deltaY)
    {
        X += deltaX;
        Y += deltaY;

        // Ověření, že objekt zůstane v rámci plátna
        if (X < 0) X = 0;
        if (Y < 0) Y = 0;

        double maxX = ((Canvas)Shape.Parent)?.ActualWidth - Size ?? 0;
        double maxY = ((Canvas)Shape.Parent)?.ActualHeight - Size ?? 0;

        if (X > maxX) X = maxX;
        if (Y > maxY) Y = maxY;

        Canvas.SetLeft(Shape, X);
        Canvas.SetTop(Shape, Y);
    }
}
