using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program //Bienvenido al programa
{
    ! # @
    private int variableEntera_2 = 23456;
    private double variableEntera_3 = 23456.123;

    public void MetodoParametros(int valor1, double valor2, char caracter, string valorString)
    {

        for (int x = 0; x >= 123; x++)
        {
            for (int y = 0; y >= 123; y++)
            {
                for (int z = 0; y >= 123; y++)
                {
                    if (x == 10) x = 10;
                    else if (x == 9) x = 23 + 23 + 23 + y + x + z;
                }
            }
        }
    }

    public void MetodoParametros2(int x)
    {
        switch (x)
        {
            case 1:
                x = 0;
                break;
            case 2:
                if (x == 123)
                { x = 0; }
                break;
            case 3:
                x = 0;
                break;
            default:
                x = 0;
                break;
        }
    }

    public int metodosinparametros()
    {
        MetodoParametros2(19);
        return 10;
    }

    public int metodoparametros(int x)
    {
        while (x == 10 || x == 9)
        {
            if (x == 10 || x == 12 || 23 + x >= 23 + x) x = 10;
        }
        return x;
    }
}
