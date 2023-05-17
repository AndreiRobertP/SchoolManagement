using Microsoft.Win32;
using SchoolManagement.Models.BusinessLogic;
using SchoolManagement.Models.EntityLayer;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace SchoolManagement.ViewModels
{
    public class StudentFileVM : BasePropertyChanged
    {
        public FileBLL FileBLL { get; set; } = new FileBLL();
        public ShtBLL ShtBLL { get; set; } = new ShtBLL();

        public ObservableCollection<Sht> Shts { get; set; } = new ObservableCollection<Sht>();
        public ObservableCollection<File> Files { get; set; } = new ObservableCollection<File>();

        private File _selectedFile = null!;
        public File SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                _selectedFile = value;
                OnPropertyChanged();
            }
        }

        private Student _fieldStudent;
        public Student FieldStudent
        {
            get { return _fieldStudent; }
            set
            {
                _fieldStudent = value;
                OnPropertyChanged();

                if (value != null)
                    UpdateListOfShts();
            }
        }

        private Sht _fieldSht = null!;
        public Sht FieldSht
        {
            get { return _fieldSht; }
            set
            {
                _fieldSht = value;
                OnPropertyChanged();

                if (value != null)
                    UpdateFilesBySht(value);
            }
        }

        void UpdateFilesBySht(Sht Sht)
        {
            Files.Clear();

            if (Sht == null)
                return;

            foreach (var item in FileBLL.GetFilesBySht(Sht))
            {
                Files.Add(item);
            }
        }

        public void UpdateListOfShts()
        {
            Shts.Clear();
            foreach (Sht Sht in ShtBLL.GetShtsByHomeroom(FieldStudent.Homeroom))
            {
                Shts.Add(Sht);
            }
        }

        //Commands
        private RelayCommand? _cmdDownload;
        public RelayCommand CmdDownload
        {
            get
            {
                return _cmdDownload ?? (_cmdDownload = new RelayCommand(
                    () =>
                    {
                        if (SelectedFile == null)
                            return;

                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        saveFileDialog.DefaultExt = System.IO.Path.GetExtension(SelectedFile.Filename);

                        if (saveFileDialog.ShowDialog() != true)
                            return;

                        System.IO.File.WriteAllBytes(saveFileDialog.FileName, SelectedFile.Binarydata);
                        MessageBox.Show("The file has been saved", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                , () => true
                ));
            }
        }
    }
}
