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
using System.Windows.Shapes;
using Outlook_Work.Models.Entities;
using Word = Microsoft.Office.Interop.Word;

namespace Outlook_Work.Windows.Create
{
    /// <summary>
    /// Логика взаимодействия для CreateReportWindow.xaml
    /// </summary>
    public partial class CreateReportWindow : Window
    {
        public static Window currentWindow { get; set; }

        public static int Itysy = 0;

        OutlookWorkDatabaseContext context;
        Order order;
        Users user;

        public CreateReportWindow(Order order, Users user)
        {
            InitializeComponent();
            this.context = OutlookWorkDatabaseContext.DbContext;
            this.order = order;
            this.user = user;
        }

        private void CheckCurrentUser()
        {
            if(user.UserCodeRole == 2)
            {
                nameLabel.Content = "Формирование отзыва";
                makeButton.Content = "Оставить отзыв";
                Title = "Создания отзыва";
                readyorNotReadyCheckBox.Visibility = Visibility.Hidden;

            }
            else if(user.UserCodeRole == 4)
            {

            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void makeButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange richText = new TextRange(contentReportRichTextBox.Document.ContentStart, contentReportRichTextBox.Document.ContentEnd);
            if (!string.IsNullOrWhiteSpace(richText.Text))
            {
                if (richText.Text.Count() < 750)
                {
                    if (user.UserCodeRole == 4)
                    {
                        Report newReport = new Report();
                        newReport.ReportContent = richText.Text;
                        newReport.CodeOrder = order.IdOrder;
                        newReport.CodeHeadDepartament = user.Iduser;
                        if (readyorNotReadyCheckBox.IsChecked == true)
                        {
                            order.CodeStatus = 1;
                        }
                        else
                        {
                            order.CodeStatus = 5;
                        }
                        context.Report.Add(newReport);
                        context.SaveChanges();
                        // создание документа с начальными данными и его отображение
                        Word.Application wordApp = new Word.Application();
                        wordApp.Visible = true;
                        Object template = Type.Missing;
                        Object newTemplate = false;
                        Object documentType = Word.WdNewDocumentType.wdNewBlankDocument;
                        Object visible = true;
                        object missing = Type.Missing;
                        Word._Document wordDoc = wordApp.Documents.Add(
                            ref missing, ref missing, ref missing, ref missing);
                        object start = 0, end = 0;

                        // создаем диапазон, в котором будем выводить информацию
                        Word.Range range = wordDoc.Range(ref start, ref end);
                        range.Text = $"IT-Компания `Outlook work`\n".ToUpper();
                        range.Text = $"Номер отчета № {newReport.Idreport}\n".ToUpper();
                        // формат диапазона
                        range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        range.ParagraphFormat.SpaceAfter = 0;
                        range.Font.Name = "Times New Roman";

                        range.Font.Size = 14;

                        start = wordDoc.Range().End - 1; end = wordDoc.Range().End - 1;
                        range = wordDoc.Range(ref start, ref end);

                        range.Text = $"Дата поступления заявки: {newReport.CodeOrderNavigation.OrderDate.ToShortDateString()}\n";
                        range.Text += $"Сотрудник компаний: {newReport.CodeOrderNavigation.CodeEmployeerNavigation.UserName} {newReport.CodeOrderNavigation.CodeEmployeerNavigation.UserSurname[0]}.\n";
                        range.Text += $"Содержания отчета:\n";
                        range.Text += $"{newReport.ReportContent}\n";
                        range.Text += $"Отчет был написан: Начальником отдела {newReport.CodeOrderNavigation.CodeHeadDepartamentNavigation.UserName} {newReport.CodeOrderNavigation.CodeHeadDepartamentNavigation.UserSurname[0]} .";
                        range.Text += $"Статус заявки: {newReport.CodeOrderNavigation.CodeStatusNavigation.StatusName}";

                        range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        range.ParagraphFormat.SpaceAfter = 0;
                        range.Font.Name = "Times New Roman";

                        range.Font.Size = 14;

                        start = wordDoc.Range().End - 1; end = wordDoc.Range().End - 1;
                        range = wordDoc.Range(ref start, ref end);
                        start = wordDoc.Range().End - 1; end = wordDoc.Range().End - 1;
                        range = wordDoc.Range(ref start, ref end);

                        range.Font.Name = "Times New Roman";
                        range.Font.Size = 14;
                    }
                    else if (user.UserCodeRole == 2)
                    {
                        Feedback newFeeback = new Feedback();
                        newFeeback.FeedbackContent = richText.Text;
                        newFeeback.CodeOrder = order.IdOrder;
                        newFeeback.CodeCompanyEmployee = user.Iduser;
                        context.Feedback.Add(newFeeback);
                        context.SaveChanges();
                    }
                    Itysy = 1;
                    Close();
                }
                else
                {
                    MessageBox.Show("Максимальное количество символов 750", "Предупреждние", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                }
                else
            {
                MessageBox.Show("Пожалуйста заполните содержимое","Предупреждние",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckCurrentUser();
        }

        private void readyorNotReadyCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }


        private void contentReportRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
    }
