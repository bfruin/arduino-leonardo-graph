using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System.Collections.Generic;
using System.IO.Ports;
using System.Diagnostics;
using System.Windows.Threading;

namespace ArduinoGraph
{

    /// <summary>
    /// This application visualizes a temporal representation of Analog In/Out and Digital In/Out 
    /// of an Arduino Leonardo by reading from the serial port specified by PORT_NUM using
    /// Dynamic Data Display 2.0 for wpf.
    ///  
    /// Modify your Arduino code to output to the serial in the form:
    /// *A#,#,...,#,#;
    /// *D#,#,...,#,#;
    /// 
    /// Where A denotes Analog and D denotes Digital and each # represents
    /// the reading where In/Out alternate in order of the port number.
    /// Note that this currently only supports devices with up to the same
    /// number of ports as a Arduino Leonardo due to screen space, but future
    /// versions will support more variable number of ports.
    /// 
    /// This is a modified version of Dynamic Data Display's Simulation example. 
    /// </summary>
    public partial class Main : Window
    {
        private static int NUM_ANALOG = 6;
        private static int NUM_DIGITAL = 14;

        private static int PORT_NUM = 15;
        private static int BAUD_RATE = 9600;

        private SerialPort serialPort;
        private string buffer = string.Empty;

        private List<System.Windows.Media.Color> colors;
        private List<System.Windows.Media.Color> digitalColors;

        // Analog Data Sources
        private List<ObservableDataSource<Point>> analogIn = null;
        private List<ObservableDataSource<Point>> analogOut = null;
        private List<LineGraph> analogInLineGraphs = null;
        private List<LineGraph> analogOutLineGraphs = null;

        // Digital Data Sources
        private List<ObservableDataSource<Point>> digitalIn = null;
        private List<ObservableDataSource<Point>> digitalOut = null;
        private List<LineGraph> digitalInLineGraphs = null;
        private List<LineGraph> digitalOutLineGraphs = null;

        int analogCount = 0;
        int digitalCount = 0;

        public Main()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            initializeColors();
            initializeGraphs();
            addAnalogLinesToGraphs();
            addDigitalLinesToGraphs();
            initializeSerial();
        }

        // Colors to be used for drawing lines on graphs
        #region Colors
        private void initializeColors()
        {
            colors = new List<System.Windows.Media.Color>();
            colors.Add(System.Windows.Media.Color.FromArgb(255, 0, 0, 255)); // Blue
            colors.Add(System.Windows.Media.Color.FromArgb(255, 255, 0, 0)); // Red
            colors.Add(System.Windows.Media.Color.FromArgb(255, 0, 255, 0)); // Green
            colors.Add(System.Windows.Media.Color.FromArgb(255, 0, 0, 0)); // Black
            colors.Add(System.Windows.Media.Color.FromArgb(255, 255, 255, 0)); // Yellow
            colors.Add(System.Windows.Media.Color.FromArgb(255, 0, 255, 255)); // Light Blue

            digitalColors = new List<System.Windows.Media.Color>();
            digitalColors.Add(System.Windows.Media.Color.FromArgb(255, 0, 0, 255)); // Blue
            digitalColors.Add(System.Windows.Media.Color.FromArgb(255, 40, 0, 0)); // Dark Red
            digitalColors.Add(System.Windows.Media.Color.FromArgb(255, 0, 255, 255)); // Light Blue
            digitalColors.Add(System.Windows.Media.Color.FromArgb(255, 255, 0, 0)); // Red
        }
        #endregion

        // Start the serial reading in a thread
        #region Setup Serial Thread
        private void initializeSerial()
        {    
            Thread thread = new Thread(new ThreadStart(initializeSerialPort));
            thread.IsBackground = true;
            thread.Start();
        }

        #endregion

