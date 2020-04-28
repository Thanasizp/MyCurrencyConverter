using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.OleDb;
using Microsoft.Ajax.Utilities;

namespace MyCurrencyConverter
{
    public partial class CurrencyConverter : System.Web.UI.Page
    {
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["name"] != null)    //this is what we have to do at every postback
            {
                dt = Session["name"] as DataTable;
            }

            // If IsPostBack is false, the page is being created for the first time, 
            // and it’s safe to initialize it.
            if (this.IsPostBack == false)
            {
                //if it is not post back, create a new DataTable and add Columns to it
                dt = new DataTable();

                dt.Columns.Add("Amount", typeof(Decimal));
                dt.Columns.Add("GBP", typeof(string));
                dt.Columns.Add("New Amount", typeof(Decimal));
                dt.Columns.Add("Other Currencies", typeof(string));
                dt.Columns.Add("Rate", typeof(Decimal));
                dt.Columns.Add("Date", typeof(DateTime));

                HttpContext.Current.Session["name"] = dt;

                // The HtmlSelect control accepts text or ListItem objects.
                Currency.Items.Add(new ListItem("EUR (Euros)", "1.11"));
                Currency.Items.Add(new ListItem("USD (American Dollars)", "1.21"));
                Currency.Items.Add(new ListItem("AUD (Australian Dollars)", "1.88"));
            }
        }

        protected void Convert_Server(object sender, EventArgs e)
        {          
            decimal amount;
            bool success = Decimal.TryParse(GBP.Value, out amount);

            if (!success)
            {
                Result.Style["color"] = "Red";
                Result.InnerText = "Use only positive numbers ...";
            }
            else
            {
                Result.Style["color"] = "Black";
             
                // Retrieve the selected ListItem object by its index number.
                ListItem item = Currency.Items[Currency.SelectedIndex];

                decimal newAmount = amount * Decimal.Parse(item.Value);
                Result.InnerText = amount.ToString() + " GBP = ";
                Result.InnerText += newAmount.ToString() + " " + item.Text;

                dt.Rows.Add(amount, "GBP", newAmount, item.Text, 
                            Decimal.Parse(item.Value), 
                            DateTime.Now);
                            //DateTime.Now.ToShortDateString());

                gridView.DataSource = dt;
                gridView.DataBind();

            }
        }

        protected void Search_DateRange(object sender, EventArgs e)
        {
            if (datePicker2.SelectedDate.Date >= datePicker1.SelectedDate.Date)
            {
                DataTable dt1 = new DataTable();
                gridView.DataSource = null;

                try
                {
                    //string p1date = datePicker1.SelectedDate.ToShortDateString();
                    //string p2date = datePicker2.SelectedDate.ToShortDateString();

                    string p1date = datePicker1.SelectedDate.ToString("MM/dd/yyyy hh:mm:ss");
                    string p2date = datePicker2.SelectedDate.ToString("MM/dd/yyyy hh:mm:ss");

                    string myfilter = "Date >= #" + p1date + "# AND Date <=#" + p2date + "#";

                    DataRow[] result = dt.Select(myfilter);

                    foreach (DataRow row in result)
                    {
                        dt1.Rows.Add(row);
                        gridView.DataSource = dt1;
                        gridView.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
 
            }
            else
            {
                Result.Style["color"] = "Red";
                Result.InnerText = "Use two different Dates for Search (2nd > 1st day) ...";
            }
        }

        protected void datePicker1_SelectionChanged(object sender, EventArgs e)
        {
            txtdpd1.Text = datePicker1.SelectedDate.ToLongDateString();
        }

        protected void datePicker2_SelectionChanged(object sender, EventArgs e)
        {
            txtdpd2.Text = datePicker2.SelectedDate.ToLongDateString();
        }

        protected void ResetControls(object sender, EventArgs e)
        {
            gridView.DataSource = null;
            gridView.DataBind();

            Result.InnerText = "";
            GBP.Value = "";
            txtdpd1.Text = "";
            txtdpd2.Text = "";

            datePicker1.SelectedDate = DateTime.MinValue;
            datePicker2.SelectedDate = DateTime.MinValue;
        }
    }

}
