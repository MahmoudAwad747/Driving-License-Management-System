using DVLD_Buisness;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;

            if(handler != null)
            {
                handler(PersonID);
            }
        }

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        private int _PersonID = -1;
        public int PersonID
        {
            get
            {
                return ctrlPersonCard1.PersonID;
            }
        }

        public clsPerson SelectedPersonInfo
        {
            get
            {
                return ctrlPersonCard1.SelectedPersonInfo;
            }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }


        public void LoadPersonInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = PersonID.ToString();
            _FindNow();

            //ctrlPersonCard1.LoadPersonInfo(PersonID);
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == -1)
                cbFilterBy.SelectedIndex = 0;

            txtFilterValue.Focus();
        }

        private void _FindNow()
        {
            switch(cbFilterBy.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
                    break;

                case "National No.":
                    ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text);
                    break;

                default:
                    break;
            }

            if (FilterEnabled)
                PersonSelected(ctrlPersonCard1.PersonID);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FindNow();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            string value = txtFilterValue.Text.Trim();

            if (string.IsNullOrEmpty(value))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");
                return;
            }

            // validation حسب نوع الفلتر
            if (cbFilterBy.Text == "Person ID")
            {
                if (!int.TryParse(value, out _))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtFilterValue, "Person ID must be a number!");
                    return;
                }
            }

            if (cbFilterBy.Text == "National No.")
            {
                bool hasLetter = false;

                foreach (char c in value)
                {
                    if (char.IsLetter(c))
                    {
                        hasLetter = true;
                        break;
                    }
                }

                if (!hasLetter)
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtFilterValue, "National No. must contains a letter!");
                    return;
                }
            }

            errorProvider1.SetError(txtFilterValue, null);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm1 = new frmAddUpdatePerson();
            frm1.DataBack += DataBackEvent;

            frm1.ShowDialog();
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            // Handle the data received

            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }
    }
}
