using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet;

namespace Dip
{
    public partial class MOB : Form
    {
        public MOB()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "Увеличиваются", "Уменьшаются", "Не меняются" });
            comboBox2.Items.AddRange(new string[] { "Увеличиваются", "Уменьшаются", "Не меняются" });
            comboBox3.Items.AddRange(new string[] { "Увеличиваются", "Уменьшаются", "Не меняются" });
        }
        int n;
        // текст для печати
        private string printer = " ";
        // обработчик события нажатия на кнопку Печать
        private void writeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // задаем текст для печати
            printer = "Строка 1\n\n";

            printer += "Строка 2\nСтрока 3";

            // объект для печати
            PrintDocument printDocument = new PrintDocument();

            // обработчик события печати
            printDocument.PrintPage += PrintPageHandler;

            // диалог настройки печати
            PrintDialog printDialog = new PrintDialog();

            // установка объекта печати для его настройки
            printDialog.Document = printDocument;

            // если в диалоге было нажато ОК
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDialog.Document.Print(); // печатаем
        }
        // обработчик события печати
        void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            // печать строки result
            e.Graphics.DrawString(printer, new Font("Arial", 14), Brushes.Black, 0, 0);
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, num.Text);
            }
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
        private void saveas_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, num.Text);
            }
        }
        private void referenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("   Межотраслевой баланс (МОБ, модель «затраты — выпуск», метод «затраты — выпуск») — экономико-математическая балансовая модель, характеризующая межотраслевые производственные взаимосвязи в экономике страны. Характеризует связи между выпуском продукции в одной отрасли и затратами, расходованием продукции всех участвующих отраслей, необходимым для обеспечения этого выпуска. Межотраслевой баланс составляется в денежной и натуральной формах.\n"

+ "  Межотраслевой баланс представлен в виде системы линейных уравнений. Межотраслевой баланс (МОБ) представляет собой таблицу, в которой отражен процесс формирования и использования совокупного общественного продукта в отраслевом разрезе. Таблица показывает структуру затрат на производство каждого продукта и структуру его распределения в экономике. По столбцам отражается стоимостной состав валового выпуска отраслей экономики по элементам промежуточного потребления и добавленной стоимости. По строкам отражаются направления использования ресурсов каждой отрасли.\n"

+ "  В модели МОБ выделяются четыре квадранта. В первом отражается промежуточное потребление и система производственных связей, во втором — структура конечного использования ВВП, в третьем — стоимостная структура ВВП, а в четвёртом — перераспределение национального дохода.", "Справка",
                    MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
        private void saveasКакToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, num.Text);
            }
        }
        private void abouttheprogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа разработана для решения задач межотраслевого баланса для кафедры прикладной математики и информатики ПГУ им. Т.Г.Шевченко.\n" +
                "\nРазработана студенткой группы ФМ16ДР62ПМ (410):\n - Кушнир И.А.\n" +
                 "\nпод руководством старшего преподавателя:\n - Белая Е.И.\n" +
                "\n © 2020, физико-математический факультет ПГУ им. Т.Г. Шевченко", "О программе!",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        private void dalee_Click(object sender, EventArgs e)
        {
            if (!(int.TryParse(num.Text, out n) && n > 0 && n < 101 /*&& n =='0'*/))
            {
                MessageBox.Show("Ошибка при заполнении данных!", "Сообщение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                num.Clear();
                return;
            }
            dataGridViewArray.RowCount = n + 2;
            dataGridViewArray.ColumnCount = n + 3;
            dataGridViewArray.RowHeadersWidth = 192;
            for (int i = 0; i < n; i++)
                dataGridViewArray.Rows[i].HeaderCell.Value = "Производящая отрасль №" + (i + 1).ToString();
            for (int j = 0; j < n; j++)
            {
                dataGridViewArray.Columns[j].HeaderText = "Потребляющая отрасль №" + (j + 1).ToString();
                //dataGridViewArray.Columns[j].Width = 70;
            }
            dataGridViewArray.Columns[n].HeaderText = "Конечный продукт:";
            dataGridViewArray.Columns[n + 1].HeaderText = "Валовый продукт:";
            dataGridViewArray.Columns[n + 2].HeaderText = "КП на плановый период:";
            dataGridViewArray.Rows[n].HeaderCell.Value = "Основные фонды:";
            dataGridViewArray.Rows[n + 1].HeaderCell.Value = "Труд:";

            
            for (int i = 0; i < dataGridViewArray.RowCount; i++)
            {
                for (int j = 0; j < dataGridViewArray.ColumnCount; j++)
                {
                    dataGridViewArray.Rows[i].Cells[j].ValueType = typeof(Double);
                }
            }

            
           
           

            MessageBox.Show("Введите значения в таблицу", "Сообщение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b')
            {
                e.Handled = true;
            }
        }
        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b')
            {
                e.Handled = true;
            }
        }
        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b')
            {
                e.Handled = true;
            }
        }
        private void textBoxCoeff1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ввод в texBox только цифр и кнопки Backspace
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }

        }
        private void textBoxCoeff2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ввод в texBox только цифр и кнопки Backspace
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
        private void textBoxCoeff3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ввод в texBox только цифр и кнопки Backspace
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ввод в texBox только цифр и кнопки Backspace
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var operationMatrix = new OperationsMatrix();

            var Xi = new double[dataGridViewArray.RowCount, dataGridViewArray.ColumnCount];
            Matrix<double> XiM = Matrix<double>.Build.DenseOfArray(Xi);
            for (int i = 0; i < dataGridViewArray.RowCount; i++)
            {
                for (int j = 0; j < dataGridViewArray.ColumnCount; j++)
                {
                    XiM[i, j] = Convert.ToDouble(dataGridViewArray.Rows[i].Cells[j].Value);
                }
            }
            //Получение квадратной матрицы отраслей:
            var branch = XiM.SubMatrix(0, n, 0, n);
            if (branch.Determinant() == 0)
            {
                MessageBox.Show("Матрица отраслей непродуктивна. \nСкорректируйте данные.", "Внимание",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            { 
                //Получение одномерной матрицы конечной продукции:
                var y0 = XiM.SubMatrix(0, n, n, 1);
                //Получение одномерной матрицы плановых объёмов конечной продукции:
                var X = XiM.SubMatrix(0, n, n + 1, 1);
                //Получение одномерной матрицы валовой продукции:
                var Y = XiM.SubMatrix(0, n, n + 1, 1);
                //Получение одномерной матрицы плановой продукции:
                var plan = XiM.SubMatrix(0, n, n + 2, 1);
                //Получение одномерной матрицы стоимости производственных фондов:
                var F = XiM.SubMatrix(n, 1, 0, n);
                //Получение одномерной матрицы затрат труда:
                var L = XiM.SubMatrix(n+1, 1, 0, n);
                //Получение матрицы коэффициентов прямых затрат:  
                var A = operationMatrix.DirectCosts(branch, X);
                //Получение матрицы "затраты-выпуск:"
                var cosMan = operationMatrix.CostsManufacturing(A);
                //Получение матрицы коэффициентов полных материальных затрат:
                var B = cosMan.Inverse();
                //Получение коэффициентов прямой фондоёмкости:
                var f = F.PointwiseDivide(X.Transpose());
                //Получение коэффициентов полной фондоемкости:
                var Ff = (f * B).Transpose();
                //Получение коэффициентов прямой трудоемкости:
                var t = L.PointwiseDivide(X.Transpose());
                //Получение коэффициентов полной трудоемкости:
                var T = t * B;
                //Получение плановых объемов валовой продукции:
                var X1 = (B * plan).Transpose();
                //Получение необходимого количества труда:
                var Li = t.PointwiseMultiply(X1);
                //Получение необходимого количества фондов:
                var F1 = f.PointwiseMultiply(X1);
                //Получение плановых объёмов межотраслевых потоков в видематрицы 3х3:
                var x = operationMatrix.PlanInterFlow(A, X1.Transpose());
                //Получение плановых объёмов чистой продукции:
                var W = operationMatrix.PlanNetProd(X1, x);

                //Приклеивание матрицы X1 к матрице plan справа:
                var RRR = plan.Append(X1.Transpose());

                //Создание двухэлементного вектора с нулями:
                Vector<double> nulle = new DenseVector(2);

                //Добавление к X! и plan 4 пустых строки снизу:
                RRR = RRR.InsertRow(RRR.RowCount, nulle);
                RRR = RRR.InsertRow(RRR.RowCount, nulle);
                RRR = RRR.InsertRow(RRR.RowCount, nulle);
                RRR = RRR.InsertRow(RRR.RowCount, nulle);

                //Добавление к x снизу строки:
                var split = x.InsertRow(x.RowCount, W.Row(0));
                split = split.InsertRow(split.RowCount, Li.Row(0));
                split = split.InsertRow(split.RowCount, F1.Row(0));
                split = split.InsertRow(split.RowCount, X1.Row(0));

                //Склеивание матриц:
                var splitEnd = split.Append(RRR);

                //Формирование выводной таблицы:
                dataGridViewResult.RowCount = n + 4;
                dataGridViewResult.ColumnCount = n + 2;
                for (int i = 0; i < n; i++)
                    dataGridViewResult.Rows[i].HeaderCell.Value = "Производящая отрасль №" + (i + 1).ToString();
                for (int j = 0; j < n; j++)
                {
                    dataGridViewResult.Columns[j].HeaderText = "Потребляющая отрасль №" + (j + 1).ToString();
                }
                dataGridViewResult.RowHeadersWidth = 192;
                dataGridViewResult.Columns[n].HeaderText = "Конечная продукция";
                dataGridViewResult.Columns[n + 1].HeaderText = "Валовая продукция";
                dataGridViewResult.Rows[n].HeaderCell.Value = "Чистая продукция";
                dataGridViewResult.Rows[n + 1].HeaderCell.Value = "Затраты труда";
                dataGridViewResult.Rows[n + 2].HeaderCell.Value = "Стоимость производственных фондов";
                dataGridViewResult.Rows[n + 3].HeaderCell.Value = "Валовая продукция";

                //Заполнение выводной таблицы данными с округлением 
                //до двух знаков после запятой (округление вверх от 5 по модулю):
                for (int i = 0; i < splitEnd.ColumnCount; i++)
                {
                    for (int j = 0; j < splitEnd.RowCount; j++)
                    {
                        dataGridViewResult.Rows[j].Cells[i].Value = Math.Round(splitEnd[j, i], 2, MidpointRounding.AwayFromZero);
                    }
                }

                //Удаление нулей из незадействованной области:
                for (int i = n; i < splitEnd.RowCount; i++)
                {
                    for (int j = n; j < splitEnd.ColumnCount; j++)
                    {
                        dataGridViewResult.Rows[i].Cells[j].Value = null;
                    }
                }
            }
        }
        private void dataGridViewResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}

