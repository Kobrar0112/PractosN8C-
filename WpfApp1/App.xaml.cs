using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string lang;
        public static string Theme
        { get { return lang; } 
            set {
                var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string path = Path.GetDirectoryName(location);
                string di = new DirectoryInfo(path).Parent.Parent.Parent.FullName;

                Uri uri = new Uri($@"{di}\LocalizationLibrary\Themes\{value}", UriKind.Absolute);
                Console.WriteLine(uri.ToString());

                var dict = new ResourceDictionary { Source = uri };

                Current.Resources.MergedDictionaries.RemoveAt(0);
                Current.Resources.MergedDictionaries.Insert(0, dict);
            }
        }
        

    }
}
