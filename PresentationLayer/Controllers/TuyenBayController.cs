using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using TransferObject;
namespace PresentationLayer.Controllers
{
    public partial class TuyenBayController : UserControl
    {
        private TuyenBayBL tuyenBayBL = new TuyenBayBL();
        private SanBayBL sanBayBL = new SanBayBL();
        private SanBayTrungGianBL sanBayTrungGianBL = new SanBayTrungGianBL();
        public TuyenBayController()
        {
            InitializeComponent();
        }
        public void TuyenBayDisplay()
        {
            dgvTuyenBay.DataSource = tuyenBayBL.GetTuyenBayList();
            // Ẩn các cột maSB
            dgvTuyenBay.Columns["MaSanBayDi"].Visible = false;
            dgvTuyenBay.Columns["MaSanBayDen"].Visible = false;
            dgvTuyenBay.Columns["Delete"].DisplayIndex = dgvTuyenBay.Columns.Count - 1; 

            // Set ComboBox for Sân bay đi
            cbSanBayDi.DataSource = sanBayBL.GetSanBayList();
            cbSanBayDi.DisplayMember = "tenSB";
            cbSanBayDi.ValueMember = "maSB";
            cbSanBayDi.SelectedIndex = -1;

            // Set ComboBox for Sân bay đến
            cbSanBayDen.DataSource = sanBayBL.GetSanBayList();
            cbSanBayDen.DisplayMember = "tenSB";
            cbSanBayDen.ValueMember = "maSB";
            cbSanBayDen.SelectedIndex = -1;

            // Set ComboBox for DiemDi
            cbDiemDi.DataSource = sanBayBL.GetSanBayList();
            cbDiemDi.DisplayMember = "tenSB";
            cbDiemDi.ValueMember = "maSB";
            cbDiemDi.SelectedIndex = -1;

            // Set ComboBox for DiemDen
            cbDiemDen.DataSource = sanBayBL.GetSanBayList();
            cbDiemDen.DisplayMember = "tenSB";
            cbDiemDen.ValueMember = "maSB";
            cbDiemDen.SelectedIndex = -1;
        }

        public void TuyenBayTrungGianDisplay()
        {
            dgvSBTrungGian.DataSource = sanBayTrungGianBL.GetSanBayTrungGianList();
            dgvSBTrungGian.Columns["maTB"].Visible = false;
            dgvSBTrungGian.Columns["maSB"].Visible = false;

            // Set ComboBox for MaTB
            cbMaTB.DataSource = tuyenBayBL.GetTuyenBayList();
            cbMaTB.DisplayMember = "tenTB";
            cbMaTB.ValueMember = "maTB";
            cbMaTB.SelectedIndex = -1;

            // Set ComboBox for MaSB
            cbMaSB.DataSource = sanBayBL.GetSanBayList();
            cbMaSB.DisplayMember = "tenSB";
            cbMaSB.ValueMember = "maSB";
            cbMaSB.SelectedIndex = -1;

            // Load DiemDi and DiemDen
            var sanBayListSearch = sanBayBL.GetSanBayTOs(); // giả định trả về List<DataRow> hoặc DataTable
            var diemDiList =sanBayListSearch.Select(x => x.TinhThanh).Distinct().ToList();
            var diemDenList = sanBayListSearch.Select(x => x.TinhThanh).Distinct().ToList();

            // Set ComboBox for DiemDi
            cbDiemDi.DataSource = diemDiList;
            cbDiemDi.DisplayMember = "tenSB";
            cbDiemDi.ValueMember = "maSB";
            cbDiemDi.SelectedIndex = -1;
            // Set ComboBox for DiemDen
            cbDiemDen.DataSource = diemDenList;
            cbDiemDen.DisplayMember = "tenSB";
            cbDiemDen.ValueMember = "maSB";
            cbDiemDen.SelectedIndex = -1;
        }
        private void TuyenBayController_Load(object sender, EventArgs e)
        {
            TuyenBayDisplay();
            
            TuyenBayTrungGianDisplay();
        }

