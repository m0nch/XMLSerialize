using XMLSerialize.Data.Models;
using XMLSerialize.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;

namespace XMLSerialize
{
    public partial class SecondForm : Form
    {
        private readonly IStudentService _studentService;
        private readonly _IAppCache _appCache;

        public SecondForm(
            IStudentService studentService,
            _IAppCache appCache)
        {
            InitializeComponent();
            _studentService = studentService;
            _appCache = appCache;
        }
        private void SecondForm_Load(object sender, EventArgs e)
        {
            grdStudents.AutoGenerateColumns = false;
            if (grdStudents.Rows.Count > 0)
            {
                grdStudents.Rows[0].Selected = true;
                if (grdStudents.SelectedRows.Count > 0)
                {
                    RefreshStudents();
                }
            }
        }
        private void SecondForm_Activated(object sender, EventArgs e)
        {
            RefreshStudents();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Student student = new Student
            {
                TeacherId = (Guid)_appCache._ViewBag["TeacherId"],
                LastName = txtLastName.Text,
                FirstName = txtFirstName.Text,
                Age = Convert.ToInt32(txtAge.Text)
            };
            _studentService.Add(student);
            RefreshStudents();
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            Student student = _studentService.Get(Guid.Parse(grdStudents.SelectedRows[0].Cells["Id"].Value.ToString()));
            Guid id = student.Id;
            _studentService.Remove(id);
            RefreshStudents();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Student student = new Student
            {
                Id = Guid.Parse(lblGuid.Text),
                LastName = txtLastName.Text,
                FirstName = txtFirstName.Text,
                Age = Convert.ToInt32(txtAge.Text),
                TeacherId = Guid.Parse(lblTGuid.Text)
            };
            _studentService.Update(student);
            RefreshStudents();
        }
        private void grdStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowRow();
        }
        private void RefreshStudents()
        {
            grdStudents.DataSource = null;
            grdStudents.DataSource = _studentService.GetAllByTeacher((Guid)_appCache._ViewBag["TeacherId"]);
            if (grdStudents.SelectedRows.Count > 0)
            {
                grdStudents.Rows[0].Selected = true;
            }
            ShowRow();
            rtxtXml.Clear();
            ReadXML("student.xml");
        }
        private void SecondForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _appCache._ViewBag.Remove("TeacherId");
        }
        private void ShowRow()
        {
            if (grdStudents.SelectedRows.Count > 0)
            {
                lblGuid.Visible = true; lblTGuid.Visible = true;
                lblGuid.Text = grdStudents.SelectedRows[0].Cells["Id"].Value.ToString();
                lblTGuid.Text = grdStudents.SelectedRows[0].Cells["TeacherId"].Value.ToString();
                txtLastName.Text = grdStudents.SelectedRows[0].Cells["LastName"].Value.ToString();
                txtFirstName.Text = grdStudents.SelectedRows[0].Cells["FirstName"].Value.ToString();
                txtAge.Text = grdStudents.SelectedRows[0].Cells["stAge"].Value.ToString();
            }
            else
            {
                lblGuid.Visible = false; lblTGuid.Visible = false;
                lblGuid.Text = "";
                lblTGuid.Text = "";
                txtLastName.Text = "";
                txtFirstName.Text = "";
                txtAge.Text = "";
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
