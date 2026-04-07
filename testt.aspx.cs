using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class testt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        DateTime fromDate, toDate;

        bool isFromValid = DateTime.TryParseExact(
            txtFromDate.Text.Trim(),
            "dd-MM-yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out fromDate);

        bool isToValid = DateTime.TryParseExact(
            txtToDate.Text.Trim(),
            "dd-MM-yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out toDate);

        if (!isFromValid || !isToValid)
        {
            lblResult.Text = "Please enter valid date in dd-MM-yyyy format";
            return;
        }

        if (toDate < fromDate)
        {
            lblResult.Text = "To date must be greater than from date";
            return;
        }

        // total months
        int months = ((toDate.Year - fromDate.Year) * 12)
                   + (toDate.Month - fromDate.Month);

        // month date add करके difference निकालो
        DateTime tempDate = fromDate.AddMonths(months);

        // अगर date आगे निकल गई तो 1 month कम
        if (tempDate > toDate)
        {
            months--;
            tempDate = fromDate.AddMonths(months);
        }

        // remaining days
        int days = (toDate - tempDate).Days;

        lblResult.Text = "Total: " + months + " Month(s) and " + days + " Day(s)";
    }
}