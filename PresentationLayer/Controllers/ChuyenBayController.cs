using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferObject;
using BusinessLayer;
namespace PresentationLayer.Controllers
{
    public partial class ChuyenBayController : UserControl
    {
        private ChuyenBayBL chuyenBayBL = new ChuyenBayBL();
        private TuyenBayBL tuyenBayBL = new TuyenBayBL();
        private TienTrinhBL tienTrinhBL = new TienTrinhBL();
        public ChuyenBayController()
        {
            InitializeComponent();
        }

        public void ChuyenBayDisplay()
        {
            dgvChuyenBay.DataSource = chuyenBayBL.GetChuyenBayList();
            dgvChuyenBay.Columns["MaTB"].Visible = false;
            dgvChuyenBay.Columns["TienTrinhID"].Visible = false;
            dgvChuyenBay.Columns["Delete"].DisplayIndex = dgvChuyenBay.Columns.Count - 1;

            // Load Tuyến bay
            cbMaTuyenBay.DataSource = tuyenBayBL.GetTuyenBayList();
            cbMaTuyenBay.DisplayMember = "TenTB";
            cbMaTuyenBay.ValueMember = "MaTB";
            cbMaTuyenBay.SelectedIndex = -1;

            // Load Tiến trình
            cbTienTrinh.DataSource = tienTrinhBL.GetTienTrinhList();
            cbTienTrinh.DisplayMember = "Ten";
            cbTienTrinh.ValueMember = "Id";
            cbTienTrinh.SelectedIndex = -1;

            // Load TuyenBay
            cbTuyenBaySearch.DataSource = tuyenBayBL.GetTuyenBayList();
            cbTuyenBaySearch.DisplayMember = "TenTB";
            cbTuyenBaySearch.ValueMember = "MaTB";
            cbTuyenBaySearch.SelectedIndex = -1;


        }

        private void ChuyenBayController_Load(object sender, EventArgs e)
        {
            ChuyenBayDisplay();
        }

        private void btnThemCB_Click(object sender, EventArgs e)
        {
            var maTB = cbMaTuyenBay.SelectedValue;
            var tienTrinh = cbTienTrinh.SelectedValue;
            var thoiGianBay = txtThoiGianBay.Text;

            if (maTB == null || tienTrinh == null || string.IsNullOrEmpty(thoiGianBay))
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin.");
                return;
            }
            DateTime ngayGioDi = datetimeThemTB.Value;
            bool result = chuyenBayBL.AddChuyenBay((int)maTB, ngayGioDi, Convert.ToInt32(thoiGianBay), (byte)tienTrinh);
            if (result)
            {
                MessageBox.Show("Thêm chuyến bay thành công.");
                ChuyenBayDisplay();
            }
            else
            {
                MessageBox.Show("Thêm chuyến bay thất bại.");
            }




        }

        private void dgvChuyenBay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvChuyenBay.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                var maCB = dgvChuyenBay.Rows[e.RowIndex].Cells["MaCB"].Value;
                if (maCB != null)
                {
                    DialogResult dialogResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa chuyến {maCB} này không?", "Xác nhận", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        chuyenBayBL.DeleteChuyenBay(Convert.ToInt32(maCB));
                        ChuyenBayDisplay();
                    }
                }
            }
        }

        private void btnHuyThemCB_Click(object sender, EventArgs e)
        {
            cbMaTuyenBay.SelectedIndex = -1;
            cbTienTrinh.SelectedIndex = -1;
            txtThoiGianBay.Clear();
            datetimeThemTB.Value = DateTime.Now;

        }

        private void btnCapNhatCB_Click(object sender, EventArgs e)
        {
            var maCB = dgvChuyenBay.CurrentRow.Cells["MaCB"].Value;
            var maTB = cbMaTuyenBay.SelectedValue;
            var tienTrinh = cbTienTrinh.SelectedValue;
            var thoiGianBay = txtThoiGianBay.Text;
            if (maCB == null || maTB == null || tienTrinh == null || string.IsNullOrEmpty(thoiGianBay))
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin.");
                return;
            }
            DateTime ngayGioDi = datetimeThemTB.Value;
            bool result = chuyenBayBL.UpdateChuyenBay(Convert.ToInt32(maCB), (int)maTB, ngayGioDi, Convert.ToInt32(thoiGianBay), (byte)tienTrinh);
            if (result)
            {
                MessageBox.Show("Cập nhật chuyến bay thành công.");
                ChuyenBayDisplay();
            }
            else
            {
                MessageBox.Show("Cập nhật chuyến bay thất bại.");
            }

        }

        private void btnTimKiemTB_Click(object sender, EventArgs e)
        {
            dgvChuyenBay.DataSource = chuyenBayBL.GetViewChuyenBayByMaTB(Convert.ToInt32(cbTuyenBaySearch.SelectedValue));

        }
    }
}
