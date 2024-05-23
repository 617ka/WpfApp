using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private List<Member> members;

        Grid mainGrid = new Grid();
        DataGrid dataGrid = new DataGrid();
        
        TextBlock textBlock = new TextBlock(new Run("Dodaj członka"));
        TextBox memberName = new TextBox();
        TextBox memberDay = new TextBox();
        TextBox memberHour = new TextBox();
        Button addNewMember = new Button();
           

        public MainWindow()
        {
            InitializeComponent();
            CreateTable();

        }

        //tworzenie tabeli i układu aplikacji
        private void CreateTable()
        {
            members = new List<Member>();
            var lines = File.ReadAllLines("../../../members.txt");
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 3)
                {
                    members.Add(new Member
                    {
                        Name = parts[0].Trim(),
                        Day = parts[1].Trim(),
                        Hour = parts[2].Trim()
                    });
                }
            }
            

            
            dataGrid.ItemsSource = members;
            dataGrid.Width = 400;
            dataGrid.Margin= new Thickness(200,0,0,0);


            
            mainGrid.Children.Add(dataGrid);

            
            textBlock.Margin = new Thickness(20,200,0,0);
            textBlock.FontSize = 22;
            


            TextBlock textBlockName = new TextBlock(new Run("Podaj imię"));
            textBlockName.Margin = new Thickness(20, 233, 0, 0);
            textBlockName.FontSize = 14;



            memberName.Margin = new Thickness(-460,80,0,0);
            memberName.Text = "";
            memberName.Width = 100;
            memberName.Height = 20;


            TextBlock textBlockDay = new TextBlock(new Run("Podaj dzień"));
            textBlockDay.Margin = new Thickness(20, 258, 0, 0);
            textBlockDay.FontSize = 14;

            memberDay.Margin = new Thickness(-460, 130, 0, 0);
            memberDay.Text = "";
            memberDay.Width = 100;
            memberDay.Height = 20;


            TextBlock textBlockHour = new TextBlock(new Run("Podaj godzinę"));
            textBlockHour.Margin = new Thickness(20, 283, 0, 0);
            textBlockHour.FontSize = 14;

            memberHour.Margin = new Thickness(-460, 180, 0, 0);
            memberHour.Text = "";
            memberHour.Width = 100;
            memberHour.Height = 20;

            
            addNewMember.Margin = new Thickness(-600,250,0,0);
            addNewMember.Width = 100;
            addNewMember.Height = 30;
            addNewMember.Content = "Dodaj";
            addNewMember.Click += new RoutedEventHandler(AddNewMemberButton_Click);


            mainGrid.Children.Add(textBlock);
            mainGrid.Children.Add(textBlockName);
            mainGrid.Children.Add(memberName);
            mainGrid.Children.Add(textBlockDay);
            mainGrid.Children.Add(memberDay);
            mainGrid.Children.Add(textBlockHour);
            mainGrid.Children.Add(memberHour);
            mainGrid.Children.Add(addNewMember);
            
            Content = mainGrid;            
        }

        //dodawanie członków do listy
        void AddMember(string name, string day, string hour)
        {            
            members.Add(new Member
            {
                Name = name,
                Day = day,
                Hour = hour
            });
            
            SaveMembersToFile();

            dataGrid.Items.Refresh();
            
            
        }

        //zapisywanie członków do pliku tekstowego
        private void SaveMembersToFile()
        {
            using (var writer = new StreamWriter("../../../members.txt"))
            {
                foreach (var member in members)
                {
                    writer.WriteLine($"{member.Name},{member.Day},{member.Hour}");
                }
            }
            

        }

        //sprawdzanie wpisanych danych i przesyłanie dalej
        private void AddNewMemberButton_Click(object sender, RoutedEventArgs e)
        {
            if (memberName.Text != "" || memberDay.Text != "" || memberHour.Text != "")
            {
                string newName = memberName.Text;
                string newDay = memberDay.Text;
                string newHour = memberHour.Text;

                AddMember(newName, newDay, newHour);

                memberName.Text = "";
                memberDay.Text = "";
                memberHour.Text = "";
            }
            else
            {
                MessageBox.Show("Wprowadzono złe dane lub brak danych");
            }

            
        }
    }

    //klasa członek 
    public class Member
    {
        public string Name { get; set; }
        public string Day { get; set; }
        public string Hour { get; set; }
    }
}
