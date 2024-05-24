using ClassLibrary;
using System;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using static ClassLibrary.SerializationHelper;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private const string filePath = "data.xml";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SerializeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = new TextBoxData
                {
                    Text1 = TextBox1.Text,
                    Text2 = TextBox2.Text
                };
                SerializationHelper.Serialize(data, filePath);
                MessageBox.Show((string)Application.Current.Resources["SerializationSuccessMessage"]);
                TextBox1.Clear();
                TextBox2.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format((string)Application.Current.Resources["SerializationFailureMessage"], ex.Message));
            }
        }

        private void DeserializeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var data = SerializationHelper.Deserialize<TextBoxData>(filePath);
                    TextBox1.Text = data.Text1;
                    TextBox2.Text = data.Text2;
                    MessageBox.Show((string)Application.Current.Resources["DeserializationSuccessMessage"]);
                }
                else
                {
                    MessageBox.Show((string)Application.Current.Resources["SerializedFileNotFoundMessage"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format((string)Application.Current.Resources["DeserializationFailureMessage"], ex.Message));
            }
        }

        private void ApplyEnglishLanguage_Click(object sender, RoutedEventArgs e)
        {
            
            App.Theme = "Strings.en.xaml";
        }

        private void ApplyRussianLanguage_Click(object sender, RoutedEventArgs e)
        {
            App.Theme = "Strings.ru.xaml";
        }

        private void ApplyLanguage(string languageUri)
        {
            var uri = new Uri(languageUri);
            var resourceDict = (ResourceDictionary)Application.LoadComponent(uri);

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }
    }
}
