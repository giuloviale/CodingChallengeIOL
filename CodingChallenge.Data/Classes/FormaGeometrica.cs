/*
 * Refactorear la clase para respetar principios de programación orientada a objetos. Qué pasa si debemos soportar un nuevo idioma para los reportes, o
 * agregar más formas geométricas?
 *
 * Se puede hacer cualquier cambio que se crea necesario tanto en el código como en los tests. La única condición es que los tests pasen OK.
 *
 * TODO: Implementar Trapecio/Rectangulo, agregar otro idioma a reporting.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingChallenge.Data.Classes
{
    public class FormaGeometrica
    {
        #region Formas

        public const int Cuadrado = 1;
        public const int TrianguloEquilatero = 2;
        public const int Circulo = 3;
        public const int Trapecio = 4;
        public const int Rectangulo = 5;

        #endregion

        #region Idiomas

        public const int Castellano = 1;
        public const int Ingles = 2;
        public const int Italiano = 3;

        #endregion

        private readonly decimal _lado;
        private readonly decimal _lado2;
        private readonly decimal _lado3;
        private readonly decimal _lado4;
        private readonly decimal _alto;

        public int Tipo { get; set; }

        public FormaGeometrica(int tipo, decimal ancho, decimal alto,decimal ancho2,decimal ancho3, decimal ancho4)
        {
            Tipo = tipo;
            _lado = ancho;
            _lado2 = ancho2;
            _lado3 = ancho3;
            _lado4 = ancho4;
            _alto = alto;
        }

        public static string Imprimir(List<FormaGeometrica> formas, int idioma)
        {
            var sb = new StringBuilder();

            if (!formas.Any())
            {
                if (idioma == Castellano)
                    sb.Append("<h1>Lista vacía de formas!</h1>");
                else if (idioma == Ingles)
                { sb.Append("<h1>Empty list of shapes!</h1>"); }
                else
                { sb.Append("<h1>Lista di forme vuota!</h1>"); }
            }
            else
            {
                // Hay por lo menos una forma
                // HEADER
                if (idioma == Castellano)
                { sb.Append("<h1>Reporte de Formas</h1>"); }
                else if (idioma == Ingles)
                { sb.Append("<h1>Shapes report</h1>"); }
                else
                { sb.Append("<h1>Rapporto di forme</h1>"); }

                var numeroCuadrados = 0;
                var numeroCirculos = 0;
                var numeroTriangulos = 0;
                var numeroTrapecios = 0;
                var numeroRectangulos = 0;

                var areaCuadrados = 0m;
                var areaCirculos = 0m;
                var areaTriangulos = 0m;
                var areaTrapecios = 0m;
                var areaRectangulos = 0m;


                var perimetroCuadrados = 0m;
                var perimetroCirculos = 0m;
                var perimetroTriangulos = 0m;
                var perimetroTrapecios = 0m;
                var perimetroRectangulos = 0m;

                for (var i = 0; i < formas.Count; i++)
                {
                    if (formas[i].Tipo == Cuadrado)
                    {
                        numeroCuadrados++;
                        areaCuadrados += formas[i].CalcularArea();
                        perimetroCuadrados += formas[i].CalcularPerimetro();
                    }
                    if (formas[i].Tipo == Circulo)
                    {
                        numeroCirculos++;
                        areaCirculos += formas[i].CalcularArea();
                        perimetroCirculos += formas[i].CalcularPerimetro();
                    }
                    if (formas[i].Tipo == TrianguloEquilatero)
                    {
                        numeroTriangulos++;
                        areaTriangulos += formas[i].CalcularArea();
                        perimetroTriangulos += formas[i].CalcularPerimetro();
                    }
                    if (formas[i].Tipo == Trapecio)
                    {
                        numeroTrapecios++;
                        areaTrapecios += formas[i].CalcularArea();
                        perimetroTrapecios += formas[i].CalcularPerimetro();
                    }
                    if (formas[i].Tipo == Rectangulo)
                    {
                        numeroRectangulos++;
                        areaRectangulos += formas[i].CalcularArea();
                        perimetroRectangulos += formas[i].CalcularPerimetro();
                    }
                }
                
                sb.Append(ObtenerLinea(numeroCuadrados, areaCuadrados, perimetroCuadrados, Cuadrado, idioma));
                sb.Append(ObtenerLinea(numeroCirculos, areaCirculos, perimetroCirculos, Circulo, idioma));
                sb.Append(ObtenerLinea(numeroTriangulos, areaTriangulos, perimetroTriangulos, TrianguloEquilatero, idioma));
                sb.Append(ObtenerLinea(numeroTrapecios, areaTrapecios, perimetroTrapecios, Trapecio, idioma));
                sb.Append(ObtenerLinea(numeroRectangulos, areaRectangulos, perimetroRectangulos, Rectangulo, idioma));

                // FOOTER
                sb.Append("TOTAL:<br/>");
                sb.Append(numeroCuadrados + numeroCirculos + numeroTriangulos + numeroTrapecios + numeroRectangulos + " " + (idioma == Castellano ? "formas" : idioma==Ingles ? "shapes":"forme") + " ");
                sb.Append((idioma == Castellano ? "Perimetro " : idioma== Ingles ?"Perimeter ": "Perimetro") + (perimetroCuadrados + perimetroTriangulos + perimetroCirculos + perimetroTrapecios + perimetroRectangulos ).ToString("#.##") + " ");
                sb.Append("Area " + (areaCuadrados + areaCirculos + areaTriangulos + areaTrapecios + areaRectangulos).ToString("#.##"));
            }

            return sb.ToString();
        }

        private static string ObtenerLinea(int cantidad, decimal area, decimal perimetro, int tipo, int idioma)
        {
            if (cantidad > 0)
            {
                if (idioma == Castellano)
                { return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimetro {perimetro:#.##} <br/>"; }
                else if (idioma == Ingles)
                { return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimeter {perimetro:#.##} <br/>"; }
                else
                { return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimetro {perimetro:#.##} <br/>"; }
            }

            return string.Empty;
        }

        private static string TraducirForma(int tipo, int cantidad, int idioma)
        {
            switch (tipo)
            {
                case Cuadrado:
                    if (idioma == Castellano)
                    { return cantidad == 1 ? "Cuadrado" : "Cuadrados"; }
                    else if (idioma == Ingles)
                    { return cantidad == 1 ? "Square" : "Squares"; }
                    else
                    { return cantidad == 1 ? "Quadrato" : "Quadrati"; }
                case Circulo:
                    if (idioma == Castellano)
                    { return cantidad == 1 ? "Circulo" : "Circulos"; }
                    else if (idioma == Ingles)
                    { return cantidad == 1 ? "Circle" : "Circles"; }
                    else
                    { return cantidad == 1 ? "Cerchio" : "Cerchi"; }
                case TrianguloEquilatero:
                    if (idioma == Castellano)
                    { return cantidad == 1 ? "Triangulo" : "Triangulos"; }
                    else if (idioma == Ingles)
                    { return cantidad == 1 ? "Triangle" : "Triangles"; }
                    else
                    { return cantidad == 1 ? "Triangolo" : "Triangoli"; }
                case Trapecio:
                    if (idioma == Castellano)
                    { return cantidad == 1 ? "Trapecio" : "Trapecios"; }
                    else if (idioma == Ingles)
                    { return cantidad == 1 ? "Trapezoid" : "Trapezoids"; }
                    else
                    { return cantidad == 1 ? "Trapezio" : "Trapazi"; }
                case Rectangulo:
                    if (idioma == Castellano)
                    { return cantidad == 1 ? "Rectangulo" : "Rectangulos"; }
                    else if (idioma == Ingles)
                    { return cantidad == 1 ? "Rectangle" : "Rectangles"; }
                    else
                    { return cantidad == 1 ? "Rettangolo" : "Rettangoli"; }
            }

            return string.Empty;
        }

        public decimal CalcularArea()
        {
            switch (Tipo)
            {
                case Cuadrado: return _lado * _lado;
                case Circulo: return (decimal)Math.PI * (_lado / 2) * (_lado / 2);
                case TrianguloEquilatero: return ((decimal)Math.Sqrt(3) / 4) * _lado * _lado;
                case Trapecio: return _alto*(_lado + _lado2) / 2;
                case Rectangulo: return _alto * _lado;
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }

        public decimal CalcularPerimetro()
        {
            switch (Tipo)
            {
                case Cuadrado: return _lado * 4;
                case Circulo: return (decimal)Math.PI * _lado;
                case TrianguloEquilatero: return _lado * 3;
                case Trapecio: return _lado + _lado2 + _lado3 + _lado4;
                case Rectangulo: return 2 * (_lado + _alto);
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }
    }
}
