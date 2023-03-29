using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        string chto_to;
        string f_n;

        List<Note> notes;


        void Json_file(string chto_to, string f_n)
        {
            if (!File.Exists(chto_to + f_n))
            {
                File.Create(chto_to + f_n);
            }
        }

        void replay(ListBox listBox, List<Note> notes, DateTime dateTime)
        {
            listBox.Items.Clear();
            foreach (Note note in notes)
            {
                if (note.Data.Date == dateTime.Date)
                    listBox.Items.Add(note.name);
            }
        }

        void DeLeTe(List<Note> notes, string name)
        {
            foreach (Note item in notes)
            {
                if (item.name == name)
                {
                    notes.Remove(item);
                    break;
                }
            }
            if (data.Text != "")
            {
                replay(NameObjects, notes, DateTime.Parse(data.Text));
                JSON.JsonWriter(chto_to, f_n, notes);
            }
        }

        void updateNote(List<Note> notes, string name)
        {
            foreach (Note item in notes)
            {
                if (item.name == name)
                {
                    if (data.Text == "")
                    {
                        MessageBox.Show("Выбирете дату для заметки!");
                    }
                    else
                    {
                        if (naimenovanie.Text.Trim() == "" && description_object.Text.Trim() == "")
                            MessageBox.Show("Все данные объекта должны быть заполнены!");
                        else
                        {
                            Note note = notes.Find(x => x.name == name);
                            note.name = naimenovanie.Text;
                            note.description = description_object.Text;
                            note.Data = DateTime.Parse(data.Text);

                            replay(NameObjects, notes, note.Data);
                            JSON.JsonWriter(chto_to, f_n, notes);
                        }
                    }
                    break;
                }
            }
        }

        bool a_note_with_the_name(List<Note> notes, string name)
        {
            foreach (Note item in notes)
                if (item.name == name)
                    return true;
            return false;
        }

        Note filter(List<Note> notes, string name)
        {
            if (notes.Count != 0)
                foreach (Note note in notes)
                    if (note.name == name)
                        return note;
            return null;
        }


        public MainWindow()
        {
            InitializeComponent();

            f_n = "File.json";
            chto_to = Directory.GetCurrentDirectory() + "/";

            Json_file(chto_to, f_n);

            notes = JSON.JsonReader(notes, chto_to, f_n);

            data.Text = DateTime.Now.ToString();
            replay(NameObjects, notes, DateTime.Now);

        }

        private void delete_click(object sender, RoutedEventArgs e)
        {
            if (NameObjects.SelectedIndex > -1)
            {
                DeLeTe(notes, NameObjects.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Следует выбрать запись!");
            }
        }

        private void create_click(object sender, RoutedEventArgs e)
        {
            if (data.Text == "")
            {
                MessageBox.Show("Выбирете дату для заметки!");
            }
            else
            {
                if (naimenovanie.Text.Trim() == "" && description_object.Text.Trim() == "")
                    MessageBox.Show("Все данные объекта должны быть заполнены!");
                else
                {
                    if (a_note_with_the_name(notes, naimenovanie.Text.Trim()))
                    {
                        MessageBox.Show("Такой объект уже существует!");
                    }
                    else
                    {
                        Note note = new Note();
                        note.name = naimenovanie.Text.Trim();
                        note.description = description_object.Text.Trim();
                        note.Data = DateTime.Parse(data.Text).Date;
                        notes.Add(note);

                        replay(NameObjects, notes, note.Data);
                        JSON.JsonWriter(chto_to, f_n, notes);
                    }
                }
            }

        }

        private void save_click(object sender, RoutedEventArgs e)
        {
            if (NameObjects.SelectedIndex > -1)
            {
                updateNote(notes, NameObjects.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Следует выбрать запись!");
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NameObjects.SelectedIndex != -1)
            {
                string s = NameObjects.SelectedItem.ToString();
                Note note = filter(notes, s);
                naimenovanie.Text = note.name;
                description_object.Text = note.description;
                data.Text = note.Data.ToString();
            }
        }

        private void data_changed(object sender, SelectionChangedEventArgs e)
        {
            if (notes.Count != 0)
            {
                replay(NameObjects, notes, DateTime.Parse(data.Text));
            }
        }
    }
}
