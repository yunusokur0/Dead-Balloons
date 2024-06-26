public class NumberFormatter : MonoSingleton<NumberFormatter>
{
    public string FormatNumber(int number)
    {
        if (number >= 1000 && number < 10000)
        {
            return $"{number / 1000f:0.0}k";
        }

        else if (number >= 10000)
        {
            return $"{number / 1000f:0}k";
        }

        else
        {
            return number.ToString();
        }
    }
}