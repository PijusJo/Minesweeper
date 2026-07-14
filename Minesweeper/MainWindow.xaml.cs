using System.Windows;
using Minesweeper.ViewModel;

namespace Minesweeper;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainViewModel VM = new MainViewModel();
        DataContext = VM;
    }
}