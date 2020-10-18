using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandsOnActivity2
{
    
    public partial class frmAddProduct : Form
    {
        BindingSource showProductList;
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellingPrice;

        public class NumberFormatException : Exception
        {
            public NumberFormatException(string qty) : base(qty) { }
        }

        public class StringFormatException : Exception
        {
            public StringFormatException(string name) : base(name) { }
        }

        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string curr) : base(curr) { }
        }
        public frmAddProduct()
        {
            InitializeComponent();
            showProductList = new BindingSource();
        }

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                _ProductName = name;
                throw new StringFormatException("Invalid input, Must be String!");
            }
            return name;
        }

        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
            {
                _Quantity = Int32.Parse(qty);
                throw new NumberFormatException("Invalid Input, Must be int!");
            }
            return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
            {
                _SellingPrice = double.Parse(price);
                throw new CurrencyFormatException("Invalid Input!, Must be double");
            }

            return Convert.ToDouble(price);
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]
           {
                "Beverages",
                "Bread/Bakery",
                "Canned/Jarred Goods",
                "Dairy",
                "Frozen Goods",
                "Meat",
                "Personal Care",
                "Other"
           };

            foreach (string ListCategory in ListOfProductCategory)
            {
                cbCategory.Items.Add(ListCategory);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellingPrice = SellingPrice(txtSellPrice.Text);
                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellingPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;

                txtProductName.ResetText();
                cbCategory.ResetText();
                dtPickerMfgDate.ResetText();
                dtPickerExpDate.ResetText();
                txtQuantity.ResetText();
                txtSellPrice.ResetText();
                richTxtDescription.ResetText();

            }

            catch (StringFormatException excep)
            {
                Console.WriteLine(excep.Message);
                txtProductName.ResetText();
            }
            catch (NumberFormatException excep)
            {
                Console.WriteLine(excep.Message);
                txtQuantity.ResetText();
            }
            catch (CurrencyFormatException excep)
            {
                Console.WriteLine(excep.Message);
                txtSellPrice.ResetText();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
                txtQuantity.ResetText();
                txtSellPrice.ResetText();
            }
        }
    }
}