        // Sân bay trung gian
        private void btnThemSBTG_Click(object sender, EventArgs e)
        {
            var maSB = cbMaSB.SelectedValue;
            var maTB = cbMaTB.SelectedValue;
            var thoiGianDung = txtThoiGianDung.Text;

            if (maSB == null || maTB == null)
            {
                MessageBox.Show("Vui lòng chọn sân bay và tuyến bay", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(txtThoiGianDung.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập thời gian dừng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            sanBayTrungGianBL.AddSanBayTrungGian((int)maSB, (int)maTB,TimeSpan.Parse(thoiGianDung));
            dgvSBTrungGian.DataSource = sanBayTrungGianBL.GetSanBayTrungGianList();
           

        }

        // Danh sách tuyến bay
        private void btnTimKiemTB_Click(object sender, EventArgs e)
        {
            var sbDi = cbDiemDi.SelectedValue;
            var sbDen = cbDiemDen.SelectedValue;
            if (sbDi == null || sbDen == null)
            {
                MessageBox.Show("Vui lòng chọn sân bay đi và đến");
                return;
            }
            var tuyenBayList = tuyenBayBL.GetTuyenBayByTinh(sbDi.ToString(), sbDen.ToString());
           
            dgvTuyenBay.DataSource = tuyenBayList;
         


        }

        private void btnThemTB_Click(object sender, EventArgs e)
        {
            var sbDi = cbSanBayDi.SelectedValue;
            var sbDen = cbSanBayDen.SelectedValue;
            var tenTB = txtTenTB.Text;
            var giaTB = txtGiaTB.Text;
            if (sbDi == null || sbDen == null)
            {
                MessageBox.Show("Vui lòng chọn sân bay đi và đến");
                return;
            }

            if (string.IsNullOrWhiteSpace(tenTB) || string.IsNullOrWhiteSpace(giaTB))
            {
                MessageBox.Show("Vui lòng nhập tên tuyến bay và giá");
                return;
            }

            if (sbDi.ToString() == sbDen.ToString())
            {
                MessageBox.Show("Sân bay đi và đến không được trùng nhau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            tuyenBayBL.AddTuyenBay(tenTB, (int)sbDi, (int)sbDen, float.Parse(giaTB));

            dgvTuyenBay.DataSource = tuyenBayBL.GetTuyenBayList();
        }

        private void dgvTuyenBay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dgvTuyenBay.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                var maTB = dgvTuyenBay.Rows[e.RowIndex].Cells["maTB"].Value;
                if (maTB != null)
                {
                    var confirm = MessageBox.Show($"Bạn có chắc muốn xóa tuyến: { maTB} không?","Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirm == DialogResult.Yes)
                    {
                        tuyenBayBL.DeleteTuyenBay(Convert.ToInt32(maTB));
                        dgvTuyenBay.DataSource = tuyenBayBL.GetTuyenBayList();
                    }
                }
            }
        }

        private void btnCapNhatTB_Click(object sender, EventArgs e)
        {
            
            var sbDi = cbSanBayDi.SelectedValue;
            var sbDen = cbSanBayDen.SelectedValue;
            var tenTB = txtTenTB.Text;
            var giaTB = txtGiaTB.Text;
            object maTB;
            try
            {
                maTB = dgvTuyenBay.SelectedRows[0].Cells["maTB"].Value;


            }
            catch (Exception)
            {

                MessageBox.Show("Vui lòng chọn tuyến bay để cập nhật");
                return;
            }
            if (sbDi == null || sbDen == null)
            {
                MessageBox.Show("Vui lòng chọn sân bay đi và đến");
                return;
            }

            if (string.IsNullOrWhiteSpace(tenTB) || string.IsNullOrWhiteSpace(giaTB))
            {
                MessageBox.Show("Vui lòng nhập tên tuyến bay và giá");
                return;
            }

            if (sbDi.ToString() == sbDen.ToString())
            {
                MessageBox.Show("Sân bay đi và đến không được trùng nhau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            tuyenBayBL.UpdateTuyenBay(Convert.ToInt32(maTB), tenTB, (int)sbDi, (int)sbDen, float.Parse(giaTB));

            dgvTuyenBay.DataSource = tuyenBayBL.GetTuyenBayList();
        }

        private void btnHuyThemTB_Click(object sender, EventArgs e)
        {
            cbSanBayDen.SelectedIndex = -1;
            cbSanBayDi.SelectedIndex = -1;
            txtTenTB.Text = string.Empty;
            txtGiaTB.Text = string.Empty;
            dgvTuyenBay.ClearSelection();
        }
    }
}
