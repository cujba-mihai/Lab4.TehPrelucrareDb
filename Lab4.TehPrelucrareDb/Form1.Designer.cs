using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Lab4.TehPrelucrareDb
{
        partial class Form1
        {
            /// <summary>
            ///  Required designer variable.
            /// </summary>
            private System.ComponentModel.IContainer components = null;

            /// <summary>
            ///  Clean up any resources being used.
            /// </summary>
            /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            #region Windows Form Designer generated code

            /// <summary>
            ///  Required method for Designer support - do not modify
            ///  the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
                dataGridView1 = new DataGridView();
                addButton = new Button();
                updateBtn = new Button();
                button1 = new Button();
                comboBoxTables = new ComboBox();
                ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
                SuspendLayout();
                // 
                // dataGridView1
                // 
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dataGridView1.Location = new Point(12, 58);
                dataGridView1.Name = "dataGridView1";
                dataGridView1.RowHeadersWidth = 51;
                dataGridView1.RowTemplate.Height = 29;
                dataGridView1.Size = new Size(776, 325);
                dataGridView1.TabIndex = 0;
                // 
                // addButton
                // 
                addButton.Location = new Point(494, 409);
                addButton.Name = "addButton";
                addButton.Size = new Size(94, 29);
                addButton.TabIndex = 1;
                addButton.Text = "Add";
                addButton.UseVisualStyleBackColor = true;
                addButton.Click += saveButton_Click;
                // 
                // updateBtn
                // 
                updateBtn.Location = new Point(594, 409);
                updateBtn.Name = "updateBtn";
                updateBtn.Size = new Size(94, 29);
                updateBtn.TabIndex = 2;
                updateBtn.Text = "Update";
                updateBtn.UseVisualStyleBackColor = true;
                updateBtn.Click += updateButton_Click;
                // 
                // button1
                // 
                button1.Location = new Point(694, 409);
                button1.Name = "button1";
                button1.Size = new Size(94, 29);
                button1.TabIndex = 3;
                button1.Text = "Delete";
                button1.UseVisualStyleBackColor = true;
                button1.Click += deleteButton_Click;
                // 
                // comboBoxTables
                // 
                comboBoxTables.FormattingEnabled = true;
                comboBoxTables.Location = new Point(12, 12);
                comboBoxTables.Name = "comboBoxTables";
                comboBoxTables.Size = new Size(235, 28);
                comboBoxTables.TabIndex = 4;
                comboBoxTables.SelectedIndexChanged += comboBoxTables_SelectedIndexChanged;
                // 
                // Form1
                // 
                AutoScaleDimensions = new SizeF(8F, 20F);
                AutoScaleMode = AutoScaleMode.Font;
                ClientSize = new Size(800, 450);
                Controls.Add(comboBoxTables);
                Controls.Add(button1);
                Controls.Add(updateBtn);
                Controls.Add(addButton);
                Controls.Add(dataGridView1);
                Name = "Form1";
                Text = "Delete";
                Load += Form1_Load;
                ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
                ResumeLayout(false);
            }

            #endregion

            private DataGridView dataGridView1;
            private Button addButton;
            private Button updateBtn;
            private Button button1;
            private ComboBox comboBoxTables;
        }
}