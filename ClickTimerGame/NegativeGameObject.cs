using System.Windows.Media.Imaging;

public class NegativeGameObject : GameObject
{
    private int clickCount = 0; // Počet kliknutí na tento objekt

    public NegativeGameObject(double x, double y, double size, double lifetime = 2.0)
        : base(x, y, size, lifetime)
    {
        // Nastavení jiného obrázku pro negativní objekt
        Shape.Source = new BitmapImage(new Uri("pack://application:,,,/Images/mareckos.png"));
    }

    // Při kliknutí na negativní objekt
    public override void OnClick()
    {
        base.OnClick(); // Zavoláme původní implementaci
    }
}
