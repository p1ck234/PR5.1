using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace PR5._1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int countDot = 500;
        List<double[]> dataList = new List<double[]>();
        DrawingGroup dg = new DrawingGroup();

        public MainWindow()
        {
            InitializeComponent();
            DataFill();
            Execute();

            image1.Source = new DrawingImage(dg);

        }
        void DataFill()
        {

            double[] fun = new double[countDot + 1];

            for (int i = 0; i < fun.Length; i++)

            {
                double angel = Math.PI * 2 / countDot * i;
                fun[i] = Math.Pow((Math.Sin(4 * angel) + 1), 2);
            }
            dataList.Add(fun);
        }
        private void BackgroundFun()
        {
            GeometryDrawing geometryDrawing = new GeometryDrawing(); // об для описания геом фигуры
            RectangleGeometry rectangleGeometry = new RectangleGeometry(); // геометрия квадрата
            rectangleGeometry.Rect = new Rect(0, 0, 1, 2);
            geometryDrawing.Geometry = rectangleGeometry;
            geometryDrawing.Pen = new Pen(Brushes.Red, 0.005); //перо рамки
            geometryDrawing.Brush = Brushes.Beige;//кисть закраски
            dg.Children.Add(geometryDrawing); // добавляем слой 
        }

        private void GridFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i < 20; i++) // добавляем параллельные линии
            {
                LineGeometry line = new LineGeometry(new Point(1.0, i * 0.1), new Point(-0.1, i * 0.1));
                geometryGroup.Children.Add(line);
            }
            GeometryDrawing geometryDr = new GeometryDrawing();
            geometryDr.Geometry = geometryGroup;

            //настройка пера
            geometryDr.Pen = new Pen(Brushes.Gray, 0.003);
            double[] ds = { 1, 1, 1, 1, 1 };//штрих
            geometryDr.Pen.DashStyle = new DashStyle(ds, -1);
            geometryDr.Brush = Brushes.Beige;
            dg.Children.Add(geometryDr);
        }

        private void SinFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();

            for (int i = 0; i < dataList[0].Length - 1; i++)
            {
                LineGeometry line = new LineGeometry(
                    new Point((double)i / (double)countDot,
                    2 - (dataList[0][i] / 2.0)),
                    new Point((double)(i + 1) / (double)countDot,
                    2 - (dataList[0][i + 1] / 2.0)));
                geometryGroup.Children.Add(line);
            }
            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;
            geometryDrawing.Pen = new Pen(Brushes.Blue, 0.005);
            dg.Children.Add(geometryDrawing);
        }
        private void MarkerFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i <= 20; i++)
            {
                FormattedText formattedText = new FormattedText(
                String.Format("{0,7:F}", 2 - i * 0.1),
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                0.05,
                Brushes.Black);

                formattedText.SetFontWeight(FontWeights.Bold);

                Geometry geometry = formattedText.BuildGeometry(new Point(-0.2, i * 0.1 - 0.03));
                geometryGroup.Children.Add(geometry);
            }
            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;

            geometryDrawing.Brush = Brushes.LightGray;
            geometryDrawing.Pen = new Pen(Brushes.Gray, 0.003);

            dg.Children.Add(geometryDrawing);
        }

        void Execute()
        {
            BackgroundFun();
            GridFun();
            SinFun();
            MarkerFun();
        }


    }
}

