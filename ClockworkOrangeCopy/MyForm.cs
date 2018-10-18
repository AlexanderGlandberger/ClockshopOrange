using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace ClockShop
{

    class MyForm : Form
    {

        class Watch
        {
            public string manufacturer;
            public string model;
            public int price;
            public Image image;
            public string info;
            public string displayPrice;
        }

        DataGridView dgv1;
        DataGridView dgv2;
        TableLayoutPanel itemInfo;
        PictureBox imageBox;
        Label infoFromCsv;
        Label priceText;
        Label priceFromCsv;
        private List<Watch> watch = new List<Watch> { };

        public MyForm()
        {

            #region Background Table
            Size = new Size(800, 500);

            TableLayoutPanel table = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 5,
                Dock = DockStyle.Fill,
                BackColor = Color.LightGray,
                Size = new Size(800, 500)
            };
            Controls.Add(table);

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));


            table.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 5));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));

            #endregion

            #region Header
            Label header = new Label
            {
                Text = "Clockshop Orange",
                Dock = DockStyle.Fill,
                ForeColor = Color.Black,
                Font = new Font("Arial", 16F),
                //Location = new Point(100, 100)
                TextAlign = ContentAlignment.MiddleRight

            };
            table.Controls.Add(header, 0, 4);
            //table.SetColumnSpan(header, 2);

            PictureBox image = new PictureBox
            {
                Image = Image.FromFile("klockaikon.png"),
                Size = new Size(50, 50),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Anchor = AnchorStyles.Left,

            };
            table.Controls.Add(image, 0, 4);




            #endregion

            #region First Column

            dgv1 = new DataGridView
            {
                ColumnCount = 1,
                Dock = DockStyle.Fill,
            };
            dgv1.Columns[0].Name = "Model";
            dgv1.Width = 300;
            dgv1.Height = 600;
            dgv1.Columns[0].Width = 300;
            dgv1.RowHeadersVisible = false;
            table.Controls.Add(dgv1, 0, 1);
            table.SetRowSpan(dgv1, 3);
            dgv1.CellClick += dgv1_CellClick;

            string[] lines = File.ReadAllLines("collection.csv");
            List<string> collectionList = new List<string> { };
            foreach (string line in lines)
            {
                string[] lines2 = line.Split(',');

                watch.Add(new Watch
                {
                    manufacturer = lines2[0],
                    model = lines2[1],
                    price = int.Parse(lines2[2]),
                    displayPrice = lines2[2],
                    image = Image.FromFile(lines2[3]),
                    info = lines2[4]
                });

            }

            foreach (Watch w in watch)
            {
                dgv1.Rows.Add(w.manufacturer + " - " + w.model);
            }

            #endregion

            #region Second Column

            itemInfo = new TableLayoutPanel
            {
                ColumnCount = 4,
                RowCount = 3,
                BackColor = Color.White,
                Size = new Size(200, 300)
            };
            table.Controls.Add(itemInfo, 1, 1);
            table.SetRowSpan(itemInfo, 3);

            for (int i = 0; i < 4; i++)
            {
                itemInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            }

            itemInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            itemInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            itemInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
            itemInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            imageBox = new PictureBox
            {
                Size = new Size(100, 130),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            itemInfo.Controls.Add(imageBox, 1, 0);
            itemInfo.SetColumnSpan(imageBox, 2);
            //Gör 3 Labels till metod
            infoFromCsv = new Label { Dock = DockStyle.Fill };
            itemInfo.Controls.Add(infoFromCsv, 1, 1);
            itemInfo.SetColumnSpan(infoFromCsv, 2);
            itemInfo.SetRowSpan(infoFromCsv, 1);

            priceFromCsv = new Label { Dock = DockStyle.Bottom };
            itemInfo.Controls.Add(priceFromCsv, 3, 2);
            itemInfo.SetColumnSpan(priceFromCsv, 1);

            priceText = new Label { Dock = DockStyle.Bottom, Font = new Font("Arial", 9F) };
            itemInfo.Controls.Add(priceText, 0, 2);
            itemInfo.SetColumnSpan(priceText, 3);

            Button addToCartButton = new Button
            {
                Text = "Add item to cart",
                BackColor = Color.LightSlateGray,
                Dock = DockStyle.Fill,
                Size = new Size(200, 30)
            };
            itemInfo.Controls.Add(addToCartButton, 0, 3);
            itemInfo.SetColumnSpan(addToCartButton, 4);

            #endregion

            #region Third Column

            TableLayoutPanel cart = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 4,
                BackColor = Color.White,
                Size = new Size(250, 400)
            };
            table.Controls.Add(cart, 2, 1);
            table.SetRowSpan(cart, 2);

            cart.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
            cart.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            cart.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            cart.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            cart.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            cart.RowStyles.Add(new RowStyle(SizeType.Percent, 70));
            cart.RowStyles.Add(new RowStyle(SizeType.Percent, 10));

            //dgv2 = new DataGridView
            //{
            //    ColumnCount = 1,
            //    Dock = DockStyle.Fill,
            //};
            //dgv2.Columns[0].Name = "";
            //dgv2.Width = 300;
            //dgv2.Height = 600;
            //dgv2.Columns[0].Width = 300;
            //dgv2.RowHeadersVisible = false;
            //cart.Controls.Add(dgv1, 0, 1);
            //cart.SetRowSpan(dgv1, 3);
            //dgv2.CellClick += dgv1_CellClick;

            //Label cartLabel = new Label
            //{
            //    Text = "Cart",
            //    TextAlign = ContentAlignment.MiddleCenter,
            //    ForeColor = Color.Black,
            //    Font = new Font("Arial", 12F),
            //    Dock = DockStyle.Fill

            //};
            //cart.Controls.Add(cartLabel, 0, 0);
            //cart.SetColumnSpan(cartLabel, 3);

            //Label cartAmountLabel = new Label
            //{
            //    Text = "#",
            //    TextAlign = ContentAlignment.MiddleCenter,
            //    ForeColor = Color.Black,
            //    Font = new Font("Arial", 10F),
            //    Dock = DockStyle.Fill

            //};
            //cart.Controls.Add(cartAmountLabel, 1, 1);

            //Label cartPriceLabel = new Label
            //{
            //    Text = "Price",
            //    TextAlign = ContentAlignment.MiddleCenter,
            //    ForeColor = Color.Black,
            //    Font = new Font("Arial", 9F),
            //    Dock = DockStyle.Fill

            //};
            //cart.Controls.Add(cartPriceLabel, 2, 1);

            //Label cartTotalAmountLabel = new Label
            //{
            //    Text = "Total amount",
            //    TextAlign = ContentAlignment.MiddleCenter,
            //    ForeColor = Color.Black,
            //    Font = new Font("Arial", 12F),
            //    Dock = DockStyle.Left

            //};
            //cart.Controls.Add(cartTotalAmountLabel, 0, 3);
            //cart.SetColumnSpan(cartTotalAmountLabel, 2);



            Button checkoutButton = new Button
            {
                Text = "Checkout",
                BackColor = Color.LightSlateGray,
                Size = new Size(250, 50)
            };
            table.Controls.Add(checkoutButton, 2, 4);

            TextBox discountBox = new TextBox
            {
                Size = new Size(250, 50)
            };
            table.Controls.Add(discountBox, 2, 3);

            //Label discountLabel = new Label
            //{
            //    Text = "Discount code:",
            //    TextAlign = ContentAlignment.MiddleCenter,
            //    Dock = DockStyle.Right
            //};
            //table.Controls.Add(discountLabel, 1, 3);

            #endregion



        }

        //private Image FromFile()
        //{
        //    throw new NotImplementedException();
        //}

        public void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string x = dgv1.CurrentRow.Cells[0].Value.ToString();
            foreach (Watch w in watch)
            {
                if (w.manufacturer + " - " + w.model == x)
                {
                    imageBox.Image = w.image;
                    infoFromCsv.Text = w.info;
                    priceText.Text = "Price (SEK)";
                    priceFromCsv.Text = w.price.ToString() + ":-";
                }
            }
        }



    }




}
