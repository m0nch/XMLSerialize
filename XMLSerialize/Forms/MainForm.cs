using XMLSerialize.Data.Models;
using XMLSerialize.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XMLSerialize
{
    public partial class MainForm : Form
    {
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private SecondForm _secondForm;
        private readonly _IAppCache _appCache;

        public MainForm(
            ITeacherService teacherService,
            IStudentService studentService,
            SecondForm secondForm,
            _IAppCache appCache)
        {
            InitializeComponent();

            _teacherService = teacherService;
            _studentService = studentService;
            _secondForm = secondForm;
            _appCache = appCache;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshTeachers();
            ReadXML("teacher.xml");
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Teacher teacher = new Teacher
            {
                LastName = txtLastName.Text,
                FirstName = txtFirstName.Text,
                Age = Convert.ToInt32(txtAge.Text)
            };
            _teacherService.Add(teacher);
            RefreshTeachers();
            ReadXML("teacher.xml");
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            Teacher teacher = _teacherService.Get(Guid.Parse(grdTeachers.SelectedRows[0].Cells["Id"].Value.ToString()));
            Guid id = teacher.Id;
            _teacherService.Remove(id); //
            RefreshTeachers();
            ReadXML("teacher.xml");
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Teacher teacher = new Teacher
            {
                Id = Guid.Parse(lblGuid.Text),
                LastName = txtLastName.Text,
                FirstName = txtFirstName.Text,
                Age = Convert.ToInt32(txtAge.Text)
            };
            _teacherService.Update(teacher);
            RefreshTeachers();
            ReadXML("teacher.xml");
        }
        private void grdTeachers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowRow();
        }
        private void grdTeachers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                var obj = ((DataGridView)sender).SelectedRows[0].Cells["Id"].Value;
                
                if (!_appCache._ViewBag.ContainsValue(obj))
                {
                    _appCache._ViewBag.Add("TeacherId", obj);
                }
                _secondForm.ShowDialog();
                _secondForm.Activate();
            }
        }
        private void RefreshTeachers()
        {
            grdTeachers.DataSource = null;
            grdTeachers.DataSource = _teacherService.GetAll();
            if (grdTeachers.Rows.Count > 0)
            {
                grdTeachers.Rows[0].Selected = true;
            }
            ShowRow();
            rtxtXml.Refresh();
        }
        private void ShowRow()
        {
            if (grdTeachers.SelectedRows.Count > 0)
            {
                lblGuid.Visible = true;
                lblGuid.Text = grdTeachers.SelectedRows[0].Cells["Id"].Value.ToString();
                txtLastName.Text = grdTeachers.SelectedRows[0].Cells["LastName"].Value.ToString();
                txtFirstName.Text = grdTeachers.SelectedRows[0].Cells["FirstName"].Value.ToString();
                txtAge.Text = grdTeachers.SelectedRows[0].Cells["Age"].Value.ToString();
            }
        }

        private void ReadXML(string file)
        {
            rtxtXml.Clear();
            using (XmlTextReader reader = new XmlTextReader(file))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            rtxtXml.SelectionColor = Color.Blue;
                            rtxtXml.AppendText("<");
                            rtxtXml.SelectionColor = Color.Brown;
                            rtxtXml.AppendText(reader.Name);
                            rtxtXml.SelectionColor = Color.Blue;
                            rtxtXml.AppendText(">");
                            break;
                        case XmlNodeType.Text: //Display the text in each element.
                            rtxtXml.SelectionColor = Color.Black;
                            rtxtXml.AppendText(reader.Value);
                            break;
                        case XmlNodeType.EndElement: //Display the end of the element.
                            rtxtXml.SelectionColor = Color.Blue;
                            rtxtXml.AppendText("</");
                            rtxtXml.SelectionColor = Color.Brown;
                            rtxtXml.AppendText(reader.Name);
                            rtxtXml.SelectionColor = Color.Blue;
                            rtxtXml.AppendText(">");
                            rtxtXml.AppendText("\n");
                            break;
                    }
                }
            }
        }
    }
}
