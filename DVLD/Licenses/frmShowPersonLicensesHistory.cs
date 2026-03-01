using DVLD_Buisness;
using System;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmShowPersonLicensesHistory : Form
    {
        private int _PersonID = -1;
        
        public frmShowPersonLicensesHistory()
        {
            InitializeComponent();
        }

        public frmShowPersonLicensesHistory(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
        }

        private void frmShowPersonLicensesHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID == -1)
            {
                ctrlPersonCardWithFilter1.FilterEnabled = true;
                ctrlPersonCardWithFilter1.FilterFocus();
                return;
            }

            if (!clsPerson.isPersonExist(_PersonID))
            {
                MessageBox.Show("Person is not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;

            if (_PersonID == -1)
            {
                ctrlDriverLicenses1.Clear();
                return;
            }

            ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            BeginInvoke(new Action(Close));
        }
    }
}