        #region Serial Port and Handler
        private void initializeSerialPort()
        {
            serialPort = new SerialPort();
            serialPort.PortName = ("COM" + PORT_NUM);
            serialPort.BaudRate = BAUD_RATE;
            serialPort.DtrEnable = true;

            try
            {
                serialPort.Open();
                if (serialPort.IsOpen)
                {
                    Debug.WriteLine("Serial port open");
                }
                serialPort.DataReceived += serialPort_DataReceived;
            }
            catch (System.UnauthorizedAccessException)
            {
                Debug.WriteLine("Problem opening serial port");
            }
        }

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            buffer += serialPort.ReadExisting();
            bool moreData = true;

            CultureInfo culture = CultureInfo.InvariantCulture;
            string messages = "";
            while (moreData)
            {
                int start = buffer.IndexOf("*");
                int end = buffer.IndexOf(";"); 
                if (start > -1 && end > -1 && start < end) // Have at least a full line
                {
                    bool analog = false;
                    if (buffer[start+1] == 'A')
                    {
                        analog = true;
                    }

                    messages += buffer.Substring(start, (end-start)+1) + "\n";
                    string packet = buffer.Substring(start + 2, (end - start) - 2);
                    buffer = buffer.Remove(start, (end - start) + 1);
                    string[] values = packet.Split(',');
                    if (values.Length % 2 == 0) // Must have even number of values for In and Out
                    {
                        if (analog)
                        {
                            double x = analogCount;

                            int analogIndex = 0;
                            for (int i = 0; i < values.Length - 1; i+=2 )
                            {
                                double yIn = Double.Parse(values[i], culture);
                                double yOut = Double.Parse(values[i + 1], culture);
                                try
                                {
                                    analogIn[analogIndex].AppendAsync(Dispatcher, new Point(x, yIn));
                                    analogOut[analogIndex].AppendAsync(Dispatcher, new Point(x, yOut));
                                }
                                catch (System.ArgumentOutOfRangeException)
                                {
                                    break;
                                }
                                analogIndex++;
                            }

                            analogCount++;
                            // Reset Analog graphs every 350 reads
                            if (analogCount > 350)
                            {
                                Dispatcher.BeginInvoke(
                                    new ThreadStart(() => removeAnalogLinesFromGraphs()));
                                analogCount = 0;
                            }
                        }
                        else
                        {
                            double x = digitalCount;

                            int digitalIndex = 0;
                            for (int i = 0; i < values.Length - 1; i+= 2)
                            {
                                try
                                {
                                    double yIn = Double.Parse(values[i], culture);
                                    double yOut = Double.Parse(values[i + 1], culture);

                                    if (digitalIndex % 2 == 0)
                                    {
                                        yIn += .07;
                                        yOut -= .07;
                                    }
                                    else
                                    {
                                        yIn += .14;
                                        yOut -= .14;
                                    }

                                    digitalIn[digitalIndex].AppendAsync(Dispatcher, new Point(x, yIn));
                                    digitalOut[digitalIndex].AppendAsync(Dispatcher, new Point(x, yOut));
                                    digitalIndex++;
                                } 
                                catch (Exception)
                                {
                                    break;
                                }
                            }

                            digitalCount++;
                            // Reset Digital graphs every 350 reads
                            if (digitalCount > 350)
                            {
                                Dispatcher.BeginInvoke(
                                    new ThreadStart(() => removeDigitalLinesFromGraphs()));
                                digitalCount = 0;
                            }
                        }
                    }
                }
                else
                {
                    moreData = false;
                    Dispatcher.BeginInvoke(
                        new ThreadStart(() => addToMesageLogger(messages)));
                    Thread.Sleep(10);
                }
            }
            buffer = string.Empty;
        }
        #endregion

        // Initialize graph bounds with fixed axis values
        #region Graph Bounds
        private void initializeGraphs()
        {
            analogInGraph.Viewport.AutoFitToView = true;
            ViewportAxesRangeRestriction restrAnalogIn = new ViewportAxesRangeRestriction();
            restrAnalogIn.XRange = new DisplayRange(-10, 375);
            restrAnalogIn.YRange = new DisplayRange(-20, 1255);
            analogInGraph.Viewport.Restrictions.Add(restrAnalogIn);

            analogOutGraph.Viewport.AutoFitToView = true;
            ViewportAxesRangeRestriction restrAnalogOut = new ViewportAxesRangeRestriction();
            restrAnalogOut.XRange = new DisplayRange(-10, 375);
            restrAnalogOut.YRange = new DisplayRange(-10, 325);
            analogOutGraph.Viewport.Restrictions.Add(restrAnalogOut);

            ViewportAxesRangeRestriction restrDigital = new ViewportAxesRangeRestriction();
            restrDigital.XRange = new DisplayRange(-10, 375);
            restrDigital.YRange = new DisplayRange(-.3, 3);

            digital01Graph.Viewport.AutoFitToView = true;
            digital01Graph.Viewport.Restrictions.Add(restrDigital);

            digital23Graph.Viewport.AutoFitToView = true;
            digital23Graph.Viewport.Restrictions.Add(restrDigital);

            digital45Graph.Viewport.AutoFitToView = true;
            digital45Graph.Viewport.Restrictions.Add(restrDigital);

            digital67Graph.Viewport.AutoFitToView = true;
            digital67Graph.Viewport.Restrictions.Add(restrDigital);

            digital89Graph.Viewport.AutoFitToView = true;
            digital89Graph.Viewport.Restrictions.Add(restrDigital);

            digital1011Graph.Viewport.AutoFitToView = true;
            digital1011Graph.Viewport.Restrictions.Add(restrDigital);

            digital1213Graph.Viewport.AutoFitToView = true;
            digital1213Graph.Viewport.Restrictions.Add(restrDigital);
        }

        #endregion

        #region Add/Remove Analog
        private void addAnalogLinesToGraphs()
        {
            analogIn = new List<ObservableDataSource<Point>>();
            analogInLineGraphs = new List<LineGraph>();

            for (int i = 0; i < NUM_ANALOG; i++)
            {
                ObservableDataSource<Point> currAnalog = new ObservableDataSource<Point>();
                currAnalog.SetXYMapping(p => p);
                analogIn.Add(currAnalog);
                analogInLineGraphs.Add(analogInGraph.AddLineGraph(analogIn[i], colors[i], 2, "A" + i));
            }

            analogOut = new List<ObservableDataSource<Point>>();
            analogOutLineGraphs = new List<LineGraph>();

            for (int i = 0; i < NUM_ANALOG; i++)
            {
                ObservableDataSource<Point> currAnalog = new ObservableDataSource<Point>();
                currAnalog.SetXYMapping(p => p);
                analogOut.Add(currAnalog);
                analogOutLineGraphs.Add(analogOutGraph.AddLineGraph(analogOut[i], colors[i], 2, "A" + i));
            }
        }

        private void removeAnalogLinesFromGraphs()
        {
            foreach (LineGraph lg in analogInLineGraphs)
            {
                analogInGraph.Children.Remove(lg);
            }

            foreach (LineGraph lg in analogOutLineGraphs)
            {
                analogOutGraph.Children.Remove(lg);
            }

            addAnalogLinesToGraphs();
        }
        #endregion

        #region Add/Remove Digital
        private void addDigitalLinesToGraphs()
        {
            digitalIn = new List<ObservableDataSource<Point>>();
            digitalInLineGraphs = new List<LineGraph>();

            for (int i = 0; i < NUM_DIGITAL; i++)
            {
                ObservableDataSource<Point> currDigital = new ObservableDataSource<Point>();
                currDigital.SetXYMapping(p => p);
                digitalIn.Add(currDigital);
                LineGraph lg = null;
                int color = i % 2;
                if (i <= 1)
                {
                    lg = digital01Graph.AddLineGraph(digitalIn[i], digitalColors[color], 2, i + " in");
                }
                else if (i <= 3)
                {
                    lg = digital23Graph.AddLineGraph(digitalIn[i], digitalColors[color], 2, i + " in");
                }
                else if (i <= 5)
                {
                    lg = digital45Graph.AddLineGraph(digitalIn[i], digitalColors[color], 2, i + " in");
                }
                else if (i <= 7)
                {
                    lg = digital67Graph.AddLineGraph(digitalIn[i], digitalColors[color], 2, i + " in");
                }
                else if (i <= 9)
                {
                    lg = digital89Graph.AddLineGraph(digitalIn[i], digitalColors[color], 2, i + " in");
                }
                else if (i <= 11)
                {
                    lg = digital1011Graph.AddLineGraph(digitalIn[i], digitalColors[color], 2, i + " in");
                }
                else
                {
                    lg = digital1213Graph.AddLineGraph(digitalIn[i], digitalColors[color], 2, i + " in");
                }
                digitalInLineGraphs.Add(lg);
            }

            digitalOut = new List<ObservableDataSource<Point>>();
            digitalOutLineGraphs = new List<LineGraph>();

            for (int i = 0; i < NUM_DIGITAL; i++)
            {
                ObservableDataSource<Point> currDigital = new ObservableDataSource<Point>();
                currDigital.SetXYMapping(p => p);
                digitalOut.Add(currDigital);
                LineGraph lg = null;
                int color = (i % 2) + 2;
                if (i <= 1)
                {
                    lg = digital01Graph.AddLineGraph(digitalOut[i], digitalColors[color], 2, i + " out");
                }
                else if (i <= 3)
                {
                    lg = digital23Graph.AddLineGraph(digitalOut[i], digitalColors[color], 2, i + " out");
                }
                else if (i <= 5)
                {
                    lg = digital45Graph.AddLineGraph(digitalOut[i], digitalColors[color], 2, i + " out");
                }
                else if (i <= 7)
                {
                    lg = digital67Graph.AddLineGraph(digitalOut[i], digitalColors[color], 2, i + " out");
                }
                else if (i <= 9)
                {
                    lg = digital89Graph.AddLineGraph(digitalOut[i], digitalColors[color], 2, i + " out");
                }
                else if (i <= 11)
                {
                    lg = digital1011Graph.AddLineGraph(digitalOut[i], digitalColors[color], 2, i + " out");
                }
                else
                {
                    lg = digital1213Graph.AddLineGraph(digitalOut[i], digitalColors[color], 2, i + " out");
                }
                digitalOutLineGraphs.Add(lg);
            }
        }

        private void removeDigitalLinesFromGraphs()
        {
            for (int i = 0; i < NUM_DIGITAL; i++)
            {
                if (i <= 1)
                {
                    digital01Graph.Children.Remove(digitalInLineGraphs[i]);
                    digital01Graph.Children.Remove(digitalOutLineGraphs[i]);
                }   
                else if (i <= 3)
                {
                    digital23Graph.Children.Remove(digitalInLineGraphs[i]);
                    digital23Graph.Children.Remove(digitalOutLineGraphs[i]);
                }
                else if (i <= 5)
                {
                    digital45Graph.Children.Remove(digitalInLineGraphs[i]);
                    digital45Graph.Children.Remove(digitalOutLineGraphs[i]);
                }
                else if (i <= 7)
                {
                    digital67Graph.Children.Remove(digitalInLineGraphs[i]);
                    digital67Graph.Children.Remove(digitalOutLineGraphs[i]);
                }
                else if (i <= 9)
                {
                    digital89Graph.Children.Remove(digitalInLineGraphs[i]);
                    digital89Graph.Children.Remove(digitalOutLineGraphs[i]);
                }
                else if (i <= 11)
                {
                    digital1011Graph.Children.Remove(digitalInLineGraphs[i]);
                    digital1011Graph.Children.Remove(digitalOutLineGraphs[i]);
                }
                else
                {
                    digital1213Graph.Children.Remove(digitalInLineGraphs[i]);
                    digital1213Graph.Children.Remove(digitalOutLineGraphs[i]);
                }
            }

            addDigitalLinesToGraphs();
        }

        #endregion

        #region Message Loggin
        private void addToMesageLogger(string s)
        {
            MessageLogger.AppendText(s);

            // Scroll to bottom on update
            MessageLogger.Focus();
            MessageLogger.CaretIndex = MessageLogger.Text.Length;
            MessageLogger.ScrollToEnd();

        }
        #endregion
    }
}
