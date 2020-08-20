using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//  引用命名空间，目前此文件内相关类仅涉及到此命名空间，
//  如果需要，还需要添加对其他命名空间的应用。
using AGI.STKObjects;

namespace TestEngineWpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //*********
        //  此为STK Engine的根，控制整个STK场景
        AgStkObjectRoot stkRoot;

        public MainWindow()
        {
            InitializeComponent();

            //  初始化
            stkRoot = new AgStkObjectRoot();
        }

        //  新建STK场景，场景名为Test
        private void NewScenario_Click(object sender, RoutedEventArgs e)
        {
            stkRoot.NewScenario("Test");
        }

        //  新建卫星
        private void NewSatellite_Click(object sender, RoutedEventArgs e)
        {
            // Create the Satellite 
            IAgSatellite satellite = stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "MySatellite") as IAgSatellite;

            // Set propagator to J2 Perturbation 
            satellite.SetPropagatorType(AgEVePropagatorType.ePropagatorJ2Perturbation);

            // Get the J2 Perturbation propagator 
            IAgVePropagatorJ2Perturbation propagator = satellite.Propagator as IAgVePropagatorJ2Perturbation;

            //  轨道积分(相当于STK软件中的Apply按钮)
            propagator.Propagate();
        }

        //  保存场景
        private void SaveScenario_Click(object sender, RoutedEventArgs e)
        {
            stkRoot.SaveAs(@"D:\Test");

            //  也可保存在具体的文件内，如D:\MySTK\Test，但要确保MySTK文件夹存在。
            //  Test为场景名称，也可改为其他名称，则自动将之前设定的场景名称更改。
            //stkRoot.SaveAs(@"D:\MySTK\Test");
        }
    }
}

